using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

using (NomeProjeto).code.dto;
using (NomeProjeto).code.apoio;
using (NomeProjeto).code.bll;
using (NomeProjeto).code.dal;

namespace (NomeProjeto).code.Dal
{
    internal class ProjetoDal
    {


        //===========================================
        //       EXEMPLO DE USO DE TRANSACTION
        //===========================================

        /*
              ||===============================================================================================================================||
              ||   A melhor pr�tica � gerenciar as transa��es na BLL (camada de neg�cios), com implementa��o t�cnica na DAL. Veja por qu�:     ||
              ||                                                                                                                               ||
              ||   Porque N�O usar no FORM:                                                                                                    ||
              ||   - Viola��o de separa��o de preocupa��es: A FORM n�o deve saber sobre transa��es de banco                                    ||
              ||   - Acoplamento indevido: Torna a interface do usu�rio dependente de detalhes de infraestrutura                               ||
              ||   - Dificuldade de reuso: L�gica de transa��o fica presa � interface                                                          ||
              ||                                                                                                                               ||
              ||   Porque N�O usar apenas na DAL:                                                                                              ||
              ||   - Vis�o limitada: A DAL n�o sabe quando opera��es devem ser at�micas                                                        ||
              ||   - Controle insuficiente: Transa��es que abrangem m�ltiplos objetos DAL precisam ser coordenadas                             ||
              ||                                                                                                                               ||
              ||===============================================================================================================================||


                //=================
                // DAL
                //=================
            
                // DAL (Prov� suporte t�cnico)
                public class PedidoDAL
                {
                    public void Inserir(Pedido pedido, IDbTransaction transaction = null)
                    {
                        using (var cmd = transaction?.Connection.CreateCommand() ?? new SqlCommand())
                        {
                            if (transaction != null)
                                cmd.Transaction = transaction;
            
                            // Implementa��o da query
                        }
                    }
                }


                //=================
                // BLL
                //=================

                // BLL (Coordena a transa��o)
                public class PedidoBLL
                {
                    public void ProcessarPedidoCompleto(Pedido pedido, List<Item> itens)
                    {
                        using (var cn = new SqlConnection("conn_string"))
                        using (var trans = cn.BeginTransaction())
                        {
                            try
                            {
                                new PedidoDAL().Inserir(pedido, trans);
                                new EstoqueDAL().Atualizar(itens, trans);
                                new LogBLL().Registrar("Pedido processado", trans);
                
                                trans.Commit();
                            }
                            catch
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                }


                //=================
                // FORM
                //=================

                // FORM (Chama apenas a BLL)
                private void btnFinalizar_Click(object sender, EventArgs e)
                {
                    try
                    {
                        new PedidoBLL().ProcessarPedidoCompleto(pedido, itens);
                        MessageBox.Show("Sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }


        */


    }
}