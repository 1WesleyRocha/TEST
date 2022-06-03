using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3UEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;

                openFileDialog.InitialDirectory = "D:\\Download";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();

                        string[] stringSeparators = new string[] { "#EXTINF:-1" };
                        var links = fileContent.Split(stringSeparators, StringSplitOptions.None);
                        foreach (var link in links)
                        {
                            string[] stringSep = new string[] { "tvg-logo=\"", "\",", "tvg-name=", "group-title=" };
                            var linksLogo = link.Split(stringSep, StringSplitOptions.None);
                            if (linksLogo[0].ToString().Contains("#EXTM3U"))
                                continue;
                            checkedListBox1.Items.AddRange(new object[]
                        {
                            linksLogo[1].ToString()
                        });
                            checkedListBox1.Location = new System.Drawing.Point(10, 15);
                            checkedListBox1.Size = new System.Drawing.Size(120, 150);
                            this.Controls.Add(checkedListBox1);
                        }
                        checkedListBox1.Visible = true;
                        checkedListBox1.Width = 450;
                        checkedListBox1.Height = 450;
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
