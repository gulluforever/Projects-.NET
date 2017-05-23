using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Likes.Com_BOT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public static CancellationTokenSource pts = new CancellationTokenSource();
        public static int timeout = 60;
        public static int pcomments = 0;
        public static int pmsent = 0;
        public static int markspam = 0;
        int remurls = 0;
        public static int tuploaded = 0;
        int commentprogress = 0;
        int pcount = 0;
        int i = 1;
        bool stop = true;
        public static List<string> details = new List<string>();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Normal);
        public static List<string> proxies = new List<string>();
        public static List<Task> tasks = new List<Task>();
        public static List<string> commentonlinks = new List<string>();
        public static List<string> profilelinks = new List<string>();
        public static string fttag = null;
        public static List<string> comments = new List<string>();
        public static List<string> scrapedusers = new List<string>();
        public static List<string> ImportedProfileUrls = new List<string>();
        public static List<string> GrabbedLinks = new List<string>();
        public static List<string> postedimages = new List<string>();
        Queue<string> logind = new Queue<string>();
        public static List<string> captions = new List<string>();
        public static string[] files;
        public static string[] pfiles;
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            CheckForIllegalCrossThreadCalls = false;
            button1.Enabled = false;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //marqueeProgressBarControl1.Properties.Stopped = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           button2.Enabled = ! checkBox1.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = checkBox3.Checked;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(process);
            t.Start();
        }
        private void process()
        {
            MainCode.Register(null,true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)  files = Directory.GetFiles(fbd.SelectedPath);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            captions.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    captions.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }
        public static Random rad = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            proxies.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    proxies.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(details.Count.ToString());
            StreamWriter s = File.AppendText("registeredlinks.txt");
            foreach (string acc in details)
            {
                s.WriteLine(acc);
            }
            s.Close();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            label3.Text = "Total Registered : " + details.Count.ToString() + " Total Uploaded : "+tuploaded.ToString();
            label4.Text = "Total DONE : " + Form1.pcomments.ToString() + " Urls Remaining : " + remurls.ToString();
            label7.Text = "Total Scraped :"+scrapedusers.Count.ToString();
            label9.Text = "Total Sent :" + pmsent.ToString()+" Total Spam : "+markspam.ToString();
            progressBar1.Value = pcount;
           // progressBar2.Value = commentprogress;
            dispatcherTimer.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fttag = textBox1.Text;
            progressBar1.Value = 0;
            dispatcherTimer.Start();
            
            if (checkBox2.Checked)
            {
                if (files.Length == 0) MessageBox.Show("Select Folder For Images");
                else if (captions.Count == 0) MessageBox.Show("Import Captions for Images");
                progressBar1.Maximum = Convert.ToInt32(numericUpDown1.Value);
                button2.Enabled = false;
                Thread regThread = new Thread(StartRegister);
                regThread.IsBackground = true;
                regThread.Start();
            }
            else
            {
                progressBar1.Maximum = Convert.ToInt32(numericUpDown1.Value);
                button2.Enabled = false;
                Thread regThread = new Thread(StartRegister);
                regThread.IsBackground = true;
                regThread.Start();
            }
            

        }
        private void StartRegister()
        {
            try
            {
                pcount = 0;
                int noofaccounts = Convert.ToInt32(numericUpDown1.Value);
                int threads = Convert.ToInt32(numericUpDown2.Value);
                var options = new ParallelOptions()
                {
                    CancellationToken = cts.Token,
                    MaxDegreeOfParallelism = threads,
                };
                Parallel.For(0, noofaccounts, options, i =>
                {
                    try
                    {

                        string proxi = null;
                        if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
                        MainCode.Register(proxi, checkBox2.Checked);
                        //pcount = pcount + 1;
                    }
                    catch { }
                    finally { pcount = pcount + 1; }
                });
                button2.Enabled = true;
                MessageBox.Show("Completed");
            }
            catch (OperationCanceledException ex)
            {
                button6.Enabled = true;
                MessageBox.Show("CompleteD");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cts.Cancel();
            }
            catch
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string FileToSaveAs = saveFileDialog1.FileName;

            System.IO.StreamWriter objwriter = new System.IO.StreamWriter(FileToSaveAs);
            int rr = 0;


            for (rr = 0; rr <= postedimages.Count - 1; rr++)
            {
                string gul = postedimages[rr];
                objwriter.WriteLine(gul);

            }
            //MessageBox.Show("Saved!");
            objwriter.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fttag = textBox1.Text;
            progressBar1.Value = 0;
            dispatcherTimer.Start();
            details.Clear();
            details.AddRange(File.ReadAllLines("registeredlinks.txt").Where(l => !string.IsNullOrWhiteSpace(l))); 
            if (files.Length == 0) MessageBox.Show("Select Folder For Images");
            else if (captions.Count == 0) MessageBox.Show("Import Captions for Images");
            else if (details.Count == 0) MessageBox.Show("No Previous Accounts");
            else
                {
                    progressBar1.Maximum = details.Count;
                    Thread t = new Thread(UploadCode);
                    t.IsBackground = true;
                    button6.Enabled = false;
                    t.Start();
                }
        }
        private void UploadCode()
        {
            try
            {
                cts = new CancellationTokenSource();
                pcount = 0;
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(numericUpDown2.Value),
                    CancellationToken = cts.Token
                };
                Parallel.ForEach(details, options, login =>
                    {
                        try
                        {
                            string proxi = null;
                            if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
                            MainCode.Upload(login, proxi);
                        }
                        catch(Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            pcount = pcount + 1;
                        }
                        
                    });
                button6.Enabled = true;
                MessageBox.Show("CompleteD");
            }
            catch(OperationCanceledException ex)
            {
                button6.Enabled = true;
                MessageBox.Show("CompleteD");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            commentonlinks.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    commentonlinks.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            comments.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    comments.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //progressBar2.Value = 0;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            details.Clear();
            details.AddRange(File.ReadAllLines("registeredlinks.txt").Where(l => !string.IsNullOrWhiteSpace(l)));
            foreach (string s in details) logind.Enqueue(s);
            if (checkBox5.Checked) if(comments.Count == 0) MessageBox.Show("Import Comments List");
            if (details.Count == 0) MessageBox.Show("No Previous Accounts");
            else if (commentonlinks.Count == 0) MessageBox.Show("Import Links for Images to Comment On");
            else
            {
                //progressBar2.Maximum = details.Count;
                button10.Enabled = false;
                Thread cthread = new Thread(CommentThread);
               // marqueeProgressBarControl1.Properties.Stopped = true;
                cthread.IsBackground = true;
                cthread.Start();
            }
        }
        private void CommentThread()
        {
            try
            {
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(numericUpDown2.Value),
                    CancellationToken = cts.Token
                };
                remurls = commentonlinks.Count;
                Parallel.ForEach(commentonlinks, options, link =>
                {
                    try
                    {
                            string login = details[rad.Next(details.Count)];
                            string proxi = null;
                            if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
                            MainCode.Comment(link, proxi, login, checkBox4.Checked, checkBox5.Checked, checkBox6.Checked);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        remurls = remurls - 1;
                        commentprogress = commentprogress + 1;
                    }
                });
                button10.Enabled = true;
                MessageBox.Show("CompleteD");
                //marqueeProgressBarControl1.Properties.Stopped = true;
            }
            catch (OperationCanceledException ex)
            {
                button6.Enabled = true;
                MessageBox.Show("CompleteD");
                //marqueeProgressBarControl1.Properties.Stopped = true;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) pfiles = Directory.GetFiles(fbd.SelectedPath);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }
        

        private void button15_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Resources.names);
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            button15.Enabled = false;
            dispatcherTimer.Start();
            stop = true;
            Thread t = new Thread(StartScrape);
            t.IsBackground = true;
            t.Start();
        }
        private void StartScrape()
        {
            i = 1;
            string proxi = null;
            if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
            string fresponse = HTTP.GetSource(textBox2.Text, 60, proxi, new System.Net.CookieContainer());
            HtmlAgilityPack.HtmlDocument page = new HtmlAgilityPack.HtmlDocument();
            var baseUrl = new Uri(textBox2.Text);
            string userid = string.Empty;
            try
            {
                page.LoadHtml(fresponse);
                userid = page.DocumentNode.SelectSingleNode("//div[contains(@id,'top_subscribe_button')]").GetAttributeValue("user_id", "");
            }
            catch(Exception ex)
            {
               
            }
            finally
            {
                page.LoadHtml("");
            }
            string posturl = textBox2.Text + "api/get_subscriptions_page";
            int threads = Convert.ToInt32(numericUpDown2.Value);
            try
            {
                for (int j = 0; j < threads; j++)
                {
                    Task t = new Task(() =>
                        {
                            ThreadTask(userid, posturl, baseUrl);
                        });
                    tasks.Add(t);
                    t.Start();

                }
                foreach (Task t in tasks) t.Wait(pts.Token);
            }
            catch
            {
            }
            finally
            {
                button15.Enabled = true;
            }
            
        }
        private void ThreadTask(string userid, string posturl, Uri baseUrl)
        {
            try
            {
                while (stop)
                {
                    string proxi = null;
                    if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
                    string postData = "page=" + i.ToString() + "&id_type=subscribers&user_id=" + userid;
                    i = i + 1;
                    string response = HTTP.PostData(postData, posturl, 60, proxi, new System.Net.CookieContainer());
                    if (response.Contains("{\"success\": false")) stop = false;
                    else
                    {
                        HtmlAgilityPack.HtmlDocument page1 = new HtmlAgilityPack.HtmlDocument();
                        try
                        {
                            page1.LoadHtml(response);
                            HtmlAgilityPack.HtmlNodeCollection hnc = page1.DocumentNode.SelectNodes("//a[contains(@class,'blue underline')]");
                            foreach (var hn in hnc)
                            {
                                string purl = new Uri(baseUrl, hn.GetAttributeValue("href", "")).AbsoluteUri.Replace("&amp;", "&");
                                scrapedusers.Add(purl);
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            page1.LoadHtml("");
                        }

                    }
                }
            }
            catch { }
        }
        
        private void button17_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            string FileToSaveAs = saveFileDialog2.FileName;

            System.IO.StreamWriter objwriter = new System.IO.StreamWriter(FileToSaveAs);
            int rr = 0;


            for (rr = 0; rr <= scrapedusers.Count - 1; rr++)
            {
                string gul = scrapedusers[rr];
                objwriter.WriteLine(gul);

            }
            //MessageBox.Show("Saved!");
            objwriter.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            stop = false;
            pts.Cancel();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            dispatcherTimer.Start();
            details.Clear();
            details.AddRange(File.ReadAllLines("registeredlinks.txt").Where(l => !string.IsNullOrWhiteSpace(l)));
            Thread t = new Thread(SendMsg);
            t.IsBackground = true;
            button19.Enabled = false;
            t.Start();

        }
        private void SendMsg()
        {
            try
            {
                pts = new CancellationTokenSource();
                string message = textBox3.Text;
                pmsent = 0;
                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(numericUpDown2.Value),
                    CancellationToken = pts.Token
                };
                Parallel.ForEach(profilelinks, options, profileurl =>
                {
                    try
                    {
                        string proxi = null;
                        if (checkBox3.Checked && proxies.Count > 0) proxi = proxies[rad.Next(proxies.Count)];
                        string logindetails = details[rad.Next(details.Count)];
                        MainCode.SendMessage(logindetails, profileurl, message, proxi);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        //pcount = pcount + 1;
                    }

                });
            }
            catch
            {
            }
            finally
            {
                button19.Enabled = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            profilelinks.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    profilelinks.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            pts.Cancel();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ImportedProfileUrls.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    ImportedProfileUrls.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {

        }
        private void CheckOnline()
        {
              int threads = Convert.ToInt32(numericUpDown2.Value);
            int urlscount = ImportedProfileUrls.Count;
            var options = new ParallelOptions()
                {
                    CancellationToken = cts.Token,
                    MaxDegreeOfParallelism = threads,
                };
                Parallel.For(0, urlscount, options, i =>
                {
                    try
                    {
                    }
                    catch{}
                    finally {}
                });
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            GrabbedLinks.Clear();
            Thread t = new Thread(()=>GrabLinks(textBox1.Text));
            t.IsBackground = true;
            t.Start();
        }
        private void GrabLinks(string tag)
        {
            System.Net.CookieContainer cookieC = new System.Net.CookieContainer();
            HtmlAgilityPack.HtmlDocument page = new HtmlAgilityPack.HtmlDocument();
            try
            {
                string PostData = "username=" + "hqlsgdam" + "&password=" + "laguxnvy" + "&ajax=true";
                string responsestring = HTTP.PostData(PostData, "http://likes.com/api/login", Form1.timeout, null, cookieC);
                string taggedurl = "http://likes.com/api/feed";
                string discoverurl = "http://likes.com/api/discover_feed";
                if (checkBox7.Checked)
                {
                    for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                    {
                        string postData = "page=" + i.ToString() + "&position=" + (i * 20).ToString() + "&per_page=20";
                        string response = HTTP.PostData(postData, taggedurl, 60, null, cookieC);
                        response = response.Substring(response.IndexOf("\"image_ids\": [") + 14);
                        if (response.Contains("\"success\": false")) break;
                        response = response.Substring(0, response.IndexOf("]"));
                        string[] imageid = response.Split(',');
                        foreach (string s in imageid)
                        {
                            string tes = s.Replace("i:", "");
                            tes = tes.Replace("\"", "");
                            GrabbedLinks.Add("http://likes.com/" + tes.Replace(" ", ""));
                        }
                    }
                }
                if (checkBox8.Checked)
                {
                    for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                    {
                        
                        string postData = "page=" + i.ToString() + "&position=" + (i * 20).ToString() + "&per_page=20&discover_feed_type=trending";
                        string response = HTTP.PostData(postData, discoverurl, 60, null, cookieC);
                        response = response.Substring(response.IndexOf("\"image_ids\": [") + 14);
                        if (response.Contains("\"success\": false")) break;
                        response = response.Substring(0, response.IndexOf("]"));
                         string[] imageid = response.Split(',');
                         foreach (string s in imageid)
                         {
                             GrabbedLinks.Add("http://likes.com/" + s.Replace(" ", ""));
                         }
                    }
                }
                if (checkBox9.Checked)
                {
                    for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                    {

                        string postData = "page=" + i.ToString() + "&position=" + (i * 20).ToString() + "&per_page=20&discover_feed_type=popular";
                        string response = HTTP.PostData(postData, discoverurl, 60, null, cookieC);
                        response = response.Substring(response.IndexOf("\"image_ids\": [") + 14);
                        if (response.Contains("\"success\": false")) break;
                        response = response.Substring(0, response.IndexOf("]"));
                        string[] imageid = response.Split(',');
                        foreach (string s in imageid)
                        {
                            GrabbedLinks.Add("http://likes.com/" + s.Replace(" ", ""));
                        }
                    }
                }
                MessageBox.Show("Completed");
            }
            catch { }
            finally
            {
                page.LoadHtml("");
            }

        }

        private void saveFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            string FileToSaveAs = saveFileDialog3.FileName;
            System.IO.StreamWriter objwriter = new System.IO.StreamWriter(FileToSaveAs);
            int rr = 0;
            for (rr = 0; rr <= GrabbedLinks.Count - 1; rr++)
            {
                string gul = GrabbedLinks[rr];
                objwriter.WriteLine(gul);

            }
            //MessageBox.Show("Saved!");
            objwriter.Close();
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            saveFileDialog3.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog3.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            GrabbedLinks = GrabbedLinks.Distinct().ToList();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }
    }
}
