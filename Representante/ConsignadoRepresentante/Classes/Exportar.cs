using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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


        public void ExportarDados(long pCargaId)
        {

            // Verificar se está conectado ao banco de dados
            // se sim, realizar a exportação

            if (ModelLibrary.MetodosDeposito.VerificarServidor()) {

                Cursor.Current = Cursors.WaitCursor;


                localDeposito.btnExportar.Text = "Exportando...";
                localDeposito.btnExportar.Enabled = false;
                localDeposito.grdExportacao.DataSource = ModelLibrary.ImportarExportar.ObterListaExportacao(pCargaId);


                localDeposito.grdExportacao.ClearSelection();




                ///criar ProgressBar com base na quantidade de linhas das tabelas e etapas realizadas com timer
                ///



               
                DataGridViewRow row;
                    
                row = localDeposito.grdExportacao.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["Rotina"].Value.ToString().Equals("ExportarAtualizarCarga"))
                        .First();

                localDeposito.grdExportacao.Rows[row.Index].Cells["Status"].Value = "Exportando...";
                localDeposito.grdExportacao.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Orange;

                Console.WriteLine("Index row = " + row.Index.ToString());


                if (ModelLibrary.ImportarExportar.ExportarAtualizarCarga())
                {

                    localDeposito.grdExportacao.Rows[row.Index].Cells["Status"].Value = "Exportado";
                    localDeposito.grdExportacao.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Green;

                }
 
                Cursor.Current = Cursors.Default;

                localDeposito.btnExportar.Text = "Exportação Realizada.";
                

  

            } else
            {

                if (MessageBox.Show("O servidor <<NOMEDOSERVIDOR>> não foi alcançado, verifique a sua conexão e tente novamente.", "Exportação de Carga", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    ExportarDados(pCargaId);
                }

            }



        }
    }
}
