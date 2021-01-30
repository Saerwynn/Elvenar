using Elvenar.Misc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Elvenar.WebRequests
{
    class HttpRequest
    {
        public string[] getXsrfPhp(UserTokens x)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Uri requestUri = new Uri("https://" + x.language + ".elvenar.com/");

            CookieContainer cookieContainer = new CookieContainer();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.Host = x.language + ".elvenar.com";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;

            ServicePointManager.Expect100Continue = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            CookieCollection cookies = cookieContainer.GetCookies(requestUri);

            string[] tokens = { cookies["XSRF-TOKEN"].ToString().Substring(11), cookies["PHPSESSID"].ToString().Substring(10) };

            //Console.WriteLine("|| XSRF-TOKEN: {0}", tokens[0]);
            //Console.WriteLine("|| PHPSESSID: {0}\n", tokens[1]);
            return tokens;
        }

        public string getGlps(UserTokens x)
        {
            Uri requestUri = new Uri("https://" + x.language + ".elvenar.com/glps/login_check");

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("PHPSESSID", x.phpsessid, "/"));
            cookieContainer.Add(requestUri, new Cookie("XSRF-TOKEN", x.xsrf, "/"));
            cookieContainer.Add(requestUri, new Cookie("device_view", "full", "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_tid", x.tid, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_data", "portal_tid=" + x.tid, "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "POST";
            request.Host = x.language + ".elvenar.com";
            request.Accept = "application/json, text/plain, */*";
            request.Headers.Add("Origin", "https://" + x.language + ".elvenar.com");
            request.Headers.Add("X-XSRF-TOKEN", x.xsrf);
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.language + ".elvenar.com/";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;

            ServicePointManager.Expect100Continue = false;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());

            try
            {
                requestWriter.Write("login%5Bremember_me%5D=true&login%5Buserid%5D=" + x.username + "&login%5Bpassword%5D=" + x.password);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            CookieCollection cookies = cookieContainer.GetCookies(requestUri);
 
            string glps_remember_me = cookies["glps_remember_me"].ToString().Substring(17);

            //Console.WriteLine("|| glps_remember_me: {0}\n", glps_remember_me);
            return glps_remember_me;
        }

        public string getRedirect(UserTokens x)
        {
            Uri requestUri = new Uri("https://" + x.language + ".elvenar.com/");

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("XSRF-TOKEN", x.xsrf, "/"));
            cookieContainer.Add(requestUri, new Cookie("PHPSESSID", x.phpsessid, "/"));
            cookieContainer.Add(requestUri, new Cookie("glps_remember_me", x.glps, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_tid", x.tid, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_data", "portal_tid=" + x.tid, "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.Host = x.language + ".elvenar.com";
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.language + ".elvenar.com/";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;

            ServicePointManager.Expect100Continue = false;

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                if (response.StatusCode != HttpStatusCode.Redirect) throw (e);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            string location = response.GetResponseHeader("Location");

            //Console.WriteLine("|| Location: {0}\n", location);
            return location;
        }

        public string getMid(UserTokens x)
        {
            Uri requestUri = new Uri(x.redirect);

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("XSRF-TOKEN", x.xsrf, "/"));
            cookieContainer.Add(requestUri, new Cookie("PHPSESSID", x.phpsessid, "/"));
            cookieContainer.Add(requestUri, new Cookie("glps_remember_me", x.glps, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_tid", x.tid, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_data", "portal_tid=" + x.tid, "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.Host = x.language + ".elvenar.com";
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.language + ".elvenar.com/";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;

            ServicePointManager.Expect100Continue = false;

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                if (response.StatusCode != HttpStatusCode.Redirect) throw (e);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            CookieCollection cookies = cookieContainer.GetCookies(requestUri);

            string _mid = cookies["_mid"].ToString().Substring(5);

            //Console.WriteLine("|| _mid: {0}\n", _mid);
            return _mid;
        }

        public string getGateway(UserTokens x)
        {
            Uri requestUri = new Uri("https://" + x.world.Substring(0,2) + "0.elvenar.com/web/login/play");

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("device_view", "full", "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_tid", x.tid, "/"));
            cookieContainer.Add(requestUri, new Cookie("portal_data", "portal_tid=" + x.tid + "&portal_ref_url=https://" + x.language + ".elvenar.com/&portal_ref_session=1", "/"));
            cookieContainer.Add(requestUri, new Cookie("_mid", x.mid, "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "POST";
            request.Host = x.world.Substring(0, 2) + "0.elvenar.com";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Referer = "https://" + x.world.Substring(0, 2) + "0.elvenar.com/web/glps";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            ServicePointManager.Expect100Continue = false;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());

            try
            {
                requestWriter.Write("world_id=" + x.world);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            string json = sr.ReadToEnd().ToString();

            var a = JsonConvert.DeserializeObject<UserTokens.GatewayJson>(json);

            //Console.WriteLine("|| redirect: {0}\n", a.redirect);
            return a.redirect;
        }

        public string getSid(UserTokens x)
        {
            Uri requestUri = new Uri(x.gateway);

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("ig_conv_last_site", "https://" + x.world.Substring(0, 2) + "0.elvenar.com/web/glps", "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.Host = x.world + ".elvenar.com";
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.world.Substring(0, 2) + "0.elvenar.com/web/glps";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;

            ServicePointManager.Expect100Continue = false;

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                if (response.StatusCode != HttpStatusCode.Redirect) throw (e);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            CookieCollection cookies = cookieContainer.GetCookies(requestUri);

            string _sid = cookies["sid"].ToString().Substring(4);

            //Console.WriteLine("|| _sid: {0}\n", _sid);
            return _sid;
        }

        public string getGameGateway(UserTokens x)
        {
            Uri requestUri = new Uri("https://" + x.world + ".elvenar.com/game");

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("ig_conv_last_site", "https://"+ x.world.Substring(0, 2) + "0.elvenar.com/web/glps", "/"));
            cookieContainer.Add(requestUri, new Cookie("sid", x.sid, "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.Host = x.world + ".elvenar.com";
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.world.Substring(0, 2) + "0.elvenar.com/web/glps";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;

            ServicePointManager.Expect100Continue = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            string s = sr.ReadToEnd().ToString();
            int pos = s.IndexOf("json_gateway_url") + "json_gateway_url".Length + 4;
            string json_gateway_url64 = s.Substring(pos, 56);

            string json_gateway_url = Encryption.Base64Decode(json_gateway_url64).Substring(2);

            //Console.WriteLine("|| json_gateway_url: {0}\n", json_gateway_url);
            return json_gateway_url;
        }

        public string Request(UserTokens x, string query)
        {
            Uri requestUri = new Uri("https://" + x.game_gateway);

            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(requestUri, new Cookie("ig_conv_last_site", "https://" + x.world + ".elvenar.com/game", "/"));
            cookieContainer.Add(requestUri, new Cookie("sid", x.sid, "/"));
            cookieContainer.Add(requestUri, new Cookie("req_page_info", "game_v1", "/"));
            cookieContainer.Add(requestUri, new Cookie("start_page_type", "game", "/"));
            cookieContainer.Add(requestUri, new Cookie("start_page_version", "v1", "/"));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "POST";
            request.Host = x.world + ".elvenar.com";
            request.Headers.Add("Origin", "https://" + x.world + ".elvenar.com");
            request.Headers.Add("Os-Type", "browser");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            request.Headers.Add("DNT", "1");
            request.Referer = "https://" + x.world + ".elvenar.com/game";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
            request.CookieContainer = cookieContainer;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;

            ServicePointManager.Expect100Continue = false;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());

            try
            {
                requestWriter.Write(query);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
            }

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return null;
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            return sr.ReadToEnd().ToString();       
        }
    }
}
