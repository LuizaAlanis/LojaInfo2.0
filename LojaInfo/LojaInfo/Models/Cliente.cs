using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaInfo.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public string CodCli { get; set; }

        [Required(ErrorMessage = "Obrigatório digitar um nome !!")]
        [Display(Name = "Nome")]
        public string NomeCli { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o telefone !!")]
        [Display(Name = "Telefone")]
        public string TelCli { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o email !!")]
        [Display(Name = "Email")]
        public string EmailCli { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o status !!")]
        [Display(Name = "Status")]
        public string StatusCli { get; set; }
    }
}