using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Solicita.TG.Utils
{
    public enum TypeMessage
    {
        TransferirResponsabilidade = 0,

        Indeferido = 1,

        Concluido = 2,

        Cancelado = 3,

        Novo = 4,

        AcessoCriado = 5
    }    
    
    public static class MailSave
    {
        private static Dictionary<TypeMessage, String> TypeMappingTemplate = new Dictionary<TypeMessage, string>() {
            { TypeMessage.Novo,  "NovoTemplate" },
            { TypeMessage.Cancelado,  "CanceladoTemplate" },
            { TypeMessage.Concluido,  "ConcluidoTemplate" },
            { TypeMessage.TransferirResponsabilidade,  "TransferidoTemplate" },
            { TypeMessage.Indeferido, "IndeferidoTemplate" },
            { TypeMessage.AcessoCriado, "AcessoCriadoTemplate" }
        };
        
        public static void SaveMail(String to, String subject, String body)
        {
            XmlDocument xDocument = new XmlDocument();

            XmlElement root = xDocument.CreateElement("Mail");

            XmlElement xTo = xDocument.CreateElement("To");
            xTo.SetAttribute("name", to);

            XmlElement xSubject = xDocument.CreateElement("Subject");
            xSubject.SetAttribute("name", subject);

            XmlElement xBody = xDocument.CreateElement("Body");
            xBody.SetAttribute("name", body);

            root.AppendChild(xTo);
            root.AppendChild(xSubject);
            root.AppendChild(xBody);
            xDocument.AppendChild(root);

            CheckDirectories();

            xDocument.Save(ConfigurationManager.AppSettings["FolderMails"].ToString() + "\\" + Guid.NewGuid().ToString() + "_" + to + "_" + subject + ".xml");
        }

        public static String GetMessage(String responsavel, String aluno, String idRequerimento, TypeMessage typeMessage, String motivo = "")
        {
            CheckDirectories();

            return GetTextInTemplate(responsavel, aluno, idRequerimento, typeMessage, motivo);
        }

        private static string ReturnPath(String key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        private static string GetTextInTemplate(String responsavel, String aluno, String idRequerimento, TypeMessage typeMessage, String motivo = "")
        {
            String retorno = string.Empty;

            retorno = File.ReadAllText(ReturnPath(TypeMappingTemplate[typeMessage])).Replace("@aluno", aluno).Replace("@idRequerimento", idRequerimento);

            if (typeMessage.Equals(TypeMessage.Cancelado) || typeMessage.Equals(TypeMessage.Indeferido))
                retorno.Replace("@motivo", motivo);

            return retorno;
        }


        #region generalizar depois
        public static String GetMessage(String aluno, String senha, String ra,TypeMessage typeMessage)
        {
            CheckDirectories();

            return GetTextInTemplate(aluno, senha, ra ,typeMessage);
        }

        private static string GetTextInTemplate(String aluno, String senha, String ra, TypeMessage typeMessage)
        {
            String retorno = string.Empty;

            String ValueMapped = TypeMappingTemplate[typeMessage];

            String Path = ReturnPath(ValueMapped);

            String TextoDoArquivo = File.ReadAllText(Path);

            retorno = TextoDoArquivo.Replace("@aluno", aluno).Replace("@usuario", ra).Replace("@senha", ra);
                       
            return retorno;
        }

        #endregion

        private static void CheckDirectories()
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["FolderDefault"].ToString()))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FolderDefault"].ToString());

            if (!Directory.Exists(ConfigurationManager.AppSettings["FolderMails"].ToString()))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FolderMails"].ToString());

            if (!Directory.Exists(ConfigurationManager.AppSettings["FolderTemplate"].ToString()))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FolderTemplate"].ToString());
        }
    }
}
