using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignWebApi.Models;
using System.Net.Http;
using System.Configuration;

namespace AssignWebApi.Controllers
{
    public class SUPPLIERViewController : Controller
    {
        // GET: SUPPLIERView
        public ActionResult Index()
        {
            List<SUPPLIERViewModel> sup = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Supplier");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<SUPPLIERViewModel>>();
                        readTask.Wait();

                        sup = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }

            return View(sup);
        }

        // GET: SUPPLIERView/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SUPPLIERViewModel sup = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Supplier/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SUPPLIERViewModel>();
                        readTask.Wait();

                        sup = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }


            if (sup == null)
            {
                return HttpNotFound();
            }
            return View(sup);
        }

        // GET: SUPPLIERView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPPLIERView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SUPLNO,SUPLNAME,SUPLADDR")] SUPPLIERViewModel sUPPLIER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<SUPPLIERViewModel>("Supplier", sUPPLIER);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }

            return View(sUPPLIER);
        }

        // GET: SUPPLIERView/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIERViewModel sup = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Supplier/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SUPPLIERViewModel>();
                        readTask.Wait();

                        sup = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (sup == null)
            {
                return HttpNotFound();
            }
            return View(sup);
        }

        // POST: SUPPLIERView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SUPLNO,SUPLNAME,SUPLADDR")] SUPPLIERViewModel sUPPLIER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<SUPPLIERViewModel>("Supplier/" + sUPPLIER.SUPLNO.Trim(), sUPPLIER);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            return View(sUPPLIER);
        }

        // GET: SUPPLIERView/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIERViewModel sup = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Supplier/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SUPPLIERViewModel>();
                        readTask.Wait();

                        sup = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (sup == null)
            {
                return HttpNotFound();
            }
            return View(sup);
        }

        // POST: SUPPLIERView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Supplier/" + id.Trim());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            return RedirectToAction("Index");
        }


    }
}





