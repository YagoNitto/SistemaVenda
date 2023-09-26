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
    public class ClienteModel
    {
        
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe o CPF do cliente")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage = "Informe o e-mail do cliente")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
        public string Email { get; set; }
        
        public string Senha { get; set; }
    

        public List<ClienteModel> ListarTodosClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = "select * from Cliente order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ClienteModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    CPF = dt.Rows[i]["cpf_cnpj"].ToString(), 
                    Email = dt.Rows[i]["Email"].ToString(),
                    Senha  = dt.Rows[i]["Senha"].ToString()
                };
                lista.Add(item);
            }
            return lista;
        }

        public ClienteModel RetornarCliente(int? id)
        {
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = $"select * from Cliente where id = '{id}' order by nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            item = new ClienteModel
            {
                Id = dt.Rows[0]["Id"].ToString(),
                Nome = dt.Rows[0]["Nome"].ToString(),
                CPF = dt.Rows[0]["cpf_cnpj"].ToString(), 
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
                sql = $"update Cliente set nome='{Nome}', cpf_cnpj='{CPF}', email='{Email}' where id = '{Id}'";
            }
            else
            {
                sql = $"insert into Cliente(nome, cpf_cnpj, email, senha) values ('{Nome}', '{CPF}', '{Email}', '123456')";
            }
            objDAL.ExecutarComandoSQL(sql);
        }

        // Delete
        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"delete from Cliente where id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}