using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Threading.Tasks;

namespace IssuuDownloader
{
    class HTTPFunctions
    {
      
        public static string AGetReponseFunctionAsync(string url, int timeout, string proxy, CookieContainer cookieC)
        {
            HttpWebRequest myReq = null;
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Opera Mini";
                myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }
        public static string GetGReponseFunctionAsync(string url, int timeout, string proxy)
        {

            HttpWebRequest myReq = null;
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                //myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Opera Mini";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }
        public static string GetGrabberReponseFunctionAsync(string url, int timeout, string proxy)
        {
            HttpWebRequest myReq = null;
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                //myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Opera Mini";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }
        public static string GetReponseFunctionAsync(string url, int timeout, string proxy)
        {
            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
          
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                //myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
            
                myReq.KeepAlive = true;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(), Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
        }
        public static string GetReponseFunctionAsync(string url, int timeout, string proxy, CookieContainer cookieC)
        {
            HttpWebRequest myReq = null;
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.KeepAlive = true;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                    using (StreamReader sr = new StreamReader(myResp.GetResponseStream(), Encoding.UTF8))
                    {
                        Char[] read = new Char[256];
                        StringBuilder sb = new StringBuilder();
                        int count = sr.Read(read, 0, 256);
                        while (count > 0)
                        {
                            String str = new String(read, 0, count);
                            sb.Append(str);
                            count = sr.Read(read, 0, 256);
                        }
                        return sb.ToString();
                    }
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            finally
            {
               myReq.Abort();
              myResp.Close();
            }

        }
        public static string GetReponseFunctionAsync(string url, int timeout, string proxy, CookieContainer cookieC, string referri)
        {
            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.KeepAlive = true;
                myReq.Referer = referri;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }

        }
        public List<string> APostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC)
        {
            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();
                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    List<string> data = new List<string>();
                    data.Add(sb.ToString());
                    data.Add(myResp.ResponseUri.ToString());
                    return data;
                }
               
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }


