using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Threading.Tasks;


namespace PayBaseWrapper
{
    public class PayBase
    {
        protected string apiKey = "";
        protected string apiURI = "https://paybase.com/api/"; // default is production
        //  protected string apiURI = "https://private-anon-c546e788d-paybase.apiary-mock.com/api/";

        public string Apikey
        {
            get { return apiKey; }
            set { apiKey = value; }
        }

        public string ApiURI
        {
            get { return apiURI; }
            set { apiURI = value; }
        }

        public string getCurrentUser()
        {

            // request take the form of:
            //   URI with method extension
            //   MIME content type 
            //   optional APIKEY: for "private" calls only
            var task = MakeAsyncRequest(apiURI + "user/", "text/html", apiKey);
            return task.Result;
        }

        public string getCurrentUserProfile()
        {

            // request take the form of:
            //   URI with method extension
            //   MIME content type 
            //   optional APIKEY: for "private" calls only
            var task = MakeAsyncRequest(apiURI + "user/profile/", "text/html", apiKey);
            return task.Result;
        }


        public string getBuysAndSells()
        {

            // request take the form of:
            //   URI with method extension
            //   MIME content type 
            //   optional APIKEY: for "private" calls only
            var task = MakeAsyncRequest(apiURI + "buys-and-sells/", "text/html", apiKey);
            return task.Result;
        }


        /*
         * 2015-01-16 | coinophrenic | MakeAsyncRequest added as the default asynchronous web request. 
         * 2015-01-22 | coinophrenic | added request token using RFC6750 : this uses API key as bearer type token
         * 2015-01-30 | coinophrenic | delegate has IsFaulted now for exception handling
         * Private method returns a Task <string> object from delegate
        */
        private static Task<string> MakeAsyncRequest(string url, string contentType, string api = "")
        {

            var request = (HttpWebRequest)WebRequest.Create(url);

            if (api.Length != 0)
                request.Headers["Authorization"] = "BEARER " + api;


            request.ContentType = contentType;
            request.Method = "get";
            request.Proxy = null;

            Task<WebResponse> task = Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, (object)null);

            // t is the continuation delegate for this call
            return task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    {
                        // sanity check
                        if (t.Exception.InnerExceptions.Count > 0)
                            return "{\"Exception\":\"" + t.Exception.InnerException.Message + "\"}";
                        else // ?? exception somewhere not handled by WebResponseException
                            return "{\"Exception\":\"Cannot process web request to API server.\"}";
                    }
                }
                else
                {
                    // ok, good response!
                    return ReadStreamFromResponse(t.Result);
                }
            });
        }


        /*
         * 2015-01-16 | coinophrenic | ReadStreamFromResponse added as utility stream reader. 
         * Private method extracts the response (strContent) from WebResponse object
         * strContent will contain JSON if call is successful.
         * 
         * Note: we are not parsing the JSON here, just processing the raw response
        */
        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
                return sr.ReadToEnd();
        }
    }

}



