using LojaInfo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LojaInfo.Repositorio
{
    public class AcoesProduto
    {
        Conexao con = new Conexao();

        public void CreateProduto(Produto produto)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbProduto(NomeProd,DescProd,ValorUnitario,StatusProd) values(@nome,@desc,@valor,@status)", con.MyConectarBD());
            string valor = produto.ValorUnitario;
            valor = valor.Replace(".", "").Replace(",", ".");
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.NomeProd;
            cmd.Parameters.Add("@desc", MySqlDbType.VarChar).Value = produto.DescProd;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = valor;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = produto.StatusProd;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<Produto> BuscarProduto()
        {
            List<Produto> Produtolist = new List<Produto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbProduto", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtolist.Add(
                    new Produto
                    {
                        CodProd = Convert.ToString(dr["CodProd"]),
                        NomeProd = Convert.ToString(dr["NomeProd"]),
                        DescProd = Convert.ToString(dr["DescProd"]),
                        ValorUnitario = Convert.ToString(dr["ValorUnitario"]),
                        StatusProd = Convert.ToString(dr["StatusProd"])

                    });
            }
            return Produtolist;
        }

        public List<Produto> ReadProduto()
        {
            List<Produto> Produtolist = new List<Produto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbProduto", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtolist.Add(
                    new Produto
                    {
                        CodProd = Convert.ToString(dr["CodProd"]),
                        NomeProd = Convert.ToString(dr["NomeProd"]),
                        DescProd = Convert.ToString(dr["DescProd"]),
                        ValorUnitario = Convert.ToString(dr["ValorUnitario"]),
                        StatusProd = Convert.ToString(dr["StatusProd"])

                    });
            }
            return Produtolist;
        }

        public bool DeleteProduto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbProduto where CodProd = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool EditProduto(Produto smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbProduto set NomeProd=@nome, DescProd=@desc, ValorUnitario=@valor, StatusProd=@status where CodProd=@cod", con.MyConectarBD());
            string valor = smodel.ValorUnitario;
            valor = valor.Replace(".", "").Replace(",", ".");
            cmd.Parameters.AddWithValue("@cod", smodel.CodProd);
            cmd.Parameters.AddWithValue("@nome", smodel.NomeProd);
            cmd.Parameters.AddWithValue("@desc", smodel.DescProd);
            cmd.Parameters.AddWithValue("@valor", valor);
            cmd.Parameters.AddWithValue("@status", smodel.StatusProd);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public DataTable PesquisarProduto(Produto prod)
        {
            MySqlCommand cmd = new MySqlCommand("call PesquisarProduto(@pesquisa)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@pesquisa", prod.NomeProd);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable produto = new DataTable();
            da.Fill(produto);
            con.MyDesconectarBD();
            return produto;
        }

        public DataTable PesquisarProduto()
        {
            MySqlCommand cmd = new MySqlCommand("select * from viewProduto", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable produto = new DataTable();
            da.Fill(produto);
            con.MyDesconectarBD();
            return produto;
        }
    }
}

