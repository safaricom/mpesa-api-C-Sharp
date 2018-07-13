using System;
using System.Text;
using System.IO;
using System.Net;

namespace SafC2Bsimulation
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Title = "C2B Simulation";
            String a = "https://sandbox.safaricom.co.ke/mpesa/c2b/v1/simulate";

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
                string json = "{\"ShortCode\":\" \"," +
                              "\"CommandID\":\"CustomerPayBillOnline\"," +
                              "\"Amount\":\" \"," +
                              "\"Msisdn\":\" \"," +
                              "\"BillRefNumber\":\" \"}";

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