using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Globalization;

namespace (NomeProjeto).Code.Apoio
{

                                    //==============================================================================
                                    //  ┌───────────────────────────────────────────────────────────────────────┐  //
                                    //  │                                                                       │  //
                                    //  │   █████████████████████████████████████████████████████████████████   │  //
                                    //  │   █                                                               █   │  //
                                    //  │   █   • DataGridView (Grade de dados com formatação profissional) █   │  //
                                    //  │   █   • TextBox (Padrão, Numérico, Moeda e com Máscara)           █   │  //
                                    //  │   █   • ComboBox (Listas suspensas com binding facilitado)        █   │  //
                                    //  │   █   • Button (Botões com estilo moderno)                        █   │  //
                                    //  │   █   • CheckBox (Caixas de seleção padronizadas)                 █   │  //
                                    //  │   █   • DateTimePicker (Seleção de datas)                         █   │  //
                                    //  │   █   • MaskedTextBox (CPF, CNPJ, Telefone, Data)                 █   │  //
                                    //  │   █   • ProgressBar (Barra de progresso)                          █   |  //
                                    //  │   █   • GroupBox (Contêineres para agrupamento)                   █   │  //
                                    //  │   █   • TabControl (Abas organizadas)                             █   │  //
                                    //  │   █                                                               █   │  //
                                    //  │   █████████████████████████████████████████████████████████████████   │  //
                                    //  │                                                                       │  //
                                    //  └───────────────────────────────────────────────────────────────────────┘  //
                                    //                                                                             //
                                    //==============================================================================
                                    //                                                                             //
                                    //                      🛠️ FUNCIONALIDADES ADICIONAIS                         //
                                    //                                                                             //
                                    //  • Validação automática de campos obrigatórios                              //
                                    //  • Exportação para Excel com formatação                                     //
                                    //  • Limpeza/habilitação de controles em lote                                 //
                                    //  • ToolTips profissionais                                                   //
                                    //  • Máscaras pré-configuradas                                                //
                                    //                                                                             //
                                    //==============================================================================




    //=====================================================================
    // CLASSE CONTROLESPADROES
    // Descrição: Centraliza a padronização de controles Windows Forms
    //=====================================================================
    public static class ControlesPadroes
    {
        //=====================================================================
        // CONFIGURARDATAGRID
        // Descrição: Padroniza a aparência e comportamento de DataGridViews
        // Parâmetros:
        //   dgv - DataGridView a ser configurado
        //   permitirEdicao - Define se o grid permitirá edição (opcional)
        //=====================================================================

