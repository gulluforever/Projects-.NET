using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace IssuuDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CookieContainer cookieC = new CookieContainer();
        Queue<string> dids = new Queue<string>();
        List<string> DownloadUrls = new List<string>();
        string dlurl = "http://issuu.com/query?documentId=DOWNLOAD&action=issuu.document.download";
        private void IssueLogin()
        {
            string postData = "username=joa.k.skds.k.kds%40gmail.com&password=swepassy12A&permission=f&loginExpiration=standard&action=issuu.user.login";
            string loginUrl = "https://issuu.com/query";
            HTTPFunctions.PostHttpAsync(postData, loginUrl, 60, null, cookieC);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(MainCode);
            t.Start();
        }
        private void MainCode()
        {
            try
            {
                IssueLogin();
                GetDocumentIds(textBox1.Text);
                progressBar1.Maximum = dids.Count-1;
                GetDLLink();
                MessageBox.Show("Completed!");
                button2.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GetDLLink()
        {
            string url = string.Empty;
            cookieC.Add(new Cookie("i18next", "en", "/", ".issuu.com"));
            cookieC.Add(new Cookie("iutk", "8316103eb5d911e4b21f004973737575", "/", ".issuu.com"));
            //cookieC.Add(new Cookie("site.model.token", "20150224094210:f:s0:060218d25a88f7e30000000000000000", "/", ".issuu.com"));
            cookieC.Add(new Cookie("site.model.username", "kdl50w656asu", "/", ".issuu.com"));
            cookieC.Add(new Cookie("__utmt", "1", "/", ".issuu.com"));
            foreach(string Did in dids)
            {
                url = dlurl.Replace("DOWNLOAD", Did);
                string pg = HTTPFunctions.GetReponseFunctionAsync(url, 60, null, cookieC);
                if (!pg.Contains("Document access denied"))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(pg);
                    XmlNodeList xnList = xml.GetElementsByTagName("redirect");
                    string DownloadUrl = xnList[0].Attributes["url"].Value;
                    //MessageBox.Show(DownloadUrl);
                    DownloadUrls.Add(DownloadUrl);
                }
                else { }
                progressBar1.Increment(1);
            }
        }
        private void GetDocumentIds(string username)
        {
            bool run = true;
            int i = 100;
            int startindex = 0;
            while (run)
            {
                string documentlist = "http://api.issuu.com/query?action=issuu.documents.list_anonymous&format=xml&documentUsername=" + username + "&startIndex="+startindex.ToString()+"&pageSize="+i.ToString();
                string pageSource = HTTPFunctions.GetReponseFunctionAsync(documentlist, 60, null, cookieC);
                XmlDocument xml = new XmlDocument();
                if (!pageSource.Contains("more=\"true\"")) run = false;
                startindex = startindex + i;
                xml.LoadXml(pageSource);
                XmlNodeList xnList = xml.GetElementsByTagName("document");
                //MessageBox.Show(xnList.Count.ToString());
                foreach (XmlNode xn in xnList)
                {
                    dids.Enqueue(xn.Attributes["documentId"].Value);
                }
            }
            label1.Text = "Total Document Id's : " + dids.Count.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string FileToSaveAs = saveFileDialog1.FileName;
            System.IO.StreamWriter objwriter = new System.IO.StreamWriter(FileToSaveAs);
            int rr = 0;
            for (rr = 0; rr <= DownloadUrls.Count - 1; rr++)
            {
                string gul = DownloadUrls[rr];
                objwriter.WriteLine(gul);
            }
            //MessageBox.Show("Saved!");
            objwriter.Close();
        }
    }
}
