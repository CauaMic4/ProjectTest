using ProjectTest.Data.VO;

namespace ProjectTest.Business.Implementations
{
    public class FileBusiness : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusiness(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadsDir\\";
        }

        public byte[] GetFile(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file)
        {
            throw new NotImplementedException();
        }

        public Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
