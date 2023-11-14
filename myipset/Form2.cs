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
            config.Name = this.fangAnName.Text;
            config.IP1 = this.textBoxip1.Text;
            config.Mask1 = this.textBoxmask1.Text;
            config.Gateway = this.textBoxgw.Text;
            config.DNS1 = this.textBoxdns1.Text;
            config.DNS2 = this.textBoxdns2.Text;
            config.IP2 = this.textBoxip2.Text;
            config.Mask2 = this.textBoxmask2.Text;
            Console.WriteLine(config.ToString());
            IpClass.netConfigDict.Add(config.Name, config);
            ((Form1)this.Owner).UpdateFanganList();
            ((Form1)this.Owner).SaveConfig();
            this.Close();
        }

        private void Buttoncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}