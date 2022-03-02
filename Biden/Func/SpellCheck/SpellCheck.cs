using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Biden.Func.SpellCheck
{
    class SpellCheck
    {
        private static SpellCheck instance = null;

        static string host = "https://api.cognitive.microsoft.com";

        static string path = "/bing/v7.0/spellcheck?";
        static string key = "<ENTER-KEY-HERE>";
        //text to be spell-checked
        static string text = "Hollo, wrld!";

        
        static string params_ = "mkt=en-US&mode=proof";


        private SpellCheck()
        {

        }

        public static SpellCheck getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpellCheck();
                }
                return instance;
            }
        }

        private async static void SpellCheckStart()
        {
            //var task1 = Task.Run(() => get());
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);


            HttpResponseMessage response = null;
            // add the rest of the code snippets here (except for main())...

            var values = new Dictionary<string, string>();
            values.Add("text", text);
            var content = new FormUrlEncodedContent(values);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            string uri = host + path + params_;
            response = await client.PostAsync(uri, new FormUrlEncodedContent(values));

            string client_id;
            if (response.Headers.TryGetValues("X-MSEdge-ClientID", out IEnumerable<string> header_values))
            {
                client_id = header_values.First();
                //Console.WriteLine("Client ID: " + client_id);
                MessageBox.Show("Client ID: " + client_id);
            }

        }


    }
}
