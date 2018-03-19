using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Utils
{
    public static class ArchiveUtility
    {

        public static void CreateFileStream(String FileString, String ArchiveTitlle, String FilePath)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(FileString);
            MemoryStream xmlStream = new MemoryStream(byteArray);
            MemoryStream streamReader = new MemoryStream();
            xmlStream.CopyTo(streamReader);

            xmlStream.Seek(0, SeekOrigin.Begin);
            xmlStream.Position = 0;

            String filePath = FilePath;

            FileStream fileStream = File.Create(filePath, (int)xmlStream.Length);

            byte[] bytesInStream = new byte[xmlStream.Length];
            xmlStream.Read(bytesInStream, 0, bytesInStream.Length);
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            fileStream.Close();
        }

        public static String GetFilePath(String fileName)
        {
            String filePath = ConfigurationManager.AppSettings["TEMP"];

            if (String.IsNullOrEmpty(filePath))
            {
                filePath = Path.GetTempPath();
            }

            filePath += "\\" + Guid.NewGuid().ToString() + fileName;
            return filePath;
        }

        public static void DeleteFileStream(String filePath)
        {
            File.Delete(filePath);
        }

        public static void DeleteFilesStream(List<String> filesPath)
        {
            foreach (var aFile in filesPath)
                File.Delete(aFile);
        }
    }
}
