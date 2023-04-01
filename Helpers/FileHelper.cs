using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Workdo.Data;
using Workdo.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;

namespace Workdo.Helpers
{
    public class FileHelper
    {
        private static bool isMacOS = Environment.CurrentDirectory.Contains("/");


        public static async Task<string> SaveFileAsync(StreamContent inputStream, string fileName)
        {
            string folder = "upload\\";
            string filePath = Environment.CurrentDirectory + "\\wwwroot\\" + folder;

            if (isMacOS)
                filePath = filePath.Replace("\\", "/");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string format = new FileInfo(fileName.ToLower()).Extension;

            string rename = DateTime.Now.Ticks + format;

            string path = Path.Combine(filePath, rename);

            await using FileStream fs = new(path, FileMode.Create);
            await inputStream.CopyToAsync(fs);
            fs.Dispose();
            inputStream.Dispose();

            return $"/{folder.Replace("\\", "/")}/{rename}";
        }


        public static void DeleteFile(string filePath) 
        {
            string absoluteFilePath = $"{Environment.CurrentDirectory}/wwwroot{filePath.Replace("/", "\\")}";
            if (File.Exists(absoluteFilePath))
            {
                File.Delete(absoluteFilePath);
            }
        }


        public static bool IsValidFileType(IBrowserFile file) 
        {
            var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx" };
            var fileExtension = Path.GetExtension(file.Name);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }
        
        public static string GetIconForFileType(string name) 
        {
            var extension = Path.GetExtension(name)?.ToLowerInvariant();
            switch(extension) 
            {
                case ".doc":
                case ".docx":
                    return "word-icon";
                case ".pdf":
                    return "pdf-icon";
                case ".xls":
                case ".xlsx":
                    return "excel-icon";
                case ".png":
                case ".jpg":
                case ".jpeg":
                    return "image-icon";
                default:
                    return "";
            }
        } 
    }
}