        public static void ConfigurarDataGrid(DataGridView dgv, bool permitirEdicao = false)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersHeight = 30;
            dgv.RowHeadersWidth = 25;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = !permitirEdicao;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.BackgroundColor = SystemColors.Window;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarDataGrid(dgvClientes, true);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTEXTBOX
        // Descrição: Padroniza TextBoxes comuns
        // Parâmetros:
        //   txt - TextBox a ser configurado
        //   maxLength - Tamanho máximo do texto (opcional)
        //   somenteLeitura - Define se será readonly (opcional)
        //=====================================================================
        public static void ConfigurarTextBox(TextBox txt, int maxLength = 255, bool somenteLeitura = false)
        {
            txt.Font = new Font("Segoe UI", 9);
            txt.MaxLength = maxLength;
            txt.ReadOnly = somenteLeitura;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = somenteLeitura ? SystemColors.Control : SystemColors.Window;
            txt.Width = 200;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarTextBox(txtNome, 100);
        // ControlesPadroes.ConfigurarTextBox(txtDescricao, 500, true);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTEXTBOXNUMERICO
        // Descrição: Configura TextBox para aceitar apenas valores numéricos
        // Parâmetros:
        //   txt - TextBox a ser configurado
        //   decimalPermitido - Se permite valores decimais (opcional)
        //=====================================================================
        public static void ConfigurarTextBoxNumerico(TextBox txt, bool decimalPermitido = false)
        {
            ConfigurarTextBox(txt, 18);
            txt.KeyPress += (sender, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (decimalPermitido && e.KeyChar != '.' && e.KeyChar != ','))
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                    ((TextBox)sender).Text.IndexOfAny(new[] { '.', ',' }) > -1)
                {
                    e.Handled = true;
                }
            };
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarTextBoxNumerico(txtIdade);
        // ControlesPadroes.ConfigurarTextBoxNumerico(txtPreco, true);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARCOMBOBOX
        // Descrição: Padroniza ComboBoxes
        // Parâmetros:
        //   cmb - ComboBox a ser configurado
        //   dropdownList - Se será apenas seleção (true) ou permitirá digitação (false)
        //=====================================================================
        public static void ConfigurarComboBox(ComboBox cmb, bool dropdownList = true)
        {
            cmb.Font = new Font("Segoe UI", 9);
            cmb.DropDownStyle = dropdownList ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;
            cmb.Width = 200;
            cmb.MaxDropDownItems = 15;
            cmb.FlatStyle = FlatStyle.Flat;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarComboBox(cmbEstados);
        // ControlesPadroes.ConfigurarComboBox(cmbPesquisa, false);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARMASKEDTEXTBOX
        // Descrição: Configura MaskedTextBox para formatos específicos
        // Parâmetros:
        //   msk - MaskedTextBox a ser configurado
        //   tipo - Tipo de máscara ("CPF", "CNPJ", "TELEFONE" ou "DATA")
        //=====================================================================
        public static void ConfigurarMaskedTextBox(MaskedTextBox msk, string tipo)
        {
            msk.Font = new Font("Segoe UI", 9);
            msk.BorderStyle = BorderStyle.FixedSingle;

            switch (tipo.ToUpper())
            {
                case "CPF":
                    msk.Mask = "000.000.000-00";
                    msk.Width = 120;
                    break;
                case "CNPJ":
                    msk.Mask = "00.000.000/0000-00";
                    msk.Width = 150;
                    break;
                case "TELEFONE":
                    msk.Mask = "(00) 00000-0000";
                    msk.Width = 140;
                    break;
                case "DATA":
                    msk.Mask = "00/00/0000";
                    msk.Width = 100;
                    break;
            }
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarMaskedTextBox(mskCpf, "CPF");
        // ControlesPadroes.ConfigurarMaskedTextBox(mskNascimento, "DATA");
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARBOTAO
        // Descrição: Padroniza a aparência de botões
        // Parâmetros:
        //   btn - Botão a ser configurado
        //   largura - Largura do botão em pixels (opcional)
        //=====================================================================
        public static void ConfigurarBotao(Button btn, int largura = 100)
        {
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Width = largura;
            btn.Height = 30;
            btn.Cursor = Cursors.Hand;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarBotao(btnSalvar);
        // ControlesPadroes.ConfigurarBotao(btnCancelar, 120);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARCHECKBOX
        // Descrição: Padroniza CheckBoxes
        // Parâmetros:
        //   chk - CheckBox a ser configurado
        //=====================================================================
        public static void ConfigurarCheckBox(CheckBox chk)
        {
            chk.Font = new Font("Segoe UI", 9);
            chk.AutoSize = true;
            chk.FlatStyle = FlatStyle.Flat;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarCheckBox(chkAtivo);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARDATETIMEPICKER
        // Descrição: Padroniza DateTimePickers
        // Parâmetros:
        //   dtp - DateTimePicker a ser configurado
        //=====================================================================
        public static void ConfigurarDateTimePicker(DateTimePicker dtp)
        {
            dtp.Font = new Font("Segoe UI", 9);
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Width = 120;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarDateTimePicker(dtpNascimento);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARCOLUNASDATAGRID
        // Descrição: Configura colunas de um DataGridView
        // Parâmetros:
        //   dgv - DataGridView a ser configurado
        //   colunas - Nomes das colunas a serem adicionadas
        //=====================================================================
        public static void ConfigurarColunasDataGrid(DataGridView dgv, params string[] colunas)
        {
            dgv.Columns.Clear();
            foreach (var coluna in colunas)
            {
                dgv.Columns.Add(coluna, coluna);
            }
            ConfigurarDataGrid(dgv);
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarColunasDataGrid(dgvClientes, "ID", "Nome", "Email");
        //
        //=====================================================================

        //=====================================================================
        // POPULARCOMBOBOX
        // Descrição: Preenche um ComboBox com itens de um dicionário
        // Parâmetros:
        //   cmb - ComboBox a ser preenchido
        //   itens - Dicionário com itens (Key=Valor, Value=Texto)
        //=====================================================================
        public static void PopularComboBox(ComboBox cmb, Dictionary<object, object> itens)
        {
            cmb.Items.Clear();
            cmb.DisplayMember = "Value";
            cmb.ValueMember = "Key";
            cmb.DataSource = new BindingSource(itens, null);
            ConfigurarComboBox(cmb);
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // var estados = new Dictionary<int, string> {
        //     {1, "SP"}, {2, "RJ"}, {3, "MG"}
        // };
        // ControlesPadroes.PopularComboBox(cmbEstado, estados);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTEXTBOXMASCARA
        // Descrição: Configura máscara temporária para TextBox
        // Parâmetros:
        //   txt - TextBox a ser configurado
        //   mascara - Texto da máscara
        //=====================================================================
        public static void ConfigurarTextBoxMascara(TextBox txt, string mascara)
        {
            txt.MaxLength = mascara.Length;
            txt.Text = mascara;
            txt.Enter += (sender, e) => {
                if (((TextBox)sender).Text == mascara)
                    ((TextBox)sender).Text = "";
            };
            txt.Leave += (sender, e) => {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    ((TextBox)sender).Text = mascara;
            };
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarTextBoxMascara(txtBusca, "Digite para pesquisar...");
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTEXTBOXMOEDA
        // Descrição: Configura TextBox para valores monetários
        // Parâmetros:
        //   txt - TextBox a ser configurado
        //=====================================================================
        public static void ConfigurarTextBoxMoeda(TextBox txt)
        {
            ConfigurarTextBox(txt, 20);
            txt.TextAlign = HorizontalAlignment.Right;
            txt.KeyPress += (sender, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
                {
                    e.Handled = true;
                }

                if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
                {
                    e.Handled = true;
                }
            };

            txt.Leave += (sender, e) => {
                if (decimal.TryParse(txt.Text, out decimal valor))
                {
                    txt.Text = valor.ToString("N2", CultureInfo.CurrentCulture);
                }
            };
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarTextBoxMoeda(txtPreco);
        //
        //=====================================================================

        //=====================================================================
        // VALIDARCAMPOSOBRIGATORIOS
        // Descrição: Valida campos marcados como obrigatórios (Tag = "Obrigatorio")
        // Parâmetros:
        //   container - Container que possui os controles (Form, Panel, etc)
        //   errorProvider - Componente ErrorProvider para exibir erros
        // Retorno: True se todos os campos obrigatórios estão preenchidos
        //=====================================================================
        public static bool ValidarCamposObrigatorios(Control container, ErrorProvider errorProvider)
        {
            bool valido = true;

            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is TextBox txt && txt.Tag?.ToString() == "Obrigatorio" && string.IsNullOrWhiteSpace(txt.Text))
                {
                    errorProvider.SetError(txt, "Campo obrigatório");
                    valido = false;
                }
                else if (ctrl is ComboBox cmb && cmb.Tag?.ToString() == "Obrigatorio" && (cmb.SelectedIndex == -1 || cmb.SelectedValue == null))
                {
                    errorProvider.SetError(cmb, "Selecione uma opção");
                    valido = false;
                }
                else
                {
                    errorProvider.SetError(ctrl, "");
                }
            }

            return valido;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // if (!ControlesPadroes.ValidarCamposObrigatorios(this, errorProvider1))
        // {
        //     MessageBox.Show("Preencha os campos obrigatórios!");
        //     return;
        // }
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARGROUPBOX
        // Descrição: Padroniza GroupBoxes
        // Parâmetros:
        //   grp - GroupBox a ser configurado
        //   titulo - Título do GroupBox (opcional)
        //=====================================================================
        public static void ConfigurarGroupBox(GroupBox grp, string titulo = "")
        {
            grp.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grp.Text = titulo;
            grp.ForeColor = Color.SteelBlue;
            grp.FlatStyle = FlatStyle.Flat;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarGroupBox(gpbDados, "Informações Pessoais");
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTABCONTROL
        // Descrição: Padroniza TabControls
        // Parâmetros:
        //   tabControl - TabControl a ser configurado
        //=====================================================================
        public static void ConfigurarTabControl(TabControl tabControl)
        {
            tabControl.Font = new Font("Segoe UI", 9);
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(100, 25);
            tabControl.SizeMode = TabSizeMode.Fixed;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarTabControl(tabControl1);
        //
        //=====================================================================

        //=====================================================================
        // EXPORTPARAEXCEL
        // Descrição: Exporta dados de um DataGridView para Excel
        // Parâmetros:
        //   dgv - DataGridView com os dados a serem exportados
        // Exceções: Pode lançar exceções de IO ou do ClosedXML
        //=====================================================================
        public static void ExportarParaExcel(DataGridView dgv)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Arquivo Excel|*.xlsx";
                saveFile.Title = "Salvar como Excel";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Dados");

                        // Cabeçalhos
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                        }

                        // Dados
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        workbook.SaveAs(saveFile.FileName);
                        MessageBox.Show("Dados exportados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ExportarParaExcel(dgvRelatorio);
        //
        //=====================================================================

        //=====================================================================
        // ADICIONARBOTAODATAGRID
        // Descrição: Adiciona coluna de botões a um DataGridView
        // Parâmetros:
        //   dgv - DataGridView a receber a coluna
        //   texto - Texto do botão
        //   nomeColuna - Nome identificador da coluna
        //=====================================================================
        public static void AdicionarBotaoDataGrid(DataGridView dgv, string texto, string nomeColuna)
        {
            var colunaBotao = new DataGridViewButtonColumn();
            colunaBotao.Name = nomeColuna;
            colunaBotao.Text = texto;
            colunaBotao.HeaderText = "Ação";
            colunaBotao.UseColumnTextForButtonValue = true;
            colunaBotao.Width = 80;
            dgv.Columns.Add(colunaBotao);
        }
        }
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.AdicionarBotaoDataGrid(dgvClientes, "Editar", "colEditar");
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARPROGRESSBAR
        // Descrição: Padroniza ProgressBars
        // Parâmetros:
        //   pgb - ProgressBar a ser configurada
        //   maximo - Valor máximo (opcional, padrão=100)
        //=====================================================================
        public static void ConfigurarProgressBar(ProgressBar pgb, int maximo = 100)
        {
            pgb.Maximum = maximo;
            pgb.Style = ProgressBarStyle.Continuous;
            pgb.ForeColor = Color.SteelBlue;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarProgressBar(pgbCarregamento, 500);
        //
        //=====================================================================

        //=====================================================================
        // CONFIGURARTOOLTIP
        // Descrição: Configura ToolTips para controles
        // Parâmetros:
        //   controle - Controle que receberá o ToolTip
        //   mensagem - Texto da dica
        //   toolTip - Componente ToolTip
        //=====================================================================
        public static void ConfigurarToolTip(Control controle, string mensagem, ToolTip toolTip)
        {
            toolTip.SetToolTip(controle, mensagem);
            toolTip.ToolTipTitle = "Informação";
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.BackColor = Color.AliceBlue;
            toolTip.ForeColor = Color.DarkSlateGray;
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.ConfigurarToolTip(btnProcessar, 
        //     "Clique para iniciar o processamento", 
        //     toolTip1);
        //
        //=====================================================================

        //=====================================================================
        // HABILITARCONTROLES
        // Descrição: Habilita/desabilita todos os controles de um container
        // Parâmetros:
        //   container - Container com os controles
        //   habilitar - True para habilitar, False para desabilitar
        //=====================================================================
        public static void HabilitarControles(Control container, bool habilitar)
        {
            foreach (Control ctrl in container.Controls)
            {
                ctrl.Enabled = habilitar;

                if (ctrl is TextBox txt)
                {
                    txt.BackColor = habilitar ? SystemColors.Window : SystemColors.Control;
                }
            }
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.HabilitarControles(pnlDados, false); // Desabilita
        // ControlesPadroes.HabilitarControles(this, true); // Habilita tudo
        //
        //=====================================================================

        //=====================================================================
        // LIMPARCONTROLES
        // Descrição: Limpa todos os controles de um container
        // Parâmetros:
        //   container - Container com os controles a serem limpos
        //=====================================================================
        public static void LimparControles(Control container)
        {
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.Text = string.Empty;
                else if (ctrl is ComboBox cmb)
                    cmb.SelectedIndex = -1;
                else if (ctrl is CheckBox chk)
                    chk.Checked = false;
                else if (ctrl is DateTimePicker dtp)
                    dtp.Value = DateTime.Now;
                else if (ctrl is DataGridView dgv)
                    dgv.DataSource = null;
                else if (ctrl.HasChildren)
                    LimparControles(ctrl);
            }
        }

        //=========================================== EXEMPLO DE USO ==========
        //
        // ControlesPadroes.LimparControles(this);
        // ControlesPadroes.LimparControles(gpbDadosPessoais);
        //
        //=====================================================================
    }
}