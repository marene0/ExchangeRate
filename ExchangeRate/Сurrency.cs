//using Exchangeclient;
//using System;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;

//namespace Exchangeclient
//{
//    public class CurrencyResponse
//    {
//        public string Date { get; set; }
//        public Currency Usd { get; set; }
//        public Currency Eur { get; set; }
//    }

//    public class Currency
//    {
//        public double uah { get; set; }
//        public double eur { get; set; }
//    }

//    class Program
//    {
//        static HttpClient client = new HttpClient();

//        static async Task<CurrencyResponse> GetCurrencyAsync(string path)
//        {
//            CurrencyResponse currency = null;
//            HttpResponseMessage response = await client.GetAsync(path);
//            if (response.IsSuccessStatusCode)
//            {
//                currency = await response.Content.ReadAsAsync<CurrencyResponse>();
//            }
//            return currency;
//        }

//        static async Task Main()
//        {
//            await RunAsync();
//        }

//        static async Task RunAsync()
//        {
//            client.BaseAddress = new Uri("https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/");
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            Stopwatch stopwatch = new Stopwatch();

//            stopwatch.Start();
//            CurrencyResponse usdCurrency = await GetCurrencyAsync("currencies/usd.json");





//            CurrencyResponse eurCurrency = await GetCurrencyAsync("currencies/eur.json");
//            stopwatch.Stop();
//            Console.WriteLine($"Time for 2 await: {stopwatch.ElapsedMilliseconds} ms");

//            Console.ReadLine();
//        }
//    }
//}







using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Exchangeclient
{
    public class CurrencyResponse
    {
        public string Date { get; set; }
        public Currency Usd { get; set; }
        public Currency Eur { get; set; }
    }

    public class Currency
    {
        public double uah { get; set; }
        public double eur { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<CurrencyResponse> GetCurrencyAsync(string path)
        {
            CurrencyResponse currency = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(path + $" {response.IsSuccessStatusCode}");
                currency = await response.Content.ReadAsAsync<CurrencyResponse>();
            }
            return currency;
        }

        static async Task Main()
        {
            await RunAsync();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            await GetCurrencyAsync("currencies/usd.json");

            await GetCurrencyAsync("currencies/eur.json");
           

            stopwatch.Stop();
            Console.WriteLine($"Time for sync tasks: {stopwatch.ElapsedMilliseconds} ms");

            await Task.Delay(5000);
            stopwatch.Restart();
            Task<CurrencyResponse> usdTask =  GetCurrencyAsync("currencies/uah.json");

            Task<CurrencyResponse> eurTask = GetCurrencyAsync("currencies/ach.json");

            await Task.WhenAll(usdTask, eurTask);

            CurrencyResponse usdCurrency = usdTask.Result;
            CurrencyResponse eurCurrency = eurTask.Result;
            stopwatch.Stop();
            Console.WriteLine($"Time for Paralles tasks: {stopwatch.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }
    }
}


