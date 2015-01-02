using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using System.IO;
using System.Windows;

namespace TestCompleteLogCompiler
{
    public partial class Form1 : Form
    {

        private const string EXpathInfo = "O campo XPath deve estar preenchido";
        private const string EFilePathInfo = "O campo Arquivo deve estar preenchido";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
                textBox2.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            XPathDocument docNav;
            XPathNavigator nav;
            XPathNodeIterator NodeIter;

            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                if (textBox1.Text.Equals(""))
                    MessageBox.Show(EXpathInfo);

                if (textBox2.Text.Equals(""))
                    MessageBox.Show(EFilePathInfo);
            }
            else
            {
                docNav = new XPathDocument(textBox2.Text);
                nav = docNav.CreateNavigator();

                NodeIter = nav.Select(textBox1.Text);

                while (NodeIter.MoveNext())
                {
                    if (NodeIter.Current != null)
                    {
                        bool exclude = false;
                        foreach (string excludeItem in listBox1.Items)
                        {
                            if (NodeIter.Current.InnerXml.Equals(excludeItem))
                            {
                                exclude = true;
                                break;
                            }
                        }

                        if (!exclude)
                        {
                            richTextBox1.AppendText(NodeIter.Current.InnerXml + "\r\n");
                            richTextBox1.AppendText("------------------\r\n");
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(""))
            {
                bool exclude = false;
                foreach (string excludeItem in listBox1.Items)
                {
                    if (listBox1.Items.Contains(textBox3.Text))
                    {
                        exclude = true;
                        break;
                    }
                }

                if (!exclude) {
                    listBox1.Items.Add(textBox3.Text);
                    textBox3.Clear();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }
}
