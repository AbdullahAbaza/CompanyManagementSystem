using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {


            // 1. Get Located Folder Path

            //string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\files" + folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            // 2. Get File Name And Make it UNIQUE
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Open new FileStream to save the file in [data per time]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            // 5. save the file in the fileStream

            file.CopyTo(fileStream);

            return fileName;

            

        }


        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

        }
    }
}
