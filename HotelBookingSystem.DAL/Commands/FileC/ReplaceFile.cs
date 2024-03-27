using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.ImageKit.Interfaces;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBookingSystem.DAL.Commands.FileC
{

    public record ReplaceFile(string Id,Stream File, string EndPoint) : IRequest<string>;
    public class ReplaceFileHandler : IRequestHandler<ReplaceFile, string>
    {
        public ReplaceFileHandler(IConfiguration config, HotelBookingContext context, IFileProcessing file)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            FileProcessing = file ?? throw new ArgumentNullException(nameof(file));
            RootPath = config["DirectorySettings:DirectoryPath"] ?? "";
        }
        private HotelBookingContext Context { get; }
        private IFileProcessing FileProcessing { get; }
        private string RootPath { get; } = string.Empty;
        public async Task<string> Handle(ReplaceFile request, CancellationToken cancellationToken)
        {
            if (request.EndPoint.StartsWith(FileEndPoint.HotelFolder))
            {
                var Hotel = await Context.HotelPictures.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (Hotel == null)
                {
                    return string.Empty;
                }
                Context.HotelPictures.Remove(Hotel);
                var hotelFile = await ReplaceFile(request);
                if (hotelFile == null)
                {
                    return string.Empty;
                }
                Context.HotelPictures.Add(new HotelPicture()
                {
                    Id = hotelFile.fileId,
                    HotelId =Hotel.HotelId,
                    ImageEndpoint = hotelFile.filePath
                });
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if(numRowsRoom>0)
                    return string.Empty;
                return hotelFile.filePath;
            }
            if (request.EndPoint.StartsWith(FileEndPoint.RoomFolder))
            {
                var room = await Context.RoomPictures.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (room == null)
                {
                    return string.Empty;
                }
                Context.RoomPictures.Remove(room);
                var File = await ReplaceFile(request);
                if (File == null)
                {
                    return string.Empty;
                }
                Context.RoomPictures.Add(new RoomPicture()
                {
                    Id = File.fileId,
                    RoomId = room.RoomId,
                    ImageEndpoint = File.filePath
                });
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if (numRowsRoom > 0)
                    return string.Empty;
                return File.filePath;
            }

            if (request.EndPoint.StartsWith(FileEndPoint.HotelFolder))
            {
                bool flag = int.TryParse(request.Id, out int id);
                if (!flag)
                    return string.Empty;
                var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                if (user == null)
                    return string.Empty;
                var file = await ReplaceFile(request);
                user.ImageEndPoint = file.filePath;
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if (numRowsRoom == 0)
                    return string.Empty;
                return file.filePath;
            }
            return string.Empty;
        }
        private async Task<Result> ReplaceFile(ReplaceFile request)
        {
            var fileId = await FileProcessing.RemoveFileByEndPointAsync(request.EndPoint);

            //if (!string.IsNullOrEmpty(RootPath))
            //    File.Delete(Path.Combine(RootPath, request.EndPoint));

            var newResult = await FileProcessing.AddFileAsync(request.File,Path.GetFileName(request.EndPoint),Path.GetDirectoryName(request.EndPoint));
            if (!string.IsNullOrEmpty(RootPath))
                using (FileStream localFileData = File.Create(Path.Combine(RootPath, request.EndPoint)))
                {
                    request.File.Seek(0, SeekOrigin.Begin);
                    request.File.CopyTo(localFileData);
                    localFileData.Close();
                }

            return newResult;
        }
    }
}
