using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SistemaVenda.Uteis;
using Newtonsoft.Json;

namespace SistemaVenda.Models
{
    public class VendaModel
    {
        public string Id { get; set; }
        public string Cliente_Id { get; set; }
        public string Vendedor_Id { get; set; }
        public double Total { get; set; }
        public string ListaProdutos { get; set; }

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
            string sql = "insert into Venda (data, total, vendedor_id, cliente_id) values " +
                            $"('{dataVenda}', {Total.ToString().Replace(",",".")}, {Vendedor_Id}, {Cliente_Id})";

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
            
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}