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
    class TestWebApiPodetail
    {
        [Test]
        public void TestGetPoDetail()
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
                    Assert.AreNotEqual(null, pod, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }

        }

        [Test]
        public void TestGetDetailById()
        {
            PODETAILViewModel pod = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());
                    //HTTP GET
                    var responseTask = client.GetAsync("Podetails/434?itemId=45");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PODETAILViewModel>();
                        readTask.Wait();

                        pod = readTask.Result;
                    }
                    Assert.AreNotEqual(null, pod, "Test Failure");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }




        [Test]
        public void TestPutDetail()
        {
            PODETAILViewModel inputView = JsonConvert.DeserializeObject<PODETAILViewModel>(@"{'PONO':'434 ','ITCODE':'45  ','QTY':67,'ITEM':null,'POMASTER':null}");
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());


                    var putTask = client.PutAsJsonAsync<PODETAILViewModel>("Podetails/" + inputView.PONO.Trim() + "?itemId=" + inputView.ITCODE.Trim(), inputView);
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
        public void TestPostPoDetail()
        {
            try
            {
                PODETAILViewModel inputView = JsonConvert.DeserializeObject<PODETAILViewModel>(@"{'PONO':'36  ','ITCODE':'45  ','QTY':56,'ITEM':null,'POMASTER':null}");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<PODETAILViewModel>("Podetails", inputView);
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
        public void TestDeletePoDetail()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Url"].ToString());

                    //HTTP POST
                    var deleteTask = client.DeleteAsync("Podetails/36?itemId=45");
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