        public List<string> APostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC, string referral)
        {
            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                myReq.Referer = referral;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    List<string> data = new List<string>();
                    data.Add(sb.ToString());
                    data.Add(myResp.ResponseUri.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }


        public static string PostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                //myReq.Referer = referral;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }
        public static string PostHttpAsync(string platform,string postData, string url, int timeout, string proxy, CookieContainer cookieC)
        {

            HttpWebRequest myReq = null;

            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                //myReq.Referer = referral;
                myReq.Timeout = timeout * 1000;
                if (platform.Contains("EEXP")) myReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(), Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }
        public List<string> MultiPostHeaderAsync(NameValueCollection postData, string url, string proxy, int timeout, string headername, CookieContainer cookieC)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            WebResponse wresp = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    WebProxy handler;
                    if (dproxy.Length > 2)
                    {
                        handler = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else handler = new WebProxy(proxy);
                    wr.Proxy = handler;
                }
                wr.Method = "POST";
                wr.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                wr.Timeout = timeout * 1000;
                wr.KeepAlive = true;
                wr.CookieContainer = cookieC;
                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in postData.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, postData[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                wresp = wr.GetResponse();
                
                
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    List<string> data = new List<string>();
                    data.Add(sb.ToString());
                    data.Add(wresp.ResponseUri.ToString());
                    foreach (string n in wresp.Headers.GetValues(headername))
                    {
                        data.Add(n);
                    }
                    return data;
                }
                
               
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                wr.Abort();
                wresp.Close();
            }

        }
        public List<string> MultiPostAsync(NameValueCollection postData, string url, string proxy, int timeout, CookieContainer cookieC)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            WebResponse wresp = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    WebProxy handler;
                    if (dproxy.Length > 2)
                    {
                        handler = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else handler = new WebProxy(proxy);
                    wr.Proxy = handler;
                }
                wr.Method = "POST";
                //wr.AllowAutoRedirect
                wr.Timeout = timeout * 1000;
                wr.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                wr.KeepAlive = true;
                wr.CookieContainer = cookieC;
                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in postData.Keys)
                {
                    
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, postData[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                wresp = wr.GetResponse();
                
                //wresp.ContentLength = 3145728;
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    List<string> data = new List<string>();
                    data.Add(sb.ToString());
                    data.Add(wresp.ResponseUri.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                wr.Abort();
                wresp.Close();
            }
        }



        public List<string> MultiPostAsync(NameValueCollection postData, string url, string proxy, int timeout, CookieContainer cookieC, string refferalurl, string platform)
        {

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            WebResponse wresp = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    WebProxy handler;
                    if (dproxy.Length > 2)
                    {
                        handler = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else handler = new WebProxy(proxy);
                    wr.Proxy = handler;
                }
                wr.Method = "POST";
                wr.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0";
                wr.Timeout = timeout * 1000;
                wr.KeepAlive = true;
                wr.Referer = refferalurl;
                if (platform.Contains("WIKI"))
                {
                    wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    wr.Headers.Add("Accept-Encoding", "gzip, deflate");
                }
                wr.CookieContainer = cookieC;
                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in postData.Keys)
                {
                    
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, postData[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                wresp = wr.GetResponse();
                
                //wresp.ContentLength = 3145728;
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream(),Encoding.UTF8))
                {
                    Char[] read = new Char[256];
                    StringBuilder sb = new StringBuilder();
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        sb.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                    List<string> data = new List<string>();
                    data.Add(sb.ToString());
                    data.Add(wresp.ResponseUri.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                wr.Abort();
                wresp.Close();
            }
        }

       

        public List<string> MultiPostAsync(NameValueCollection postData, string url, string proxy, int timeout, CookieContainer cookieC, string refferalurl)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            WebResponse wresp = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    WebProxy handler;
                    if (dproxy.Length > 2)
                    {
                        handler = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else handler = new WebProxy(proxy);
                    wr.Proxy = handler;
                }
                wr.Method = "POST";
                wr.Timeout = timeout * 1000;
                wr.KeepAlive = true;
                wr.Referer = refferalurl;
                wr.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                wr.CookieContainer = cookieC;
                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in postData.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, postData[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                wresp = wr.GetResponse();
                
                //wresp.ContentLength = 3145728;
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream(),Encoding.UTF8))
                {
                    string data1 = sr.ReadToEnd();
                    
                    List<string> data = new List<string>();
                    data.Add(data1);
                    data.Add(wresp.ResponseUri.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                wr.Abort();
                wresp.Close();
            }
        }


        public List<string> MultiPostAsync(string accepttext,NameValueCollection postData, string url, string proxy, int timeout, CookieContainer cookieC, string refferalurl)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            WebResponse wresp = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                wr.ContentType = "multipart/form-data; boundary=" + boundary;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    WebProxy handler;
                    if (dproxy.Length > 2)
                    {
                        handler = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else handler = new WebProxy(proxy);
                    wr.Proxy = handler;
                }
                wr.Method = "POST";
                wr.Timeout = timeout * 1000;
                wr.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                wr.Accept = accepttext;
                wr.KeepAlive = true;
                wr.Referer = refferalurl;
                wr.CookieContainer = cookieC;
                Stream rs = wr.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in postData.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, postData[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
                rs.Close();
                wresp = wr.GetResponse();
                
                //wresp.ContentLength = 3145728;
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream(), Encoding.UTF8))
                {
                    string data1 = sr.ReadToEnd();
                    
                    List<string> data = new List<string>();
                    data.Add(data1);
                    data.Add(wresp.ResponseUri.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                wr.Abort();
                wresp.Close();
            }
        }


        public Image GetCapImageAsync(string url, int timeout, CookieContainer cookieC, string proxy)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                Image capimage = null;
                capimage = new System.Drawing.Bitmap(myResp.GetResponseStream());
                return capimage;

            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }


        public Stream GetCapImageAsync(CookieContainer cookieC, string proxy, string url, int timeout)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                Stream s = myResp.GetResponseStream();
                return s;

            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {

            }
        }

        public static string CheckAsync(string url, int timeout, string proxy, string keyword)
        {
            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                //myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
               // myReq.UserAgent = "Opera Mini";
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream()))
                {
                    string resp = sr.ReadToEnd();
                    if (resp.IndexOf(keyword) != -1) return url;
                    else return null;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }

        }



        public static string PostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC, string userReferrer)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                myReq.Referer = userReferrer;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }


        public static string DostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC, string userReferrer)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                myReq.Accept = "application/xml, text/xml, */*; q=0.01";
                myReq.Headers.Add("Pragma", "no-cache");
                myReq.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                myReq.Headers.Add("Cache-Control", "no-cache");
             //   myReq.Headers.Add("Connection", "keep-alive");
                myReq.Headers.Add("Accept-Encoding", "gzip, deflate");
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                myReq.Referer = userReferrer;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "text/plain; charset=UTF-8";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {

               myReq.Abort();
                myResp.Close();
            }
        }

        public static string JostHttpAsync(string postData, string url, int timeout, string proxy, CookieContainer cookieC, string userReferrer)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Method = "POST";
                myReq.Accept = "application/xml, text/xml, */*; q=0.01";
                myReq.Headers.Add("Pragma", "no-cache");
                myReq.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                myReq.Headers.Add("Cache-Control", "no-cache");
                myReq.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
               // myReq.Headers.Add("Connection", "keep-alive");
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.Headers.Add("Accept-Encoding", "gzip, deflate");
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                myReq.Referer = userReferrer;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                // myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();

                
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream(),Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               myReq.Abort();
                myResp.Close();
            }
        }


        public Stream GetMGBEncodedCaptcha(string url, string proxy, int timeout, CookieContainer cookieC, string refferal)
        {

            HttpWebRequest myReq = null;
            
            HttpWebResponse myResp = null;
            try
            {
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                if (!String.IsNullOrEmpty(proxy))
                {
                    string[] dproxy = proxy.Split(':');
                    if (dproxy.Length > 2)
                    {
                        myReq.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                    }
                    else myReq.Proxy = new WebProxy(proxy);
                }
                myReq.CookieContainer = cookieC;
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                myReq.Referer = refferal;
                myReq.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
                myReq.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                // myReq.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                myResp = (HttpWebResponse)myReq.GetResponse();
                Stream s = myResp.GetResponseStream();
                return s;

            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {

            }
        }



    }
}