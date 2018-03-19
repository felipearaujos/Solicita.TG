using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using Solicita.TG.Processors.Strategies;
using Solicita.TG.Utils;
using System.IO;
namespace Solicita.TG.Serviços.ProcessadorDeExcel
{
    
    public class ProcessadorDeArquivoService : IProcessadorDeArquivoService
    {
    
        private Dictionary<String, IStrategy> Processors = new Dictionary<string, IStrategy>();

        public ProcessadorDeArquivoService()
        {
            Processors.Add("ALUNO", new ProcessExcelAluno());
        }


        public void ProcessarExcel(String Archive, String Content, String Entidade)
        {

            try
            {
                String arquivo = String.Empty;

                Byte[] conteudo = Convert.FromBase64String(Content);

                String filePath = ArchiveUtility.GetFilePath(Archive);

                File.WriteAllBytes(filePath, conteudo);

                String excelConnection = ConfigurationManager.AppSettings["ExcelConnection"];

                excelConnection = excelConnection.Replace("#NOMEDOARQUIVO#", filePath);
                 
                Processors[Entidade].Processar(excelConnection);

                ArchiveUtility.DeleteFileStream(filePath);
            }
            catch (Exception ex)
            {
            
            }

        }

        public String DownloadExcelModelo()
        {
            String ArchivePath = ConfigurationManager.AppSettings["ExcelPath"].ToString();

            byte[] byteArray = File.ReadAllBytes(ArchivePath);

            String ExcelBase64 = String.Format("data:application/vnd.ms-excel;base64,{0}", Convert.ToBase64String(byteArray));

            return ExcelBase64;
        }
    
    }
}
