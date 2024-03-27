using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.ImageKit.Interfaces;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBookingSystem.DAL.Commands.FileC
{
    public record RemoveFile(string Id,string EndPoint) : IRequest<bool>;
    public class RemoveFileHandler : IRequestHandler<RemoveFile, bool>
    {
        public RemoveFileHandler(IConfiguration config, HotelBookingContext context, IFileProcessing file)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            FileProcessing = file ?? throw new ArgumentNullException(nameof(file));
            RootPath = config["DirectorySettings:DirectoryPath"] ?? "";
        }
        private HotelBookingContext Context { get; }
        private IFileProcessing FileProcessing { get; }
        private string RootPath { get; } = string.Empty;
        public async Task<bool> Handle(RemoveFile request, CancellationToken cancellationToken)
        {
            if (request.EndPoint.StartsWith(FileEndPoint.HotelFolder))
            {
                var Hotel = await Context.HotelPictures.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (Hotel == null)
                {
                    return false;
                }
                Context.HotelPictures.Remove(Hotel);
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if (numRowsRoom > 0)
                    await DeleteFile(request);
                return true;
            }
            if(request.EndPoint.StartsWith(FileEndPoint.RoomFolder))
            {
                var room = await Context.RoomPictures.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (room == null)
                {
                    return false;
                }
                Context.RoomPictures.Remove(room);
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if (numRowsRoom > 0)
                    await DeleteFile(request);
                return true;
            }

            if (request.EndPoint.StartsWith(FileEndPoint.HotelFolder))
            {
                bool flag = int.TryParse(request.Id, out int id);
                if (!flag)
                    return false;
                var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                if (user == null)
                    return false;
                user.ImageEndPoint = null;
                int numRowsRoom = await Context.SaveChangesAsync(cancellationToken);
                if (numRowsRoom > 0)
                    await DeleteFile(request);
                return true;
            }
            return false;

        }

        private async Task<string> DeleteFile(RemoveFile request)
        {
            var fileId = await FileProcessing.RemoveFileByEndPointAsync(request.EndPoint);

            //if (!string.IsNullOrEmpty(RootPath))
            //    File.Delete(Path.Combine(RootPath, request.EndPoint));
            return fileId;
        }
    }
}
