using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BUDCC.DropcamClient
{
    public static class WebRequestHelper
    {
        public static string UserAgent { get; set; }

        static WebRequestHelper()
        {
            //Once upon a time, 'libcurl' needed to be in the query string... no longer it seems.

            //TODO: Improve this support
            //string osVersionNumber = "8.1";
            //UserAgent = string.Format("BUDCC/{0}.{1} (compatible; Windows {2}; X64; libcurl?)", Windows.ApplicationModel.Package.Current.Id.Version.Major, Windows.ApplicationModel.Package.Current.Id.Version.Minor, osVersionNumber);
            //UserAgent = string.Format("BUDCC/{0}.{1} (compatible; Windows {2}; X64; libcurl?)", Environment.Version.Major, Environment.Version.Minor, osVersionNumber);
            UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko";
        }

        static CookieContainer _cookies;
        static public CookieContainer Cookies
        {
            get
            {
                if (_cookies == null)
                {
                    _cookies = new CookieContainer();
                }
                return _cookies;
            }
        }

        public static void ClearCookies()
        {
            _cookies = null;
        }

        //TODO: Consider storing login cookie to app settings, then using it later, if get a 302, redirect then capture new
        public static HttpClient GetClient()
        {
            var handler = new HttpClientHandler() {
                CookieContainer = Cookies,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", WebRequestHelper.UserAgent);
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Referer", "https://home.nest.com/");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");

            //WebProxy myproxy = new WebProxy("127.0.0.1", 8888);
            //HttpWebRequest.DefaultWebProxy = myproxy;

            return client;
        }
    }
}
