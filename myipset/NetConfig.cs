using System;
using System.Text;

namespace myipset
{
    public class NetConfig
    {
        private string name = "";           // 方案名字
        private string ip_1 = "";             // IP地址
        private string mask_1 = "";           // 掩码
        private string gateway = "";        // 网关
        private string dns_1 = "";            // DNS
        private string dns_2 = "";          // 第二DNS
        private string ip_2 = "";           // 第二IP地址
        private string mask_2 = "";         // 第二掩码

        public NetConfig(string config)
        {
            char[] sep = { '#' };
            string[] data = config.Trim().Split(sep);

            if (data.Length == 8)
            {
                this.name = data[0].Replace("方案名字:", "");
                this.ip_1 = data[1];
                this.mask_1 = data[2];
                this.gateway = data[3];
                this.dns_1 = data[4];
                this.dns_2 = data[5];
                this.ip_2 = data[6];
                this.mask_2 = data[7];
            }

            Console.WriteLine(data.Length);
        }

        public NetConfig(string name, string ip_1, string mask_1, string gateway, string dns_1, string dns_2, string ip_2, string mask_2)
        {
            this.name = name;
            this.ip_1 = ip_1;
            this.mask_1 = mask_1;
            this.gateway = gateway;
            this.dns_1 = dns_1;
            this.dns_2 = dns_2;
            this.ip_2 = ip_2;
            this.mask_2 = mask_2;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string IP1
        {
            get { return this.ip_1; }
            set { this.ip_1 = value; }
        }

        public string Mask1
        {
            get { return this.mask_1; }
            set { this.mask_1 = value; }
        }

        public string Gateway
        {
            get { return this.gateway; }
            set { this.gateway = value; }
        }

        public string DNS1
        {
            get { return this.dns_1; }
            set { this.dns_1 = value; }
        }

        public string DNS2
        {
            get { return this.dns_2; }
            set { this.dns_2 = value; }
        }

        public string IP2
        {
            get { return this.ip_2; }
            set { this.ip_2 = value; }
        }

        public string Mask2
        {
            get { return this.mask_2; }
            set { this.mask_2 = value; }
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("方案名字:" + this.name + "#");
            strBuilder.Append(this.ip_1 + "#");
            strBuilder.Append(this.mask_1 + "#");
            strBuilder.Append(this.gateway + "#");
            strBuilder.Append(this.dns_1 + "#");
            strBuilder.Append(this.dns_2 + "#");
            strBuilder.Append(this.ip_2 + "#");
            strBuilder.Append(this.mask_2 + "|");
            return strBuilder.ToString();
        }
    }
}
