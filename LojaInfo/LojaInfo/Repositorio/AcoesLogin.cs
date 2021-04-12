using LojaInfo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaInfo.Repositorio
{
    public class AcoesLogin
    {
        // instanciando a classe conexão do banco
        Conexao con = new Conexao();
        MySqlCommand cmd = new MySqlCommand();

        // criando o metodo para testar o usuario e senha no banco
        public Usuario TestarUsuario(Usuario user)
        {
            MySqlCommand cmd = new MySqlCommand("call TestarUsuario(@usuario,@senha)", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.NomeUsuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.SenhaUsuario;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    
                    user.NomeUsuario = Convert.ToString(leitor["NomeUsuario"]);
                    user.SenhaUsuario = Convert.ToString(leitor["SenhaUsuario"]);
                    user.Tipo = Convert.ToString(leitor["tipo"]);
                }
            }
            else
            {
                user.NomeUsuario = null;
                user.SenhaUsuario = null;
                user.Tipo = null;
            }

            return user;
        }
    }
}