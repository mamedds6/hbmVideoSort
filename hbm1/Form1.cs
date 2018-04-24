using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hbm1
{
    public partial class Form1 : Form
    {
        private void nazwijLabele()
        {
            foreach (TextBox tb in flp.Controls)
            {
                tb.Size = new Size(40, 20);
                Label label = new Label();
                label.Name = "label" + tb.Name;
                label.Text = tb.Name;
                label.MaximumSize = new Size(20, 13);
                Padding dupa = new Padding(15, 1, 11, 1);
                label.Margin = dupa;
                flowLayoutPanel1.Controls.Add(label);
            }
        }
        public Form1()
        {
            InitializeComponent();
            nazwijLabele();
        }

        private void labelFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void utworzSciezke()
        {
            if (textBoxAuto.Text != "" && textBoxFolder.Text != "")
            {
                string sciezka = textBoxFolder.Text + "\\" + textBoxAuto.Text;
                System.IO.Directory.CreateDirectory(sciezka);
                buttonJedziemy.BackColor = Color.LightGray;
            }
            else
            {
                //textBoxFolder.BackColor = Color.Red;
                //textBoxAuto.BackColor = Color.Red;
                buttonJedziemy.BackColor = Color.Red;
            }
        }

        private void przeniesPliki()
        {
            foreach (TextBox tb in flp.Controls)
            {
                if (tb.Text != "" && int.TryParse(tb.Text, out int n))
                {
                    tb.BackColor = Color.White;
                    string plik = "\\" + tb.Name + " (" + tb.Text + ")." + textBoxRozszerzenie.Text;
                    string org = textBoxFolder.Text + plik;
                    string kopia = textBoxFolder.Text + "\\" + textBoxAuto.Text + plik;
                    if (File.Exists(org))
                        File.Move(org, kopia);
                        //File.Copy(org, kopia, true);                
                    else
                        tb.BackColor = Color.OrangeRed;
                }
                else
                    tb.BackColor = Color.LightGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sprawdza folder i auto i...
            utworzSciezke();

            //sprawdza czy nie puste i liczba w przejezdzie
            //i czy plik istnieje i...
            przeniesPliki();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            flp.Size = new Size(ActiveForm.Size.Width - 100, flp.Height);
            flowLayoutPanel1.Size = new Size(ActiveForm.Size.Width - 100, flowLayoutPanel1.Height);
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxAuto.Text = "";
            buttonJedziemy.BackColor = Color.LightGray;
            foreach (TextBox tb in flp.Controls)
            {
                tb.BackColor = Color.White;
                tb.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) ActiveForm.TopMost = true;
            else ActiveForm.TopMost = false;
        }

        private void flp_Paint(object sender, PaintEventArgs e)
        {
            flowLayoutPanel1.HorizontalScroll.Value = flp.HorizontalScroll.Value;
        }
    }
}
