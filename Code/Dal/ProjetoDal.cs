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
              ||   A melhor prática é gerenciar as transações na BLL (camada de negócios), com implementação técnica na DAL. Veja por quê:     ||
              ||                                                                                                                               ||
              ||   Porque NÃO usar no FORM:                                                                                                    ||
              ||   - Violação de separação de preocupações: A FORM não deve saber sobre transações de banco                                    ||
              ||   - Acoplamento indevido: Torna a interface do usuário dependente de detalhes de infraestrutura                               ||
              ||   - Dificuldade de reuso: Lógica de transação fica presa à interface                                                          ||
              ||                                                                                                                               ||
              ||   Porque NÃO usar apenas na DAL:                                                                                              ||
              ||   - Visão limitada: A DAL não sabe quando operações devem ser atômicas                                                        ||
              ||   - Controle insuficiente: Transações que abrangem múltiplos objetos DAL precisam ser coordenadas                             ||
              ||                                                                                                                               ||
              ||===============================================================================================================================||


                //=================
                // DAL
                //=================
            
                // DAL (Provê suporte técnico)
                public class PedidoDAL
                {
                    public void Inserir(Pedido pedido, IDbTransaction transaction = null)
                    {
                        using (var cmd = transaction?.Connection.CreateCommand() ?? new SqlCommand())
                        {
                            if (transaction != null)
                                cmd.Transaction = transaction;
            
                            // Implementação da query
                        }
                    }
                }


                //=================
                // BLL
                //=================

                // BLL (Coordena a transação)
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