using LojaInfo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LojaInfo.Repositorio
{
    public class AcoesVenda
    {
        Conexao con = new Conexao();

        public void CreateVenda(Venda venda)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbVenda(CodVenda, DataVenda, CodFunc, CodCli, CodStatus, CodForma, Arquivar) values(@cod, @data, @func, @cli, @status, @forma, 0)", con.MyConectarBD());

            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = venda.CodVenda;
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = venda.DataVenda;
            cmd.Parameters.Add("@func", MySqlDbType.VarChar).Value = venda.CodFunc;
            cmd.Parameters.Add("@cli", MySqlDbType.VarChar).Value = venda.CodCli;
            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = venda.CodStatus;
            cmd.Parameters.Add("@forma", MySqlDbType.VarChar).Value = venda.CodForma;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void CreateItens(Venda venda)
        {
            MySqlCommand cm = new MySqlCommand("select ValorUnitario from tbProduto where CodProd = @prod", con.MyConectarBD());
            cm.Parameters.Add("@prod", MySqlDbType.VarChar).Value = venda.CodProd;
            venda.ValorUnitario = cm.ExecuteScalar().ToString();
            string valor = venda.ValorUnitario;
            valor = valor.Replace(".", "").Replace(",", ".");

            MySqlCommand cmd = new MySqlCommand("insert into tbItens(CodVenda, CodProd, Quantidade, ValorUnitario) values(@cod, @prod, @quantidade, @valor)", con.MyConectarBD());

            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = venda.CodVenda;
            cmd.Parameters.Add("@prod", MySqlDbType.VarChar).Value = venda.CodProd;
            cmd.Parameters.Add("@quantidade", MySqlDbType.VarChar).Value = venda.Quantidade;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = valor;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void RemoverItem(Venda venda)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbItens where CodVenda = @cod and CodProd = @prod;", con.MyConectarBD());

            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = venda.CodVenda;
            cmd.Parameters.Add("@prod", MySqlDbType.VarChar).Value = venda.CodProd;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void ArquivarVenda(string id)
        {
            MySqlCommand cmd = new MySqlCommand("update tbVenda set Arquivar = 1 where CodVenda = @cod", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void RecuperarVenda(string id)
        {
            MySqlCommand cmd = new MySqlCommand("update tbVenda set Arquivar = 0 where CodVenda = @cod", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void DeleteVenda(string id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbItens where CodVenda = @cod", con.MyConectarBD());
            MySqlCommand cm = new MySqlCommand("delete from tbVenda where CodVenda = @cod", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;
            cm.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;

            cmd.ExecuteNonQuery();
            cm.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void DeleteItem(string id, string id1)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbItens where CodVenda = @CodVenda and CodProd = @CodProd", con.MyConectarBD());
            cmd.Parameters.Add("@CodVenda", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@CodProd", MySqlDbType.VarChar).Value = id1;

            cmd.ExecuteNonQuery();
          
            con.MyDesconectarBD();
        }

        public List<Venda> ReadVenda()
        {

            List<Venda> Vendalist = new List<Venda>();

            MySqlCommand cmd = new MySqlCommand("select distinct CodVenda CodVenda, DataVenda, NomeFunc, NomeCli, StatusVenda from viewVenda where Arquivar = 0 order by CodVenda DESC", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Vendalist.Add(
                    new Venda
                    {
                        CodVenda = Convert.ToString(dr["CodVenda"]),
                        DataVenda = Convert.ToString(dr["DataVenda"]),
                        NomeFunc = Convert.ToString(dr["NomeFunc"]),
                        NomeCli = Convert.ToString(dr["NomeCli"]),
                        StatusVenda = Convert.ToString(dr["StatusVenda"]),
                        //Total = Convert.ToString(dr["Total"])
                    });
            }
            return Vendalist;
        }

        public List<Venda> ReadVendaArquivada()
        {

            List<Venda> Vendalist = new List<Venda>();

            MySqlCommand cmd = new MySqlCommand("select distinct CodVenda CodVenda, DataVenda, NomeFunc, NomeCli, StatusVenda from viewVenda where Arquivar = 1", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Vendalist.Add(
                    new Venda
                    {
                        CodVenda = Convert.ToString(dr["CodVenda"]),
                        DataVenda = Convert.ToString(dr["DataVenda"]),
                        NomeFunc = Convert.ToString(dr["NomeFunc"]),
                        NomeCli = Convert.ToString(dr["NomeCli"]),
                        StatusVenda = Convert.ToString(dr["StatusVenda"]),
                        //Total = Convert.ToString(dr["Total"])
                    });
            }
            return Vendalist;
        }

        public List<Venda> Carrinho(string id)
        {

            List<Venda> Vendalist = new List<Venda>();

            MySqlCommand cmd = new MySqlCommand("select * from viewVenda where CodVenda = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Vendalist.Add(
                    new Venda
                    {
                        CodVenda = Convert.ToString(dr["CodVenda"]),
                        CodProd = Convert.ToString(dr["CodProd"]),
                        DataVenda = Convert.ToString(dr["DataVenda"]),
                        FormaPagamento = Convert.ToString(dr["FormaPagamento"]),
                        StatusVenda = Convert.ToString(dr["StatusVenda"]),
                        NomeFunc = Convert.ToString(dr["NomeFunc"]),
                        NomeCli = Convert.ToString(dr["NomeCli"]),
                        NomeProd = Convert.ToString(dr["NomeProd"]),
                        ValorUnitario = Convert.ToString(dr["ValorUnitario"]),
                        Quantidade = Convert.ToString(dr["Quantidade"]),
                        Subtotal = Convert.ToString(dr["Subtotal"])
                    });
            }
            return Vendalist;
        }

        public string Total(string id)
        {
            MySqlCommand cmd = new MySqlCommand("select sum(Subtotal) from viewVenda where CodVenda = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);
            string total = cmd.ExecuteScalar().ToString();
            con.MyDesconectarBD();

            return total;
        }

        public void EditStatus(Venda venda)
        {
            MySqlCommand cmd = new MySqlCommand("update tbVenda set CodStatus = @CodStatus where CodVenda = @CodVenda", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@CodVenda", venda.CodVenda);
            cmd.Parameters.AddWithValue("@CodStatus", venda.CodStatus);
            
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}