using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaInfo.Models
{
    public class Usuario
    {
        public int CodUsuario { get; set; }

        // criando o modelo vindo do banco de dados para ser tratad, campo obrigatório e mostrar o texto indicado
        [Required(ErrorMessage = "Obrigatório digitar um nome !!")]
        [Display(Name = "Usuário")]
        public string NomeUsuario { get; set; }

        // criando o modelo vindo do banco de dados para ser tratad, campo obrigatório e mostrar o texto indicado
        [Required(ErrorMessage = "Obrigatório digitar uma senha !!")]
        [Display(Name = "Senha")]
        public string SenhaUsuario { get; set; }

        public string Tipo { get; set; }
    }
}