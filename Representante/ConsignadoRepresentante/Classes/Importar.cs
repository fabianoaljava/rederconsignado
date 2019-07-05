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
    public partial class Importar
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public long cImportarProdutoId;
        public string cModoConferenciaProduto;


        public FormDeposito localDeposito = null;

        public Importar(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }

        ////////////////////////////////////////
        /// Carregar Formulário Local / Servidor
        ////////////////////////////////////////

        public void CarregarFormularioLocal()
        {

            CarregarListaPracaLocal();
            CarregarListaRepresentanteLocal();

        }



        void CarregarListaPracaLocal()
        {
            localDeposito.cbbImportarPraca.DataSource = ModelLibrary.MetodosRepresentante.ObterListaPracas();
            localDeposito.cbbImportarPraca.DisplayMember = "Descricao";
            localDeposito.cbbImportarPraca.ValueMember = "Id";
            localDeposito.cbbImportarPraca.Invalidate();
            localDeposito.cbbImportarPraca.SelectedIndex = -1;

        }
        void CarregarListaRepresentanteLocal()
        {
            localDeposito.cbbImportarRepresentante.DataSource = ModelLibrary.MetodosRepresentante.ObterListaRepresentantes();
            localDeposito.cbbImportarRepresentante.DisplayMember = "Nome";
            localDeposito.cbbImportarRepresentante.ValueMember = "Id";
            localDeposito.cbbImportarRepresentante.Invalidate();
            localDeposito.cbbImportarRepresentante.SelectedIndex = -1;

        }

        public void CarregarFormularioServer()
        {
            CarregarListaPracaServer();
            CarregarListaRepresentanteServer();
        }


        void CarregarListaPracaServer()
        {
            localDeposito.cbbImportarPraca.DataSource = ModelLibrary.MetodosDeposito.ObterListaPracas();
            localDeposito.cbbImportarPraca.DisplayMember = "Descricao";
            localDeposito.cbbImportarPraca.ValueMember = "Id";
            localDeposito.cbbImportarPraca.Invalidate();
            localDeposito.cbbImportarPraca.SelectedIndex = -1;

        }
        void CarregarListaRepresentanteServer()
        {
            localDeposito.cbbImportarRepresentante.DataSource = ModelLibrary.MetodosDeposito.ObterListaRepresentantes();
            localDeposito.cbbImportarRepresentante.DisplayMember = "Nome";
            localDeposito.cbbImportarRepresentante.ValueMember = "Id";
            localDeposito.cbbImportarRepresentante.Invalidate();
            localDeposito.cbbImportarRepresentante.SelectedIndex = -1;

        }




        ////////////////////////////////////////
        /// Importacao
        ////////////////////////////////////////


        public void ImportarLimpar()
        {
            localDeposito.cbbImportarPraca.SelectedIndex = -1;
            localDeposito.cbbImportarRepresentante.SelectedIndex = -1;
            localDeposito.txtImportarCodPraca.Text = "";
            localDeposito.txtImportarCodPraca.WaterMark = "Cod. Praça";
            localDeposito.txtImportarCodRepresentante.Text = "";
            localDeposito.txtImportarCodRepresentante.WaterMark = "Cod. Representante";
            localDeposito.cbbImportarMesAno.ResetText();
            localDeposito.tbcImportarConferencia.Visible = false;
            localDeposito.txtConfCodigoBarras.Text = "";
            localDeposito.txtConfProduto.Text = "";
            localDeposito.txtConfQuantidade.Text = "";
            localDeposito.btnConferenciaConfirmar.Enabled = false;

            localDeposito.pnlImportarPesquisa.Enabled = true;
            localDeposito.pnlExportacaoMain.Enabled = true;
        }






        public void ImportarCarga(Boolean pAnalisarFirst = false)
        {

            if (ModelLibrary.MetodosDeposito.VerificarServidor())
            {
                /* Obter os Campos Selecionados */


                ModelLibrary.Representante representante = (ModelLibrary.Representante)localDeposito.cbbImportarRepresentante.SelectedItem;
                var representanteId = representante.Id;
                ModelLibrary.Praca praca = (ModelLibrary.Praca)localDeposito.cbbImportarPraca.SelectedItem;
                var pracaId = praca.Id;
                int mes = localDeposito.cbbImportarMesAno.Value.Month;
                int ano = localDeposito.cbbImportarMesAno.Value.Year;

                /* Procurar Carga no BD com os dados selecionados */
                var carga = ModelLibrary.MetodosDeposito.ObterCarga(representanteId, pracaId, mes, ano);

                if (carga != null) /* Se existir Carga */
                {
                    


                    //verificar status da carga e aplicar regras de negócio // Colocar código no motor de regras.
                    if (ControllerLibrary.Regras.PermiteImportacaoCarga(carga.Status))
                    {



                        Console.WriteLine("Importação da carga" + carga.Id);

                        Cursor.Current = Cursors.WaitCursor;

                        if (pAnalisarFirst)
                        {

                            localDeposito.btnImportarAnalisar.Text = "Obtendo Análise...";
                            localDeposito.btnImportarAnalisar.Enabled = false;
                            


                            localDeposito.grdImportacao.DataSource = ModelLibrary.ImportarExportar.ObterListaImportacao(carga.Id);


                            localDeposito.grdImportacao.ClearSelection();


                            localDeposito.btnImportarAnalisar.Text = "Analisar";
                            localDeposito.btnImportarAnalisar.Enabled = true;
                        }
                        else
                        {

                            // realizar importação 

                            localDeposito.btnImportar.Text = "Importando...";
                            localDeposito.pnlImportarPesquisa.Enabled = false;


                            //if (ModelLibrary.ImportarExportar.ImportarOldStyle(representanteId, pracaId, mes, ano))
                            //{

                            //    MessageBox.Show("Carga Importada com sucesso");
                            //    localDeposito.CarregarRepresentante();

                            //}

                            localDeposito.grdImportacao.DataSource = ModelLibrary.ImportarExportar.ObterListaImportacao(carga.Id);

                            localDeposito.grdImportacao.ClearSelection();


                            /////criar ProgressBar com base na quantidade de linhas das tabelas e etapas realizadas com timer
                            ///


                            localDeposito.bgwImportar.RunWorkerAsync();





                        }


                        Cursor.Current = Cursors.Default;




                    }
                    else
                    {
                        MessageBox.Show("A carga informada não pode ser importada, pois não está em aberto. Status: " + carga.Status, "Importação de Carga Não Permitida.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
                else /* Se não existir */
                {
                    MessageBox.Show("Não foi encontrada nenhuma carga com os dados informados.", "Não encontrado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {

                if (MessageBox.Show("O servidor <<NOMEDOSERVIDOR>> não foi alcançado, verifique a sua conexão e tente novamente.", "Importação de Carga", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    ImportarCarga(pAnalisarFirst);
                }

            }
        }


        public Task ProcessarImportacao(string pRotina, int row)
        {



            localDeposito.grdImportacao.Rows[row].Cells["Status"].Value = "...";
            //localDeposito.grdImportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Orange;

            Console.WriteLine("Index row = " + row.ToString());


            Boolean result = ModelLibrary.ImportarExportar.ProcessarRotina(pRotina);


            if (result == true)
            {

                localDeposito.grdImportacao.Rows[row].Cells["Status"].Value = "Importado";
                localDeposito.grdImportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Green;

            }
            else
            {

                localDeposito.grdImportacao.Rows[row].Cells["Status"].Value = "Erro";
                localDeposito.grdImportacao.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
            }

            return Task.CompletedTask;



        }


        public void ExibirImportacao()
        {



            var carga = ModelLibrary.MetodosRepresentante.ObterCargaAtual();

            int cargaId = Convert.ToInt32(carga.Id);

            localDeposito.cCargaId = cargaId;
            localDeposito.cStatus = carga.Status;

            int representanteId = Convert.ToInt32(carga.RepresentanteId);
            int pracaId = Convert.ToInt32(carga.PracaId);


            localDeposito.txtImportarCodPraca.WaterMark = carga.PracaId.ToString();
            localDeposito.txtImportarCodPraca.Text = carga.PracaId.ToString();
            localDeposito.txtImportarCodRepresentante.WaterMark = carga.RepresentanteId.ToString();
            localDeposito.txtImportarCodPraca.Text = carga.RepresentanteId.ToString();

            localDeposito.cbbImportarMesAno.Text = carga.Mes.ToString() + "/" + carga.Ano.ToString();

            var praca = ModelLibrary.MetodosRepresentante.ObterPraca(pracaId);
            localDeposito.cbbImportarPraca.SelectedIndex = praca == null ? -1 : localDeposito.cbbImportarPraca.FindString(praca.Descricao);
            var representante = ModelLibrary.MetodosRepresentante.ObterRepresentante(representanteId);
            localDeposito.cbbImportarRepresentante.SelectedIndex = representante == null ? -1 : localDeposito.cbbImportarRepresentante.FindString(representante.Nome);

            localDeposito.lblCarga.Text += " " + praca.Descricao.Trim() + " | " + representante.Nome.Trim() + " | " + carga.Mes.ToString() + "/" + carga.Ano.ToString();

            localDeposito.btnImportar.Text = "Importar";
            localDeposito.btnImportar.Visible = true;            
            localDeposito.btnImportarLimpar.Visible = true;


            var totalizadores = ModelLibrary.MetodosRepresentante.ObterTotalizadores(cargaId);

            localDeposito.dlbQtdProdutos.Text = totalizadores.QtdProdutos.ToString();
            localDeposito.dlbTotalProdutos.Text = String.Format("{0:C2}", totalizadores.TotalProdutos.ToString());


            localDeposito.dlbImportarDataAbertura.Text = carga.DataAbertura.HasValue ? carga.DataAbertura.Value.ToShortDateString() : "-";
            localDeposito.dlbCargaDataExportacao.Text = carga.DataExportacao.HasValue ? carga.DataExportacao.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataRetorno.Text = carga.DataRetorno.HasValue ? carga.DataRetorno.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataConferencia.Text = carga.DataConferencia.HasValue ? carga.DataConferencia.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataFinalizacao.Text = carga.DataFinalizacao.HasValue ? carga.DataFinalizacao.Value.ToShortDateString() : "-";

            
            var cargaanterior = ModelLibrary.MetodosRepresentante.ObterCargaAnterior();

            if (cargaanterior != null)
            {

                localDeposito.dlbCargaAnteriorDataAbertura.Text = cargaanterior.DataAbertura.HasValue ? cargaanterior.DataAbertura.Value.ToShortDateString() : "-";
                localDeposito.dlbCargaAnteriorDataExportacao.Text = cargaanterior.DataExportacao.HasValue ? cargaanterior.DataExportacao.Value.ToShortDateString() : "-";
                localDeposito.dlbCargaAnteriorDataRetorno.Text = cargaanterior.DataRetorno.HasValue ? cargaanterior.DataRetorno.Value.ToShortDateString() : "-";
                localDeposito.dlbCargaAnteriorDataConferencia.Text = cargaanterior.DataConferencia.HasValue ? cargaanterior.DataConferencia.Value.ToShortDateString() : "-";
                localDeposito.dlbCargaAnteriorDataFinalizacao.Text = cargaanterior.DataFinalizacao.HasValue ? cargaanterior.DataFinalizacao.Value.ToShortDateString() : "-";

            }
            else
            {

                localDeposito.dlbCargaAnteriorDataAbertura.Text = "ND";
                localDeposito.dlbCargaAnteriorDataExportacao.Text = "ND";
                localDeposito.dlbCargaAnteriorDataRetorno.Text = "ND";
                localDeposito.dlbCargaAnteriorDataConferencia.Text = "ND";
                localDeposito.dlbCargaAnteriorDataFinalizacao.Text = "ND";

            }
            




            //Exibir opção para Re-importar
            localDeposito.btnExcluirImportacao.Visible = true;
            localDeposito.btnExcluirImportacao.Enabled = true;


            //obtem dados da carga (local) com tudo bloqueado
            localDeposito.pnlImportarPesquisa.Enabled = false;


            //se a carga já foi exportada

            if (carga.Status != "A")
            {
                localDeposito.lblExportacaoAlerta.Text = "A Carga foi exportada em " + carga.DataExportacao.ToString();
                localDeposito.pnlExportacaoMain.Enabled = false;

                //Desativar aba conferencia de produtos 
                localDeposito.pnlConferirProduto.Enabled = false;

                //Desativar aba suplemento
            } else
            {
                localDeposito.pnlExportacaoMain.Enabled = true;
                localDeposito.pnlConferirProduto.Enabled = true;
            }
            

           


        }


        public void ExcluirImportacao(Boolean pReverteCarga = true)
        {



            localDeposito.btnExcluirImportacao.Text = "Excluindo...";
            localDeposito.btnExcluirImportacao.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ModelLibrary.ImportarExportar.ExcluirImportacao();



            if (pReverteCarga)
            {
                ModelLibrary.MetodosDeposito.AlterarStatusCarga(localDeposito.cCargaId, "A");

            }

            MessageBox.Show("Importação excluída com sucesso!");
            localDeposito.btnExcluirImportacao.Text = "Exclur \n Importação";
            ImportarLimpar();
            localDeposito.CarregarRepresentante();
            localDeposito.btnExcluirImportacao.Text = "Excluir";
            localDeposito.btnExcluirImportacao.Visible = false;
            Cursor.Current = Cursors.Default;

        }


        


    }
}
