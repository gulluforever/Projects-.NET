using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Likes.Com_BOT
{
    class MainCode
    {
        public static void Register(string proxy,bool uploadalso)
        {
            string username = RandomValues.GenerateEmailandUserName(2);
            string password = RandomValues.GenerateEmailandUserName(2);
            string email = RandomValues.GenerateEmailandUserName(1);
            string name = RandomValues.GetName();
            string registerurl = "http://likes.com/api/signupprocess?ajax=true&mobile=true&email=" + email + "&name=" + name + "&nickname=" + username + "&password=" + password + "&redirect_url=%2F&error_redirect=%2F&token=&referer_url=";
            string regpage = HTTP.GetSource(registerurl, Form1.timeout, proxy,new CookieContainer());
            
            if (regpage.Contains("\"success\": true"))
            {
                string details = username + ":" + password + ":" + email + ":" + name;
                Form1.details.Add(details);
                if (uploadalso)
                {
                    
                    Upload(details, proxy);
                }
                if (Form1.pfiles.Length > 0)
                {
                    CookieContainer cookieC = new CookieContainer();
                    string loginurl = "http://likes.com/login";
                    string loginpage = HTTP.GetSource(loginurl, Form1.timeout, proxy, cookieC);
                    string[] ldetails = details.Split(':');
                    string PostData = "username=" + ldetails[0] + "&password=" + ldetails[1] + "&ajax=true";
                    string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, proxy, cookieC);
                    string uploadimageurl = "http://" + ldetails[0] + ".likes.com" + "/api/uploadprocess?form_id=mobile_settings_form";
                    string profilepic = Form1.pfiles[rad.Next(Form1.pfiles.Length)];
                    string uploadtext = HTTP.UploadImage(profilepic, uploadimageurl, Form1.timeout, proxy, cookieC);
                    try
                    {
                        string imageurl = uploadtext.Substring(uploadtext.IndexOf("http://likes.images"));
                        imageurl = imageurl.Substring(0, imageurl.IndexOf("\""));
                        string profiledata = "name=" + ldetails[3] + "&user_name=" + ldetails[0] + "&homepage_url=&about=&is_mature=0&picture=" + imageurl;
                        string profileupdateurl = "http://" + ldetails[0] + ".likes.com" + "/api/update_user_info";
                        //MessageBox.Show(profileupdateurl);
                        string responseadd = HTTP.PostData(profiledata, profileupdateurl, Form1.timeout, proxy, cookieC);
                    }
                    catch (Exception chec)
                    {
                        //MessageBox.Show(chec.ToString());
                    }
                }
            }
        }
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Random rad = new Random();
        public static void Comment(string imageurl, string proxy, string logindetails,bool like,bool docmt,bool share)
        {
            CookieContainer cookieC = new CookieContainer();
            string loginurl = "http://likes.com/login";
            string loginpage = HTTP.GetSource(loginurl, Form1.timeout, proxy, cookieC);
            string[] ldetails = logindetails.Split(':');
            string PostData = "username=" + ldetails[0] + "&password=" + ldetails[1] + "&ajax=true";
            string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, proxy, cookieC);
            string imageid = imageurl.Substring(imageurl.LastIndexOf("/") + 1);
            imageurl = "http://" + ldetails[0] + ".likes.com/" + imageid;
            HTTP.GetSource(imageurl, Form1.timeout, proxy, cookieC);
            if (docmt)
            {
                string postcommenturl = "http://" + ldetails[0] + ".likes.com" + "/api/add_comment";
                string comment = Form1.comments[rad.Next(Form1.comments.Count)];
                string commentdata = "image_id=" + imageid + "&comment_text=" + comment;
                string commentstring = HTTP.PostData(commentdata, postcommenturl, Form1.timeout, proxy, cookieC);
                if (commentstring.Contains("\"success\": true")) Form1.pcomments = Form1.pcomments + 1;
            }
            if (share)
            {
                string shareactionurl = "http://likes.com/api/reshare_image";
                string sharepostdata = "reshared_image_id="+imageid+"&caption=&add_source=mobile_reshare";
                HTTP.PostData(sharepostdata, shareactionurl, Form1.timeout, proxy, cookieC);
                Form1.pcomments = Form1.pcomments + 1;
            }
            if (like)
            {
                string likeactionurl = "http://likes.com/api/add_like";
                string likepostdata = "image_id=" + imageid;
                HTTP.PostData(likepostdata, likeactionurl, Form1.timeout, proxy, cookieC);
                Form1.pcomments = Form1.pcomments + 1;
            }
        }
        public static bool Login(string logindetails,string proxy,CookieContainer cookieC)
        {

            string loginurl = "http://friendlife.com/login";
            string loginpage = HTTP.GetSource(loginurl, Form1.timeout, proxy, cookieC);
            string[] ldetails = logindetails.Split(':');
            string PostData = "username=" + ldetails[0] + "&password=" + ldetails[1] + "&ajax=true";
            string responsestring = HTTP.PostData(PostData, "http://friendlife.com/api/login", Form1.timeout, proxy, cookieC);
            Console.WriteLine(responsestring);
            if (responsestring.Contains("true")) return true;
            else return false;
        }
        public static void SendMessage(string logindetails, string targetprofile, string message, string proxy)
        {
            CookieContainer cookieC = new CookieContainer();
            string loginurl = "http://likes.com/login";
            string loginpage = HTTP.GetSource(loginurl, Form1.timeout, proxy, cookieC);
            string[] ldetails = logindetails.Split(':');
            string PostData = "username=" + ldetails[0] + "&password=" + ldetails[1] + "&ajax=true";
            string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, proxy, cookieC);
            string targetusername = targetprofile.Substring(7);
            targetusername = targetusername.Substring(0, targetusername.IndexOf("."));
            string msg = Spinner.Spin(message);
            string postData = "target_user_name=" + targetusername + "&message=" + msg + "&send=true&source=web";
            string posturl = "http://likes.com/api/save_message";
            string postedtext = HTTP.PostData(postData, posturl, 60, proxy, cookieC);
            if (postedtext.Contains("\"success\": true"))
            {
                if(postedtext.Contains("\"is_spam\": true")) Form1.markspam = Form1.markspam+1;
                else Form1.pmsent = Form1.pmsent + 1;
            }
        }
        public static void Upload(string logindetails,string proxy)
        {
            CookieContainer cookieC = new CookieContainer();
            string loginurl = "http://likes.com/login";
            string loginpage = HTTP.GetSource(loginurl, Form1.timeout,proxy, cookieC);
            string[] ldetails = logindetails.Split(':');
            string PostData = "username="+ldetails[0]+"&password="+ldetails[1]+"&ajax=true";
            string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, proxy, cookieC);
            string uploadimageurl = "http://"+ldetails[0]+".likes.com"+"/api/uploadprocess?form_id=upload_file_form";
            string postimageurl = "http://"+ldetails[0]+".likes.com"+"/api/add_image";
            foreach (string file in Form1.files)
            {
                try
                {
                    Bitmap bit = new Bitmap(file);
                    //string uploadtext = HTTP.UploadImage(file,uploadimageurl, Form1.timeout, proxy, cookieC);
                    string uploadtext = HTTP.HttpUploadFile(uploadimageurl, file, proxy, "uploaded", "image/jpeg", new NameValueCollection(), cookieC);
                    string imageurl = null;
                    string caption = Form1.captions[rad.Next(Form1.captions.Count)];
                    if (uploadtext.Contains("likes.images"))
                    {
                        imageurl = uploadtext.Substring(uploadtext.IndexOf("http://likes.images"));
                        imageurl = imageurl.Substring(0, imageurl.IndexOf("\""));
                        //string postData = "caption=" + caption + "&nsfw=0&source_url=" + imageurl + "&add_source=mobile_add";
                        string postData = "source_url="+imageurl+"&nsfw=0&add_image_url="+imageurl+"&add_source=file_upload&ft_tag="+Form1.fttag+"&caption="+caption+"&tags="+Form1.fttag;
                        string responseadd = HTTP.PostData(postData, postimageurl, Form1.timeout, proxy, cookieC);
                        string imageid = responseadd.Substring(responseadd.IndexOf("\":")+3);
                        imageid = imageid.Substring(0, imageid.IndexOf(","));
                        string postedimageurl = "http://" + ldetails[0] + ".likes.com" + "/" + imageid;
                        Form1.postedimages.Add(postedimageurl);
                        Form1.tuploaded = Form1.tuploaded + 1;
                    }
                }
                catch(Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }
            Thread.Sleep(30000);
        }
        public static void FollowAllTest(string profileurl, string proxy, string logindetails)
        {
            HtmlAgilityPack.HtmlDocument page = new HtmlAgilityPack.HtmlDocument();
            try
            {
                CookieContainer cookieC = new CookieContainer();
                string loginurl = "http://likes.com/login";
                string loginpage = HTTP.GetSource(loginurl, Form1.timeout, proxy, cookieC);
                string[] ldetails = logindetails.Split(':');
                string PostData = "username=" + ldetails[0] + "&password=" + ldetails[1] + "&ajax=true";
                string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, proxy, cookieC);
                string profilepage = HTTP.GetSource(profileurl, Form1.timeout, proxy, cookieC);
                page.LoadHtml(profilepage);
                string userid = page.DocumentNode.SelectSingleNode("//div[contains(@id,'top_subscribe_button')]").GetAttributeValue("user_id", "");
                string followdata = "id=" + userid + "&id_type=subscribers";
                string followedstring = HTTP.PostData(followdata, profileurl + "api/follow_all", Form1.timeout, proxy, cookieC);
            }
            catch { }
            finally
            {
                page.LoadHtml("");
            }
        }
    }
}
