using LojaInfo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LojaInfo.Repositorio
{
    public class AcoesCliente
    {
        Conexao con = new Conexao();

        public void CreateCliente(Cliente cliente)
        {

            MySqlCommand cmd = new MySqlCommand("call createCliente(@nome,@telefone,@email,@status);", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.NomeCli;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.TelCli;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.EmailCli;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = cliente.StatusCli;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<Cliente> BuscarCliente()
        {
            List<Cliente> Clientelist = new List<Cliente>();

            MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Clientelist.Add(
                    new Cliente
                    {
                        CodCli = Convert.ToString(dr["CodCli"]),
                        NomeCli = Convert.ToString(dr["NomeCli"]),
                        TelCli = Convert.ToString(dr["TelCli"]),
                        EmailCli = Convert.ToString(dr["EmailCli"]),
                        StatusCli = Convert.ToString(dr["StatusCli"])

                    });
            }
            return Clientelist;
        }

        public List<Cliente> ReadCliente()
        {
            List<Cliente> list = new List<Cliente>();

            MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new Cliente
                    {
                        CodCli = Convert.ToString(dr["CodCli"]),
                        NomeCli = Convert.ToString(dr["NomeCli"]),
                        TelCli = Convert.ToString(dr["TelCli"]),
                        EmailCli = Convert.ToString(dr["EmailCli"]),
                        StatusCli = Convert.ToString(dr["StatusCli"])

                    });
            }
            return list;
        }

        public bool DeleteCliente(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbCliente where CodCli = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool EditCliente(Cliente smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCliente set NomeCli=@nome, TelCli=@telefone, EmailCli=@email, StatusCli=@status where CodCli=@cod", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@cod", smodel.CodCli);
            cmd.Parameters.AddWithValue("@nome", smodel.NomeCli);
            cmd.Parameters.AddWithValue("@telefone", smodel.TelCli);
            cmd.Parameters.AddWithValue("@email", smodel.EmailCli);
            cmd.Parameters.AddWithValue("@status", smodel.StatusCli);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public DataTable PesquisarCliente(Cliente cli)
        {
            MySqlCommand cmd = new MySqlCommand("call PesquisarCliente(@pesquisa)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@pesquisa", cli.NomeCli);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable cliente = new DataTable();
            da.Fill(cliente);
            con.MyDesconectarBD();
            return cliente;
        }

        public DataTable PesquisarCliente()
        {
            MySqlCommand cmd = new MySqlCommand("select * from viewCliente", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable cliente = new DataTable();
            da.Fill(cliente);
            con.MyDesconectarBD();
            return cliente;
        }
    }
}