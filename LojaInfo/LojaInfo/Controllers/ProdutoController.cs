using LojaInfo.Models;
using LojaInfo.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LojaInfo.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        AcoesProduto ac = new AcoesProduto();
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.ReadProduto());
            }
        }

        public ActionResult BuscaProduto()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                GridView dgv = new GridView(); // Instância para a tabela
                dgv.DataSource = ac.PesquisarProduto(); //Atribuir ao grid o resultado da consulta
                dgv.DataBind(); //Confirmação do Grid
                StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
                HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
                dgv.RenderControl(htw); //Comando para construção do Grid na tela
                ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
                return View();
            }
        }

        [HttpPost]
        public ActionResult BuscaProduto(Produto prod)
        {
            GridView dgv = new GridView();
            dgv.DataSource = ac.PesquisarProduto(prod);
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }

        public ActionResult Create()
        {
            if (Session["usuarioLogado"] == null || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("acessoInvalido", "Login");
            }
            else
            {
                if (Session["tipoLogado1"] == null)
                {
                    return RedirectToAction("acessoInvalido", "Login");
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Create(Produto at)
        {
            try
            {
                ac.CreateProduto(at);
                ViewBag.msg = "Produto cadastrado";
                return View();
            }
            catch
            {
                ViewBag.msg = "Não foi possivel cadastrar";
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
                return RedirectToAction("Login", "Login");

            else
            {
                return View(ac.BuscarProduto().Find(smodel => smodel.CodProd == id));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Produto smodel)
        {
            try
            {
                ac.EditProduto(smodel);
                ViewBag.msg = "Dados alterados com sucesso";
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (ac.DeleteProduto(id))
                {
                    ViewBag.AlertMsg = "Produto excluído com sucesso";
                }
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }
    }
}