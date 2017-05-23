using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.Net.Http;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Threading.Tasks;

namespace Likes.Com_BOT
{
    class HTTP
    {
        public static string GetSource(string url, int timeout, string proxy, CookieContainer cookieC)
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
                myReq.UserAgent = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16";
                myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                myResp = (HttpWebResponse)myReq.GetResponse();
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream()))
                {

                    while ((sr.Peek() >= 0))
                    {
                        if ((Form1.cts.IsCancellationRequested || Form1.pts.IsCancellationRequested)) break;
                        sb.Append(sr.ReadLine());
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                myReq.Abort();
                myResp.Close();
            }
        }
        public static string GetSourceNormal(string url, int timeout, string proxy, CookieContainer cookieC)
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
                myReq.UserAgent = "Mozilla/5.0";
                //myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                myResp = (HttpWebResponse)myReq.GetResponse();
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream()))
                {

                    while ((sr.Peek() >= 0))
                    {
                        if ((Form1.cts.IsCancellationRequested || Form1.pts.IsCancellationRequested)) break;
                        sb.Append(sr.ReadLine());
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                myReq.Abort();
                myResp.Close();
            }
        }
        public static string PostData(string postData, string url, int timeout, string proxy, CookieContainer cookieC)
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
                myReq.Method = "POST";
                myReq.Timeout = timeout * 1000;
                myReq.UserAgent = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16";
                myReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = byteArray.Length;
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                myResp = (HttpWebResponse)myReq.GetResponse();
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(myResp.GetResponseStream()))
                {

                    while ((sr.Peek() >= 0))
                    {
                        if ((Form1.cts.IsCancellationRequested || Form1.pts.IsCancellationRequested)) break;
                        sb.Append(sr.ReadLine());
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                myReq.Abort();
                myResp.Close();
            }
        }
        public static string post_Multipart(String url, String referer, String imagePath, CookieContainer cookies,string videoid,string sessiontoken)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("video_id", videoid);
            nvc.Add("is_ajax", "1");
            nvc.Add("session_token", sessiontoken);
            wr.KeepAlive = true;
            wr.Referer = referer;
            wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            wr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
            wr.CookieContainer = cookies;
            Stream rs = wr.GetRequestStream();
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "imagefile", Path.GetFileName(imagePath), "image/jpeg");
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
                //  log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                return null;
            }
            finally
            {
                wr = null;
            }
        }
        public static string HttpUploadFile(string url, string file, string proxy, string paramName, string contentType, NameValueCollection nvc, CookieContainer cookieC)
        {
            // log.Debug(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
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
            wr.KeepAlive = true;
            wr.CookieContainer = cookieC;
            Stream rs = wr.GetRequestStream();
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, Path.GetFileName(file), contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
                //  log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                return null;
            }
            finally
            {
                wr = null;
            }
        }
        public static string UploadImage(string fname, string url, int timeout, string proxy, CookieContainer cookieC)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    if (!String.IsNullOrEmpty(proxy))
                    {
                        string[] dproxy = proxy.Split(':');
                        if (dproxy.Length > 2)
                        {
                            handler.Proxy = new WebProxy(dproxy[0] + ":" + dproxy[1], true, null, new NetworkCredential(dproxy[2], dproxy[3]));
                        }
                        else handler.Proxy = new WebProxy(proxy);
                        handler.UseProxy = true;
                    }
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    handler.CookieContainer = cookieC;
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                        client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                        client.Timeout = new TimeSpan(0, 0, timeout);
                        var requestContent = new MultipartFormDataContent();
                        var imageContent = new StreamContent(new MemoryStream(File.ReadAllBytes(fname)));
                        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                        requestContent.Add(imageContent, "uploaded", Path.GetFileName(fname));
                        var response = client.PostAsync(url, requestContent).Result;
                        var responseContent = response.Content;
                        string responseString = responseContent.ReadAsStringAsync().Result;
                        return responseString;
                    }
                }
            }
            catch (Exception eop)
            {
                //MessageBox.Show(eop.ToString());
                return null;
            }
        }

    }
}
