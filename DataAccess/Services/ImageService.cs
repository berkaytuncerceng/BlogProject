using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Services
{
    public static class ImageService
    {
        public static async Task<string> SaveImageAsync(IFormFile imageFile, string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/blogs/" + uniqueFileName;
        }

        public static void DeleteImage(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return;

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
