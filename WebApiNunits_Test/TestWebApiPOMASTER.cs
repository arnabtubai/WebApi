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
namespace WebApiNunit_Test
{
    [TestFixture]
    class TestWebApiPOMASTER
    {
        [Test]
        public void TestGetPoMaster()
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
                    Assert.AreNotEqual(null, pOMASTERs, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }

        }

        [Test]
        public void TestGetPoMasterById()
        {
            POMASTERViewModel pom = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Pomasters/36");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<POMASTERViewModel>();
                        readTask.Wait();

                        pom = readTask.Result;
                    }
                    Assert.AreNotEqual(null, pom, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }




        [Test]
        public void TestPutPoMaster()
        {
            POMASTERViewModel inputView = JsonConvert.DeserializeObject<POMASTERViewModel>(@"{'PONO':'36','PODATE':'2018-11-27T00:00:00','SUPLNO':'SP12','PODETAILs':[],'SUPPLIER':null}");
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<POMASTERViewModel>("Pomasters/" + inputView.PONO.Trim(), inputView);
                    putTask.Wait();
                    string str = Newtonsoft.Json.JsonConvert.SerializeObject(inputView);
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
        public void TestPostPoMaster()
        {
            try
            {
                POMASTERViewModel inputView = JsonConvert.DeserializeObject<POMASTERViewModel>(@"{'PONO':'P34','PODATE':'2018-12-12T00:00:00','SUPLNO':'SP12','PODETAILs':[],'SUPPLIER':null}");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<POMASTERViewModel>("Pomasters", inputView);
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
        public void TestDeletePoMaster()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Pomasters/P34");
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    Assert.AreNotEqual(false, result.IsSuccessStatusCode, "Test Failure");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}

