﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36366
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Solicita.TG.UI.View.ProcessadorDeArquivoService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProcessadorDeArquivoService.IProcessadorDeArquivoService")]
    public interface IProcessadorDeArquivoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProcessadorDeArquivoService/ProcessarExcel", ReplyAction="http://tempuri.org/IProcessadorDeArquivoService/ProcessarExcelResponse")]
        void ProcessarExcel(string Archive, string Content, string Entidade);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProcessadorDeArquivoService/ProcessarExcel", ReplyAction="http://tempuri.org/IProcessadorDeArquivoService/ProcessarExcelResponse")]
        System.Threading.Tasks.Task ProcessarExcelAsync(string Archive, string Content, string Entidade);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProcessadorDeArquivoService/DownloadExcelModelo", ReplyAction="http://tempuri.org/IProcessadorDeArquivoService/DownloadExcelModeloResponse")]
        string DownloadExcelModelo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProcessadorDeArquivoService/DownloadExcelModelo", ReplyAction="http://tempuri.org/IProcessadorDeArquivoService/DownloadExcelModeloResponse")]
        System.Threading.Tasks.Task<string> DownloadExcelModeloAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProcessadorDeArquivoServiceChannel : Solicita.TG.UI.View.ProcessadorDeArquivoService.IProcessadorDeArquivoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessadorDeArquivoServiceClient : System.ServiceModel.ClientBase<Solicita.TG.UI.View.ProcessadorDeArquivoService.IProcessadorDeArquivoService>, Solicita.TG.UI.View.ProcessadorDeArquivoService.IProcessadorDeArquivoService {
        
        public ProcessadorDeArquivoServiceClient() {
        }
        
        public ProcessadorDeArquivoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProcessadorDeArquivoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessadorDeArquivoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessadorDeArquivoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ProcessarExcel(string Archive, string Content, string Entidade) {
            base.Channel.ProcessarExcel(Archive, Content, Entidade);
        }
        
        public System.Threading.Tasks.Task ProcessarExcelAsync(string Archive, string Content, string Entidade) {
            return base.Channel.ProcessarExcelAsync(Archive, Content, Entidade);
        }
        
        public string DownloadExcelModelo() {
            return base.Channel.DownloadExcelModelo();
        }
        
        public System.Threading.Tasks.Task<string> DownloadExcelModeloAsync() {
            return base.Channel.DownloadExcelModeloAsync();
        }
    }
}
