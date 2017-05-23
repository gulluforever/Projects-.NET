using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Article_Copier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] files = null;
        int fno = 0;
        string text = string.Empty;
        string title = string.Empty;
        string article = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
             DialogResult result = folderBrowserDialog1.ShowDialog();
             if (result == DialogResult.OK)
             {
                 files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
             }
        }
        public static Random rad = new Random();
        private void button2_Click(object sender, EventArgs e)
        {
            readFile();
            title = string.Empty;
            if(!string.IsNullOrEmpty(textBox1.Text) && checkBox2.Checked)
            {
                Console.WriteLine("HERE");
                string keyword = textBox1.Lines[rad.Next(textBox1.Lines.Count())];
                Console.WriteLine(keyword);
                title = keyword + " " + title;
            }
            title = title + text.Substring(0, text.IndexOf(Environment.NewLine));
            Clipboard.SetText(title);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            readFile();
        }

        private void readFile()
        {
            fno = fno + 1;
            if (fno == files.Length) fno = 0;
            using (FileStream fileStream = new FileStream(files[fno], FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    text = sr.ReadToEnd();
                }
            }
            label1.Text = "Article Number : " + fno.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            readFile();
            article = text;
            article = article + Environment.NewLine + GetLsi();
            Clipboard.SetText(article);
            webBrowser1.DocumentText = article;
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(article);
            AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> anchors = document.QuerySelectorAll("a");
            foreach(AngleSharp.Dom.IElement anchor in anchors)
            {
                wName.Text = anchor.InnerHtml;
                wUrl.Text = anchor.GetAttribute("href");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            readFile();
            article = Converters.FormatHtmlIntoBBCode(text);
            article = article + Environment.NewLine + GetLsi();
            Clipboard.SetText(article);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            readFile();
            article = Converters.FormatHTMLtoPlainUrl(text);
            article = article + Environment.NewLine + GetLsi();
            Clipboard.SetText(article);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox1.Checked;
        }
        public static Random r = new Random();
        public string GetLsi()
        {
            if (textBox1.Lines.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 10; i++)
                {
                    int index = r.Next(0, textBox1.Lines.Length);
                    string line = textBox1.Lines[index];
                    sb.AppendLine(line);
                }
                return sb.ToString();
            }
            else return null;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if(checkBox4.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    int index = r.Next(0, textBox1.Lines.Length);
                    string line = textBox1.Lines[index];
                    sb.AppendLine(line);
                }
                textBox2.Text = sb.ToString();
                textBox2.SelectAll();
            }
            else if (checkBox3.Checked)
            {
                textBox2.SelectAll();
                //Clipboard.SetText(textBox2.Text);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            readFile();
            article = text;
            article = article + Environment.NewLine + GetLsi();
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(article);
            AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> anchors = document.QuerySelectorAll("a");
            foreach(AngleSharp.Dom.IElement anchor in anchors)
            {
                string wantReplace = anchor.OuterHtml;
                string link = anchor.GetAttribute("href");
                string anchorKeyword = anchor.InnerHtml;
                string textTile = @"[" + anchorKeyword + "]" + "(" + link + ")";
                article = article.Replace(wantReplace, textTile);
            }
            Clipboard.SetText(article);
        }

        private string getSmall()
        {
            article = GetLsi();
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(text);
            string anchorHTML = string.Empty;
            AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> anchors = document.QuerySelectorAll("a");
            foreach (AngleSharp.Dom.IElement anchor in anchors)
            {
                anchorHTML = anchor.OuterHtml;
            }
            article = anchorHTML + Environment.NewLine + article;
            return article;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            article = getSmall();
            Clipboard.SetText(article);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            article = getSmall();
            article = Converters.FormatHtmlIntoBBCode(article);
            Clipboard.SetText(article);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            article = GetLsi();
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(text);
            string anchorHTML = string.Empty;
            AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> anchors = document.QuerySelectorAll("a");
            foreach (AngleSharp.Dom.IElement anchor in anchors)
            {
                string wantReplace = anchor.OuterHtml;
                string link = anchor.GetAttribute("href");
                string anchorKeyword = anchor.InnerHtml;
                string textTile = @"[" + anchorKeyword + "]" + "(" + link + ")";
                article = textTile + Environment.NewLine + article;
            }
            //article = anchorHTML + Environment.NewLine + article;
            Clipboard.SetText(article);
        }

        private void wName_Click(object sender, EventArgs e)
        {
            wName.SelectAll();
            Clipboard.SetText(wName.Text);
        }

        private void wUrl_Click(object sender, EventArgs e)
        {
            wUrl.SelectAll();
            Clipboard.SetText(wUrl.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            readFile();
            article = text;
            webBrowser1.DocumentText = article;
            webBrowser1.Document.ExecCommand("SelectAll", false, null);
            webBrowser1.Document.ExecCommand("Copy", false, null);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            article = getSmall();
            webBrowser2.DocumentText = article;
            webBrowser2.Document.ExecCommand("SelectAll", false, null);
            webBrowser2.Document.ExecCommand("Copy", false, null);
        }
    }
}

