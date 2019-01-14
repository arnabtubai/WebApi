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
    public class PODETAILViewController : Controller
    {
        // GET: PODETAILView
        public ActionResult Index()
        {
            List<PODETAILViewModel> pod = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Podetails");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<PODETAILViewModel>>();
                        readTask.Wait();

                        pod = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }

            return View(pod);
        }

        // GET: PODETAILView/Details/5
        public ActionResult Details(string id, string itemId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PODETAILViewModel pod = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Podetails/" + id + "?itemId=" + itemId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PODETAILViewModel>();
                        readTask.Wait();

                        pod = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }


            if (pod == null)
            {
                return HttpNotFound();
            }
            return View(pod);
        }


        private List<ITEMViewModel> GetItems()
        {
            List<ITEMViewModel> item = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Items");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<ITEMViewModel>>();
                        readTask.Wait();

                        item = readTask.Result;

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
            return item;
        }

        private List<POMASTERViewModel> GetPomaster()
        {
            List<POMASTERViewModel> pom = null;
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
            return pom;
        }

        // GET: PODETAILView/Create
        public ActionResult Create()
        {
            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC");
            ViewBag.PONO = new SelectList(GetPomaster(), "PONO", "SUPLNO");
            return View();
        }

        // POST: PODETAILView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PONO,ITCODE,QTY")] PODETAILViewModel pODETAILViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<PODETAILViewModel>("Podetails", pODETAILViewModel);
                        postTask.Wait();

                        var result = postTask.Result;
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



            return View(pODETAILViewModel);
        }

        // GET: PODETAILView/Edit/5
        public ActionResult Edit(string id, string itemId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PODETAILViewModel pod = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Podetails/" + id.Trim() + "?itemId=" + itemId.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PODETAILViewModel>();
                        readTask.Wait();

                        pod = readTask.Result;
                        ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC", pod.ITCODE);
                        ViewBag.PONO = new SelectList(GetPomaster(), "PONO", "SUPLNO", pod.PONO);
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
            if (pod == null)
            {
                return HttpNotFound();
            }

            return View(pod);
        }

        // POST: PODETAILView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PONO,ITCODE,QTY")] PODETAILViewModel pODETAILViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());


                        var putTask = client.PutAsJsonAsync<PODETAILViewModel>("Podetails/" + pODETAILViewModel.PONO.Trim() + "?itemId=" + pODETAILViewModel.ITCODE.Trim(), pODETAILViewModel);
                        putTask.Wait();
                        string str = Newtonsoft.Json.JsonConvert.SerializeObject(pODETAILViewModel);
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
            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC", pODETAILViewModel.ITCODE);
            ViewBag.PONO = new SelectList(GetPomaster(), "PONO", "SUPLNO", pODETAILViewModel.PONO);
            return View(pODETAILViewModel);
        }

        // GET: PODETAILView/Delete/5
        public ActionResult Delete(string id, string itemId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PODETAILViewModel pod = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Podetails/" + id.Trim() + "?itemId=" + itemId.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PODETAILViewModel>();
                        readTask.Wait();

                        pod = readTask.Result;
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
            if (pod == null)
            {
                return HttpNotFound();
            }
            return View(pod);
        }

        // POST: PODETAILView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string itemId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Podetails/" + id.Trim() + "?itemId=" + itemId.Trim());
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

