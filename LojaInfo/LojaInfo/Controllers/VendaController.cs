using LojaInfo.Models;
using LojaInfo.Repositorio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaInfo.Controllers
{
    public class VendaController : Controller
    {
        // GET: Venda

        public void Funcionario()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = dbInfo ; User = userLojaInfo ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where StatusFunc = 'Ativo' order by NomeFunc", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.funcionario = new SelectList(ag, "Value", "Text");
        }

        public void Cliente()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = dbInfo ; User = userLojaInfo ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where StatusCli = 'Ativo' order by NomeCli", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.cliente = new SelectList(ag, "Value", "Text");
        }

        public void Status()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = dbInfo ; User = userLojaInfo ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbStatusVenda", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.status = new SelectList(ag, "Value", "Text");
        }

        public void Produto()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = dbInfo ; User = userLojaInfo ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProduto where StatusProd = 'Ativo' order by NomeProd", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.produto = new SelectList(ag, "Value", "Text");
        }

        public void FormaPagamento()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = dbInfo ; User = userLojaInfo ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbFormaPagamento", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.forma = new SelectList(ag, "Value", "Text");
        }





        AcoesVenda ac = new AcoesVenda();
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.ReadVenda());
            }
        }

        public ActionResult Create()
        {
            if (Session["usuarioLogado"] == null || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("acessoInvalido", "Login");
            }
            else
            {
                Funcionario();
                Cliente();
                Status();
                FormaPagamento();
                return View();     
            }
        }

        [HttpPost]
        public ActionResult Create(Venda at)
        {
            try
            {
                Funcionario();
                Cliente();
                Status();
                FormaPagamento();
                at.CodFunc = Request["funcionario"];
                at.CodCli = Request["cliente"];
                at.CodStatus = Request["status"];
                at.CodForma = Request["forma"];
                ac.CreateVenda(at);
                ViewBag.msg = "Venda iniciada, volte para a lista e adicione produtos no carrinho";
                return RedirectToAction("CreateItens");
            }
            catch
            {
                ViewBag.msg = "Não foi possivel cadastrar";
                return View();
            }
        }


        public ActionResult CreateItens()
        {
            if (Session["usuarioLogado"] == null || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Produto();
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateItens(Venda at)
        {
            try
            {
                Produto();
                at.CodProd = Request["produto"];
                ac.CreateItens(at);
                ViewBag.msg = "Produto(s) adicionado(s) com sucesso";
                return View();
            }
            catch
            {
                ViewBag.msg = "Não foi possivel cadastrar";
                return View();
            }
        }


        public ActionResult Carrinho(string id)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
                return RedirectToAction("Login", "Login");

            else
            {
                ViewBag.total = ac.Total(id).ToString();
                return View(ac.Carrinho(id));
            }
        }

        public ActionResult Arquivar(string id)
        {
            try
            {
                ac.ArquivarVenda(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult VendasArquivadas()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.ReadVendaArquivada());
            }
        }

        public ActionResult Recuperar(string id)
        {
            try
            {
                ac.RecuperarVenda(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                ac.DeleteVenda(id);
                return RedirectToAction("VendasArquivadas");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteItem(string id, string id1)
        {
            try
            {
                ac.DeleteItem(id, id1);
                return RedirectToAction("Carrinho", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditStatus(string id)
        {
            Status();
            ViewBag.cod = id;
            return View();
        }

        [HttpPost]
        public ActionResult EditStatus(string id, Venda at)
        {
            try
            {
                Status();
                at.CodStatus = Request["status"];
                at.CodVenda = id;
                ac.EditStatus(at);
                return RedirectToAction("Carrinho", new { id = at.CodVenda });
            }
            catch
            {
                return RedirectToAction("Carrinho", new { id = at.CodVenda });
            }
        }
    }
}