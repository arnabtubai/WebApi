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
    public class ITEMViewController : Controller
    {
        // GET: ITEMView
        public ActionResult Index()
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
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }

            return View(item);
        }

        // GET: ITEMView/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ITEMViewModel item = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Items/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ITEMViewModel>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }


            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: ITEMView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ITEMView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ITCODE,ITDESC,ITRATE")] ITEMViewModel iTEM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ITEMViewModel>("Items", iTEM);
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

            return View(iTEM);
        }

        // GET: ITEMView/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ITEMViewModel item = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Items/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ITEMViewModel>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: ITEMView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ITCODE,ITDESC,ITRATE")] ITEMViewModel iTEM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<ITEMViewModel>("Items/" + iTEM.ITCODE.Trim(), iTEM);
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
            return View(iTEM);
        }

        // GET: ITEMView/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ITEMViewModel item = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Items/" + id.Trim());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ITEMViewModel>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error:" + ex.Message;
            }
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: ITEMView/Delete/5
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
                    var deleteTask = client.DeleteAsync("Items/" + id.Trim());
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

