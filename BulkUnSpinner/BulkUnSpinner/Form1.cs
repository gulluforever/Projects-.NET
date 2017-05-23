using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkUnSpinner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        bool stop = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false; //Now One can call GUI update from any thread withour error!
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fbd.SelectedPath))
            {
                button2.Enabled = false;
                progressBar1.Maximum = Convert.ToInt32(numericUpDown1.Value); //converting from decimal value to Integer.
                stop = false;
                progressBar1.Value = 0; //setting up progress to zero..
                Thread t = new Thread(MainLogic);
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                MessageBox.Show("Select Save Folder");
            }
        }
        private void MainLogic()
        {
            for (int i = 1; i <= Convert.ToInt32(numericUpDown1.Value); i++)
            {
                if (stop) break;
                string unspun = NewSpin.Spin(textBox1.Text); //Spinning Stuff
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fbd.SelectedPath + "/" + i.ToString()+".txt"))
                {
                    file.Write(unspun);
                }
                progressBar1.Increment(1);
            }
            MessageBox.Show("Completed");
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stop = true;
        }
    }
}
