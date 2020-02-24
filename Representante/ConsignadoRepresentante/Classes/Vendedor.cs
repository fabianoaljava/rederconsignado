using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using System.Drawing;

namespace ConsignadoRepresentante
{
    public partial class Vendedor
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        

        public FormRepresentante localRepresentanteForm = null;

        public long cVendedorId;

        public string cTipoPessoa;

        public string cVendedorModo;

        public string cVendedorPedidoModo;

        public string cVendedorProdRetModo;

        public long cPedidoId;

        decimal cValorRecebido, cValorTotalAPagar;

        long cDuplicataReceberId, cDuplicataId;





        public Vendedor(FormRepresentante formRepresentante)
        {

            localRepresentanteForm = formRepresentante;
        
        }


        public void CarregarFormulario()
        {


            CarregarListaPesquisa();
            CarregarListaEstado();



        }


        ////////////////////////////////////////
        /// Pesquisar Vendedor
        ////////////////////////////////////////



        public void CarregarListaPesquisa()
        {
            localRepresentanteForm.cbbPesqVendedor.DataSource = ModelLibrary.MetodosRepresentante.ObterListaVendedor();
            localRepresentanteForm.cbbPesqVendedor.DisplayMember = "Nome";
            localRepresentanteForm.cbbPesqVendedor.ValueMember = "Id";
            localRepresentanteForm.cbbPesqVendedor.Invalidate();
            localRepresentanteForm.cbbPesqVendedor.SelectedIndex = -1;

            localRepresentanteForm.cbbPesqVendedor.SelectedIndexChanged += PesquisaVendedor_Change;

        }


        public void PesquisaVendedor_Change(object sender, EventArgs e)
        {
            if (localRepresentanteForm.cbbPesqVendedor.SelectedIndex > -1)
            {
                ModelLibrary.RepVendedor vendedor = (ModelLibrary.RepVendedor)localRepresentanteForm.cbbPesqVendedor.SelectedItem;
                VendedorExibir(vendedor.Id);

            }

        }

        public void VendedorPesquisar()
        {
            var vendedor = ModelLibrary.MetodosRepresentante.PesquisarVendedor(localRepresentanteForm.txtVendedorPesqCodigo.Text, localRepresentanteForm.txtVendedorPesqCpfCnpj.Text);

            if (vendedor != null)
            {
                VendedorExibir(vendedor.Id);
            }

            else
            {

                MessageBox.Show("Vendedor não encontrado.");
            }
        }


        ///////////////////////////////////////////////
        /// Carregar Campos de Cadastro de Vendedor
        ///////////////////////////////////////////////

        public void CarregarListaEstado()
        {


            localRepresentanteForm.cbbUF.DataSource = ModelLibrary.MetodosRepresentante.ObterListaEstado();
            localRepresentanteForm.cbbUF.DisplayMember = "Sigla";
            localRepresentanteForm.cbbUF.ValueMember = "Id";
            localRepresentanteForm.cbbUF.Invalidate();
            localRepresentanteForm.cbbUF.SelectedIndex = -1;

            localRepresentanteForm.cbbUF.SelectedIndexChanged += ListaEstado_Change;

        }




        public void ListaEstado_Change(object sender, EventArgs e)
        {
            if (localRepresentanteForm.cbbUF.SelectedIndex > 0)
            {
                ModelLibrary.RepEstado estado = (ModelLibrary.RepEstado)localRepresentanteForm.cbbUF.SelectedItem;
                CarregarListaCidade(estado.Id);
            }
        }


        void CarregarListaCidade(long pEstadoId = 0)
        {
            localRepresentanteForm.cbbCidade.DataSource = ModelLibrary.MetodosRepresentante.ObterListaCidade(pEstadoId);
            localRepresentanteForm.cbbCidade.DisplayMember = "Descricao";
            localRepresentanteForm.cbbCidade.ValueMember = "Id";
            localRepresentanteForm.cbbCidade.Invalidate();
            localRepresentanteForm.cbbCidade.SelectedIndex = -1;
        }


        public void SelecionarTipoPessoa(string pTipoPessoa)
        {

            if (pTipoPessoa == "Pessoa Física")
            {
                localRepresentanteForm.lblCPFCNPJ.Text = "CPF";
                localRepresentanteForm.txtCPFCnpj.WaterMark = "CPF";

                localRepresentanteForm.txtRazaoSocial.Enabled = false;
                localRepresentanteForm.lblRGInscricao.Text = "RG";
                localRepresentanteForm.txtRGInscricao.WaterMark = "RG";
                cTipoPessoa = "PF";

                VendedorHabilitar();

                localRepresentanteForm.txtCPFCnpj.Focus();
            }
            else if (pTipoPessoa == "Pessoa Jurídica")
            {
                localRepresentanteForm.lblCPFCNPJ.Text = "CNPJ";
                localRepresentanteForm.txtCPFCnpj.WaterMark = "CNPJ";
                localRepresentanteForm.txtRazaoSocial.Enabled = true;
                localRepresentanteForm.lblRGInscricao.Text = "Insc. Estadual";
                localRepresentanteForm.txtRGInscricao.WaterMark = "Insc. Estadual";
                cTipoPessoa = "PJ";

                VendedorHabilitar();

                localRepresentanteForm.txtCPFCnpj.Focus();
            } else
            {
                VendedorDesabilitar();
            }
        }




        



        ///////////////////////////////////////////////
        /// Validações do Formulario
        ///////////////////////////////////////////////


        public bool ValidarCPFCnpj(string pCPFCnpj, object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            if (pCPFCnpj != "")
            {
                //verificar se o CPF/CNPJ é valido
                if (!ControllerLibrary.Funcoes.CpfCnpjUtils.IsValid(pCPFCnpj))
                {
                    MessageBox.Show("CPF/CNPJ Inválido!");
                    e.Cancel = true;

                    return false;
                } else
                {
                    return true;
                }
                
            } else
            {
                return false;
            }


        }


