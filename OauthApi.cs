using System;
using System.Text;
using System.IO;
using System.Net;

namespace Safoauth
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Title = "OAUTH";
            String a = "https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";
            String baseUrl = a;

            String app_key = "YOUR_APP_CONSUMER_KEY";
            String app_secret = "YOUR_APP_CONSUMER_SECRET";

            byte[] auth = Encoding.UTF8.GetBytes(app_key + ":" + app_secret);
            String encoded = System.Convert.ToBase64String(auth);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";
            request.Headers.Add("cache-control", "no-cache");
            request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Console.WriteLine(readStream.ReadToEnd());
                response.Close();
                readStream.Close();
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(resp);

            }
        }
    }
}