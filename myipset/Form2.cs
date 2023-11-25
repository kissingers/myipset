using System;
using System.Windows.Forms;

namespace myipset
{
    public partial class Form2 : Form
    {
        private readonly NetConfig config;

        public Form2(NetConfig config)
        {
            this.config = config;
            InitializeComponent();
        }

        private void Buttonok_Click(object sender, EventArgs e)
        {
            IpClass.netConfigDict.Remove(config.Name);
            config.Name = fangAnName.Text;
            config.IP1 = textBoxip1.Text;
            config.Mask1 = textBoxmask1.Text;
            config.Gateway = textBoxgw.Text;
            config.DNS1 = textBoxdns1.Text;
            config.DNS2 = textBoxdns2.Text;
            config.IP2 = textBoxip2.Text;
            config.Mask2 = textBoxmask2.Text;
            Console.WriteLine(config.ToString());
            IpClass.netConfigDict.Add(config.Name, config);
            ((Form1)Owner).UpdateFanganList();
            ((Form1)Owner).SaveConfig();
            Close();
        }

        private void Buttoncancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}