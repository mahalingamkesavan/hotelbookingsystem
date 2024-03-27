using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.ImageKit.Interfaces;
using Imagekit.Sdk;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DAL.Queries.FileQ
{
    public record class GetFile(string Path, string Filter = null!) : IRequest<IFileInfo>;
    public class GetFileHandler : IRequestHandler<GetFile, IFileInfo>
    {
        public GetFileHandler(HotelBookingContext context, IFileProcessing processing)
        {
            Context = context;
            Processing = processing;
        }
        //public GetFileHandler(IFileProvider provider, HotelBookingContext context, IFileProcessing processing)
        //{
        //    Provider = provider;
        //    Context = context;
        //    Processing = processing;
        //}

        //public IFileProvider Provider { get; }
        public HotelBookingContext Context { get; }
        public IFileProcessing Processing { get; }

        public Task<IFileInfo> Handle(GetFile request, CancellationToken cancellationToken)
        {
            //if(request.Filter != null)
            //{
            //    IDirectoryContents files = Provider.GetDirectoryContents(request.Path);
            //    if (files == null)
            //        return null!;
            //    if (!string.IsNullOrEmpty(request.Filter))
            //    {
            //        return Task.FromResult(files.Where(x => !x.IsDirectory && x.Name.Contains(request.Filter)).FirstOrDefault() ?? null!);
            //    }
            //}
            //IFileInfo file = Provider.GetFileInfo(request.Path);
            //return Task.FromResult(file);
            throw new NotImplementedException();
        }
    }
}
