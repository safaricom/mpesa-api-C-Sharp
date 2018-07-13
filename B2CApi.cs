using System;
using System.Text;
using System.IO;
using System.Net;

namespace SafB2C
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Title = "B2C";
            String a = "https://sandbox.safaricom.co.ke/mpesa/b2c/v1/paymentrequest";

            string baseUrl = a;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
            String token = "ACCESS_TOKEN";
            request.Headers.Add("authorization", "Bearer " + token);
            request.ContentType = "application/json";
            request.Headers.Add("cache-control", "no-cache");

            request.KeepAlive = false;
            request.Method = "POST";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"InitiatorName\":\" \"," +
                              "\"SecurityCredential\":\" \"," +
                              "\"CommandID\":\" \"," +
                              "\"Amount\":\" \"," +
                              "\"PartyA\":\" \"," +
                              "\"PartyB\":\" \"," +
                              "\"Remarks\":\" \"," +
                              "\"QueueTimeOutURL\":\"http://your_timeout_url\"," +
                              "\"ResultURL\":\"http://your_result_url\"," +
                              "\"Occasion\":\" \"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


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