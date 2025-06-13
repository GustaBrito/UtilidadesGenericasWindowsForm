using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

using (NomeProjeto).code.apoio;

namespace (NomeProjeto).Code.Apoio
{
    public static class Utils
    {

        //======================================
        // REMOVE TODOS OS ACENTOS DAS PALAVRAS
        //======================================


        //============= EXEMPLO DE USO ===============
        //
        //      Utils.LimpaCaracter(txtCampo)
        //
        //============================================

        public static string LimpaCaracter(string pCampo)
        {
            string sCampo = pCampo.Replace("'", "").Replace("Á", "A")
                .Replace("É", "E").Replace("Í", "I").Replace("Ó", "O")
                .Replace("Ú", "U").Replace("Ã", "A").Replace("Õ", "O")
                .Replace("Â", "A").Replace("Ê", "E").Replace("Ç", "C")
                .Replace("Ô", "O").Replace("á", "a").Replace("à", "a")
                .Replace("é", "e").Replace("í", "i").Replace("ó", "o")
                .Replace("ô", "o").Replace("ú", "u").Replace("ç", "c")
                .Replace("ã", "a").Replace("õ", "o").Replace(".", "")
                .Replace("-", "").Trim();
            return sCampo;
        }


        //==============================
        // CONVERTE O CAMPO PARA DATA 
        // JA FAZ A VERIFICAÇÃO DE DATA
        //==============================

        //============= EXEMPLO DE USO ===============
        // 
        //      Utils.IsDate(txtCampo)
        //
        //============================================

        public static bool IsDate(string pData)
        {
            bool bRetorno = true;
            try
            {
                DateTime dData = Convert.ToDateTime(pData);
            }
            catch (FormatException)
            {
                bRetorno = false;
            }
            return bRetorno;
        }


    //=====================================================
    // DELETA UMA LINHA DE UM DATATABLE COM VALOR DEFINIDO
    // pValor = PARAMETRO PARA IDENTIFICAR A LINHA
    // pDataTable = QUAL DATATABLE SERÁ  REMOVIDO A LINHA
    //=====================================================

    //============= EXEMPLO DE USO ===============
    // 
    //      tabelaClientes.Rows.Add(1, "João Silva", "joao@email.com");
    //      tabelaClientes.Rows.Add(2, "Maria Souza", "maria@email.com");
    //      tabelaClientes.Rows.Add(3, "Carlos Oliveira", "carlos@email.com");
    //
    //      Utils.DeleteRowDataTable("2", tabelaClientes);
    //
    //============================================

    public static DataTable DeleteRowDataTable(string pValor, DataTable pDataTable)
        {
            try
            {
                foreach (DataRow row in pDataTable.Rows)
                {
                    if (row[0].ToString().Trim() == pValor.Trim())
                    {
                        row.Delete();
                        break;
                    }
                }
                pDataTable.AcceptChanges();
            }
            catch (Exception myError)
            {
                throw new Exception(myError.Message);
            }
            return pDataTable;
        }


        //=======================
        // CRIPTOGRAFA EM BASE64 
        //=======================

        //============= EXEMPLO DE USO ===============
        //
        //      Utils.cCrypt(SenhaOriginal)
        //
        //============================================

        public static string cCrypt(string pCampo)
        {
            byte[] b = Encoding.ASCII.GetBytes(pCampo);
            string vCript = Convert.ToBase64String(b);
            return vCript;
        }

        //==========================
        // DESCRIPTOGRAFA EM BASE64 
        //==========================


        //============= EXEMPLO DE USO ===============
        // 
        //      Utils.dCrypt(Senha)
        //
        //============================================

        public static string dCrypt(string pCampo)
        {
            byte[] b = Convert.FromBase64String(pCampo);
            string vDecript = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return vDecript;
        }


        //=================================================
        // TRUNCA O VALOR DO CAMPO
        // pCampo = Texto original
        // pTamanho = Tamanho máximo que o valor vai ter (0 = não trunca)
        // pRemoverUltimoNumero = Se true, remove o último dígito numérico
        //=================================================

        //==========================  EXEMPLO DE USO ============================ 
        // 
        //      Utils.TruncCampo(textoOriginal, tamanhoMaximo, false/true);
        //
        //      Truncar tamanho = TruncCampo("Texto12345", 5);
        //      Remove Ultimo Numero = TruncCampo("Texto12345", 0, true);
        //      Ambos = TruncCampo("Texto12345", 5, true);
        //
        //======================================================================= 

