using LojaInfo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LojaInfo.Repositorio
{
    public class AcoesFuncionario
    {
        Conexao con = new Conexao();

        public void CreateFuncionario(Funcionario funcionario)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbFuncionario(NomeFunc,TelFunc,StatusFunc) values(@nome,@telefone,@status)", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = funcionario.NomeFunc;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = funcionario.TelFunc;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = funcionario.StatusFunc;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<Funcionario> BuscarFuncionario()
        {
            List<Funcionario> Funcionariolist = new List<Funcionario>();

            MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Funcionariolist.Add(
                    new Funcionario
                    {
                        CodFunc = Convert.ToString(dr["CodFunc"]),
                        NomeFunc = Convert.ToString(dr["NomeFunc"]),
                        TelFunc = Convert.ToString(dr["TelFunc"]),
                        StatusFunc = Convert.ToString(dr["StatusFunc"])

                    });
            }
            return Funcionariolist;
        }

        public List<Funcionario> ReadFuncionario()
        {
            List<Funcionario> Funcionariolist = new List<Funcionario>();

            MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Funcionariolist.Add(
                    new Funcionario
                    {
                        CodFunc = Convert.ToString(dr["CodFunc"]),
                        NomeFunc = Convert.ToString(dr["NomeFunc"]),
                        TelFunc = Convert.ToString(dr["TelFunc"]),
                        StatusFunc = Convert.ToString(dr["StatusFunc"])

                    });
            }
            return Funcionariolist;
        }

        public bool DeleteFuncionario(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbFuncionario where CodFunc = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool EditFuncionario(Funcionario smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbFuncionario set NomeFunc=@nome, TelFunc=@telefone, StatusFunc=@status where CodFunc=@cod", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@cod", smodel.CodFunc);
            cmd.Parameters.AddWithValue("@nome", smodel.NomeFunc);
            cmd.Parameters.AddWithValue("@telefone", smodel.TelFunc);
            cmd.Parameters.AddWithValue("@status", smodel.StatusFunc);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public DataTable PesquisarFuncionario(Funcionario func)
        {
            MySqlCommand cmd = new MySqlCommand("call PesquisarFuncionario(@pesquisa)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@pesquisa", func.NomeFunc);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable funcionario = new DataTable();
            da.Fill(funcionario);
            con.MyDesconectarBD();
            return funcionario;
        }

        public DataTable PesquisarFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("select * from viewFuncionario", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable funcionario = new DataTable();
            da.Fill(funcionario);
            con.MyDesconectarBD();
            return funcionario;
        }
    }
}