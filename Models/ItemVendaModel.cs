using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class ItemVendaModel
    {
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string QtdeProduto { get; set; }
        public string PrecoUnitario { get; set; }
        public string Total { get; set; }
    }   
}