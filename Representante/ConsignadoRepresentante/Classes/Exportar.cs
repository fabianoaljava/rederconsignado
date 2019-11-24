using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoRepresentante;

namespace ConsignadoRepresentante
{
    public partial class Exportar
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public FormDeposito localDeposito = null;

        public Exportar(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }


        public void ExportarLimpar()
        {

            localDeposito.lblExportacaoAlerta.Text = "ATENÇÃO!" + Environment.NewLine + "Na exportação, os pedidos que estiverem com baixa parcial do contas a receber serão baixados integralmente.";
            localDeposito.btnExportarAnalisar.Enabled = true;
            localDeposito.btnExportar.Enabled = true;

            localDeposito.grdExportacao.DataSource = null;
            localDeposito.grdExportacao.Refresh();

        }


        public void ExportarAnalisar()
        {

            if (ModelLibrary.MetodosDeposito.VerificarServidor())
            {
                ModelLibrary.MetodosRepresentante.Manutencao();


                localDeposito.btnExportarAnalisar.Text = "Obtendo Análise...";
                localDeposito.btnExportarAnalisar.Enabled = false;
                localDeposito.grdExportacao.DataSource = ModelLibrary.ImportarExportar.ObterListaExportacao(localDeposito.cCargaId);


                localDeposito.grdExportacao.ClearSelection();


                localDeposito.btnExportarAnalisar.Text = "Analisar";
                localDeposito.btnExportarAnalisar.Enabled = true;



            }
            else
            {

                if (MessageBox.Show("O servidor <<NOMEDOSERVIDOR>> não foi alcançado, verifique a sua conexão e tente novamente.", "Exportação de Carga", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    ExportarAnalisar();
                }

            }

        }

        public void ExportarDados()
        {


            // Verificar se está conectado ao banco de dados
            // se sim, realizar a exportação

            if (ModelLibrary.MetodosDeposito.VerificarServidor())
            {

                ModelLibrary.MetodosRepresentante.Manutencao();

                localDeposito.btnExportar.Text = "Exportando...";
                localDeposito.btnExportar.Enabled = false;
                localDeposito.grdExportacao.DataSource = ModelLibrary.ImportarExportar.ObterListaExportacao(localDeposito.cCargaId);


                localDeposito.grdExportacao.ClearSelection();

                ///criar ProgressBar com base na quantidade de linhas das tabelas e etapas realizadas com timer
                ///


                localDeposito.bgwExportar.RunWorkerAsync();


            }
            else
            {

                if (MessageBox.Show("O servidor <<NOMEDOSERVIDOR>> não foi alcançado, verifique a sua conexão e tente novamente.", "Exportação de Carga", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    ExportarDados();
                }

            }



        }




        public Task ProcessarExportacao(string pRotina, int row)
        {



            localDeposito.grdExportacao.Rows[row].Cells["Status"].Value = "Exportando...";
            localDeposito.grdExportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Orange;

            Console.WriteLine("Index row = " + row.ToString());


            Boolean result = ModelLibrary.ImportarExportar.ProcessarRotina(pRotina);
          


            if (result == true)
            {

                localDeposito.grdExportacao.Rows[row].Cells["Status"].Value = "Exportado";
                localDeposito.grdExportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Green;                

            } else
            {

                localDeposito.grdExportacao.Rows[row].Cells["Status"].Value = "Erro";
                localDeposito.grdExportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Red;            
            }

            return Task.CompletedTask;



        }


        public void ExibirExportacao()
        {


        }
    }
}
