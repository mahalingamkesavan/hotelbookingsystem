using MediatR;
using Microsoft.Extensions.FileProviders;

namespace HotelBookingSystem.DAL.Queries.FileQ
{
    public record GetFiles(string path,string filter = null!) : IRequest<IDirectoryContents>;
    public class GetFilesHandler : IRequestHandler<GetFiles, IDirectoryContents>
    {
        //public GetFilesHandler(IFileProvider provider)
        //{
        //    Provider = provider;
        //}

        //public IFileProvider Provider { get; }

        public Task<IDirectoryContents> Handle(GetFiles request, CancellationToken cancellationToken)
        {
            //IDirectoryContents files = Provider.GetDirectoryContents(request.path);
            //if (files == null)
            //    return null!;
            //if (string.IsNullOrEmpty(request.filter))
            //    files.Where(x=>!x.IsDirectory && x.Name.Contains(request.filter));
            //return Task.FromResult(files);
            throw new NotImplementedException();
        }
    }
}
