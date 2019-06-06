using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            localDeposito.txtImportarCodRepresentante.Text = "";
            localDeposito.cbbImportarMesAno.ResetText();
            localDeposito.tbcImportarConferencia.Visible = false;
            localDeposito.txtConfCodigoBarras.Text = "";
            localDeposito.txtConfProduto.Text = "";
            localDeposito.txtConfQuantidade.Text = "";
            localDeposito.btnConferenciaConfirmar.Enabled = false;

            localDeposito.pnlImportarPesquisa.Enabled = true;
        }



        public void ImportarCarga()
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
                //verificar status da carga e aplicar regras de negócio
                // realizar importação 

                Cursor.Current = Cursors.WaitCursor;

                localDeposito.btnImportar.Text = "Importando...";
                localDeposito.pnlImportarPesquisa.Enabled = false;



                if (ModelLibrary.ImportarExportar.Importar(representanteId, pracaId, mes, ano))
                {

                    MessageBox.Show("Carga Importada com sucesso");
                    localDeposito.CarregarRepresentante();

                }

                Cursor.Current = Cursors.Default;

                //- alterar status da carga
            }
            else /* Se não existir */
            {
                MessageBox.Show("Não foi encontrada nenhuma carga com os dados informados.", "Não encontrado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void ExibirImportacao()
        {

            //obtem dados da carga (local) com tudo bloqueado
            localDeposito.pnlImportarPesquisa.Enabled = false;

            var carga = ModelLibrary.MetodosRepresentante.ObterCargaAtual();

            int cargaId = Convert.ToInt32(carga.Id);

            localDeposito.cCargaId = cargaId;

            int representanteId = Convert.ToInt32(carga.RepresentanteId);
            int pracaId = Convert.ToInt32(carga.PracaId);


            localDeposito.txtImportarCodPraca.Text = carga.PracaId.ToString();
            localDeposito.txtImportarCodRepresentante.Text = carga.RepresentanteId.ToString();

            localDeposito.cbbImportarMesAno.Text = carga.Mes.ToString() + "/" + carga.Ano.ToString();

            var praca = ModelLibrary.MetodosRepresentante.ObterPraca(pracaId);
            localDeposito.cbbImportarPraca.SelectedIndex = praca == null ? -1 : localDeposito.cbbImportarPraca.FindString(praca.Descricao);
            var representante = ModelLibrary.MetodosRepresentante.ObterRepresentante(representanteId);
            localDeposito.cbbImportarRepresentante.SelectedIndex = representante == null ? -1 : localDeposito.cbbImportarRepresentante.FindString(representante.Nome);

            localDeposito.lblCarga.Text += " " + praca.Descricao.Trim() + " | " + representante.Nome.Trim() + " | " + carga.Mes.ToString() + "/" + carga.Ano.ToString();

            localDeposito.btnImportar.Text = "Importar";
            localDeposito.btnImportar.Visible = true;
            localDeposito.btnImportarPesquisar.Visible = true;
            localDeposito.btnImportarLimpar.Visible = true;



            localDeposito.tbcImportarConferencia.Visible = true;


            ExibirConferenciaProduto(cargaId);


            localDeposito.dlbImportarDataAbertura.Text = carga.DataAbertura.HasValue ? carga.DataAbertura.Value.ToShortDateString() : "-";
            localDeposito.dlbCargaDataExportacao.Text = carga.DataExportacao.HasValue ? carga.DataExportacao.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataRetorno.Text = carga.DataRetorno.HasValue ? carga.DataRetorno.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataConferencia.Text = carga.DataConferencia.HasValue ? carga.DataConferencia.Value.ToShortDateString() : "-";
            localDeposito.dlbImportarDataFinalizacao.Text = carga.DataFinalizacao.HasValue ? carga.DataFinalizacao.Value.ToShortDateString() : "-";

            /*
             * var viagemanterior = ModelLibrary.MetodosRepresentante.ObterViagemAnterior(representanteId, pracaId, carga.DataAbertura.Value);

            if (viagemanterior != null)
            {

                dlbCargaVADataAbertura.Text = viagemanterior.DataAbertura.HasValue ? viagemanterior.DataAbertura.Value.ToShortDateString() : "-";
                dlbCargaVADataExportacao.Text = viagemanterior.DataExportacao.HasValue ? viagemanterior.DataExportacao.Value.ToShortDateString() : "-";
                dlbCargaVADataRetorno.Text = viagemanterior.DataRetorno.HasValue ? viagemanterior.DataRetorno.Value.ToShortDateString() : "-";
                dlbCargaVADataConferencia.Text = viagemanterior.DataConferencia.HasValue ? viagemanterior.DataConferencia.Value.ToShortDateString() : "-";
                dlbCargaVADataFinalizacao.Text = viagemanterior.DataFinalizacao.HasValue ? viagemanterior.DataFinalizacao.Value.ToShortDateString() : "-";

            }
            else
            {

                dlbCargaVADataAbertura.Text = "ND";
                dlbCargaVADataExportacao.Text = "ND";
                dlbCargaVADataRetorno.Text = "ND";
                dlbCargaVADataConferencia.Text = "ND";
                dlbCargaVADataFinalizacao.Text = "ND";

            }
            */

            //Exibir opção para Re-importar
            localDeposito.btnExcluirImportacao.Visible = true;
            localDeposito.btnExcluirImportacao.Enabled = true;
        }


        public void ExcluirImportacao()
        {
            localDeposito.btnExcluirImportacao.Text = "Excluindo...";
            localDeposito.btnExcluirImportacao.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ModelLibrary.ImportarExportar.ExcluirImportacao();
            MessageBox.Show("Importação excluída com sucesso!");
            localDeposito.btnExcluirImportacao.Text = "Exclur \n Importação";
            ImportarLimpar();
            localDeposito.CarregarRepresentante();
            localDeposito.btnExcluirImportacao.Text = "Excluir";
            localDeposito.btnExcluirImportacao.Visible = false;
            Cursor.Current = Cursors.Default;
        }


        ////////////////////////////////////////
        /// Conferencia de Produtos
        ////////////////////////////////////////


        public void ConferenciaProdutoLimpar()
        {
            localDeposito.txtConfCodigoBarras.Text = "";
            localDeposito.txtConfProduto.Text = "";
            localDeposito.txtConfQuantidade.Text = "";
            localDeposito.btnConferenciaConfirmar.Enabled = false;
            localDeposito.btnConfCancelar.Enabled = false;
            localDeposito.txtConfCodigoBarras.ReadOnly = false;
            cImportarProdutoId = 0;
            cModoConferenciaProduto = "Insert";
        }


        public void PesquisarConferenciaProduto(string pCodigo)
        {



            var produtograde = ModelLibrary.MetodosRepresentante.ObterProdutoGrade(pCodigo);

            if (produtograde != null)
            {

                var produto = ModelLibrary.MetodosRepresentante.ObterProduto(produtograde.CodigoBarras);

                localDeposito.txtConfProduto.Text = produto.Descricao;

                if (localDeposito.txtConfCodigoBarras.Text != produtograde.CodigoBarras + produtograde.Digito)
                {
                    localDeposito.txtConfCodigoBarras.Text = produtograde.CodigoBarras + produtograde.Digito;
                    if (localDeposito.chkConfQuantidade.Checked == false)
                    {
                        localDeposito.chkConfQuantidade.Checked = true;
                        localDeposito.txtConfQuantidade.Enabled = true;
                    }
                }



                cImportarProdutoId = produtograde.Id;

                localDeposito.btnConferenciaConfirmar.Enabled = true;
                localDeposito.btnConfCancelar.Enabled = true;

                if (localDeposito.chkConfQuantidade.Checked)
                {
                    localDeposito.txtConfQuantidade.Focus();

                }
                else
                {
                    //inserir direto qtd=1
                    InserirCargaProdutoConferencia();
                }

            }
            else
            {

                MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                cImportarProdutoId = 0;
                localDeposito.txtConfCodigoBarras.Focus();
                localDeposito.btnConferenciaConfirmar.Enabled = false;
                localDeposito.btnConfCancelar.Enabled = false;


            }
        }

        public void ExibirConferenciaProduto(long pCargaId)
        {

            ModelLibrary.RepRepresentante representante = (ModelLibrary.RepRepresentante)localDeposito.cbbImportarRepresentante.SelectedItem;
            var representanteId = representante.Id;
            ModelLibrary.RepPraca praca = (ModelLibrary.RepPraca)localDeposito.cbbImportarPraca.SelectedItem;
            var pracaId = praca.Id;
            int mes = localDeposito.cbbImportarMesAno.Value.Month;
            int ano = localDeposito.cbbImportarMesAno.Value.Year;

            localDeposito.grdConfProduto.DataSource = ModelLibrary.MetodosRepresentante.ObterProdutosConferencia(pCargaId);

            /// Ocultar coluna CargaProdutoId
            localDeposito.grdConfProduto.Columns[5].Visible = false;
            localDeposito.grdConfProduto.Columns[6].Visible = false;
            localDeposito.grdConfProduto.Columns[7].Visible = false;
            localDeposito.grdConfProduto.Columns[8].Visible = false;


            /// Alterar Título da Coluna
            localDeposito.grdConfProduto.Columns[2].HeaderText = "Quantidade Carga";
            localDeposito.grdConfProduto.Columns[3].HeaderText = "Quantidade Informada";
            localDeposito.grdConfProduto.Columns[4].HeaderText = "Diferença";




        }

        public void InserirCargaProdutoConferencia()
        {
            try
            {
                decimal vQuantidade;

                if (localDeposito.chkConfQuantidade.Checked)
                {

                    if (localDeposito.txtConfQuantidade.Text != "")
                    {
                        vQuantidade = Convert.ToDecimal(localDeposito.txtConfQuantidade.Text);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, informe uma quantidade.");
                        vQuantidade = 0;
                    }

                }
                else
                {

                    vQuantidade = 1;

                }

                if (vQuantidade > 0)
                {
                    ModelLibrary.MetodosRepresentante.InserirProdutoConferencia(localDeposito.cCargaId, cImportarProdutoId, vQuantidade);
                    ExibirConferenciaProduto(localDeposito.cCargaId);
                    ConferenciaProdutoLimpar();
                }


            }
            catch (IOException vE)
            {
                MessageBox.Show("Ocorreu um erro ao Inserir o produto. Verifique os dados digitados e tente novamente. Se o erro persisitr, contate o administrador.");
                Console.WriteLine(vE.Message);
            }


        }




        public void EditarConferenciaProduto()
        {

            //ClearCargaProduto();


            cModoConferenciaProduto = "Edit";
            cImportarProdutoId = Convert.ToInt32(localDeposito.grdConfProduto.CurrentRow.Cells[8].Value);

            localDeposito.txtConfCodigoBarras.Text = localDeposito.grdConfProduto.CurrentRow.Cells[0].Value.ToString();
            localDeposito.txtConfCodigoBarras.ReadOnly = true;


            if (localDeposito.chkConfQuantidade.Checked == false)
            {
                localDeposito.chkConfQuantidade.Checked = true;
                localDeposito.txtConfQuantidade.Enabled = true;
            }

            if (localDeposito.grdConfProduto.CurrentRow.Cells[3].Value is null)
            {
                cModoConferenciaProduto = "Insert";
                localDeposito.txtConfQuantidade.Text = "";
                localDeposito.txtConfQuantidade.Focus();
            }
            else
            {
                localDeposito.txtConfQuantidade.Text = localDeposito.grdConfProduto.CurrentRow.Cells[3].Value.ToString();
                localDeposito.txtConfQuantidade.Focus();
            }


            localDeposito.txtConfProduto.Text = localDeposito.grdConfProduto.CurrentRow.Cells[1].Value.ToString();


            localDeposito.btnConferenciaConfirmar.Enabled = true;
            localDeposito.btnConfCancelar.Enabled = true;


        }




        public void AlterarCargaProdutoConferencia()
        {

            /*try
            {*/

            ModelLibrary.MetodosRepresentante.AlterarProdutoConferencia(localDeposito.cCargaId, cImportarProdutoId, Convert.ToDecimal(localDeposito.txtConfQuantidade.Text));

            ExibirConferenciaProduto(localDeposito.cCargaId);

            ConferenciaProdutoLimpar();

            /*} catch
            {

                MessageBox.Show("Não foi possível alterar o produto. Por favor, verifique os dados digitados e tente novamente");

            }*/


        }


        public void ConfirmarCargaProdutoConferencia()
        {

            if (cModoConferenciaProduto == "Edit")
            {
                AlterarCargaProdutoConferencia();
            }
            else
            {
                InserirCargaProdutoConferencia();
            }

        }

        public void ExcluirCargaProdutoConferencia()
        {



            if (MessageBox.Show("Deseja realmente excluir o lançamento selecionado?", "ATENÇÃO! Exclusão de Produto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ConferenciaProdutoLimpar();
                cImportarProdutoId = Convert.ToInt32(localDeposito.grdConfProduto.CurrentRow.Cells[8].Value);


                ModelLibrary.MetodosRepresentante.ExcluirProdutoConferencia(localDeposito.cCargaId, cImportarProdutoId);

                ExibirConferenciaProduto(localDeposito.cCargaId);
            }


        }


    }
}
