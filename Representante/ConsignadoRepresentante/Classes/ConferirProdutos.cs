﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoRepresentante;
using Equin.ApplicationFramework;

namespace ConsignadoRepresentante
{
    public partial class ConferirProdutos
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public long cImportarProdutoId;
        public string cModoConferenciaProduto;


        public FormDeposito localDeposito = null;

        public ConferirProdutos(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }



        public void Limpar()
        {

            ConferenciaProdutoLimpar();
            localDeposito.grdConfProduto.DataSource = null;
            localDeposito.grdConfProduto.Refresh();

        }

        ////////////////////////////////////////
        /// Conferencia de Produtos
        ////////////////////////////////////////



        public void ConferenciaProdutoLimpar()
        {
            localDeposito.txtConfCodigoBarras.Text = "";
            localDeposito.txtConfProduto.Text = "";
            localDeposito.txtConfQuantidade.Text = "";
            localDeposito.chkConfQuantidade.Checked = false;
            localDeposito.btnConferenciaConfirmar.Enabled = false;
            localDeposito.btnConfCancelar.Enabled = false;
            localDeposito.txtConfCodigoBarras.ReadOnly = false;
            cImportarProdutoId = 0;
            cModoConferenciaProduto = "Insert";
            localDeposito.txtConfCodigoBarras.Focus();
        }


