using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaInfo.Models
{
    public class Produto
    {
        [Display(Name = "Código")]
        public string CodProd { get; set; }

        [Required(ErrorMessage = "Obrigatório digitar um nome !!")]
        [Display(Name = "Nome")]
        public string NomeProd { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a descrição !!")]
        [Display(Name = "Descrição")]
        public string DescProd { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o valor !!")]
        [Display(Name = "Valor unitário")]
        public string ValorUnitario { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o status !!")]
        [Display(Name = "Status")]
        public string StatusProd { get; set; }

    }
}