        public static string TruncCampo(string pCampo, int pTamanho = 0, bool pRemoverUltimoNumero = false)
        {
            string sRetorno = pCampo.Trim();

            // Primeiro aplica a remoção do último número se solicitado
            if (pRemoverUltimoNumero && !string.IsNullOrEmpty(sRetorno))
            {
                if (char.IsDigit(sRetorno[sRetorno.Length - 1]))
                {
                    sRetorno = sRetorno.Substring(0, sRetorno.Length - 1);
                }
            }

            // Depois aplica o truncamento pelo tamanho se necessário
            if (pTamanho > 0 && sRetorno.Length > pTamanho)
            {
                sRetorno = sRetorno.Substring(0, pTamanho);
            }

            return sRetorno;
        }

        //=====================================================================
        // METODOS DE MENSAGEM, APENAS PARA PUXAR PRONTA 
        // pMensagem = (JA DEFINIDO NA CLASSE CONSTANTES)
        // nomeLabel = NOME A SER PASSADO PELO PROGRAMA NA MENSAGEM DE AGUARDE
        // mensagem = MENSAGEM NA MENSAGEM DE AGUARDE
        // exibir = SITUAÇÃO DA MENSAGEM AGUARDE
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      Utils.MsgAguarde( "Aviso de segurança" , " Aguarde a operação carregar", true)
        //      **(Os outros campos seria somente a mensagem, mas com os icones personalizados)**
        //      Utils.MsgExclamation(" Os campos devem estar todos preenchidos ");
        //
        //========================================================================================================

        public static void MsgAguarde(Label nomeLabel, string mensagem, bool exibir)
        {
            nomeLabel.Text = mensagem;
            nomeLabel.Visible = exibir;
            nomeLabel.Refresh();
        }

