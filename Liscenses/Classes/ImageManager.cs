using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liscenses.Classes
{

public static class ImageManager
    {
        public static string UpdateStoredImage(
            string storeFilePath,
            string oldImagePath,
            string newImageSourcePath,
             string targetDirectory = @"C:\Images\"
            )
        {
            if (!File.Exists(newImageSourcePath))
            {
              MessageBox.Show("New image source does not exist.");
                return null;
            }

           
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            try
            {
                var lines = File.Exists(storeFilePath)
                    ? File.ReadAllLines(storeFilePath)
                    : Array.Empty<string>();
                
                    var updatedLines = lines
                        .Where(line => !line.Equals(oldImagePath, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                

                if (!string.IsNullOrEmpty(oldImagePath) && File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                   // MessageBox.Show("Old image file deleted.");
                }

              
                string ext = Path.GetExtension(newImageSourcePath);
                string uniqueFileName = $"{Guid.NewGuid()}{ext}";
                string newImagePath = Path.Combine(targetDirectory, uniqueFileName);

                File.Copy(newImageSourcePath, newImagePath);

                updatedLines.Add(newImagePath);
                string directory = Path.GetDirectoryName(storeFilePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory); 
                }

                File.WriteAllLines(storeFilePath, updatedLines);

                return newImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating stored image: " + ex.Message);
                return null;
            }
        }
    }
}

