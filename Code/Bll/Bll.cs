using System;
using System.Collections.Generic;
using System.Data;

using (NomeProjeto).code.dto;
using (NomeProjeto).code.apoio;
using (NomeProjeto).code.bll;
using (NomeProjeto).code.dal;


namespace (NomeProjeto).code.Bll
{
    public class ProjetoBll
    {
        private readonly ProjetoDal dal = new ProjetoDal();
        private readonly ProjetoDto dto = new ProjetoDto();

        public (tipo) ProjetoFuncao()
        {
            try
            {
                return dal.Funcao();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro.", ex);
            }
        }
    }
}