        public void PesquisarConferenciaProduto(string pCodigo)
        {

            long vProdutoGradeId = 0;
            List<ModelLibrary.RepProdutoGrade> produtosgrade = ModelLibrary.MetodosRepresentante.ObterProdutosGrade(pCodigo);

            if (produtosgrade != null)
            {
                if (produtosgrade.Count > 1)
                {
                    Modal.FormProdutosGrade formProdutosGrade = new Modal.FormProdutosGrade(pCodigo, "Conferencia");

                    var result = formProdutosGrade.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        vProdutoGradeId = formProdutosGrade.cProdutoGradeId;
                        ExibirProdutoGrade(vProdutoGradeId);
                    } else
                    {
                        vProdutoGradeId = 0;
                    }
                } else
                {
                    vProdutoGradeId = (produtosgrade.FirstOrDefault() != null) ? produtosgrade.FirstOrDefault().Id : 0;
                    ExibirProdutoGrade(vProdutoGradeId);
                }
            } else
            {
                vProdutoGradeId = 0;
                ExibirProdutoGrade(vProdutoGradeId);
            }
            
        }


        public void ExibirProdutoGrade(long pProdutoGradeId)
        {
            var produtograde = ModelLibrary.MetodosRepresentante.ObterProdutoGrade("", pProdutoGradeId);

            if (produtograde != null)
            {
                string vTipo;
                var cargaproduto = ModelLibrary.MetodosRepresentante.ObterCargaProduto(localDeposito.cCargaId, produtograde.Id);
                if (cargaproduto != null)
                {
                    vTipo = cargaproduto.Tipo;                    
                } else
                {
                    vTipo = null;
                }

                if (vTipo == "S")
                {

                    MessageBox.Show("O produto informado está registrado como suplemento.");

                    cImportarProdutoId = 0;
                    localDeposito.txtConfCodigoBarras.Text = "";
                    localDeposito.txtConfCodigoBarras.Focus();
                    localDeposito.btnConferenciaConfirmar.Enabled = false;
                    localDeposito.btnConfCancelar.Enabled = false;

                } else
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
                



            }
            else
            {

                MessageBox.Show("Dígito verificador ou Código inválido. Não foi possível encontrar a grade deste produto.");

                cImportarProdutoId = 0;
                localDeposito.txtConfCodigoBarras.Text = "";
                localDeposito.txtConfCodigoBarras.Focus();
                localDeposito.btnConferenciaConfirmar.Enabled = false;
                localDeposito.btnConfCancelar.Enabled = false;


            }
        }

        public void ExibirConferenciaProduto(long pCargaId)
        {

            Console.WriteLine("Exibindo conferencia de produto CargaID = " + pCargaId.ToString());

            localDeposito.pnlConferirProduto.Visible = true;

            //Desativar aba conferencia de produtos 
            localDeposito.pnlConferirProdutoTop.Enabled = true;



            List<ModelLibrary.ListaRepProdutosConferencia> prodconferencia = ModelLibrary.MetodosRepresentante.ObterProdutosConferencia(pCargaId);

            BindingListView<ModelLibrary.ListaRepProdutosConferencia> view = new BindingListView<ModelLibrary.ListaRepProdutosConferencia>(prodconferencia);

            localDeposito.grdConfProduto.DataSource = view;

            /// Ocultar coluna CargaProdutoId
            localDeposito.grdConfProduto.Columns[6].Visible = false;
            localDeposito.grdConfProduto.Columns[7].Visible = false;
            localDeposito.grdConfProduto.Columns[8].Visible = false;
            localDeposito.grdConfProduto.Columns[9].Visible = false;
            localDeposito.grdConfProduto.Columns[10].Visible = false;

            localDeposito.grdConfProduto.Columns[5].DefaultCellStyle.Format = "c";

            /// Alterar Título da Coluna
            localDeposito.grdConfProduto.Columns[2].HeaderText = "Quantidade Carga";
            localDeposito.grdConfProduto.Columns[3].HeaderText = "Quantidade Informada";
            localDeposito.grdConfProduto.Columns[4].HeaderText = "Diferença";
            localDeposito.grdConfProduto.Columns[5].HeaderText = "Valor Diferença";


            localDeposito.grdConfProduto.Columns[1].Width = 250;


            bool vFinalizado = true;
            for (int i = 0; i < localDeposito.grdConfProduto.Rows.Count; ++i)
            {
                if (vFinalizado == true)
                {
                    if (localDeposito.grdConfProduto.Rows[i].Cells[3].Value == null)
                    {
                        vFinalizado = false;
                    } 
                }
                
            }


            if (!vFinalizado)
            {
                localDeposito.grdConfProduto.Columns[2].Visible = false;
                localDeposito.grdConfProduto.Columns[4].Visible = false;
                localDeposito.grdConfProduto.Columns[5].Visible = false;
                localDeposito.grdConfProduto.Columns[11].Visible = false;
            } 
            


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

                if (vQuantidade > -1)
                {

                    ModelLibrary.MetodosRepresentante.InserirProdutoConferencia(localDeposito.cCargaId, cImportarProdutoId, vQuantidade);

                    if (localDeposito.chkConfQuantidade.Checked)
                    {


                        ModelLibrary.RepCargaProduto repCargaProduto = ModelLibrary.MetodosRepresentante.ObterCargaProduto(localDeposito.cCargaId, cImportarProdutoId);

                        if (repCargaProduto != null)
                        {
                            if (vQuantidade != repCargaProduto.Quantidade) //se a quantidade da carga for diferente da informada soar bipe
                            {

                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\RederConsignado\honk.wav");
                                player.Play();

                            }
                        }



                    }




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
            cImportarProdutoId = Convert.ToInt32(localDeposito.grdConfProduto.CurrentRow.Cells["ProdutoGradeId"].Value);

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



            ModelLibrary.RepCargaProduto repCargaProduto = ModelLibrary.MetodosRepresentante.ObterCargaProduto(localDeposito.cCargaId, cImportarProdutoId);

            if (repCargaProduto != null)
            {
                if (Convert.ToDecimal(localDeposito.txtConfQuantidade.Text) != repCargaProduto.Quantidade) //se a quantidade da carga for diferente da informada soar bipe
                {

                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\RederConsignado\honk.wav");
                    player.Play();

                }
            }

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
                cImportarProdutoId = Convert.ToInt32(localDeposito.grdConfProduto.CurrentRow.Cells["ProdutoGradeId"].Value);


                ModelLibrary.MetodosRepresentante.ExcluirProdutoConferencia(localDeposito.cCargaId, cImportarProdutoId);

                ExibirConferenciaProduto(localDeposito.cCargaId);
            }


        }


        public void ResolverConflito(int pProdutoGradeID, int pQuantidade)
        {
            ModelLibrary.MetodosRepresentante.ResolverConflitoProdutoConferencia(localDeposito.cCargaId, pProdutoGradeID, pQuantidade);
            ExibirConferenciaProduto(localDeposito.cCargaId);

            localDeposito.RecarregarDados();
        }

    }
}
