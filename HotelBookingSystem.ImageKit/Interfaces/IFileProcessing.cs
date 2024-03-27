using Imagekit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.ImageKit.Interfaces
{
    public interface IFileProcessing
    {
        Task<Result> AddFileAsync(byte[] file, string filename, string folderName);
        Task<Result> AddFileAsync(Stream file, string filename, string folderName);
        Task<Result> AddFileAsync(string fileUrl, string filename, string folderName);
        Task<string> RemoveFileByEndPointAsync(string EndPoints);
        Task<List<string>> RemoveFilesAsync(params string[] ids);
        Result GetFileDetails(string id);
        Task<Result> GetFileDetailsAsync(string id);
        Task<Root> GetFileDetailByEndPoint(string endPoint);
        Task<ResultList> GetFileListAsync(GetFileListRequest folder);

    }
}
