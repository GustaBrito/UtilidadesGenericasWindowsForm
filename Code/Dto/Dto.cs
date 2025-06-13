using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using (NomeProjeto).code.dto;
using (NomeProjeto).code.apoio;
using (NomeProjeto).code.bll;
using (NomeProjeto).code.dal;


namespace (NomeProjeto).code.Dto
{
    
    public class DtoProjeto
    {
        public int Exemplo1 { get; set; }

        public string Exemplo2 { get; set; }

        public string Exemplo3 { get; set; }

        public string Exemplo4 { get; set; }

        public bool Exemplo5 { get; set; }

        public bool Exemplo6 { get; set; }

        public List<int> PermissoesTelas { get; set; } = new List<int>();

        public DtoProjeto()
        {
            PermissoesTelas = new List<int>();
        }
    }
}