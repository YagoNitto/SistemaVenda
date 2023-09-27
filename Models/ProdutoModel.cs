using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using SistemaVenda.Uteis;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class ProdutoModel
    {
        
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Informe o nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço unitário do Produto")]
        public decimal? Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de estoque do Produto")]
        public decimal? Quantidade_Estoque { get; set; }

        [Required(ErrorMessage = "Informe a unidade de medida do Produto")]
        public string Unidade_Medida { get; set; }

        public string Link_Foto { get; set; }
        

        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            DAL objDAL = new DAL();
            string sql = "select * from Produto order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    Descricao = dt.Rows[i]["Descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[i]["Preco_Unitario"].ToString()),
                    Quantidade_Estoque = decimal.Parse(dt.Rows[i]["Quantidade_Estoque"].ToString()),
                    Unidade_Medida = dt.Rows[i]["Unidade_Medida"].ToString(),
                    Link_Foto = dt.Rows[i]["Link_Foto"].ToString()

                };
                lista.Add(item);
            }
            return lista;
        }

        public ProdutoModel RetornarProduto(int? id)
        {
            ProdutoModel item;
            DAL objDAL = new DAL();
            string sql = $"select * from Produto where id = '{id}' order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            item = new ProdutoModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["Nome"].ToString(),
                Descricao  = dt.Rows[0]["Descricao"].ToString(),
                Preco_Unitario = decimal.Parse(dt.Rows[0]["Preco_Unitario"].ToString()),
                Quantidade_Estoque = decimal.Parse(dt.Rows[0]["Quantidade_Estoque"].ToString()),
                Unidade_Medida = dt.Rows[0]["Unidade_Medida"].ToString(),
                Link_Foto = dt.Rows[0]["Link_Foto"].ToString()
            };
            return item;
        }

        //Insert ou Update
        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;
            if (Id != null)
            {
                sql = $"update Produto set nome='{Nome}', " + 
                        $"descricao='{Descricao}', " +
                        $"preco_unitario={Preco_Unitario.ToString().Replace(",",".")}, " +
                        $"quantidade_estoque={Quantidade_Estoque.ToString().Replace(",",".")}," +  
                        $"unidade_medida='{Unidade_Medida}', " +
                        $"link_foto='{Link_Foto}' " +
                        $"where id = '{Id}'";
            }
            else
            {
                sql = $"insert into Produto(nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto) values " +
                    $"('{Nome}', '{Descricao}', {Preco_Unitario.ToString().Replace(",",".")}, '{Quantidade_Estoque.ToString().Replace(",",".")}', " +
                    $"'{Unidade_Medida}', '{Link_Foto}')";
            }
            objDAL.ExecutarComandoSQL(sql);
        }

        // Delete
        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"delete from Produto where id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}