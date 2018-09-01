using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSNAlbum
{
    public partial class Form1 : Form
    {
        private AlbumParser parser;
        private List<string> Links;
        public Form1()
        {
            InitializeComponent();
            this.parser = new AlbumParser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentAlbumLink = textBox1.Text;
            textBox2.Text = "";
            //var result = parser.ParseAlbum(currentAlbumLink.Trim());
            this.Links = parser.ParseAlbum(currentAlbumLink.Trim());
            textBox2.Text = Links.Count.ToString()+"\r\n";
            textBox2.Text += string.Join("\r\n", Links);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";

            Links.ForEach(link => {
                var bestFormat = parser.GetMp3Links(link).Last();
                var prefix = "<a href=\"";
                var mp3Url = bestFormat.Substring(prefix.Length, bestFormat.Length - prefix.Length - 1);
                textBox3.Text += "\r\n"+ mp3Url;
            });
        }
    }
}
