using HotelBookingSystem.ImageKit.Interfaces;
using Imagekit;
using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.ImageKit.Concrets
{
    public class FileProcessing : IFileProcessing
    {
        public FileProcessing(IConfiguration config)
        {
            string publicKey = config["imagekit.io:publicKey"];
            string privateKey = config["imagekit.io:PrivateKey"];
            string urlEndpointKey = config["imagekit.io:URL-endpoint"];
            var imageKit = new ImagekitClient
                                    (
                                    publicKey,
                                    privateKey,
                                    urlEndpointKey
                                    );
            Imagekit = imageKit;
        }

        private ImagekitClient Imagekit { get; }
        public async Task<Result> AddFileAsync(byte[] file, string filename, string folderName)
        {
            return await AddFileAsyncCommon(file, filename, folderName);
        }
        public async Task<Result> AddFileAsync(Stream file, string filename, string folderName)
        {
            return await AddFileAsyncCommon(file, filename, folderName);
        }
        public async Task<Result> AddFileAsync(string fileUrl, string filename, string folderName)
        {
            return await AddFileAsyncCommon(fileUrl, filename, folderName);
        }
        private async Task<Result> AddFileAsyncCommon(object file, string filename, string folderName)
        {
            FileCreateRequest request = new()
            {
                file = file,
                fileName = filename + Guid.NewGuid().ToString(),
                useUniqueFileName = true,
                folder = folderName,
                isPrivateFile = false,
                overwriteFile = true,
                overwriteAITags = true,
                overwriteTags = true,
                overwriteCustomMetadata = true,
            };
            AutoTags autoTags = new()
            {
                name = "google-auto-tagging",
                maxTags = 5,
                minConfidence = 95
            };
            List<Extension> ext = new()
            {
                autoTags
            };
            request.extensions = ext;
            Result res = await Imagekit.UploadAsync(request);
            return res;

        }
        public async Task<string> RemoveFileByEndPointAsync(string endPoint)
        {
            var requestData = new GetFileListRequest();
            if (!Path.HasExtension(endPoint))
                throw new Exception("No file Name Found");
            else
            {
                requestData.Path = Path.GetDirectoryName(endPoint);
                requestData.SearchQuery = "name=" + Path.GetFileName(endPoint);
            }

            var dataList = await Imagekit.GetFileListRequestAsync(requestData);

            var data = await Imagekit.DeleteFileAsync(dataList.FileList[0].fileId);
            return data.fileId;
        }
        public async Task<List<string>> RemoveFilesAsync(params string[] ids)
        {
            var data = await Imagekit.BulkDeleteFilesAsync(ids.ToList());
            //datar
            return data.MissingfileIds;
        }
        public Result GetFileDetails(string id)
        {
            var data = Imagekit.GetFileDetail(id);
            return data;
        }
        public async Task<Root> GetFileDetailByEndPoint(string endPoint)
        {
            var requestData = new GetFileListRequest();
            if (!Path.HasExtension(endPoint))
                throw new Exception("No file Name Found");
            else
            {
                requestData.Path = Path.GetDirectoryName(endPoint);
                requestData.SearchQuery = "name="+Path.GetFileName(endPoint);
            }

            var data = await Imagekit.GetFileListRequestAsync(requestData);

            if (data.FileList.Count <= 0)
                throw new Exception("No file Found");

            return data.FileList[0];
        }
        public async Task<Result> GetFileDetailsAsync(string id)
        {
            var data = await Imagekit.GetFileDetailAsync(id);
            return (Result)data;
        }
        public async Task<ResultList> GetFileListAsync(GetFileListRequest request)
        {
            var data = await Imagekit.GetFileListRequestAsync(request);
            return data;
        }
    }
}
