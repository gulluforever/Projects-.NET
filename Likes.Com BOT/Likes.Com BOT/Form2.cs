using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Likes.Com_BOT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread loginThread = new Thread(loginMethod);
            loginThread.Start();
        }
        private void loginMethod()
        {
            CookieContainer cookieC = new CookieContainer();
            bool loggedIn = MainCode.Login(textBox1.Text + ":" + textBox2.Text, null,cookieC);
            Console.WriteLine(loggedIn);
            if(loggedIn)
            {
                HTTP.GetSourceNormal("http://friendlife.com/lifestylez/friends", Form1.timeout, null, cookieC);
            }
        }
    }
}
