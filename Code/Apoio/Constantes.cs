using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace (NomeProjeto).Code.Apoio
{
    public static class Constantes
    {
        public const string NOME_SISTEMA = " NOME DO SEU PROJETO";
        public static string VERSAO_SISTEMA = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public const string MENSAGEM_INTEGRIDADE_REFERENCIAL = "O registro não pode ser excluido, pois o mesmo esta sendo usado em outro processo !";
        public const string MENSAGEM_JA_EXISTE = "O registro ja existe !";
        public const string MENSAGEM_NAO_ENCONTRADO = "Nenhum registro foi encontrado !";
        public const string MENSAGEM_DATAATE_DATADE = "Campo (Data Até) não pode ser menor que o campo (Data De)!";
        public const string MENSAGEM_NENHUM_PROD_SELECIONADO = "Nenhum produto foi selecionado !";
        public const string MENSAGEM_NENHUM_PEDIDO_SELECIONADO = "Nenhum pedido foi selecionado !";
        public const string MENSAGEM_OPERACAO_REALIZADA_COM_SUCESSO = "Operação realizada com sucesso!";
        public const string MENSAGEM_OPERACAO_NAO_REALIZADA = "Operação NÃO realizada!";
        public const string MENSAGEM_APENAS_ADM_TEM_ACESSO = "Apenas usuários com permissões de ADMINISTRADOR podem realizar essa operação.";
        public const string PARAM_OUTPUT_SYSREFCURSOR = "";

        public const string DIRETORIO_BASE_EXP = @"C:\Temp\NOMEPROJETO"; // gDiretorioBaseExp = @"C:\Temp\Magento"; // diretorio base do sistema para geração de arquivos de exportação


    }
    public static class MensagemInsercao 
    {

    }
    public static class MensagemExcluir
    {

    }
    public static class MensagemUpdate
    {

    }
    public static class MensagemAviso
    {

    }

}
