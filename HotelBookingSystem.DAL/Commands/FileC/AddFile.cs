using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.ImageKit.Interfaces;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using Imagekit.Sdk;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Drawing;

namespace HotelBookingSystem.DAL.Commands.FileC
{
    public record AddFile : IRequest<string>
    {
        public AddFile(int id,Stream file, string fileName, string folder)
        {
            Id = id;
            File = file ?? throw new ArgumentNullException(nameof(file));
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            else
                    if (Path.HasExtension(fileName))
                FileName = fileName;
            else
                FileName = fileName + "txt";
            Folder = folder ?? throw new ArgumentNullException(nameof(folder)); 
        }

        public Stream File { get; set; } = null!;
        public string FileName {get; set;} = null!;
        public string Folder { get; set; } = null!;
        public int Id { get; set; } = 0;


    }
    public class AddFileHandler : IRequestHandler<AddFile, string>
    {
        public AddFileHandler(IConfiguration config, HotelBookingContext context, IFileProcessing file)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            FileProcessing = file ?? throw new ArgumentNullException(nameof(file));
            RootPath = config["DirectorySettings:DirectoryPath"] ?? "";
        }
        private HotelBookingContext Context { get; }
        private IFileProcessing FileProcessing { get; }
        private string RootPath { get; } = string.Empty;

        public async Task<string?> Handle(AddFile request, CancellationToken cancellationToken)
        {
            switch (request.Folder)
            {
                case FileEndPoint.HotelFolder:
                    var Hotel = await Context.Hotels.AnyAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    if (!Hotel)
                    {
                        return null;
                    }
                    var HotelfileData = await CreateFile(request);
                    await Context.HotelPictures.AddAsync(new HotelPicture()
                    {
                        HotelId= request.Id,
                        Id = HotelfileData.fileId,
                        ImageEndpoint = HotelfileData.filePath
                    }, cancellationToken);
                    await Context.SaveChangesAsync(cancellationToken);
                    return HotelfileData.filePath;
                case FileEndPoint.RoomFolder:
                    var room = await Context.HotelRooms.AnyAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    if (!room)
                    {
                        return null;
                    }
                    var roomFileData = await CreateFile(request);
                    await Context.HotelPictures.AddAsync(new HotelPicture()
                    {
                        HotelId = request.Id,
                        Id = roomFileData.fileId,
                        ImageEndpoint = roomFileData.filePath
                    }, cancellationToken);
                    await Context.SaveChangesAsync(cancellationToken);
                    return roomFileData.filePath;
                case FileEndPoint.UserFolder:
                    var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    if (user == null)
                    {
                        return null;
                    }
                    var userFileData = await CreateFile(request);
                    user.ImageEndPoint = userFileData.filePath;
                    await Context.SaveChangesAsync(cancellationToken);
                    return userFileData.filePath;
                default:
                    return null;
            }
        }
        private async Task<Result> CreateFile(AddFile request)
        {
            var fileData = await FileProcessing.AddFileAsync(request.File, request.FileName, request.Folder);

            //if (!string.IsNullOrEmpty(RootPath))
            //    using (FileStream localFileData = File.Create(Path.Combine(RootPath, request.Folder, request.FileName)))
            //    {
            //        request.File.Seek(0, SeekOrigin.Begin);
            //        request.File.CopyTo(localFileData);
            //        localFileData.Close();
            //    }
            return fileData;
        }
    }
}
