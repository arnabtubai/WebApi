using AssignWebApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
namespace WebApiNunits_Test
{
    [TestFixture]
    class TestWebApiSupplier
    {
        [Test]
        public void TestGetSupplier()
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
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        sup = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SUPPLIERViewModel>>(readTask.Result.ToString());
                    }

                }
                Assert.AreNotEqual(null, sup, "Test Failure");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void TestGetSupplierById()
        {
            SUPPLIERViewModel sup = null;
            string id = "SP12";
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
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        sup = Newtonsoft.Json.JsonConvert.DeserializeObject<SUPPLIERViewModel>(readTask.Result.ToString());
                    }

                }
                Assert.AreNotEqual(null, sup, "Test Failure" + sup.SUPLNAME);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }




        [Test]
        public void TestPutSupplier()
        {
            SUPPLIERViewModel sup = null;
            try
            {
                SUPPLIERViewModel inputView = JsonConvert.DeserializeObject<SUPPLIERViewModel>(@"{'SUPLNO':'3   ','SUPLNAME':'Robert','SUPLADDR':'address23','POMASTERs':[]}");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<SUPPLIERViewModel>("Supplier/" + inputView.SUPLNO.Trim(), inputView);
                    putTask.Wait();

                    var result = putTask.Result;
                    Assert.AreNotEqual(false, result.IsSuccessStatusCode, "Test Failure");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void TestPostSupplier()
        {
            SUPPLIERViewModel sup = null;
            string id = "3";
            try
            {
                SUPPLIERViewModel inputView = JsonConvert.DeserializeObject<SUPPLIERViewModel>(@"{'SUPLNO':'SP22','SUPLNAME':'Arnab','SUPLADDR':'New  York','POMASTERs':[]}");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<SUPPLIERViewModel>("Supplier", inputView);
                    postTask.Wait();

                    var result = postTask.Result;

                    Assert.AreNotEqual(false, result.IsSuccessStatusCode, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        [Test]
        public void TestDeleteSupplier()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Supplier/" + "SP22");
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    Assert.AreNotEqual(false, result.IsSuccessStatusCode, "Test Failure" + result.ToString());
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
   
            }

        }
    }
}

