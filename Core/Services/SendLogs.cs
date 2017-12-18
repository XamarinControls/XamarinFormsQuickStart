using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SendLogs : ISendLogs
    {
        //HttpResponseMessage response;

        public async Task<bool> Send(Errors errors)
        {
            //return await Task.Run(() => setPart(name));
            var client = new HttpClient();
            //string sContentType = "application/json"; // or application/xml
            //string sContentType = "text/plain";
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // test with:  http://lew0038pc.smpcorp.com/api/PTOItems/72794313681
            client.BaseAddress = new Uri($"http://lewwebtest02.smpcorp.com/");

            //var postData = new List<KeyValuePair<string, string>>();

            //var jsonList = JsonConvert.SerializeObject(errors);

            //postData.Add(new KeyValuePair<string, string>("errors", errors));

            //var nowJson = JsonConvert.SerializeObject(errors);

            //var content = new System.Net.Http.StringContent(nowJson, Encoding.UTF8, sContentType);


            //var response = await client.PostAsJsonAsync("api/Logs", errors);
            var response = await client.PostAsync("ptoscannerapi/v1.0/Logs", new StringContent(
                JsonConvert.SerializeObject(errors), Encoding.UTF8, "application/json"));

            //var JsonResult = response.Content.ReadAsStringAsync().Result;

            //if (typeof(T) == typeof(string))
            //	return null;

            //var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);

            //return rootobject;

            //try
            //{
            //	response = await client.PostAsync("Logs", content);
            //}

            //catch (HttpRequestException ex)
            //{
            //	//LogMe.Log(ex.Message);
            //	MessagingCenter.Send<IDataService, string>(this, "mTransmissionError", ex.Message);
            //}
            //catch (Newtonsoft.Json.JsonSerializationException)
            //{
            //	return null;
            //}
            if (response != null)
            {
                //var partsJson = response.Content.ReadAsStringAsync().Result;

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