        public static void MsgErro(string pMensagem)
        {
            MessageBox.Show(pMensagem, Constantes.NOME_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

        public static void MsgExclamation(string pMensagem)
        {
            MessageBox.Show(pMensagem, Constantes.NOME_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void MsgInformation(string pMensagem)
        {
            MessageBox.Show(pMensagem, Constantes.NOME_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        public static DialogResult MsgPergunta(string mensagem)
        {
            return MessageBox.Show(mensagem, Constantes.NOME_SISTEMA, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }


        //=====================================================================
        // VERIFICACAO DE CAMPOS PARA TIPOS NUMERICOS
        // pCampo = (JA DEFINIDO NA CLASSE CONSTANTES)
        // pTipoCampo 'I'= Inteiro
        //            'S'= Float
        //            'D'= Double
        //            'T'= Ulong
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        // // Verificando se uma string pode ser convertida para inteiro (tipo 'I')
        // string campoId = "12345";
        // bool ehInteiro = IsNumeric(campoId, 'I');  // Retorna true
        //===================================================================================
        // Verificando se uma string pode ser convertida para float (tipo 'S')
        // string campoSalario = "2500.75";
        // bool ehFloat = IsNumeric(campoSalario, 'S');  // Retorna true
        //===================================================================================
        // Verificando se uma string pode ser convertida para double (tipo 'D')
        // string campoPrecisao = "3.14159265359";
        // bool ehDouble = IsNumeric(campoPrecisao, 'D');  // Retorna true
        //===================================================================================
        // Verificando se uma string pode ser convertida para ulong (tipo 'T')
        // string campoGrande = "18446744073709551615";
        // bool ehUlong = IsNumeric(campoGrande, 'T');  // Retorna true
        //===================================================================================
        // Casos de teste que retornam false
        // string campoInvalido1 = "ABC123";
        // bool teste1 = IsNumeric(campoInvalido1, 'I');  // Retorna false
        //===================================================================================
        // string campoInvalido2 = "25,00";  // Note que usa vírgula em vez de ponto
        // bool teste2 = IsNumeric(campoInvalido2, 'D');  // Retorna false
        //===================================================================================
        // string campoVazio = "";
        // bool teste3 = IsNumeric(campoVazio, 'S');  // Retorna false
        //========================================================================================================

        public static bool IsNumeric(string pCampo, char pTipoCampo)
        {
            bool bRetorno = false;

            if (pTipoCampo == 'I')
            {
                try
                {
                    int iValor = Convert.ToInt32(pCampo);
                    bRetorno = true;
                }
                catch (FormatException)
                {
                    bRetorno = false;
                }
            }

            else if (pTipoCampo == 'S')
            {
                try
                {
                    Single sValor = Convert.ToSingle(pCampo);
                    bRetorno = true;
                }
                catch (FormatException)
                {
                    bRetorno = false;
                }
            }

            else if (pTipoCampo == 'D')
            {
                try
                {
                    double dValor = Convert.ToDouble(pCampo);
                    bRetorno = true;
                }
                catch (FormatException)
                {
                    bRetorno = false;
                }
            }

            else if (pTipoCampo == 'T')
            {
                try
                {
                    double dValor = Convert.ToUInt64(pCampo);
                    bRetorno = true;
                }
                catch (FormatException)
                {
                    bRetorno = false;
                }
            }
            return bRetorno;
        }


        //=====================================================================
        // EXTRAI A PRIMEIRA PARTE DE UMA STRING ATÉ ENCONTRAR UM CARACTERE SEPARADOR  
        // pCaracterSeparador: O caractere que delimita onde a string deve ser "cortada".
        // pCampoString: A string original da qual queremos extrair a parte inicial.
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        // string texto = "123-456-789";
        // string separador = "-";
        //
        // string id = RetornaIDObjeto(separador, texto); // Retorna "123" (tudo antes do primeiro "-")
        //
        //========================================================================================================

        public static string RetornaIDObjeto(string pCaracterSeparador, string pCampoString)
        {
            string sRet;
            string sStr = "";

            if (pCaracterSeparador.Trim() == "")
            {
                sRet = pCampoString.Trim();
            }
            else
            {
                for (int i = 0; i <= pCampoString.Trim().Length; i++)
                {
                    if (pCampoString.Substring(i, 1) != pCaracterSeparador)
                    {
                        sStr += pCampoString.Substring(i, 1);
                    }

                    else
                    {
                        break;
                    }
                }
                sRet = sStr;
            }
            return sRet;
        }


        //=====================================================================
        // COPIA OS DADOS DE UM DATAGRID PARA UM DATATABLE, IGNORANDO COLUNAS TIPO IMAGEME E TRATA DADOS PARA EVISTAR QUEBRA DE LINHA
        // pDataGrid = NOME DO DATAGRID
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      // Suponha que você tenha um DataGridView chamado "dgvClientes" populado com dados.
        //      DataTable tabelaCopiada = Copia_DataGrid_DataTable(dgvClientes);
        // Agora você pode usar o DataTable para exportar para Excel, salvar em banco de dados, etc.
        // foreach (DataRow row in tabelaCopiada.Rows)
        // {
        // Console.WriteLine(row["Nome"].ToString()); // Exemplo: acessa a coluna "Nome"
        // }
        //
        //========================================================================================================

        public static DataTable Copia_DataGrid_DataTable(DataGridView pDataGrid)
        {
            DataTable dt = new DataTable();
            DataRow dRow;

            foreach (DataGridViewColumn col in pDataGrid.Columns)
            {
                if (col.GetType() != typeof(DataGridViewImageColumn))
                {
                    dt.Columns.Add(col.HeaderText.Trim(), typeof(string));
                }
            }

            foreach (DataGridViewRow row in pDataGrid.Rows)
            {
                dRow = dt.NewRow();
                foreach (DataGridViewColumn col in pDataGrid.Columns)
                {
                    if (col.GetType() != typeof(DataGridViewImageColumn))
                    {
                        if (row.Cells[col.Name].Value != null && row.Cells[col.Name].Value.ToString() != string.Empty)
                            dRow[col.HeaderText] = row.Cells[col.Name].Value.ToString().Replace("\n", string.Empty);
                    }
                }
                dt.Rows.Add(dRow);
            }
            return dt;
        }


        //=====================================================================
        // EXPORTA UM DATATABLE PARA UM ARQUIVO EXCEL .XLS OU .TXT
        // pDiretorio: CAMINHO ONDE O ARQUIVO SERÁ SALVO (ex: @"C:\Exportados").
        // pNomeArquivo: NOME DO ARQUIVO (ex: "RelatorioClientes").
        // pDataTable: DADOS A SEREM EXPORTADOS
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      DataTable dados = new DataTable();
        //      dados.Columns.Add("Nome");
        //      dados.Columns.Add("Idade");
        //      dados.Rows.Add("João Silva", "30");
        //      dados.Rows.Add("Maria Çosta", "25");
        //
        //      string diretorio = @"C:\Exportados";
        //      string nomeArquivo = "Clientes_2024";
        //
        //      bool sucesso = GeraArquivoSaida(diretorio, nomeArquivo, dados);
        //
        //========================================================================================================

        public static bool GeraArquivoSaida(string pDiretorio, string pNomeArquivo, DataTable pDataTable)
        {
            StreamWriter sTexto = null;
            short iCol = 0;
            bool bRetorno = false;
            string sDados = string.Empty;

            try
            {
                //
                // Cria o novo arquivo, verifica a quantida de de linhas caso tenha menos que 65000 linhas gera em excel caso contrario gera em .txt
                // isso foi feito para dar compatibilidade entre o uso dos arquivos em excel (xls e xlsx)
                //

                if (!Directory.Exists(pDiretorio))
                {
                    Directory.CreateDirectory(pDiretorio);
                }

                string extensao = pDataTable.Rows.Count <= 65000 ? ".xls" : ".txt";

                if (File.Exists(pDiretorio + "\\" + pNomeArquivo + extensao))
                    File.Delete(pDiretorio + "\\" + pNomeArquivo + extensao);

                sTexto = new StreamWriter(pDiretorio + "\\" + pNomeArquivo + extensao);

                //
                // Cria o cabeçalho com os nomes das colunas
                //

                foreach (DataColumn col in pDataTable.Columns)
                {
                    sDados += col.Caption + "\t";
                    iCol++;
                }

                sTexto.WriteLine(sDados);

                //
                // Inicia a geração das linhas do arquivo
                //
                for (int iLinha = 0; iLinha <= pDataTable.Rows.Count - 1; iLinha++)
                {
                    sDados = string.Empty;
                    for (iCol = 0; iCol <= pDataTable.Columns.Count - 1; iCol++)
                    {
                        sDados += pDataTable.Rows[iLinha][iCol].ToString().Trim().Replace("Ç", "C").Replace("ç", "c").Replace("Ã", "A").Replace("ã", "a") + "\t"; //row[iRow].ToString().Trim().Replace("Ç", "C").Replace("Ã", "A") + "\t";
                    }
                    sTexto.WriteLine(sDados);
                }
                bRetorno = true;
            }
            catch (IOException myError)
            {
                MsgErro("Falha na geração do arquivo de saida !\n\nDescrição do erro: " + myError.Message);
            }
            catch (Exception myError)
            {
                MsgErro(myError.Message);
            }
            finally
            {
                if (sTexto != null)
                    sTexto.Close();
            }

            return bRetorno;
        }


        //=====================================================================
        // ABRE UM DIRETÓRIO, SE EXISTIR 
        // pLocal = Caminho do arquivo
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      string caminhoDownloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        //      AbreDiretorio_Programa(caminhoDownloads);
        //
        //      Tenta abrir uma pasta que não existe (exibe mensagem de erro)
        //      AbreDiretorio_Programa(@"C:\PastaInexistente");
        //========================================================================================================

        public static void AbreDiretorio_Programa(string pLocal)
        {
            try
            {
                if (Directory.Exists(pLocal))
                {
                    Process.Start(pLocal);
                }

                else
                {
                    MsgExclamation("O local selecionado não foi encontrado !");
                }
            }
            catch (Exception myError)
            {
                throw new Exception(myError.Message);
            }
        }


        //=====================================================================
        // VERIFICA SE UMA STRING TEM UM VALOR INTEIRO VALIDO
        // pValor: STRING A SER VERIFICADA (ex: "123", "12.5", "1,000").
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      Console.WriteLine(ConsisteValorInteiro("123"));     // true (é inteiro)
        //      Console.WriteLine(ConsisteValorInteiro("12.3"));    // false (contém ponto)
        //      Console.WriteLine(ConsisteValorInteiro("1,000"));   // false (contém vírgula)
        //      Console.WriteLine(ConsisteValorInteiro(""));        // Exception (string vazia)
        //  
        //========================================================================================================  

        public static bool ConsisteValorInteiro(string pValor)
        {
            try
            {
                // caso encontre o caracter ponto ou virgula retorna false neste caso o valor nao é inteiro
                return !(pValor.IndexOf('.', 0, pValor.Length - 1) > 0 || pValor.IndexOf(',', 0, pValor.Length - 1) > 0);
            }
            catch (Exception myError)
            {
                throw new Exception(myError.Message);
            }
        }

        //=====================================================================
        // LE UMA CONFIG DE APP.CONFIG OU WEB.CONFIG DA APLICAÇÃO
        // chave = etorna o valor correspondente da seção <appSettings>
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      string servidor = LerConfiguracao("Servidor"); // Retorna "servidor01"
        //
        //========================================================================================================

        public static string LerConfiguracao(string chave)
        {
            return ConfigurationManager.AppSettings[chave].ToString();
        }



        //=====================================================================
        // FORMATA VALORES NUMÉRICOS DE ATÉ 3 CASAS DECIMAIS
        // valor = FORMATA O VALOR
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      FormataValorQtd(12.3456);    // Retorna "12.346"
        //      FormataValorQtd(0);          // Retorna "0.000"
        //      FormataValorQtd(-5.1234);    // Retorna "-5.123"
        //
        //========================================================================================================

        public static string FormataValorQtd(double valor)
        {
            if (valor > 0)
                return valor.ToString("F3", CultureInfo.InvariantCulture);

            else
                return valor.ToString("F3", CultureInfo.InvariantCulture);
        }

        //=====================================================================
        // FORMATA VALORES DOUBLE
        // valor = VALOR A SER VERIFICADO COMO DOUBLE
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      FormataValor(15.789); // Retorna "15.79"
        //      FormataValor(-3.2);   // Retorna "-3.20"
        //
        //========================================================================================================

        public static string FormataValor(double valor)
        {
            if (valor > 0)
                return valor.ToString("F2", CultureInfo.InvariantCulture);

            else
                return valor.ToString("F2", CultureInfo.InvariantCulture);
        }

        //=====================================================================
        // FILTRA UM DATATABLE USANDO UM WHERE E RETORNA OUTRO DATATABLE COM CLONE()
        // pNomeDataTable = DATATABLE ESCOLHIDO PARA CLONAR
        // pWhere = CLAUSULA DE PARAMETROS PARA A CLONAGEM
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //     DataTable clientesFiltrados = DatatableSelect(dtClientes, "Idade > 30 AND Cidade = 'São Paulo'");
        //
        //========================================================================================================

        public static DataTable DatatableSelect(DataTable pNomeDataTable, string pWhere)
        {
            // Declaro as variaveis do datatable
            DataTable dtResultado = new DataTable();

            // Cria as colunas do datatable
            foreach (DataColumn col in pNomeDataTable.Columns)
            {
                dtResultado.Columns.Add(col.Caption, typeof(string));
            }

            // Faz o filtro para o datatable
            // Exemplo: DataRow[] result = table.Select("Size >= 230 AND Sex = 'm'");
            DataRow[] result = pNomeDataTable.Select(pWhere);

            // Carrego o datatable de saida da rotina com os dados ja filtrados pela clausula where
            foreach (DataRow row in result)
            {
                DataRow linha = dtResultado.NewRow();
                foreach (DataColumn col in pNomeDataTable.Columns)
                {
                    linha[col.Caption] = row[col.Caption];
                }
                dtResultado.Rows.Add(linha);
            }

            return dtResultado;
        }


        //=====================================================================
        // LIMPA O TEXTBOX 
        // lista = PODE-SE FAZER UMA LISTA DE CAMPOS TEXTBOX PARA PASSAR NESSA FUNÇÃO E LIMPA-LOS
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      List<TextBox> campos = new List<TextBox>{ txtNome, txtEmail };
        //      LimpaTxtBox(campos);
        //
        //========================================================================================================

        public static void LimpaTxtBox(List<TextBox> lista)
        {
            try
            {
                foreach (var item in lista)
                    item.Text = string.Empty;
            }
            catch (Exception erro)
            {
                MsgErro(erro.Message);
            }
        }


        //=====================================================================
        // REMOVE AS TAGS HTML E SCRIPTS DE UMA STRING, MANTENDO APENAS O TEXTO PURO
        // texto = STRING PARA RECEBER A VERIFICAÇÃO HTML
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      string html = "<p>Texto <b>importante</b></p>";
        //      string texto = html.RemoveHTML(); // Retorna "Texto importante"
        //
        //========================================================================================================

        public static string RemoveHTML(this string texto)
        {
            texto = texto.Replace("&nbsp; ", " ");

            Regex regJs = new Regex(@"(?s)<\s?script.*?(/\s?>|<\s?/\s?script\s?>)", RegexOptions.IgnoreCase);
            texto = regJs.Replace(texto, "");

            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            texto = reg.Replace(texto, "");

            texto = Regex.Replace(texto, @"\s+", " ");

            Regex.Replace(texto.Replace(@"\n", " "), "<[a-zA-Z/].*?>", string.Empty);
            texto = HttpUtility.HtmlDecode(texto);

            return texto.Replace("  ", " ").TrimStart().TrimEnd();
        }


        //=====================================================================
        // VALIDA SE UMA STRING É UM ENDEREÇO DE EMAIL VALIDO
        // email = STRING PARA VERIFICAR EMAIL 
        //=====================================================================


        //=========================================== EXEMPLO DE USO =============================================
        //
        //      bool valido = "user@example.com".IsToEmail(); // true
        //      bool invalido = "user@.com".IsToEmail(); // false
        //
        //========================================================================================================

        public static bool IsToEmail(this string email)
        {
            try
            {
                // Define uma expressão regular para validar o formato do email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                // Verifica se o email corresponde ao padrão
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }



    }
}