        public void VerificarCPFCnpjExistente(string pCPFCnpj)
        {


            pCPFCnpj = localRepresentanteForm.MascaraCnpjCpf(pCPFCnpj);

            if (pCPFCnpj != "")
            {



                var vendedor = ModelLibrary.MetodosRepresentante.PesquisarVendedor("0", pCPFCnpj);

                if (vendedor != null)
                {

                    if (MessageBox.Show("CPF/CNPJ já cadastrado. Deseja carregar os dados para atualização?", "Reder Consignado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        VendedorExibir(vendedor.Id);
                    }
                    else
                    {

                        VendedorLimpar();
                    }

                }
                else
                {
                    var vendedorbase = ModelLibrary.MetodosRepresentante.PesquisarVendedorBase(pCPFCnpj);

                    if (vendedorbase != null)
                    {

                        if (MessageBox.Show("O CPF/CNPJ informado está cadastrado em uma outra carga. Deseja carregar os dados e continuar com a inclusão?", "Reder Consignado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            if (vendedorbase.PedidoAberto != "")
                            {
                                MessageBox.Show(vendedorbase.PedidoAberto, "Reder Consignado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                VendedorLimpar();

                            } else
                            {

                                if (vendedorbase.DebitoAReceber > 0)
                                {
                                    if (MessageBox.Show("O vendedor possui débitos anteriores no valor de " + vendedorbase.DebitoAReceber.ToString() + ". Deseja registrar o recebimento deste pagamento?", "Reder Consignado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        Decimal valorrecebido = ControllerLibrary.Funcoes.ShowDialogNumeric("Informe o valor recebido", "Valor");

                                        if (valorrecebido != vendedorbase.DebitoAReceber)
                                        {
                                            //if (MessageBox.Show("O valor informado foi " + valorrecebido.ToString() + " e está diferente do débito anterior = " + vendedorbase.DebitoAReceber  + ". O cadastro do vendedor só será permitido após o recebimento total do débito. Deseja registrar o pagamento mesmo assim?", "Reder Consignado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            //{
                                            //    ModelLibrary.MetodosRepresentante.ReceberExtra(vendedorbase.Id, localRepresentanteForm.cCargaId, valorrecebido, vendedorbase.DebitoAReceber);

                                            //}

                                            //VendedorLimpar();

                                            MessageBox.Show("O valor informado foi " + valorrecebido.ToString() + " e está diferente do débito anterior = " + vendedorbase.DebitoAReceber + ". O cadastro do vendedor só será permitido após o recebimento total do débito.", "Reder Consignado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            localRepresentanteForm.txtCPFCnpj.Focus();
                                            
                                        } else
                                        {

                                            ModelLibrary.MetodosRepresentante.ReceberExtra(vendedorbase.Id, localRepresentanteForm.cCargaId, valorrecebido, vendedorbase.DebitoAReceber);
                                            VendedorBaseExibir(vendedorbase.Id);

                                        }
                                        
                                    } else
                                    {
                                        VendedorLimpar();
                                    }

                                } else
                                {
                                    VendedorBaseExibir(vendedorbase.Id);
                                }
                            }
                            
                        }
                        else
                        {
                            VendedorLimpar();
                        }

                    }
                }




            }




        }


        public bool ValidarEmail(string pEmail, object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (pEmail != "")
            {
                //verificar se o CPF/CNPJ é valido
                if (!ControllerLibrary.Funcoes.IsEmail(pEmail))
                {
                    MessageBox.Show("Email Inválido!");
                    e.Cancel = true;

                    return false;
                } else
                {
                    return true;
                }
            } else
            {
                return false;
            }


        }



        //////////////////////////////////////////////////////
        /// ABA INICIO
        //////////////////////////////////////////////////////


        public void ExibirInicio()
        {

            ///

            var pedido = ModelLibrary.MetodosRepresentante.ObterVendedorPedido(cVendedorId, localRepresentanteForm.cCargaId);
            var vendedor = ModelLibrary.MetodosRepresentante.ObterVendedor(cVendedorId);
            var totaltitulos = ModelLibrary.MetodosRepresentante.ObterTotalTitulos(cVendedorId);

            if (vendedor != null)
            {

                localRepresentanteForm.dlbLimitePedido.Text = (vendedor.LimitePedido == 0 || vendedor.LimitePedido == 99999) ? "<ILIMITADO>" : string.Format("{0:N}", vendedor.LimitePedido);
                localRepresentanteForm.dlbLimiteCredito.Text = (vendedor.LimiteCredito == 0 || vendedor.LimiteCredito == 99999) ? "<ILIMITADO>" : string.Format("{0:N}", vendedor.LimiteCredito);

            }

            if (pedido != null)
            {
                var creditoutilizado = (pedido.ValorAReceber != null) ? pedido.ValorAReceber : 0;

                if (totaltitulos != null)
                {

                    creditoutilizado += (totaltitulos.ValorAReceber != null) ? totaltitulos.ValorAReceber : 0;

                }

                localRepresentanteForm.dlbLimitePedidoUtilizado.Text = string.Format("{0:N}", pedido.ValorPedido);
                localRepresentanteForm.dlbLimiteCreditoUtilizado.Text = string.Format("{0:N}", creditoutilizado);

                if (creditoutilizado > 0)
                {
                    localRepresentanteForm.dlbLimiteCreditoUtilizado.ForeColor = Color.Red;
                } else
                {
                    localRepresentanteForm.dlbLimiteCreditoUtilizado.ForeColor = Color.Black;
                }

                localRepresentanteForm.smnVendedorRelatorioPedido.Enabled = true;
                localRepresentanteForm.btnPedidoImprimir.Enabled = true;
                localRepresentanteForm.smnVendedorRelatorioRetorno.Enabled = true;
                localRepresentanteForm.btnRetornoImprimir.Enabled = true;

            } else
            {
                if (totaltitulos != null)
                {                    
                    localRepresentanteForm.dlbLimiteCreditoUtilizado.Text = ((totaltitulos.ValorAReceber != null) ? totaltitulos.ValorAReceber : 0).ToString();
                } else
                {
                    localRepresentanteForm.dlbLimiteCreditoUtilizado.Text = "0";
                }

                localRepresentanteForm.dlbLimitePedidoUtilizado.Text = "0";
                localRepresentanteForm.smnVendedorRelatorioPedido.Enabled = false;
                localRepresentanteForm.btnPedidoImprimir.Enabled = false;
                localRepresentanteForm.smnVendedorRelatorioRetorno.Enabled = false;
                localRepresentanteForm.btnRetornoImprimir.Enabled = false;

            }


        }

        public void InicioLimpar()
        {

            localRepresentanteForm.dlbVendedorNome.Text = "";
            localRepresentanteForm.dlbVendedorCPF.Text = "";
            localRepresentanteForm.dlbVendedorEndereco.Text = "";
            localRepresentanteForm.dlbVendedorTelefone.Text = "";

            localRepresentanteForm.dlbLimitePedido.Text = "N/A";
            localRepresentanteForm.dlbLimiteCredito.Text = "N/A";

            localRepresentanteForm.dlbLimitePedidoUtilizado.Text = "N/A";
            localRepresentanteForm.dlbLimiteCreditoUtilizado.Text = "N/A";
        }


        ///////////////////////////////////////////////
        /// CRUD Vendedor
        ///////////////////////////////////////////////


        public void VendedorLimpar()
        {
            localRepresentanteForm.cbbPesqVendedor.SelectedIndex = -1;
            localRepresentanteForm.txtVendedorPesqCodigo.Text = "";
            localRepresentanteForm.txtVendedorPesqCpfCnpj.Text = "";

            localRepresentanteForm.tbcVendedor.Visible = false;

            localRepresentanteForm.cbbTipoPessoa.SelectedIndex = -1;
            localRepresentanteForm.txtCPFCnpj.Text = "";
            localRepresentanteForm.txtDataInicial.Text = "";
            localRepresentanteForm.txtDataFinal.Text = "";
            localRepresentanteForm.txtStatus.Text = "";
            localRepresentanteForm.txtNome.Text = "";
            localRepresentanteForm.txtRazaoSocial.Text = "";
            localRepresentanteForm.txtRGInscricao.Text = "";
            localRepresentanteForm.cbbDataNasc.ResetText();
            localRepresentanteForm.txtCep.Text = "";
            localRepresentanteForm.txtEndereco.Text = "";
            localRepresentanteForm.txtNumero.Text = "";
            localRepresentanteForm.txtComplemento.Text = "";
            localRepresentanteForm.txtBairro.Text = "";
            localRepresentanteForm.cbbUF.SelectedIndex = -1;
            localRepresentanteForm.cbbCidade.SelectedIndex = -1;
            localRepresentanteForm.txtTelefone.Text = "";
            localRepresentanteForm.txtTelefoneComercial.Text = "";
            localRepresentanteForm.txtCelular.Text = "";
            localRepresentanteForm.txtEmail.Text = "";
            localRepresentanteForm.txtLimitePedido.Text = "0,00";
            localRepresentanteForm.txtLimiteCredito.Text = "99999,00";
            localRepresentanteForm.txtObservacao.Text = "";


            localRepresentanteForm.txtNome.ReadOnly = false;
            localRepresentanteForm.txtCPFCnpj.ReadOnly = false;
            localRepresentanteForm.txtRGInscricao.ReadOnly = false;
            localRepresentanteForm.cbbTipoPessoa.Enabled = true;


            localRepresentanteForm.dlbPedidoTotal.Text = "N/A";
            localRepresentanteForm.dlbRetornoTotalPedido.Text = "N/A";

            
            localRepresentanteForm.dlbRetornoValorCompra.Text = "N/A";


            cVendedorId = 0;


            InicioLimpar();
            PedidoLimpar();
            RetornoProdutoLimpar();
            AcertoLimpar();


            localRepresentanteForm.grdVendedorPedido.DataSource = null;
            localRepresentanteForm.grdVendedorRetorno.DataSource = null;
            localRepresentanteForm.grdFinanceiroTitulos.DataSource = null;



            localRepresentanteForm.smnVendedorPedidoIncluir.Visible = false;            
            localRepresentanteForm.smnVendedorRelatorioPedido.Enabled = false;
            localRepresentanteForm.btnPedidoImprimir.Enabled = false;
            localRepresentanteForm.smnVendedorRelatorioRetorno.Enabled = false;
            localRepresentanteForm.btnRetornoImprimir.Enabled = false;

        }

        public void VendedorHabilitar()
        {
            localRepresentanteForm.txtCPFCnpj.Enabled = true;
            localRepresentanteForm.txtDataInicial.Enabled = true;
            localRepresentanteForm.txtDataFinal.Enabled = true;
            localRepresentanteForm.txtStatus.Enabled = true;
            localRepresentanteForm.txtNome.Enabled = true;           
            localRepresentanteForm.txtRGInscricao.Enabled = true;
            localRepresentanteForm.cbbDataNasc.Enabled = true;
            localRepresentanteForm.txtCep.Enabled = true;
            localRepresentanteForm.txtEndereco.Enabled = true;
            localRepresentanteForm.txtNumero.Enabled = true;
            localRepresentanteForm.txtComplemento.Enabled = true;
            localRepresentanteForm.txtBairro.Enabled = true;
            localRepresentanteForm.cbbUF.Enabled = true;
            localRepresentanteForm.cbbCidade.Enabled = true;
            localRepresentanteForm.txtTelefone.Enabled = true;
            localRepresentanteForm.txtTelefoneComercial.Enabled = true;
            localRepresentanteForm.txtCelular.Enabled = true;
            localRepresentanteForm.txtEmail.Enabled = true;
           localRepresentanteForm.txtObservacao.Enabled = true;

            localRepresentanteForm.btnVendedorSalvar.Enabled = true;


        }


        public void VendedorDesabilitar()
        {
            localRepresentanteForm.txtCPFCnpj.Enabled = false;
            localRepresentanteForm.txtDataInicial.Enabled = false;
            localRepresentanteForm.txtDataFinal.Enabled = false;
            localRepresentanteForm.txtStatus.Enabled = false;
            localRepresentanteForm.txtNome.Enabled = false;
            localRepresentanteForm.txtRazaoSocial.Enabled = false;
            localRepresentanteForm.txtRGInscricao.Enabled = false;
            localRepresentanteForm.cbbDataNasc.Enabled = false;
            localRepresentanteForm.txtCep.Enabled = false;
            localRepresentanteForm.txtEndereco.Enabled = false;
            localRepresentanteForm.txtNumero.Enabled = false;
            localRepresentanteForm.txtComplemento.Enabled = false;
            localRepresentanteForm.txtBairro.Enabled = false;
            localRepresentanteForm.cbbUF.Enabled = false;
            localRepresentanteForm.cbbCidade.Enabled = false;
            localRepresentanteForm.txtTelefone.Enabled = false;
            localRepresentanteForm.txtTelefoneComercial.Enabled = false;
            localRepresentanteForm.txtCelular.Enabled = false;
            localRepresentanteForm.txtEmail.Enabled = false;
            localRepresentanteForm.txtObservacao.Enabled = false;

            localRepresentanteForm.btnVendedorSalvar.Enabled = false;


        }



        public void VendedorExibir(long pVendedorId)
        {

            PedidoLimpar();
            RetornoProdutoLimpar();
            AcertoLimpar();

            var vendedor = ModelLibrary.MetodosRepresentante.ObterVendedor(pVendedorId);


            if (vendedor != null)
            {

                cVendedorId = vendedor.Id;
                localRepresentanteForm.tbcVendedor.Visible = true;
                localRepresentanteForm.cbbTipoPessoa.Text = vendedor.TipoPessoa == "PF" ? "Pessoa Física" : "Pessoa Jurídica";
                localRepresentanteForm.txtCPFCnpj.Text = vendedor.CpfCnpj.Trim();

                localRepresentanteForm.txtVendedorPesqCodigo.Text = cVendedorId.ToString();
                localRepresentanteForm.txtVendedorPesqCpfCnpj.Text = vendedor.CpfCnpj.Trim();

                localRepresentanteForm.txtDataInicial.Text = vendedor.DataInicial.ToString();
                localRepresentanteForm.txtDataFinal.Text = vendedor.DataFinal.ToString();


                switch (vendedor.Status)
                {
                    case "2":
                        localRepresentanteForm.txtStatus.Text = "Negativado";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Red;
                        break;
                    case "0":
                        localRepresentanteForm.txtStatus.Text = "Inativo";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Orange;
                        break;
                    default:
                        localRepresentanteForm.txtStatus.Text = "Ativo";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Green;
                        break;
                }
                //cbbStatus --- Ver quais os status apresentados
                localRepresentanteForm.txtNome.Text = vendedor.Nome.Trim();
                localRepresentanteForm.txtRazaoSocial.Text = vendedor.RazaoSocial.Trim();
                localRepresentanteForm.txtRGInscricao.Text = vendedor.RGInscricao.Trim();
                localRepresentanteForm.cbbDataNasc.Text = vendedor.DataNascimento.ToString();
                localRepresentanteForm.txtCep.Text = vendedor.Cep.Trim();
                localRepresentanteForm.txtEndereco.Text = vendedor.Endereco.Trim();
                localRepresentanteForm.txtNumero.Text = vendedor.Numero.ToString();
                localRepresentanteForm.txtComplemento.Text = vendedor.Complemento.Trim();
                localRepresentanteForm.txtBairro.Text = vendedor.Bairro.Trim();
                localRepresentanteForm.cbbUF.Text = vendedor.UF.Trim();
                localRepresentanteForm.cbbCidade.Text = vendedor.Cidade.Trim();
                localRepresentanteForm.txtTelefone.Text = vendedor.Telefone.Trim();
                localRepresentanteForm.txtTelefoneComercial.Text = vendedor.TelefoneComercial.Trim();
                localRepresentanteForm.txtCelular.Text = vendedor.Celular.Trim();
                localRepresentanteForm.txtEmail.Text = vendedor.Email.Trim();
                localRepresentanteForm.txtLimitePedido.Text = String.Format("{0:N}", vendedor.LimitePedido);
                localRepresentanteForm.txtLimiteCredito.Text = String.Format("{0:N}", vendedor.LimiteCredito);
                localRepresentanteForm.txtObservacao.Text = vendedor.Observacao.Trim();

                cVendedorModo = "Edit";

                localRepresentanteForm.txtNome.ReadOnly = true;
                localRepresentanteForm.txtCPFCnpj.ReadOnly = true;
                localRepresentanteForm.txtRGInscricao.ReadOnly = true;
                localRepresentanteForm.cbbTipoPessoa.Enabled = false;
                localRepresentanteForm.txtLimiteCredito.ReadOnly = true;
                localRepresentanteForm.txtLimitePedido.ReadOnly = true;


                localRepresentanteForm.cbbPesqVendedor.Text = vendedor.Nome;
                localRepresentanteForm.txtVendedorPesqCodigo.Text = vendedor.Id.ToString();
                localRepresentanteForm.txtVendedorPesqCpfCnpj.Text = vendedor.CpfCnpj.ToString();


                ///// Labels da aba Inicial
                ///

                localRepresentanteForm.dlbVendedorNome.Text = vendedor.Nome;
                localRepresentanteForm.dlbVendedorCPF.Text = vendedor.CpfCnpj;
                localRepresentanteForm.dlbVendedorEndereco.Text = vendedor.Endereco + " - " + vendedor.Complemento + " - " + vendedor.Bairro;
                localRepresentanteForm.dlbVendedorTelefone.Text = vendedor.Telefone + " / " + vendedor.Celular;


                ExibirPedido(cVendedorId);
                ExibirRetornoProduto(cVendedorId);
                //ExibirAcerto(); //--> Inserido em ExibirPedido
                ExibirTitulos();
                ExibirInicio();





                localRepresentanteForm.tbcVendedor.SelectedTab = localRepresentanteForm.tabVendedorInicio;


            }
            else
            {
                MessageBox.Show("Vendedor não encontrado.");
            }



        }

        public void VendedorIncluir()
        {
            VendedorLimpar();
            localRepresentanteForm.tbcVendedor.Visible = true;
            localRepresentanteForm.tbcVendedor.SelectedTab = localRepresentanteForm.tabVendedorCadastro;
            localRepresentanteForm.pnlVendedorPedidoMontar.Enabled = false;
            localRepresentanteForm.pnlVendedorRetorno.Enabled = false;
            localRepresentanteForm.grpFinanceiroCalculo.Visible = false;
            localRepresentanteForm.lblAcertoInfo.Visible = true;

            cVendedorModo = "Create";

        }




        public bool VendedorValidar()
        {
            bool result = true;

            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.cbbTipoPessoa.Text, "Informe o Tipo Pessoa", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtCPFCnpj.Text, "Informe o CPF/CNPJ", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtNome.Text, "Informe o Nome", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtRGInscricao.Text, "Informe o RG/Inscrição Estadual", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtEndereco.Text, "Informe o Endereço", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtBairro.Text, "Informe o Bairro", result);
            result = ControllerLibrary.Funcoes.ValidarNotEmpty(localRepresentanteForm.txtCep.Text, "Informe o Cep", result);

            localRepresentanteForm.txtCPFCnpj.Focus();

            return result;

        }

        public void VendedorSalvar()
        {

            localRepresentanteForm.btnVendedorSalvar.Text = "Salvando...";
            localRepresentanteForm.btnVendedorSalvar.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;


            if (VendedorValidar())
            {
                ModelLibrary.RepVendedor vendedor = new ModelLibrary.RepVendedor();


                vendedor.TipoPessoa = localRepresentanteForm.cbbTipoPessoa.Text == "Pessoa Física" ? "PF" : "PJ";
                vendedor.CpfCnpj = localRepresentanteForm.txtCPFCnpj.Text;
                vendedor.DataInicial = DateTime.Now.Date;
                vendedor.Nome = localRepresentanteForm.txtNome.Text;
                vendedor.RazaoSocial = localRepresentanteForm.txtRazaoSocial.Text;
                vendedor.RGInscricao = localRepresentanteForm.txtRGInscricao.Text;
                vendedor.DataNascimento = localRepresentanteForm.cbbDataNasc.Value;
                vendedor.Cep = localRepresentanteForm.txtCep.Text;
                vendedor.Endereco = localRepresentanteForm.txtEndereco.Text;
                vendedor.Numero = Convert.ToInt32(localRepresentanteForm.txtNumero.Text);
                vendedor.Complemento = localRepresentanteForm.txtComplemento.Text;
                vendedor.Bairro = localRepresentanteForm.txtBairro.Text;
                vendedor.UF = localRepresentanteForm.cbbUF.Text;
                vendedor.Cidade = localRepresentanteForm.cbbCidade.Text;
                vendedor.Telefone = localRepresentanteForm.txtTelefone.Text;
                vendedor.TelefoneComercial = localRepresentanteForm.txtTelefoneComercial.Text;
                vendedor.Celular = localRepresentanteForm.txtCelular.Text;
                vendedor.Email = localRepresentanteForm.txtEmail.Text;
                vendedor.LimitePedido = localRepresentanteForm.txtLimitePedido.Text != "" ? Convert.ToDecimal(localRepresentanteForm.txtLimitePedido.Text) : 0;
                vendedor.LimiteCredito = localRepresentanteForm.txtLimiteCredito.Text != "" ? Convert.ToDecimal(localRepresentanteForm.txtLimiteCredito.Text) : 0;
                vendedor.Observacao = localRepresentanteForm.txtObservacao.Text;


                ModelLibrary.MetodosRepresentante.SalvarVendedor(cVendedorModo, vendedor, cVendedorId);


                if (cVendedorModo == "Create")
                {
                    MessageBox.Show("Vendedor Incluído com Sucesso");
                    cVendedorModo = "Edit";
                    string NovoCPF = localRepresentanteForm.txtCPFCnpj.Text;
                    CarregarFormulario();
                    VendedorLimpar();

                    localRepresentanteForm.txtVendedorPesqCpfCnpj.Text = NovoCPF;
                    VendedorPesquisar();

                }
                else
                {
                    MessageBox.Show("Vendedor Alterado com Sucesso");
                }

                VendedorReload();
            }

            

            Cursor.Current = Cursors.Default;

            localRepresentanteForm.btnVendedorSalvar.Text = "Salvar";
            localRepresentanteForm.btnVendedorSalvar.Enabled = true;

        }

        public void VendedorExcluir()
        {

        }


        public void VendedorReload()
        {
            ExibirPedido(cVendedorId);
            ExibirRetornoProduto(cVendedorId);
            ExibirAcerto();
            ExibirTitulos();
            ExibirInicio();

            localRepresentanteForm.cHome.CarregarFormulario();
            localRepresentanteForm.cFinanceiro.ExibirPosicaoFinancera();
            localRepresentanteForm.tabPosicaoFinanceira.Refresh();
            localRepresentanteForm.cProduto.ExibirProdutos();
        }


        //////////////////////////////////////////////////////
        ///VENDEDOR BASE
        //////////////////////////////////////////////////////


        public void VendedorBaseExibir(long pVendedorId)
        {
            var vendedor = ModelLibrary.MetodosRepresentante.ObterVendedorBase(pVendedorId);


            if (vendedor != null)
            {

                cVendedorId = vendedor.Id;
                localRepresentanteForm.tbcVendedor.Visible = true;
                localRepresentanteForm.cbbTipoPessoa.Text = vendedor.TipoPessoa == "PF" ? "Pessoa Física" : "Pessoa Jurídica";
                localRepresentanteForm.txtCPFCnpj.Text = vendedor.CpfCnpj.Trim();
                localRepresentanteForm.txtDataInicial.Text = vendedor.DataInicial.ToString();
                localRepresentanteForm.txtDataFinal.Text = vendedor.DataFinal.ToString();

                switch (vendedor.Status)
                {
                    case "2":
                        localRepresentanteForm.txtStatus.Text = "Negativado";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Red;
                        break;
                    case "0":
                        localRepresentanteForm.txtStatus.Text = "Inativo";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Orange;
                        break;
                    default:
                        localRepresentanteForm.txtStatus.Text = "Ativo";
                        localRepresentanteForm.txtStatus.ForeColor = Color.Green;
                        break;
                }
                

                localRepresentanteForm.txtNome.Text = vendedor.Nome.Trim();
                localRepresentanteForm.txtRazaoSocial.Text = vendedor.RazaoSocial.Trim();
                localRepresentanteForm.txtRGInscricao.Text = vendedor.RGInscricao.Trim();
                localRepresentanteForm.cbbDataNasc.Text = vendedor.DataNascimento.ToString();
                localRepresentanteForm.txtCep.Text = vendedor.Cep.Trim();
                localRepresentanteForm.txtEndereco.Text = vendedor.Endereco.Trim();
                localRepresentanteForm.txtNumero.Text = vendedor.Numero.ToString();
                localRepresentanteForm.txtComplemento.Text = vendedor.Complemento.Trim();
                localRepresentanteForm.txtBairro.Text = vendedor.Bairro.Trim();
                localRepresentanteForm.cbbUF.Text = vendedor.UF.Trim();
                localRepresentanteForm.cbbCidade.Text = vendedor.Cidade.Trim();
                localRepresentanteForm.txtTelefone.Text = vendedor.Telefone.Trim();
                localRepresentanteForm.txtTelefoneComercial.Text = vendedor.TelefoneComercial.Trim();
                localRepresentanteForm.txtCelular.Text = vendedor.Celular.Trim();
                localRepresentanteForm.txtEmail.Text = vendedor.Email.Trim();
                localRepresentanteForm.txtLimitePedido.Text = String.Format("{0:N}", vendedor.LimitePedido);
                localRepresentanteForm.txtLimiteCredito.Text = String.Format("{0:N}", vendedor.LimiteCredito);
                localRepresentanteForm.txtObservacao.Text = vendedor.Observacao.Trim();

                cVendedorModo = "Create";

            }
            else
            {
                MessageBox.Show("Vendedor não encontrado.");
            }



        }


       
        //////////////////////////////////////////////////////
        ///PEDIDO
        //////////////////////////////////////////////////////


        // PedidoLimpar

        public void PedidoLimpar()
        {
            localRepresentanteForm.txtPedidoCodigoBarras.Text = "";
            localRepresentanteForm.txtPedidoProduto.Text = "";
            localRepresentanteForm.txtPedidoQuantidade.Text = "";
            localRepresentanteForm.chkPedidoQuantidade.Checked = false;
            localRepresentanteForm.txtPedidoPrecoUnit.Text = "";
            localRepresentanteForm.txtPedidoProdutoGradeId.Text = "";


            localRepresentanteForm.btnPedidoConfirmar.Enabled = false;
            localRepresentanteForm.btnPedidoCancelar.Enabled = false;
            localRepresentanteForm.txtPedidoCodigoBarras.ReadOnly = false;
            localRepresentanteForm.pnlVendedorPedidoMontar.Enabled = true;






            cVendedorPedidoModo = "Insert";

            localRepresentanteForm.txtPedidoCodigoBarras.Focus();





        }


        // PedidoPesquisar

        public void PedidoPesquisar(string pCodigo)
        {

            long vProdutoGradeId = 0;
            List<ModelLibrary.RepProdutoGrade> produtosgrade = ModelLibrary.MetodosRepresentante.ObterProdutosGrade(pCodigo);

            if (produtosgrade != null)
            {
                if (produtosgrade.Count > 1)
                {
                    Modal.FormProdutosGrade formProdutosGrade = new Modal.FormProdutosGrade(pCodigo);

                    var result = formProdutosGrade.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        vProdutoGradeId = formProdutosGrade.cProdutoGradeId;
                        ExibirProdutoGrade(vProdutoGradeId);
                    }
                    else
                    {
                        vProdutoGradeId = 0;
                    }
                }
                else
                {                    
                    vProdutoGradeId = (produtosgrade.FirstOrDefault() != null) ? produtosgrade.FirstOrDefault().Id : 0;
                    ExibirProdutoGrade(vProdutoGradeId);
                }
            }
            else
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

                var produto = ModelLibrary.MetodosRepresentante.ObterProduto(produtograde.CodigoBarras);

                localRepresentanteForm.txtPedidoProduto.Text = produto.Descricao;
                localRepresentanteForm.txtPedidoPrecoUnit.Text = produtograde.ValorSaida.ToString();
                localRepresentanteForm.txtPedidoProdutoGradeId.Text = produtograde.Id.ToString();

                if (localRepresentanteForm.txtPedidoCodigoBarras.Text != produtograde.CodigoBarras + produtograde.Digito)
                {
                    localRepresentanteForm.txtPedidoCodigoBarras.Text = produtograde.CodigoBarras + produtograde.Digito;
                    if (localRepresentanteForm.chkPedidoQuantidade.Checked == false)
                    {
                        localRepresentanteForm.chkPedidoQuantidade.Checked = true;
                        localRepresentanteForm.txtPedidoQuantidade.Enabled = true;
                    }
                }



                //cImportarProdutoId = produtograde.Id;

                localRepresentanteForm.btnPedidoConfirmar.Enabled = true;
                localRepresentanteForm.btnPedidoCancelar.Enabled = true;

                if (localRepresentanteForm.chkPedidoQuantidade.Checked)
                {
                    localRepresentanteForm.txtPedidoQuantidade.Focus();

                }
                else
                {
                    //inserir direto qtd=1
                    PedidoIncluir();
                }

            }
            else
            {

                MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                //cImportarProdutoId = 0;
                localRepresentanteForm.txtPedidoCodigoBarras.Text = "";
                localRepresentanteForm.txtPedidoCodigoBarras.Focus();
                localRepresentanteForm.btnPedidoConfirmar.Enabled = false;
                localRepresentanteForm.btnPedidoCancelar.Enabled = false;


            }
        }


        public void ExibirPedido(long pVendedorId)
        {





            var pedido = ModelLibrary.MetodosRepresentante.ObterVendedorPedido(pVendedorId, localRepresentanteForm.cCargaId);
            cPedidoId = pedido != null ? pedido.Id : 0;

            List<ModelLibrary.ListaRepVendedorPedido> listapedido = ModelLibrary.MetodosRepresentante.ObterVendedorPedidoItem(cPedidoId);

            BindingListView<ModelLibrary.ListaRepVendedorPedido> view = new BindingListView<ModelLibrary.ListaRepVendedorPedido>(listapedido);

            localRepresentanteForm.grdVendedorPedido.DataSource = view;


            localRepresentanteForm.grdVendedorPedido.Columns[0].Visible = false;
            localRepresentanteForm.grdVendedorPedido.Columns[1].Visible = false;
            localRepresentanteForm.grdVendedorPedido.Columns[2].Visible = false;
            localRepresentanteForm.grdVendedorPedido.Columns[8].Visible = false;
            localRepresentanteForm.grdVendedorPedido.Columns[9].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdVendedorPedido.Columns[4].Width = 300;

            localRepresentanteForm.grdVendedorPedido.ClearSelection();

            if (localRepresentanteForm.grdVendedorPedido.RowCount <= 0)
            {
                localRepresentanteForm.grpFinanceiroCalculo.Visible = false;
            } else
            {
                localRepresentanteForm.grpFinanceiroCalculo.Visible = true;
                ExibirRetornoProduto(cVendedorId);
                ExibirAcerto();

                
            }

            if (pedido != null)
            {

               
                localRepresentanteForm.dlbPedidoTotal.Text = String.Format("{0:C2}", pedido.ValorPedido);
                localRepresentanteForm.dlbPedidoLimite.Text = (localRepresentanteForm.txtLimitePedido.Text == "0,00") ? "<ILIMITADO>" : String.Format("{0:C2}", localRepresentanteForm.txtLimitePedido.Text);
                localRepresentanteForm.dlbRetornoTotalPedido.Text = String.Format("{0:C2}", pedido.ValorPedido);

                localRepresentanteForm.dlbRetornoValorCompra.Text = String.Format("{0:C2}", pedido.ValorCompra);

                if (pedido.DataRetorno != null)
                {
                    localRepresentanteForm.pnlVendedorPedidoMontar.Enabled = false;
                }
                else
                {
                    localRepresentanteForm.pnlVendedorPedidoMontar.Enabled = true;
                }
            }
            else
            {
                localRepresentanteForm.pnlVendedorPedidoMontar.Enabled = true;
                               
            }


            int countpedidos = ModelLibrary.MetodosRepresentante.ContarPedidos(pVendedorId);

            if (countpedidos == 1)
            {
                localRepresentanteForm.smnVendedorPedidoIncluir.Visible = true;
                localRepresentanteForm.btnPedidoImprimir.Enabled = true;
                localRepresentanteForm.smnVendedorRelatorioPedido.Enabled = true;
            }
            else
            {
                localRepresentanteForm.smnVendedorPedidoIncluir.Visible = false;
                localRepresentanteForm.btnPedidoImprimir.Enabled = true;
                localRepresentanteForm.smnVendedorRelatorioPedido.Enabled = false;
            }




        }

        public void ConfirmarPedido()
        {

            if (cVendedorPedidoModo == "Edit")
            {
                PedidoAtualizar();
            }
            else
            {
                PedidoIncluir();
            }

        }


        // PedidoNovo 

        public void PedidoNovo()
        {

            ModelLibrary.MetodosRepresentante.RetornarPedido(cPedidoId);
            ModelLibrary.MetodosRepresentante.InserirPedido(cVendedorId, localRepresentanteForm.cCargaId);
            VendedorReload();
        }
        // PedidoIncluir

        public void PedidoIncluir()
        {



            try
            {
                decimal vQuantidade;

                if (localRepresentanteForm.chkPedidoQuantidade.Checked)
                {

                    if (localRepresentanteForm.txtPedidoQuantidade.Text != "" && localRepresentanteForm.txtPedidoQuantidade.Text != "0")
                    {
                        vQuantidade = Convert.ToDecimal(localRepresentanteForm.txtPedidoQuantidade.Text);
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

                decimal vPreco = Convert.ToDecimal(localRepresentanteForm.txtPedidoPrecoUnit.Text);
                long vProdutoGradeId = Convert.ToInt64(localRepresentanteForm.txtPedidoProdutoGradeId.Text);

                if (vQuantidade > 0)
                {

                    decimal valorpedido = Convert.ToDecimal(vQuantidade) * Convert.ToDecimal(localRepresentanteForm.txtPedidoPrecoUnit.Text);

                    var pedidoliberado = PedidoValidar(valorpedido);

                    if (pedidoliberado)
                    {
                        ModelLibrary.MetodosRepresentante.InserirPedidoItem(localRepresentanteForm.cCargaId, cVendedorId, vProdutoGradeId, vQuantidade, vPreco);

                        //ExibirPedido(cVendedorId);
                        VendedorReload();

                        if (localRepresentanteForm.grdVendedorPedido.RowCount > 0) localRepresentanteForm.grdVendedorPedido.Rows[localRepresentanteForm.grdVendedorPedido.RowCount - 1].Selected = true;

                        GridSelecionar(localRepresentanteForm.grdVendedorPedido, localRepresentanteForm.txtPedidoCodigoBarras.Text);

                        PedidoLimpar();
                    }




                }


            }
            catch (IOException vE)
            {
                MessageBox.Show("Ocorreu um erro ao Inserir o produto. Verifique os dados digitados e tente novamente. Se o erro persisitr, contate o administrador.");
                Console.WriteLine(vE.Message);
            }

        }

        // PedidoEditar

        public void PedidoEditar()
        {


            //ClearCargaProduto();


            cVendedorPedidoModo = "Edit";
            //cImportarProdutoId = Convert.ToInt32(localDeposito.grdConfProduto.CurrentRow.Cells[8].Value);

            localRepresentanteForm.txtPedidoProdutoGradeId.Text = localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["ProdutoGradeId"].Value.ToString();
            localRepresentanteForm.txtPedidoCodigoBarras.Text = localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["CodigoBarras"].Value.ToString();
            localRepresentanteForm.txtPedidoCodigoBarras.ReadOnly = true;
            localRepresentanteForm.txtPedidoProduto.Text = localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["Descricao"].Value.ToString();


            localRepresentanteForm.txtPedidoQuantidade.Text = localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["Quantidade"].Value.ToString();
            localRepresentanteForm.txtPedidoQuantidade.Focus();

            localRepresentanteForm.txtPedidoQtdOriginal.Text = localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["Quantidade"].Value.ToString();

            localRepresentanteForm.txtPedidoPrecoOriginal.Text = string.Format("{0:N}", localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["Preco"].Value);

            localRepresentanteForm.txtPedidoPrecoUnit.Text = string.Format("{0:N}", localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells["Preco"].Value);

            


            localRepresentanteForm.btnPedidoConfirmar.Enabled = true;
            localRepresentanteForm.btnPedidoCancelar.Enabled = true;

        }


        public void PedidoAtualizar()
        {

            Console.WriteLine("Editando Produtos do Pedido...");

            //Inserir Regra de Limite Pedido / Limite Crédito

            decimal valorpedido = Convert.ToDecimal(localRepresentanteForm.txtPedidoQuantidade.Text) * Convert.ToDecimal(localRepresentanteForm.txtPedidoPrecoUnit.Text) - Convert.ToDecimal(localRepresentanteForm.txtPedidoQtdOriginal.Text) * Convert.ToDecimal(localRepresentanteForm.txtPedidoPrecoOriginal.Text);

            var pedidoliberado = PedidoValidar(valorpedido);


            if (pedidoliberado)
            {
                ModelLibrary.MetodosRepresentante.AtualizarPedidoItem(cPedidoId, Convert.ToInt64(localRepresentanteForm.txtPedidoProdutoGradeId.Text), Convert.ToDecimal(localRepresentanteForm.txtPedidoQuantidade.Text), Convert.ToDecimal(localRepresentanteForm.txtPedidoPrecoUnit.Text));

                VendedorReload();

                GridSelecionar(localRepresentanteForm.grdVendedorPedido, localRepresentanteForm.txtPedidoCodigoBarras.Text);

                PedidoLimpar();
            }




            
        }

        // PedidoExcluir

        public void PedidoExcluir()
        {

            if (MessageBox.Show("Deseja realmente excluir o produto selecionado?", "ATENÇÃO! Exclusão de Produto do Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                PedidoLimpar();
                var vPedidoItemId = Convert.ToInt32(localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells[0].Value);
                var vPedidoId = Convert.ToInt32(localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells[1].Value);
                var vQuantidade = Convert.ToDecimal(localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells[7].Value);
                var vPreco = Convert.ToDecimal(localRepresentanteForm.grdVendedorPedido.CurrentRow.Cells[8].Value);


                ModelLibrary.MetodosRepresentante.ExcluirPedidoItem(vPedidoItemId, vPedidoId, vQuantidade, vPreco);

                //ExibirPedido(cVendedorId);

                VendedorReload();
            }

        }


        // PedidoValidarInclusaoAtualizacao

        public bool PedidoValidar(decimal pValorPedido, string pAcao = "")
        {

            if (ValidarLimitePedido(pValorPedido, pAcao))
            {
                if (ValidarLimiteCredito(pValorPedido, pAcao))
                {
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
            
        }


        public bool ValidarLimitePedido(decimal pValorPedido, string pAcao = "")
        {
            if (localRepresentanteForm.dlbLimitePedido.Text != "<ILIMITADO>")
            {

                if (Convert.ToDecimal(localRepresentanteForm.dlbLimitePedidoUtilizado.Text) + pValorPedido > Convert.ToDecimal(localRepresentanteForm.dlbLimitePedidoUtilizado.Text) && Convert.ToDecimal(localRepresentanteForm.dlbLimitePedidoUtilizado.Text) + pValorPedido >= Convert.ToDecimal(localRepresentanteForm.dlbLimitePedido.Text))
                {
                    Console.WriteLine("A operação não pode ser completada pois o valor ultrapassa o limite do pedido. " + localRepresentanteForm.dlbLimitePedidoUtilizado.Text + " + " + pValorPedido.ToString() + " >= " + localRepresentanteForm.dlbLimitePedido.Text);
                    MessageBox.Show("A operação não pode ser completada pois o valor ultrapassa o limite do pedido.");
                    return false;

                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool ValidarLimiteCredito(decimal pValorPedido, string pAcao = "")
        {
            if (localRepresentanteForm.dlbLimiteCredito.Text != "<ILIMITADO>")
            {
                if (Convert.ToDecimal(localRepresentanteForm.dlbLimiteCreditoUtilizado.Text) + pValorPedido >= Convert.ToDecimal(localRepresentanteForm.dlbLimiteCredito.Text))
                {
                    Console.WriteLine("A operação não pode ser completada pois o valor ultrapassa o limite do crédito. " + localRepresentanteForm.dlbLimiteCreditoUtilizado.Text + " + " + pValorPedido.ToString() + " >= " + localRepresentanteForm.dlbLimiteCredito.Text);
                    MessageBox.Show("A operação não pode ser completada pois o valor ultrapassa o limite do crédito.");
                    return false;

                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

        }



        // PedidoTotalizador
        public void PedidoTotalizador()
        {

            Console.WriteLine("Exibindo Totalizador de Produtos do Pedido...");

        }





        //////////////////////////////////////////////////////
        /// RETORNO DE PRODUTOS
        //////////////////////////////////////////////////////


        // RetornoProdutoLimpar

        public void RetornoProdutoLimpar()
        {
            localRepresentanteForm.txtRetornoCodigoBarras.Text = "";
            localRepresentanteForm.txtRetornoProdutoGradeId.Text = "";
            localRepresentanteForm.txtRetornoProduto.Text = "";
            localRepresentanteForm.txtRetornoQuantidade.Text = "";
            localRepresentanteForm.chkRetornoQuantidade.Checked = false;


            localRepresentanteForm.btnRetornoConfirmar.Enabled = false;
            localRepresentanteForm.btnRetornoCancelar.Enabled = false;
            localRepresentanteForm.txtRetornoCodigoBarras.ReadOnly = false;

            cVendedorProdRetModo = "Insert";


            localRepresentanteForm.txtRetornoCodigoBarras.Focus();

        }


        // RetornoProdutoPesquisar

        public void RetornoProdutoPesquisar(string pCodigo)
        {



            try
            {
                int rowIndex = -1;

                DataGridViewRow row = localRepresentanteForm.grdVendedorRetorno.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["CodigoBarras"].Value.ToString().Equals(pCodigo) || r.Cells["ProdutoGradeId"].Value.ToString().Equals(pCodigo))
                    .First();

                rowIndex = row.Index;

                if (rowIndex != -1)
                {
                    
                    

                    localRepresentanteForm.txtRetornoCodigoBarras.Text = localRepresentanteForm.grdVendedorRetorno.Rows[rowIndex].Cells["CodigoBarras"].Value.ToString();
                    localRepresentanteForm.txtRetornoProdutoGradeId.Text = localRepresentanteForm.grdVendedorRetorno.Rows[rowIndex].Cells["ProdutoGradeId"].Value.ToString();
                    localRepresentanteForm.txtRetornoProduto.Text = localRepresentanteForm.grdVendedorRetorno.Rows[rowIndex].Cells["Descricao"].Value.ToString();


                    localRepresentanteForm.txtRetornoQtdPedido.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Quantidade"].Value != null ? localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Quantidade"].Value.ToString() : "";
                    localRepresentanteForm.txtRetornoPreco.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Preco"].Value != null ? localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Preco"].Value.ToString() : "";

                    /*if (localRepresentanteForm.chkRetornoQuantidade.Checked == false)
                    {
                        localRepresentanteForm.chkRetornoQuantidade.Checked = true;
                        localRepresentanteForm.chkRetornoQuantidade.Enabled = true;
                    }*/

                    localRepresentanteForm.btnRetornoConfirmar.Enabled = true;
                    localRepresentanteForm.btnRetornoCancelar.Enabled = true;

                    if (localRepresentanteForm.chkRetornoQuantidade.Checked)
                    {
                        localRepresentanteForm.txtRetornoQuantidade.Focus();

                    }
                    else
                    {
                        //inserir direto qtd=1
                        string vQuantidade = localRepresentanteForm.grdVendedorRetorno.Rows[rowIndex].Cells["Retorno"].Value.ToString();

                        int vIncQuantidade = vQuantidade != "" ? Convert.ToInt32(vQuantidade) : 0;

                        localRepresentanteForm.txtRetornoQuantidade.Text = (vIncQuantidade + 1).ToString();
                        ConfirmarRetornoProduto();
                    }

                    localRepresentanteForm.grdVendedorRetorno.Rows[rowIndex].Selected = true;



                }
                else
                {

                    MessageBox.Show("Código inválido. Não foi possível encontrar este produto no pedido.");

                    //cImportarProdutoId = 0;
                    localRepresentanteForm.txtRetornoCodigoBarras.Text = "";
                    localRepresentanteForm.txtRetornoProduto.Text = "";
                    localRepresentanteForm.txtRetornoProdutoGradeId.Text = "";
                    localRepresentanteForm.txtRetornoCodigoBarras.Focus();
                    localRepresentanteForm.btnRetornoConfirmar.Enabled = false;
                    localRepresentanteForm.btnRetornoCancelar.Enabled = false;


                }
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro ao processar Pesquisa de Produto", "Pesquisa de Produto", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //cImportarProdutoId = 0;
                localRepresentanteForm.txtRetornoCodigoBarras.Text = "";
                localRepresentanteForm.txtRetornoProduto.Text = "";
                localRepresentanteForm.txtRetornoProdutoGradeId.Text = "";
                localRepresentanteForm.txtRetornoCodigoBarras.Focus();
                localRepresentanteForm.btnRetornoConfirmar.Enabled = false;
                localRepresentanteForm.btnRetornoCancelar.Enabled = false;
            }



        }


        public void ExibirRetornoProduto(long pVendedorId)
        {


            List<ModelLibrary.ListaRepVendedorPedido> listaretorno = ModelLibrary.MetodosRepresentante.ObterVendedorPedidoItem(cPedidoId);

            BindingListView<ModelLibrary.ListaRepVendedorPedido> view = new BindingListView<ModelLibrary.ListaRepVendedorPedido>(listaretorno);

            localRepresentanteForm.grdVendedorRetorno.DataSource = view;

            localRepresentanteForm.grdVendedorRetorno.Columns[0].Visible = false;
            localRepresentanteForm.grdVendedorRetorno.Columns[1].Visible = false;
            localRepresentanteForm.grdVendedorRetorno.Columns[2].Visible = false;
            localRepresentanteForm.grdVendedorRetorno.Columns[8].HeaderText = "Qtd. Retorn.";
            localRepresentanteForm.grdVendedorRetorno.Columns[9].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdVendedorRetorno.Columns[4].Width = 300;





        }

        public void ConfirmarRetornoProduto()
        {


            Boolean vAlerta;

            int vTotal = 0;

            for (int i = 0; i < localRepresentanteForm.grdVendedorRetorno.Rows.Count; i++)
            {
                if (localRepresentanteForm.grdVendedorRetorno.Rows[i].Cells[8] != null && localRepresentanteForm.grdVendedorRetorno.Rows[i].Cells[8].Value != null)
                {
                    vTotal += int.Parse(localRepresentanteForm.grdVendedorRetorno.Rows[i].Cells[8].Value.ToString());
                }
            }

            if (vTotal <= 0)
            {
                vAlerta = MessageBox.Show("Deseja realmente retornar os produtos deste pedido? ATENÇÂO: Caso confirme essa ação, NÃO será possível adicionar produtos ao pedido novamente.", "Retornar Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
            }
            else
            {
                vAlerta = true;
            }

            if (vAlerta)
            {



                Console.WriteLine("Atualizando Retorno de Produtos do Pedido...");


                decimal valorpedido = (Convert.ToDecimal(localRepresentanteForm.txtRetornoQtdPedido.Text) - Convert.ToDecimal(localRepresentanteForm.txtRetornoQuantidade.Text)) * Convert.ToDecimal(localRepresentanteForm.txtRetornoPreco.Text);
                var pedidoliberado = PedidoValidar(-valorpedido);


                if (pedidoliberado)
                {
                    ModelLibrary.MetodosRepresentante.RetornarPedidoItem(cPedidoId, Convert.ToInt64(localRepresentanteForm.txtRetornoProdutoGradeId.Text), Convert.ToDecimal(localRepresentanteForm.txtRetornoQuantidade.Text));

                    //ExibirRetornoProduto(cVendedorId);

                    VendedorReload();


                    GridSelecionar(localRepresentanteForm.grdVendedorRetorno, localRepresentanteForm.txtRetornoCodigoBarras.Text);

                    RetornoProdutoLimpar();
                }

            }





        }


        // RetornoProdutoEditar

        public void RetornoProdutoEditar()
        {
            localRepresentanteForm.chkRetornoQuantidade.Checked = true;
            localRepresentanteForm.chkRetornoQuantidade.Enabled = true;

            localRepresentanteForm.txtRetornoProdutoGradeId.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["ProdutoGradeId"].Value.ToString();
            localRepresentanteForm.txtRetornoCodigoBarras.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["CodigoBarras"].Value.ToString();
            localRepresentanteForm.txtRetornoProduto.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Descricao"].Value.ToString();
            localRepresentanteForm.txtRetornoCodigoBarras.ReadOnly = true;

            
            localRepresentanteForm.txtRetornoQtdPedido.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Quantidade"].Value != null ? localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Quantidade"].Value.ToString() : "";
            localRepresentanteForm.txtRetornoPreco.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Preco"].Value != null ? localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Preco"].Value.ToString() : "";

            localRepresentanteForm.txtRetornoQuantidade.Text = localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Retorno"].Value != null ? localRepresentanteForm.grdVendedorRetorno.CurrentRow.Cells["Retorno"].Value.ToString() : "";
            localRepresentanteForm.txtRetornoQuantidade.Focus();


            localRepresentanteForm.btnRetornoConfirmar.Enabled = true;
            localRepresentanteForm.btnRetornoCancelar.Enabled = true;

        }

                     

        // RetornoProdutoTotalizador
        public void RetornoProdutoTotalizador()
        {

            Console.WriteLine("Exibindo Totalizador de Retorno de Produtos do Pedido...");

        }



        //////////////////////////////////////////////////////
        /// ACERTO
        //////////////////////////////////////////////////////
        

        public void ExibirAcerto()
        {

            // carregar valores do pedido-retorno e calcular

            var pedido = ModelLibrary.MetodosRepresentante.ObterVendedorPedido(cVendedorId, localRepresentanteForm.cCargaId);

            if (pedido != null)
            {
                localRepresentanteForm.dlbValorPedido.Text = string.Format("{0:C2}", pedido.ValorPedido);
                localRepresentanteForm.dlbValorCompra.Text = string.Format("{0:C2}", pedido.ValorCompra);
                localRepresentanteForm.dlbPercentualCompra.Text = string.Format("{0}%", pedido.PercentualCompra);
                localRepresentanteForm.dlbFaixaComissao.Text = string.Format("{0}", pedido.FaixaComissao);
                localRepresentanteForm.dlbPercentualComissao.Text = string.Format("{0}%", pedido.PercentualFaixa);
                localRepresentanteForm.dlbValorComissao.Text = string.Format("{0:C2}", pedido.ValorComissao);
                localRepresentanteForm.dlbValorLiquido.Text = string.Format("{0:C2}", pedido.ValorLiquido);
                localRepresentanteForm.dlbRecebimentoAnterior.Text = string.Format("{0:C2}", pedido.ValorAReceber);

                localRepresentanteForm.dlbTotalAPagar.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber);

                localRepresentanteForm.dlbTotalAcerto.Text = string.Format("{0:C2}", pedido.ValorAcerto);

                localRepresentanteForm.dlbTotalPendente.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber - pedido.ValorAcerto);

                cValorTotalAPagar = Convert.ToDecimal(pedido.ValorLiquido + pedido.ValorAReceber);
                cValorRecebido = Convert.ToDecimal(pedido.ValorAcerto);

                if (cValorRecebido > 0)
                {
                    localRepresentanteForm.pnlVendedorRetorno.Enabled = false;
                } else
                {
                    localRepresentanteForm.pnlVendedorRetorno.Enabled = true;
                }
                
                
                string pedidostatus = "";

                switch (pedido.Status)
                {
                    case "0":
                        pedidostatus = "Aberto";
                        break;
                    case "1":
                        pedidostatus = "Aguardando retorno";
                        break;
                    case "2":
                        pedidostatus = "Retornado";
                        break;
                    case "3":
                        pedidostatus = "Acerto realizado";
                        break;
                    case "4":
                        pedidostatus = "Fechado";
                        break;
                }


                localRepresentanteForm.lblAcertoInfo.Text = "Status do Pedido: " + pedidostatus;
                localRepresentanteForm.lblAcertoInfo.Visible = true;

            }








        }


        public void ExibirTitulos()
        {
            //carregar grid de recebimentos

            List<ModelLibrary.ListaTitulos> titulos = ModelLibrary.MetodosRepresentante.ObterListaTitulos(cVendedorId);

            BindingListView<ModelLibrary.ListaTitulos> view = new BindingListView<ModelLibrary.ListaTitulos>(titulos);


            localRepresentanteForm.grdFinanceiroTitulos.DataSource = view;

            localRepresentanteForm.grdFinanceiroTitulos.Columns[0].Visible = false;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[1].Width = 90;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[2].Width = 40;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[3].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdFinanceiroTitulos.Columns[4].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdFinanceiroTitulos.Columns[5].Width = 100;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[6].Width = 100;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[7].Width = 100;
            localRepresentanteForm.grdFinanceiroTitulos.Columns[8].Width = 220;


        }


        public void ReceberAcerto()
        {
            ////exibir form de recebimento

            //if (localRepresentanteForm.txtValorRecebido.Text != "")
            //{
            //    ModelLibrary.MetodosRepresentante.ReceberAcerto(cPedidoId, Convert.ToDecimal(localRepresentanteForm.txtValorRecebido.Text));

            //    VendedorReload();
            //} else
            //{
            //    MessageBox.Show("Informe o valor recebido!");
            //}
                



        }

        public void CalcularValorEmAberto()
        {

            //cValorRecebido = localRepresentanteForm.txtValorRecebido.Text == "" ? 0 : Convert.ToDecimal(localRepresentanteForm.txtValorRecebido.Text);           
            //localRepresentanteForm.dlbAcertoAberto.Text = string.Format("{0:N}", (cValorTotalAPagar - cValorRecebido));
            
        }

        public void AcertoLimpar()
        {

            //cDuplicataId = 0;
            //cDuplicataReceberId = 0;
            //localRepresentanteForm.dlbDuplicataTotal.Text = "";
            //localRepresentanteForm.grpReceberTitulo.Visible = false;
            //localRepresentanteForm.txtValorRecebido.Text = "";
            //localRepresentanteForm.txtDuplicataReceber.Text = "";
            localRepresentanteForm.lblAcertoInfo.Text = "O vendedor não possui nehum pedido em aberto.";

        }

        public void ExibirDuplicata()
        {

            //localRepresentanteForm.grpReceberTitulo.Visible = true;
            //cDuplicataId = localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["Id"].Value == null ? 0 : Convert.ToInt64(localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["Id"].Value);
            //cDuplicataReceberId = Convert.ToInt64(localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["ReceberId"].Value);
            //localRepresentanteForm.txtDuplicataReceber.Text = localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["ValorRecebido"].Value != null ? localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["ValorRecebido"].Value.ToString() : "0";
            //localRepresentanteForm.dlbDuplicataTotal.Text = string.Format("{0:C2}", localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["ValorDuplicata"].Value);
            //localRepresentanteForm.txtDuplicataReceber.Focus();


        }

        public void ReceberDuplicata()
        {

            //if (localRepresentanteForm.txtDuplicataReceber.Text != "")
            //{

            //    if (Convert.ToDecimal(localRepresentanteForm.txtDuplicataReceber.Text) <= Convert.ToDecimal(localRepresentanteForm.grdFinanceiroRecebimentos.CurrentRow.Cells["ValorDuplicata"].Value))
            //    {
            //        ModelLibrary.MetodosRepresentante.ReceberDuplicata(cDuplicataId, cDuplicataReceberId, localRepresentanteForm.cCargaId, Convert.ToDecimal(localRepresentanteForm.txtDuplicataReceber.Text));
            //        //ExibirTitulos();
            //        VendedorReload();
            //        DuplicataLimpar();
            //    } else
            //    {
            //        MessageBox.Show("O valor informado está acima do valor da duplicata, por favor, verifique os dados digitados", "Acerto - Receber Duplicata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }

            //} else
            //{
            //    MessageBox.Show("Informe o valor recebido da duplicata!");
            //}

            

        }

        //////
        //////
        ////// Rotinas comuns
        //////
        //////

        public void GridSelecionar(DataGridView datagrid, string pCodigoBarras)
        {
            try
            {
                datagrid.ClearSelection();
                foreach (DataGridViewRow row in datagrid.Rows)
                {
                    if (row.Cells["CodigoBarras"].Value.ToString() == pCodigoBarras)
                        row.Selected = true;
                }
                datagrid.FirstDisplayedScrollingRowIndex = datagrid.SelectedRows[0].Index;
            } catch
            {
                ///escrever no log.
            }
        }









    }
}
