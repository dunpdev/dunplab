using DUNPLab.API.Infrastructure;

namespace DUNPLab.API.Services
{
    public class FileBackupService: IFileBackupService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileBackupService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void BackupFiles()
        {
            var sourcePath = Path.Combine(_hostingEnvironment.WebRootPath, "zahtevi");
            var destinationPath = Path.Combine(sourcePath, "backup");

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            foreach (var file in Directory.GetFiles(sourcePath))
            {
                var destFile = Path.Combine(destinationPath, Path.GetFileName(file));

                if (!File.Exists(destFile))
                {
                    File.Copy(file, destFile);
                }
            }
        }
    }
}
