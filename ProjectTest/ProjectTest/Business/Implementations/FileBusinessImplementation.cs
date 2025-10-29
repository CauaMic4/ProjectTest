using ProjectTest.Data.VO;

namespace ProjectTest.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadsDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".txt")
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocURL = Path.Combine(baseUrl + "api/file/v1/", docName);

                    using (var stream = new FileStream(destination, FileMode.Create))

                        await file.CopyToAsync(stream);
                }
            }
            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new();

            foreach (var item in files)
                list.Add(await SaveFileToDisk(item));


            return list;
        }
    }
}
