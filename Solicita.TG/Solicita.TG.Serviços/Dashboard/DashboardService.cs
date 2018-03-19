using Solicita.TG.Domínio.EntidadeAplicacao;
using Solicita.TG.Domínio.Entidades.Requerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.StatusRequerimento;
using Solicita.TG.Domínio.Entidades.Requerimento.TiposRequerimento;
using Solicita.TG.Persistência.Repositories.RequerimentoRepository;
using Solicita.TG.Serviços.Dashboard.Modelos;
using Solicita.TG.Serviços.Mapper;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Serviços.Dashboard
{
    public class DashboardService: IDashboardService
    {
        private RequerimentoRepository RequerimentoRepository;

        public DashboardService()
        {
            RequerimentoRepository = new RequerimentoRepository();
        }

        public VolumetriaAnual CriarGraficoDeVolumetriaAnual()
        {
            VolumetriaAnual volumetria = new VolumetriaAnual();

            DateTime inicio = DateTime.Now.AddMonths(-5);
            DateTime final = DateTime.Now;

            for (DateTime dt = inicio; dt <= final; dt = dt.AddMonths(1))
            {
                String LabelDate = dt.Year.ToString() + " " + System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(dt.Month).ToUpper();

                volumetria.MesesNoPeriodo.Add(LabelDate);

                var retorno = RequerimentoRepository.VerificarVolumetriaMensal(dt);
                if (retorno.Count.Equals(0))
                {
                    Array values = Enum.GetValues(typeof(TiposDeDocumentos));

                    foreach (TiposDeDocumentos val in values)
                    {
                        String NomeTipo = Enumerations.GetEnumDescription(val);
                        Int32 valor = 0;
                        
                        if (!volumetria.Chart.Any(x => x.Tipo.Equals(NomeTipo)))
                        {
                            VolumetriaAnualChartData ChartData = new VolumetriaAnualChartData();
                            ChartData.Tipo = NomeTipo;
                            ChartData.LabelDate = LabelDate;
                            volumetria.Chart.Add(ChartData);
                        }
                        volumetria.Chart.Single(x => x.Tipo.Equals(NomeTipo)).value.Add(valor);
                    }

                }


                foreach (var aItem in retorno)
                {

                    String NomeTipo = Enumerations.GetEnumDescription(aItem.Key);
                    Int32 valor = aItem.Value;

                    if(!volumetria.Chart.Any(x => x.Tipo.Equals(NomeTipo)))
                    {
                        VolumetriaAnualChartData ChartData =  new VolumetriaAnualChartData();
                        ChartData.Tipo = NomeTipo;
                        ChartData.LabelDate = LabelDate;
                        volumetria.Chart.Add(ChartData);
                    }

                    volumetria.Chart.Single(x => x.Tipo.Equals(NomeTipo)).value.Add(valor);
                }                
            }


            return volumetria;
        }

        public VolumetriaMensalModel CriarGraficoDeVolumetriaMensal()
        {
            VolumetriaMensalModel volumetria = new VolumetriaMensalModel();

            try
            {
                var retorno = RequerimentoRepository.VerificarVolumetriaMensal(DateTime.Now);

                foreach(var aItem in retorno)
                {
                    volumetria.VolumetriaPorMes.Add(new VolumetriaMensalPorCategoriaModel() { Name = Enumerations.GetEnumDescription(aItem.Key), Value = aItem.Value });
                }
            }
            catch(Exception e)
            {

            }

            return volumetria;
        }

        public List<CustomDashBoardModel> GraficoQuantidadePorTipo(List<Int32> status, Guid aluno, Guid responsavel, Boolean agruparPorTipo, Guid Disciplina, String dataInicial, String dataFinal)
        {
            List<CustomDashBoardModel> retorno = new List<CustomDashBoardModel>();
            RequerimentoRepository requerimentoRepository = new RequerimentoRepository();

            List<QuantidadePorTipo> dadosDoRelatorio = requerimentoRepository.RelatorioCustomizadoRequerimento(aluno, responsavel, status, agruparPorTipo, Disciplina, dataInicial, dataFinal);

            
            if (agruparPorTipo)
            {
                Array values = Enum.GetValues(typeof(TiposDeDocumentos));
                foreach (TiposDeDocumentos val in values)
                {
                    String NomeTipo = Enumerations.GetEnumDescription(val);
                    Int32 valor = 0;

                    CustomDashBoardModel modelo = new CustomDashBoardModel();
                    modelo.Tipo = NomeTipo;
                    modelo.CountPorTipo = valor;

                    retorno.Add(modelo);
                }
            }
            else
            {
                Array values = Enum.GetValues(typeof(TipoDeStatus));
                foreach (TipoDeStatus val in values)
                {
                    String NomeTipo = Enumerations.GetEnumDescription(val);
                    Int32 valor = 0;

                    CustomDashBoardModel modelo = new CustomDashBoardModel();
                    modelo.Tipo = NomeTipo;
                    modelo.CountPorTipo = valor;

                    retorno.Add(modelo);
                }
            }

            foreach (QuantidadePorTipo ent in dadosDoRelatorio)
            {
                retorno.Single(x => x.Tipo.ToUpper().Equals(ent.Tipo.ToUpper())).CountPorTipo = ent.CountPorTipo;
            }

            return retorno;

        }


        public ResumoAtual ResumoAtual()
        {
            RequerimentoRepository requerimentoRepository = new RequerimentoRepository();


            List<TipoDeStatus> status = new List<TipoDeStatus>();
            status.Add(TipoDeStatus.AguardandoInformações);
            status.Add(TipoDeStatus.EmAnálise);
            status.Add(TipoDeStatus.Novo);
            Int32 totalEmAndamento = requerimentoRepository.TotalDeRequerimentosPorStatus(status, DateTime.Now);
           
            double mediaEmHoras = 0;
            var requerimentoMapper = new RequerimentoModelMapper();
            String RA = String.Empty; String nome = String.Empty; Guid curso = Guid.Empty; Int32 periodo = 0; String type = "SolicitacaoDeDocumentos"; Guid alunoId = Guid.Empty;

            var TodosReq = requerimentoRepository.ListAll(requerimentoMapper.ReturnEspecificType(type), requerimentoMapper.ReturnEspecificTypeEnum(type));
            var requerimentos = TodosReq.Where(x => x.StatusAtual.StatusType.Equals(TipoDeStatus.Concluido)).ToList();

            List<Int32> TempoGastoEmCadaRequerimento = new List<int>();
            foreach (var req in requerimentos)
            {
                Int32 tempo = (req.StatusAtual.DataEntrada - req.HistóricoDeStatus.First().DataEntrada).Hours;
                TempoGastoEmCadaRequerimento.Add(tempo);
            }

            if(TempoGastoEmCadaRequerimento.Count > 0)
                mediaEmHoras = TempoGastoEmCadaRequerimento.Average();

            if(mediaEmHoras != 0)
                mediaEmHoras = Math.Round(mediaEmHoras, 2);

            return new ResumoAtual() { MediaConclusao = mediaEmHoras, QuantidadeEmAndamento = totalEmAndamento};

        }

        
    }
}
