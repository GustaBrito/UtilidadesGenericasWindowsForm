using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using (NomeProjeto).code.dto;
using (NomeProjeto).code.apoio;
using (NomeProjeto).code.bll;
using (NomeProjeto).code.dal;

namespace (NomeProjeto).code.dal
{
    public class Conexao
    {
        // Homologação
        private static readonly string StringConexao = "";

        // Produção
        //private static readonly string StringConexao = "";


        public static string Banco()
        {
            return StringConexao;
        }
    }
}
