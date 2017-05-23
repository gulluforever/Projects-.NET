using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using Amib.Threading;
using Limilabs.Client.IMAP;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Proxy;
using Limilabs.Client.POP3;
using System.Windows;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HateSpam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> logins = new List<string>();
        int count = 0;
        int movedcount = 0;
        long workingacc = 0;
        bool break1 = false;
        int portsmtp = 465;
        bool break2 = false;
        long totalacc = 0;
        long incprogress = 0;
        Queue<string> loginsquery = new Queue<string>();
        List<string> proxies = new List<string>();
        List<string> subjects = new List<string>();
        
        List<string> WorkingAccounts = new List<string>();
        CancellationTokenSource cts = new CancellationTokenSource();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    logins.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
            progressBar1.Maximum = logins.Count;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
       
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    subjects.AddRange(File.ReadAllLines(dialog.FileName).Where(l => !string.IsNullOrWhiteSpace(l)));
                }
                catch
                {

                }
            }
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            break1 = false;
            count = 0;
            movedcount = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = logins.Count;
            Thread t = new Thread(SpawnThreads);
            t.IsBackground = true;
            t.Start();
        }
        private void SpawnThreads()
        {
            try
            {
                string imaptext = "imap.gmail.com";
                string smtptet = "smtp.mail.yahoo.com";
                if (radioButton1.Checked)
                {
                    imaptext = "imap.gmail.com";
                    smtptet = "smtp.gmail.com";
                    portsmtp = 465;
                }
                else if (radioButton2.Checked)
                {
                    imaptext = "imap.mail.yahoo.com";
                    smtptet = "smtp.mail.yahoo.com";
                    portsmtp = 465;
                }
                else if (radioButton3.Checked)
                {
                    imaptext = "imap-mail.outlook.com";
                    smtptet = "smtp-mail.outlook.com";
                    portsmtp = 587;
                }
                else
                {
                    imaptext = "imap.aol.com";
                    smtptet = "smtp.aol.com";
                    portsmtp = 587;
                }
                SmartThreadPool HS = new SmartThreadPool();
                HS.MaxThreads = Convert.ToInt32(numericUpDown1.Value);
                int sendmailcount = 0;
                foreach (string login in logins)
                {
                    try
                    {
                        if (break1) break;
                        sendmailcount = Interlocked.Increment(ref sendmailcount);
                        if (sendmailcount == Convert.ToInt32(numericUpDown2.Value))
                        {
                         
                            HS.QueueWorkItem(dummy => startprocess(login, imaptext, true, smtptet), new object());
                            sendmailcount = 0;
                        }
                        else
                        {
                            
                            HS.QueueWorkItem(dummy => startprocess(login, imaptext, false, smtptet), new object());
                        }
                    }
                    catch { }
                }
                try
                {
                    HS.WaitForIdle();
                    HS.Shutdown();
                }

                catch { }
                button3.Enabled = true;
                MessageBox.Show("CompleteD");
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        public static Random rad = new Random();
        private void startprocess(string logindetails, string imapselect, bool sendmail, string smtptet)
        {
            try
            {
                int type = 0;
                Socket socket = null;
              
                if (proxies.Count > 0)
                {
                    type = 1;
                    ProxyFactory factory = new ProxyFactory();
                    string proxi = proxies[rad.Next(proxies.Count)];
                    string[] prox = proxi.Split(':');
                    IProxyClient proxy = null;
                    if (prox.Length == 4) proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]), prox[2], prox[3]);
                    else proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]));
                    socket = proxy.Connect(imapselect, Imap.DefaultSSLPort);
                }
                using (Imap imap = new Imap())
                {
                    if (type == 0) imap.ConnectSSL(imapselect) ;
                    else imap.AttachSSL(socket, imapselect);
                    string[] cred = logindetails.Split(':');
                    imap.Login(cred[0], cred[1]);                   // You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
                    CommonFolders folders = new CommonFolders(imap.GetFolders());
                    imap.Select(folders.Spam);
                    foreach (long ouid in imap.GetAll())
                    {
                        IMail email = new MailBuilder().CreateFromEml(
                         imap.GetMessageByUID(ouid));
                        
                        List<long> unseenReports = new List<long>();
                        foreach (string sub in subjects)
                        {
                            if (email.Subject.Contains(sub) || string.Equals(email.Subject,sub))
                            {
                                unseenReports.Add(ouid);
                               
                                if (!checkBox1.Checked && sendmail && !radioButton4.Checked)
                                {
                                    IMail original = email;
                                    Socket socket1 = null;
                                    if (proxies.Count > 0)
                                    {
                                        type = 1;
                                        ProxyFactory factory = new ProxyFactory();
                                        string proxi = proxies[rad.Next(proxies.Count)];
                                        string[] prox = proxi.Split(':');
                                        IProxyClient proxy = null;
                                        if (prox.Length == 4) proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]), prox[2], prox[3]);
                                        else proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]));
                                        socket1 = proxy.Connect(smtptet, portsmtp);
                                    }
                                    ReplyBuilder replyBuilder = original.Reply();

                                    // You can specify your own, custom, body and subject templates:
                                    replyBuilder.HtmlReplyTemplate = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
    <html>
    <head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
        <title>[Subject]</title>
    </head>
    <body>
    [Html]
    <br /><br />
    On [Original.Date] [Original.Sender.Name] wrote:
    <blockquote style=""margin-left: 1em; padding-left: 1em; border-left: 1px #ccc solid;"">
        [QuoteHtml]
    </blockquote>
    </body>
    </html>";
                                    replyBuilder.SubjectReplyTemplate = "Re: [Original.Subject]";

                                    replyBuilder.Html = NewSpin.Spin(textBox1.Text);
                                    
                                    MailBuilder builder = replyBuilder.ReplyToAll(cred[0]);
                                        
                                    // You can add attachments to your reply
                                    //builder.AddAttachment("report.csv");

                                    IMail reply = builder.Create();
                                    using (Smtp smtp = new Smtp())
                                    {

                                        if (type == 0)
                                        {
                                            if (radioButton3.Checked || radioButton4.Checked)
                                            {
                                                smtp.Connect(smtptet, portsmtp); 
                                                smtp.StartTLS();
                                            }
                                            else smtp.ConnectSSL(smtptet, portsmtp);
                                        }
                                        else
                                        {
                                            if (radioButton3.Checked || radioButton4.Checked)
                                            {
                                                
                                                smtp.Attach(socket1);
                                                smtp.StartTLS();
                                            }
                                            else smtp.AttachSSL(socket1, smtptet);
                                        }
                                        smtp.ReceiveTimeout = new TimeSpan(0, 0, 100);
                                        //MessageBox.Show("Sending Mail");
                                        smtp.UseBestLogin(cred[0], cred[1]);
                                        smtp.SendMessage(reply);
                                        smtp.Close();
                                    }
                                }
                            }
                        }
                       
                        foreach (long uid in unseenReports)        // Download emails from the last result.
                        {

                           // MessageBox.Show(uid.ToString());
                            imap.MoveByUID(uid, folders.Inbox);
                            
                            imap.FlagMessageByUID(uid, Flag.Seen);
                            movedcount = movedcount+1;
                        }
                    }
                    imap.Close();
                }
                
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.ToString());
            }
            finally
            {
                count = count + 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            break1 = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(TestingMSN);
            t.IsBackground = false;
            t.Start();
        }
        private void TestingMSN()
        {
            try
            {
                using (Imap client = new Imap())
                {
                    client.ConnectSSL("imap.aol.com");
                    client.UseBestLogin("allser.game@aol.com", "acidfreak123");
                   // MessageBox.Show(client.Connected.ToString());
                    CommonFolders folders = new CommonFolders(client.GetFolders());
                    client.Select(folders.Spam);
                    foreach (long msg in client.GetAll())
                    {
                       // MessageBox.Show(client.GetMessageByUID(msg));
                        client.FlagMessageByUID(msg, Flag.Seen);
                        client.MoveByUID(msg, folders.Inbox);
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Total Checked : "+count.ToString()+" Total Moved : "+movedcount.ToString();
            progressBar1.Value = count;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Thread t = new Thread(TestingMSN);
            t.IsBackground = false;
            t.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            break2 = false;
            WorkingAccounts.Clear();
            incprogress = 0;
            workingacc = 0;
            totalacc = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = logins.Count;
            Thread t = new Thread(MainThreadCheck);
            t.IsBackground = true;
            t.Start();
        }
        private void MainThreadCheck()
        {
            SmartThreadPool s = new SmartThreadPool();
            s.MaxThreads = Convert.ToInt32(numericUpDown1.Value);
            foreach (string login in logins)
            {
                try
                {
                    if (break2) break;
                    s.QueueWorkItem(dummy => CheckAccount(login), new object());
                }
                catch { }
            }
            try
            {
                s.WaitForIdle();
                s.Shutdown();
            }

            catch { }
            button7.Enabled = true;
            MessageBox.Show("CompleteD");
        }
        private void CheckAccount(string login)
        {
            try
            {
                int type = 0;
                Socket socket = null;
                string imaptext = "imap.gmail.com";
                if (radioButton1.Checked) imaptext = "imap.gmail.com";
                else if (radioButton2.Checked) imaptext = "imap.mail.yahoo.com";
                else imaptext = "imap-mail.outlook.com";
                if (proxies.Count > 0)
                {
                    type = 1;
                    ProxyFactory factory = new ProxyFactory();
                    string proxi = proxies[rad.Next(proxies.Count)];
                    string[] prox = proxi.Split(':');
                    IProxyClient proxy = null;
                    if (prox.Length == 4)
                    {
                        proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]), prox[2], prox[3]);
                        
                    }
                    else proxy = factory.CreateProxy(ProxyType.Http, prox[0], Convert.ToInt32(prox[1]));
                    socket = proxy.Connect(imaptext, Imap.DefaultSSLPort);
                }
                using (Imap imap = new Imap())
                {
                    incprogress = incprogress + 1;
                    if (type == 0) imap.ConnectSSL(imaptext);
                    else imap.AttachSSL(socket, imaptext);
                    string[] cred = login.Split(':');
                    imap.Login(cred[0], cred[1]);
                    WorkingAccounts.Add(login);
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = "Total Working : " + WorkingAccounts.Count.ToString() + " Total Accounts : " + logins.Count.ToString();
            progressBar1.Value = Convert.ToInt32(incprogress);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string FileToSaveAs = saveFileDialog1.FileName;

            System.IO.StreamWriter objwriter = new System.IO.StreamWriter(FileToSaveAs);
            int rr = 0;


            for (rr = 0; rr <= WorkingAccounts.Count - 1; rr++)
            {
                string gul = WorkingAccounts[rr];
                objwriter.WriteLine(gul);

            }
            //MessageBox.Show("Saved!");
            objwriter.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            break2 = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string[] aa = { "1", "2" };
            MessageBox.Show(aa.Length.ToString());
            if (aa[0] == "1") MessageBox.Show("Equal");
            MessageBox.Show(aa[0].Length.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(NewSpin.Spin(textBox1.Text));
        }
    }
}