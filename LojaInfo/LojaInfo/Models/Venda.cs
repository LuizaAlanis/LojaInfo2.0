using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaInfo.Models
{
    public class Venda
    {
        // dados da view
        
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string CodVenda { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string DataVenda { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string FormaPagamento { get; set; }
        public string CodForma { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string StatusVenda { get; set; }
        public string CodStatus { get; set; }

        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeFunc { get; set; }
        public string CodFunc { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeCli { get; set; }
        public string CodCli { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeProd { get; set; }
        public string CodProd { get; set; }

        [Display(Name = "Valor unitário")]
        public string ValorUnitario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Quantidade { get; set; }

        public string Subtotal { get; set; }

        public string Total { get; set; }

    }
}