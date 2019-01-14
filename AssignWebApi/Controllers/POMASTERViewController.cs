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
    public class POMASTERViewController : Controller
    {
        // GET: POMASTERView
        public ActionResult Index()
        {
            List<POMASTERViewModel> pOMASTERs = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Pomasters");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<POMASTERViewModel>>();
                        readTask.Wait();

                        pOMASTERs = readTask.Result;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            return View(pOMASTERs.ToList());
        }

        // GET: POMASTERView/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POMASTERViewModel pom = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Pomasters/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<POMASTERViewModel>();
                        readTask.Wait();

                        pom = readTask.Result;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (pom == null)
            {
                return HttpNotFound();
            }
            return View(pom);
        }

        private List<SUPPLIERViewModel> GetSuppliers()
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
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            return sup;
        }
        // GET: POMASTERView/Create
        public ActionResult Create()
        {

            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME");
            return View();
        }

        // POST: POMASTERView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PONO,PODATE,SUPLNO")] POMASTERViewModel pOMASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<POMASTERViewModel>("Pomasters", pOMASTER);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            // ViewBag.SUPLNO = new SelectList(db.SUPPLIERs, "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }

            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME");

            return View(pOMASTER);
        }

        // GET: POMASTERView/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POMASTERViewModel pom = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Pomasters/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<POMASTERViewModel>();
                        readTask.Wait();

                        pom = readTask.Result;
                        ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME", pom.SUPLNO);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (pom == null)
            {
                return HttpNotFound();
            }
            return View(pom);
            //  ViewBag.SUPLNO = new SelectList(db.SUPPLIERs, "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
            // return View(pOMASTER);
        }

        // POST: POMASTERView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PONO,PODATE,SUPLNO")] POMASTERViewModel pOMASTER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<POMASTERViewModel>("Pomasters/" + pOMASTER.PONO.Trim(), pOMASTER);
                        putTask.Wait();
                        string str = Newtonsoft.Json.JsonConvert.SerializeObject(pOMASTER);
                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
            return View(pOMASTER);
        }

        // GET: POMASTERView/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POMASTERViewModel pom = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("PoMasters/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<POMASTERViewModel>();
                        readTask.Wait();

                        pom = readTask.Result;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (pom == null)
            {
                return HttpNotFound();
            }
            return View(pom);
        }

        // POST: POMASTERView/Delete/5
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
                    var deleteTask = client.DeleteAsync("Pomasters/" + id.Trim());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error:" + result.StatusCode.ToString();
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