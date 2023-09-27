using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SistemaVenda.Uteis;
using Newtonsoft.Json;
using System.Data.Common;
using Microsoft.AspNetCore.Builder.Extensions;

namespace SistemaVenda.Models
{
    public class VendaModel
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Cliente_Id { get; set; }
        public string Vendedor_Id { get; set; }
        public double Total { get; set; }
        public string ListaProdutos { get; set; }

        // Para atender o filtro do relatório
        public List<VendaModel> ListagemVendas(string DataDe, string DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }

        // Listagem geral
        public List<VendaModel> ListagemVendas()
        {
            return RetornarListagemVendas("1900/01/01", "2200/01/01");   
        }

        private List<VendaModel> RetornarListagemVendas(string DataDe, string DataAte)
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;
            DAL objDAL = new DAL();
            string sql = "select v1.id, v1.data, v1.total, v2.nome as vendedor, c.nome as cliente from " +
                            "Venda v1 inner join Vendedor v2 on v1.vendedor_id = v2.id inner join Cliente c " +
                            $"on v1.cliente_id = c.id where v1.data >= '{DataDe}' and v1.data <= '{DataAte}' order by data, total; ";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()
                };
                lista.Add(item);
            }
            return lista;  
        }

        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarTodosVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public void Inserir()
        {
            DAL objDAL = new DAL();
            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string sql = "insert into Venda (data, total, Vendedor_id, Cliente_id) values " +
                            $"('{dataVenda}', {Total.ToString().Replace(",",".")}, {Vendedor_Id}, {Cliente_Id})";

            objDAL.ExecutarComandoSQL(sql);

            //Recuperar o ID da venda
            sql = $"select id from Venda where data='{dataVenda}' and Vendedor_Id={Vendedor_Id} and Cliente_Id={Cliente_Id} order by id desc limit 1";
            DataTable dt = objDAL.RetDataTable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            //Deserializar o JSON da lista de produtos e gravá-los na tabela itens_venda
            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for (int i = 0; i < lista_produtos.Count; i++)
            {
                sql = "insert into Itens_Venda(Venda_Id, Produto_Id, qtde_produto, preco_produto) " +
                        $"values ({id_venda}, {lista_produtos[i].CodigoProduto.ToString()}," + 
                        $"{lista_produtos[i].QtdeProduto.ToString()}, " +
                        $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",",".")})";
                
                objDAL.ExecutarComandoSQL(sql);
            }
        }
    }
}