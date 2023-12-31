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
    public class VendedorModel
    {
        
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Informe o nome do Vendedor")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe o e-mail do Vendedor")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
        public string Email { get; set; }
        
        public string Senha { get; set; }
    

        public List<VendedorModel> ListarTodosVendedores()
        {
            List<VendedorModel> lista = new List<VendedorModel>();
            VendedorModel item;
            DAL objDAL = new DAL();
            string sql = "select * from Vendedor order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendedorModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    Email = dt.Rows[i]["Email"].ToString(),
                    Senha  = dt.Rows[i]["Senha"].ToString()
                };
                lista.Add(item);
            }
            return lista;
        }

        public VendedorModel RetornarVendedor(int? id)
        {
            VendedorModel item;
            DAL objDAL = new DAL();
            string sql = $"select * from Vendedor where id = '{id}' order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            item = new VendedorModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["Nome"].ToString(),
                Email = dt.Rows[0]["Email"].ToString(),
                Senha  = dt.Rows[0]["Senha"].ToString()
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
                sql = $"update Vendedor set nome='{Nome}', email='{Email}' where id = '{Id}'";
            }
            else
            {
                sql = $"insert into Vendedor(nome, email, senha) values ('{Nome}', '{Email}', '123456')";
            }
            objDAL.ExecutarComandoSQL(sql);
        }

        // Delete
        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"delete from Vendedor where id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}