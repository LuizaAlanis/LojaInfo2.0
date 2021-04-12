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
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        AcoesFuncionario ac = new AcoesFuncionario();
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.ReadFuncionario());
            }
        }

        public ActionResult BuscaFuncionario()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                GridView dgv = new GridView(); // Instância para a tabela
                dgv.DataSource = ac.PesquisarFuncionario(); //Atribuir ao grid o resultado da consulta
                dgv.DataBind(); //Confirmação do Grid
                StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
                HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
                dgv.RenderControl(htw); //Comando para construção do Grid na tela
                ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
                return View();
            }
        }

        [HttpPost]
        public ActionResult BuscaFuncionario(Funcionario func)
        {
            GridView dgv = new GridView();
            dgv.DataSource = ac.PesquisarFuncionario(func);
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }

        public ActionResult Create()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Create(Funcionario at)
        {
            try
            {
                ac.CreateFuncionario(at);
                ViewBag.msg = "Funcionario cadastrado";
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
                return View(ac.BuscarFuncionario().Find(smodel => smodel.CodFunc == id));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Funcionario smodel)
        {
            try
            {
                ac.EditFuncionario(smodel);
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
                if (ac.DeleteFuncionario(id))
                {
                    ViewBag.msg = "Funcionario excluído com sucesso";
                }
                return RedirectToAction("index");
            }
            catch
            {
                return RedirectToAction("index");
            }
        }
    }
}