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
    class TestWebApiITEM
    {
        [Test]
        public void TestGetItem()
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
                    Assert.AreNotEqual(null, item, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.InnerException.Message);
                Assert.AreNotEqual(null, item);
            }

        }

        [Test]
        public void TestGetItemById()
        {
            ITEMViewModel item = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Items/57");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ITEMViewModel>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                    Assert.AreNotEqual(null, item, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }




        [Test]
        public void TestPutItem()
        {
            ITEMViewModel inputView = JsonConvert.DeserializeObject<ITEMViewModel>(@"{'ITCODE':'45  ','ITDESC':'TRItems','ITRATE':46.00,'PODETAILs':[]}");
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ITEMViewModel>("Items/" + inputView.ITCODE.Trim(), inputView);
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
        public void TestPostItem()
        {
            try
            {
                ITEMViewModel inputView = JsonConvert.DeserializeObject<ITEMViewModel>(@"{'ITCODE':'IT23','ITDESC':'TestItem','ITRATE':33.0,'PODETAILs':[]}");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ITEMViewModel>("Items", inputView);
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
        public void TestDeleteItem()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Items/IT23");
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

