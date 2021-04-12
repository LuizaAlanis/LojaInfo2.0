using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaInfo.Models
{
    public class Funcionario
    {
        [Display(Name = "Código")]
        public string CodFunc { get; set; }

        [Required(ErrorMessage = "Obrigatório digitar um nome !!")]
        [Display(Name = "Nome")]
        public string NomeFunc { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o telefone !!")]
        [Display(Name = "Telefone")]
        public string TelFunc { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o status !!")]
        [Display(Name = "Status")]
        public string StatusFunc { get; set; }



    }
}