using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helpers
{
    //  Call web api using sync method
    public static class ShariatiWebApi
    {
        //public static string GetStock()
        //{
        //    using(var client = new WebClient())
        //    {
        //        client.Headers.Add("content-type", "application/json");
        //        string response = client.DownloadString("http://localhost:32283/api/Stock/4");

        //        return response;
        //    }
        //}

        public static string GetAvailableArticles()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                //string url = "http://api.aliatyabi.ir/api/ShariatiGetAvailableArticles";
                string url = "http://localhost:32283/api/ShariatiGetAvailableArticles";
                string response = client.DownloadString(url);

                return response;
            }
        }

        public static string GetStock(string article, string size)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                string url = "http://api.aliatyabi.ir/api/ShariatiStock?article=" + article + "&size=" + size;
                string response = client.DownloadString(url);

                return response;
            }
        }

        public static string GetSizesStock(string article)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                string url = "http://api.aliatyabi.ir/api/ShariatiSizesStock?article=" + article;
                string response = client.DownloadString(url);

                return response;
            }
        }


        public static void CreateSaleFactor(int factorPrice)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                string url = "http://api.aliatyabi.ir/api/ShariatiFactor?factorPrice=" + factorPrice;
                string data = "";
                //string response = client.DownloadString(url);
                client.UploadString(url, "POST", data);

            }
        }

        public static void CreateSaleFactorDetails(string article, string size, int qty, int mfee, int fee, int factorPrice)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                string url = "http://api.aliatyabi.ir/api/ShariatiFactorDetails?article=" + article + "&size=" + size + "&qty=" + qty + "&mfee=" + mfee + "&fee=" + fee + "&factorPrice=" + factorPrice;
                string data = "";
                //string response = client.DownloadString(url);
                client.UploadString(url, "POST", data);

            }
        }
    }
}
