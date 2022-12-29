using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace myipset
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowAdapterInfo();
            NetWorkList();
            Savelastip();
            ReadConfig();
        }

        private void ComboBoxNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectNetCard();
            ChangeUI();
        }

        private void Buttonapply_Click(object sender, EventArgs e)
        {
            SetNetworkAdapter2();
        }

        private void CheckBoxDHCP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDHCP.Checked)
                IpClass.UseDhcp = true;
            else
                IpClass.UseDhcp = false;
            ChangeUI();
        }


        private void CheckBox2IP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2IP.Checked)
                IpClass.Use2Ip = true;
            else
                IpClass.Use2Ip = false;
            ChangeUI();
        }

        /// <summary>  
        /// 显示网卡信息  
        /// </summary>  
        public void ShowAdapterInfo()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            traceMessage.Items.Add("主机名字：" + Dns.GetHostName());
            traceMessage.Items.Add("网卡个数：" + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                //显示网络适配器描述信息、名称、类型、速度、MAC 地址  
                traceMessage.Items.Add("------------------------第" + index + "个适配器信息------------------------");
                traceMessage.Items.Add("网卡名字：" + adapter.Name);
                traceMessage.Items.Add("网卡描述：" + adapter.Description);
                traceMessage.Items.Add("网卡标识：" + adapter.Id);
                traceMessage.Items.Add("网卡类型：" + adapter.NetworkInterfaceType);
                traceMessage.Items.Add("点亮情况：" + adapter.OperationalStatus);
                traceMessage.Items.Add("网卡地址：" + adapter.GetPhysicalAddress());
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    traceMessage.Items.Add("网卡类型：有线网卡");
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    traceMessage.Items.Add("网卡类型：无线网卡");
                traceMessage.Items.Add("网卡速度：" + adapter.Speed / 1000 / 1000 + "MB");

                IPInterfaceProperties ip = adapter.GetIPProperties();
                traceMessage.Items.Add("自动获取：" + (ip.GetIPv4Properties() == null ? false : ip.GetIPv4Properties().IsDhcpEnabled));
                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                foreach (UnicastIPAddressInformation ipadd in netIpAdds)
                {
                    if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        traceMessage.Items.Add("IPV4地址：" + ipadd.Address);
                    if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        traceMessage.Items.Add("IPV6地址：" + ipadd.Address);
                }

                IPAddressCollection dnsServers = ip.DnsAddresses;
                foreach (IPAddress dns in dnsServers)
                {
                    if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        traceMessage.Items.Add("IPV4域名：" + dns);
                    if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        traceMessage.Items.Add("IPV6域名：" + dns);
                }
            }
            traceMessage.Items.Add("-----------------------适配器信息输出结束-------------------------");
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        /// <summary>  
        /// 选择网卡下拉列表时候显示对应的网卡 
        /// </summary> 
        public void SelectNetCard()
        {
            IpClass.NiceEnable = false;
            IpClass.UseDhcp = false;
            IpClass.NicConnect = false;
            IpClass.Use2Ip = false;


            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (!(adapter.Name == comboBoxnet.SelectedValue.ToString()))
                    continue;        //处理下拉列表,和前面读取的表项比较如果不匹配就继续匹配

                labelnicdes.Text = adapter.Description;
                IpClass.NiceEnable = true;    //匹配成功说明网卡起来了,读取网卡信息

                IPInterfaceProperties ip = adapter.GetIPProperties();
                IPv4InterfaceProperties ipstats = ip.GetIPv4Properties();
                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
                GatewayIPAddressInformationCollection gatewayIpAdds = ip.GatewayAddresses;
                IPAddressCollection dnsServers = ip.DnsAddresses;

                IpClass.NicName = adapter.Name;                 //如果匹配先保存网卡名字和描述到ip临时表
                IpClass.NicDescript = adapter.Description;
                IpClass.NicMAC = adapter.GetPhysicalAddress().ToString();
                IpClass.UseDhcp = ipstats.IsDhcpEnabled;
                if (adapter.OperationalStatus == OperationalStatus.Up)
                    IpClass.NicConnect = true;

                //处理IP和掩码,最多2组IPv4
                int index1 = 0;
                textBoxip1.Text = "";
                textBoxip2.Text = "";
                textBoxmask1.Text = "";
                textBoxmask2.Text = "";
                textBoxgw.Text = "";
                textBoxdns1.Text = "";
                textBoxdns2.Text = "";
                foreach (UnicastIPAddressInformation ipadd in netIpAdds)
                {
                    if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //判断ipV4
                    {
                        index1++;   //多个ip处理
                        if (index1 == 1)
                        {
                            textBoxip1.Text = ipadd.Address.ToString();
                            textBoxmask1.Text = ipadd.IPv4Mask.ToString();
                        }
                        if (index1 == 2)
                        {
                            IpClass.Use2Ip = true;
                            textBoxip2.Text = ipadd.Address.ToString();
                            textBoxmask2.Text = ipadd.IPv4Mask.ToString();
                        }
                    }
                }

                //处理网关
                foreach (GatewayIPAddressInformation gateway in gatewayIpAdds)
                {
                    textBoxgw.Text = gateway.Address.ToString();
                    IpClass.lastgw = gateway.Address.ToString();
                }

                //处理DNS服务器地址,最多2组
                int index2 = 0;
                foreach (IPAddress dns in dnsServers)
                {
                    if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //判断ipv4的dns
                    {
                        index2++;   //多个dns处理
                        if (index2 == 1) textBoxdns1.Text = dns.ToString();
                        if (index2 == 2) textBoxdns2.Text = dns.ToString();
                    }
                }
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }


        /// <summary>  
        /// 选择网卡下拉列表时候显示对应的网卡 
        /// </summary> 
        public void SelectNetCard2()
        {
            IpClass.NiceEnable = false;
            IpClass.UseDhcp = false;
            IpClass.NicConnect = false;
            IpClass.Use2Ip = false;

            //string manage = "SELECT * From Win32_NetworkAdapter where PNPDeviceID LIKE 'PCI%";
            string manage = "SELECT * From Win32_NetworkAdapter where PhysicalAdapter=1";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject obj in collection)
            {
                if (!(obj["NetConnectionID"].ToString() == comboBoxnet.SelectedValue.ToString()))
                    continue;        //处理下拉列表,和前面读取的表项比较如果不匹配就继续匹配

                labelnicdes.Text = obj["Name"].ToString();
                IpClass.NiceEnable = (bool)obj["NetEnabled"];    //读取网卡是否起来的信息,保存网卡名字和描述到ip临时表等
                IpClass.NicName = obj["NetConnectionID"].ToString();
                IpClass.NicDescript = obj["Description"].ToString();
                IpClass.NicMAC = obj["MacAddress"].ToString();
                if (obj["NetConnectionStatus"].ToString() == "2")
                    IpClass.NicConnect = true;

                //处理IP和掩码,最多2组IPv4
                textBoxip1.Text = "";
                textBoxip2.Text = "";
                textBoxmask1.Text = "";
                textBoxmask2.Text = "";
                textBoxgw.Text = "";
                textBoxdns1.Text = "";
                textBoxdns2.Text = "";

                //下面两句等效再下面的三句,不过可以可以通过select先匹配
                //string manageconfig = "SELECT * From Win32_NetworkAdapterConfiguration where Description=IpClass.NicDescript";
                //ManagementObjectSearcher searcherconfig = new ManagementObjectSearcher(manageconfig);
                //ManagementObjectCollection moc = searcherconfig.Get();

                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    if (mo["Description"] == obj["Name"])
                    {
                        IpClass.UseDhcp = (bool)mo["DHCPEnabled"];
                        string[] tmp_ip = (string[])mo["IPAddress"];
                        string[] tmp_mask = (string[])mo["IPSubnet"];
                        string[] tmp_dns = (string[])mo["DNSServerSearchOrder"];

                        foreach (string tmp1ip in tmp_ip)
                        {
                            traceMessage.Items.Add(tmp1ip);
                        }

                        foreach (string tmp1mask in tmp_mask)
                        {
                            traceMessage.Items.Add(tmp1mask);
                        }

                        foreach (string tmp1dns in tmp_dns)
                        {
                            traceMessage.Items.Add(tmp1dns);
                        }
                    }
                }
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }




        /// <summary>
        /// 设置网卡ip地址
        /// </summary>
        public bool SetNetworkAdapter()
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (!(mo["Description"].Equals(IpClass.NicDescript)))    //网卡和选择的描述不相等就继续比较
                    continue;

                //如果匹配,则开始下面的修改过程，先把要改的网卡记录到信息表里面
                traceMessage.Items.Add("修改网卡  " + mo["Description"] + "  IP地址等" + IpClass.setgw + IpClass.lastgw);

                //先判断ip是否合法
                if (IpClass.UseDhcp)
                    IpClass.IpCheckOk = true;
                else
                    IpClass.IpCheckOk = Checkinput();

                //不合法不重置输入界面直接退出
                if (!IpClass.IpCheckOk)
                {
                    traceMessage.Items.Add("----------------需要修改的IP不符合规范,更改IP不成功----------------");
                    traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                    return false;
                }
                //如果是地址是自动获取的,上面已经修改为dhcp模式了,完成任务直接结束
                if (IpClass.UseDhcp)
                {
                    //mo.InvokeMethod("ReleaseDHCPLease", null);
                    mo.InvokeMethod("EnableDHCP", null);
                    mo.InvokeMethod("SetDNSServerSearchOrder", null);
                    traceMessage.Items.Add("--------------------修改网卡自动获取结束---------------------\r\n");
                    traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                    return true;
                }

                //现在网关是空白,但是下拉选择时候不是空白,就说明网关变了,通过dhcp模式清理下,否则不清理,避免网卡无故断开
                if ((IpClass.setgw == "") && (IpClass.lastgw != ""))
                {
                    traceMessage.Items.Add("清理网卡 " + mo["Description"] + " 原来的地址  " + IpClass.lastgw);
                    mo.InvokeMethod("EnableDHCP", null);
                }

                //如果ip和掩码都不为空,则设置ip地址和子网掩码 
                if ((IpClass.setip1 != "") && (IpClass.setmask1 != ""))
                {
                    inPar = mo.GetMethodParameters("EnableStatic");
                    //勾选双IP且第2个ip和掩码都不为空就设置2个ip,否则只设置1个IP(1个IP为空在校验函数中已经禁止为不允许了,所以这里不用校验)
                    if ((IpClass.Use2Ip) && (IpClass.setip2 != "") && (IpClass.setmask2 != ""))
                    {
                        inPar["IPAddress"] = new string[] { IpClass.setip1, IpClass.setip2 };
                        inPar["SubnetMask"] = new string[] { IpClass.setmask1, IpClass.setmask2 };
                    }
                    else
                    {
                        inPar["IPAddress"] = new string[] { IpClass.setip1 };
                        inPar["SubnetMask"] = new string[] { IpClass.setmask1 };
                    }
                    //mo.InvokeMethod("EnableStatic", null);
                    outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                    if (outPar["returnvalue"].ToString() == "0") traceMessage.Items.Add("IP修改成功"); else traceMessage.Items.Add("IP修改失败");
                }

                //如果网关不为空,则设置网关地址 
                if (IpClass.setgw != "")
                {
                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = new string[] { IpClass.setgw };  //如果设置和IP一致,增加个到自己的路由,官方推荐删除网关方法,但是不好
                    outPar = mo.InvokeMethod("SetGateways", inPar, null);
                    traceMessage.Items.Add("修改网卡 3 " + mo["Description"] + "  IP地址等" + IpClass.setgw + IpClass.lastgw);
                    if (outPar.ToString() == "0") traceMessage.Items.Add("网关修改成功"); else traceMessage.Items.Add("网关修改成功");
                }


                //如果任意一个DNS非空,那么设置DNS
                if ((IpClass.setdns1 != "") || (IpClass.setdns2 != ""))
                {
                    inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    inPar["DNSServerSearchOrder"] = new string[] { IpClass.setdns1, IpClass.setdns2 }; //先假定都非空,dns字符串设置2个
                    if (IpClass.setdns1 == "")
                        inPar["DNSServerSearchOrder"] = new string[] { IpClass.setdns2 };              //如果dns1空则设置dns2
                    if (IpClass.setdns2 == "")
                        inPar["DNSServerSearchOrder"] = new string[] { IpClass.setdns1 };              //如果dns2空则设置dns1
                    //mo.InvokeMethod("SetDNSServerSearchOrder", null);
                    outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                    if (outPar["returnvalue"].ToString() == "0") traceMessage.Items.Add("DNS修改成功"); else traceMessage.Items.Add("DNS修改失败");
                }
            }
            traceMessage.Items.Add("-----------------------修改网卡结束-------------------------\r\n");
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            return true;
        }


        /// <summary>
        /// 设置网卡ip地址第二种方法
        /// </summary>
        public bool SetNetworkAdapter2()
        {
            //先判断ip是否合法
            if (IpClass.UseDhcp)
                IpClass.IpCheckOk = true;
            else
                IpClass.IpCheckOk = Checkinput();

            //不合法不重置输入界面直接退出
            if (!IpClass.IpCheckOk)
            {
                traceMessage.Items.Add("----------------需要修改的IP不符合规范,更改IP不成功----------------\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return false;
            }

            //检查合格保存当前网卡状态，以备可以回退一次
            Savelastip();

            //如果是地址是自动获取的,上面已经修改为dhcp模式了,完成任务直接结束
            if (IpClass.UseDhcp)
            {
                traceMessage.Items.Add("运行命令 netsh interface ip set address name =" + IpClass.NicName + " source = dhcp");
                traceMessage.Items.Add("运行命令 netsh interface ip set dns name =" + IpClass.NicName + " source = dhcp");
                RunCommand("interface ip set address name =" + IpClass.NicName + " source = dhcp");
                RunCommand("interface ip set dns name =" + IpClass.NicName + " source = dhcp");
                traceMessage.Items.Add("---------------------修改网卡方法2dhcp结束-----------------------\r\n");
                SelectNetCard();
                ChangeUI();
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return true;
            }

            //如果ip、掩码和网关都不为空,则设置ip地址和子网掩码 
            if ((IpClass.setip1 != "") && (IpClass.setmask1 != "") && (IpClass.setgw != ""))
            {
                traceMessage.Items.Add("interface ipv4 set address name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setip1 + " mask=" + IpClass.setmask1 + " gateway=" + IpClass.setgw);
                RunCommand("interface ipv4 set address name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setip1 + " mask=" + IpClass.setmask1 + " gateway=" + IpClass.setgw);
            }

            //如果ip和掩码都不为空，但是没网关，则设置ip地址和子网掩码 
            if ((IpClass.setip1 != "") && (IpClass.setmask1 != "") && (IpClass.setgw == ""))
            {
                traceMessage.Items.Add("interface ipv4 set address name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setip1 + " mask=" + IpClass.setmask1);
                RunCommand("interface ipv4 set address name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setip1 + " mask=" + IpClass.setmask1);
            }

            //如果有第二个IP和掩码且不为空，则加入第二个IP和掩码
            if ((IpClass.Use2Ip) && (IpClass.setip2 != "") && (IpClass.setmask2 != ""))
            {
                traceMessage.Items.Add("interface ipv4 add address name=" + IpClass.NicName + " addr=" + IpClass.setip2 + " mask=" + IpClass.setmask2);
                RunCommand("interface ipv4 add address name=" + IpClass.NicName + " addr=" + IpClass.setip2 + " mask=" + IpClass.setmask2);
            }

            //如果任意一个DNS非空,那么设置DNS
            if (IpClass.setdns1 != "")
            {
                traceMessage.Items.Add("interface ipv4 set dns name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setdns1 + " register=primary");
                RunCommand("interface ipv4 set dns name=\"" + IpClass.NicName + "\" source =static addr=" + IpClass.setdns1 + " register=primary");
            }
            else
            {
                RunCommand("interface ipv4 delete dns name=\"" + IpClass.NicName + "\" all");
            }
            if (IpClass.setdns2 != "")
            {
                traceMessage.Items.Add("interface ipv4 add dns name=\"" + IpClass.NicName + "\" addr=" + IpClass.setdns2);
                RunCommand("interface ipv4 add dns name=\"" + IpClass.NicName + "\" addr=" + IpClass.setdns2);
            }
            if (!IpClass.NicConnect) MessageBox.Show("当前网卡未激活，设置为静态IP后，仍可能仍然显示为DHCP模式，且多一个169的未获取ip状态的地址，当点亮网卡时候自动生效！");
            traceMessage.Items.Add("---------------------修改网卡方法2结束-----------------------\r\n");
            SelectNetCard();
            ChangeUI();
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            return true;
        }

        public bool CheckIP(string ip)
        {
            //第一位在1到223之间 1-9  01-99   001-009      010-099  100-199 200-223        第二位0-99  000--199  200-249  250-255
            string pattrn = @"^([1-9]|\d[1-9]|[0][0][1-9]|[0][1-9]\d|1\d\d|2[0-1]\d|22[0-3])\.(\d{1,2}|[0-1]\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|[0-1]\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|[0-1]\d\d|2[0-4]\d|25[0-5])$";
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, pattrn))
            {
                traceMessage.Items.Add("这是合法的IP网关DNS地址：" + ip);
                return true;
            }
            else
            {
                traceMessage.Items.Add("这是非法的IP网关DNS地址：" + ip);
                return false;
            }
        }

        /// <summary>
        /// 验证子网掩码正确性,最后一个1后面应该是全0
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public bool CheckMask(string mask)
        {
            string[] vList = mask.Split('.');
            if (vList.Length != 4) return false;   //如果不是4组掩码就无效

            int.TryParse(vList[0], out int m);   //如果掩码第一组是0就无效
            if (m == 0) return false;

            bool vZero = false; // 出现0标记为true
            for (int j = 0; j < vList.Length; j++)
            {
                if (!int.TryParse(vList[j], out int i)) return false;    //没转成数字字符则无效
                if ((i < 0) || (i > 255)) return false;              //超出范围则无效
                if (vZero)
                {
                    if (i != 0) return false;
                }
                else
                {
                    for (int k = 7; k >= 0; k--)
                    {
                        if (((i >> k) & 1) == 0) // 出现0则标记已经有0
                        {
                            vZero = true;
                        }
                        else
                        {
                            if (vZero) return false; // 出现0后有非0位则无效 
                        }
                    }
                }
            }
            traceMessage.Items.Add("这是合法的网络掩码 地址：" + mask);
            return true;
        }


        /// <summary> 
        /// 获得广播地址 
        /// </summary> 
        /// <param name="ipAddress">IP地址</param> 
        /// <param name="subnetMask">子网掩码</param> 
        /// <returns>广播地址</returns> 
        public static string GetBroadcast(string ipAddress, string subnetMask)
        {

            byte[] ip = IPAddress.Parse(ipAddress).GetAddressBytes();
            byte[] sub = IPAddress.Parse(subnetMask).GetAddressBytes();

            // 广播地址=子网按位求反 再 或IP地址 
            for (int i = 0; i < ip.Length; i++)
            {
                ip[i] = (byte)((~sub[i]) | ip[i]);
            }
            return new IPAddress(ip).ToString();
        }


        /// <summary> 
        /// 获得网络地址 
        /// </summary> 
        /// <param name="ipAddress">IP地址</param> 
        /// <param name="subnetMask">子网掩码</param> 
        /// <returns>网络地址</returns> 
        public static string GetNetSegment(string ipAddress, string subnetMask)
        {

            byte[] ip = IPAddress.Parse(ipAddress).GetAddressBytes();
            byte[] sub = IPAddress.Parse(subnetMask).GetAddressBytes();

            // 网络地址=子网按位与IP地址 
            for (int i = 0; i < ip.Length; i++)
            {
                ip[i] = (byte)((sub[i]) & ip[i]);
            }
            return new IPAddress(ip).ToString();
        }



        /// <summary>
        /// 把条框中显示的保存到ip临时表中,并返回是否成功审核
        /// </summary>
        public bool Checkinput()
        {
             if (CheckIP(textBoxip1.Text))
                IpClass.setip1 = textBoxip1.Text;
            else
            {
                MessageBox.Show("无效的IP地址, 确保输入的地址正确");
                traceMessage.Items.Add("错误的 IP 1：" + textBoxip1.Text);
                IpClass.IpCheckOk = false;
                return false;
            }

            if (CheckMask(textBoxmask1.Text))
                IpClass.setmask1 = textBoxmask1.Text;
            else
            {
                MessageBox.Show("无效的掩码地址,确保输入的地址正确");
                traceMessage.Items.Add("错误的掩码1：" + textBoxmask1.Text);
                return false;
            }

            if (textBoxgw.Text == "")
                IpClass.setgw = "";
            else
                if (CheckIP(textBoxgw.Text))
                IpClass.setgw = textBoxgw.Text;
            else
            {
                MessageBox.Show("无效的网关IP地址, 确保输入的地址正确");
                traceMessage.Items.Add("错误的网 关：" + textBoxgw.Text);
                return false;
            }

            if (textBoxdns1.Text == "")
                IpClass.setdns1 = "";
            else
                if (CheckIP(textBoxdns1.Text))
                IpClass.setdns1 = textBoxdns1.Text;
            else
            {
                MessageBox.Show("无效的DNS地址, 确保输入的地址正确");
                traceMessage.Items.Add("错误的 DNS1：" + textBoxdns1.Text);
                return false;
            }


            if (textBoxdns2.Text == "")
                IpClass.setdns2 = "";
            else
                if (CheckIP(textBoxdns2.Text))
                IpClass.setdns2 = textBoxdns2.Text;
            else
            {
                MessageBox.Show("无效的第二DNS地址, 确保输入的地址正确");
                traceMessage.Items.Add("错误的 DNS2：" + textBoxdns2.Text);
                return false;
            }

            if (IpClass.Use2Ip)
            {
                if (textBoxip2.Text == "")
                    IpClass.setip2 = "";
                else
                    if (CheckIP(textBoxip2.Text))
                    IpClass.setip2 = textBoxip2.Text;
                else
                {
                    MessageBox.Show("无效的第二IP地址, 确保输入的地址正确");
                    traceMessage.Items.Add("错误的 IP 2：" + textBoxip2.Text);
                    return false;
                }

                if (textBoxmask2.Text == "")
                    IpClass.setmask2 = "";
                else
                    if (CheckMask(textBoxmask2.Text))
                    IpClass.setmask2 = textBoxmask2.Text;
                else
                {
                    MessageBox.Show("无效的第二掩码地址, 确保输入的地址正确，或者去掉第二IP的勾选");
                    traceMessage.Items.Add("错误的掩码2：" + textBoxmask2.Text);
                    return false;
                }
            }

            //如果ip1和网关不为空,或者ip2和网关不为空
            if (((IpClass.setip1 != "") && (IpClass.setgw != "")) || ((IpClass.setip2 != "") && (IpClass.setgw != "")))
            {
                string Ip1BraodCheck = GetNetSegment(IpClass.setip1, IpClass.setmask1);
                string Gw1BraodCheck = GetNetSegment(IpClass.setgw, IpClass.setmask1);
                if (Ip1BraodCheck != Gw1BraodCheck)
                {
                    if (IpClass.Use2Ip)   //启用第二个IP就检查网关和第二个ip是否同一网段,否则说明第一个IP校验不成功,直接返回失败
                    {
                        string Ip2BraodCheck = GetNetSegment(IpClass.setip2, IpClass.setmask2);
                        string Gw2BraodCheck = GetNetSegment(IpClass.setgw, IpClass.setmask2);
                        if (Ip2BraodCheck != Gw2BraodCheck)
                        {
                            MessageBox.Show("无效的网关地址：IP1和网关不匹配，IP1网段为:" + Ip1BraodCheck + "，IP1网关对应网段为" + Gw1BraodCheck + "；且IP2和网关也不匹配，IP2网段为:" + Ip2BraodCheck + "，IP2网关对应网段为" + Gw2BraodCheck);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("无效的网关地址：IP1和网关不匹配：IP1网段为:" + Ip1BraodCheck + "，IP1网关对应网段为" + Gw1BraodCheck);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 网卡列表
        /// </summary>
        public void NetWorkList()
        {
            string manage = "SELECT * From Win32_NetworkAdapter where PhysicalAdapter=1";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
            ManagementObjectCollection collection = searcher.Get();
            List<string> netWorkList = new List<string>();

            foreach (ManagementObject obj in collection)
            {
                netWorkList.Add(obj["NetConnectionID"].ToString());
            }
            comboBoxnet.DataSource = netWorkList;
            comboBoxnet.SelectedItem = IpClass.NicDefaultName;    //默认选取预定义网卡,如果没有,那么就显示第一个
        }

        /// <summary>
        /// 禁用网卡
        /// </summary>5
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool DisableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Disable", null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 启用网卡
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool EnableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Enable", null);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 网卡状态
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool NetWorkState(string netWorkName)
        {
            string netState = "SELECT * From Win32_NetworkAdapter  where PhysicalAdapter=1";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection)
            {
                if (manage["NetConnectionID"].ToString() == netWorkName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到指定网卡
        /// </summary>
        /// <param name="networkname">网卡名字</param>
        /// <returns></returns>
        public ManagementObject NetWork(string networkname)
        {
            string netState = "SELECT * From Win32_NetworkAdapter  where PhysicalAdapter=1";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject manage in collection)
            {
                if (manage["NetConnectionID"].ToString() == networkname)
                {
                    return manage;
                }
            }
            return null;
        }


        private void Buttonnicenable_Click(object sender, EventArgs e)
        {
            IpClass.NicDefaultName = comboBoxnet.SelectedValue.ToString();       //记住上次选择的网卡
            if (buttonnicenable.Text == "启用")
                if (NetWorkState(comboBoxnet.SelectedValue.ToString()))
                {
                    if (!EnableNetWork(NetWork(comboBoxnet.SelectedValue.ToString())))
                    {
                        MessageBox.Show("开启网卡失败!");
                    }
                    else
                    {
                        MessageBox.Show("开启网卡成功!");
                    }
                }
                else
                {
                    MessageBox.Show("网卡己开启!");
                }

            if (buttonnicenable.Text == "停用")
                if (NetWorkState(comboBoxnet.SelectedValue.ToString()))
                {
                    if (!DisableNetWork(NetWork(comboBoxnet.SelectedValue.ToString())))
                    {
                        MessageBox.Show("禁用网卡失败!");
                    }
                    else
                    {
                        MessageBox.Show("禁用网卡成功!");
                    }
                }
                else
                {
                    MessageBox.Show("网卡己禁用!");
                }
            Thread.Sleep(1000);
            SelectNetCard();
            Thread.Sleep(1000);
            ChangeUI();
        }

        public void ChangeUI()
        {
            if (IpClass.Use2Ip)
            {
                checkBox2IP.Checked = true;
                textBoxip2.Show();
                textBoxmask2.Show();
                labelip2.Show();
                labelmask2.Show();
            }
            else
            {
                checkBox2IP.Checked = false;
                textBoxip2.Hide();
                textBoxmask2.Hide();
                labelip2.Hide();
                labelmask2.Hide();
            }

            if (IpClass.UseDhcp)
                checkBoxDHCP.Checked = true;
            else
                checkBoxDHCP.Checked = false;

            if ((IpClass.NiceEnable) && (!IpClass.NicConnect))          //如果网卡可用且没联网,变红色并显示地址,可编辑
            {
                buttonnicenable.Text = "停用";
                buttonnicenable.ForeColor = Color.FromArgb(255, 0, 128, 0);

                textBoxip1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxip2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxmask1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxmask2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxgw.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxdns1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxdns2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxip1.Enabled = true;
                textBoxip2.Enabled = true;
                textBoxmask1.Enabled = true;
                textBoxmask2.Enabled = true;
                textBoxgw.Enabled = true;
                textBoxdns1.Enabled = true;
                textBoxdns2.Enabled = true;
                checkBoxDHCP.Enabled = true;
                checkBox2IP.Enabled = true;
            }

            if ((!IpClass.NiceEnable) && (!IpClass.NicConnect))         //如果网卡不可用且没联网,变红清空且不可编辑
            {
                buttonnicenable.Text = "启用";
                buttonnicenable.ForeColor = Color.FromArgb(255, 128, 0, 0);
                textBoxip1.Text = "";
                textBoxip2.Text = "";
                textBoxmask1.Text = "";
                textBoxmask2.Text = "";
                textBoxgw.Text = "";
                textBoxdns1.Text = "";
                textBoxdns2.Text = "";

                textBoxip1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxip2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxmask1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxmask2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxgw.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxdns1.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxdns2.BackColor = Color.FromArgb(255, 255, 128, 128);
                textBoxip1.Enabled = false;
                textBoxip2.Enabled = false;
                textBoxmask1.Enabled = false;
                textBoxmask2.Enabled = false;
                textBoxgw.Enabled = false;
                textBoxdns1.Enabled = false;
                textBoxdns2.Enabled = false;
                checkBoxDHCP.Enabled = false;
                checkBox2IP.Enabled = false;
            }

            if (IpClass.UseDhcp && IpClass.NicConnect)  //如果网卡是DHCP且联网了,输入界面变绿色,且不可编辑
            {
                buttonnicenable.Text = "停用";
                buttonnicenable.ForeColor = Color.FromArgb(255, 0, 128, 0);

                textBoxip1.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxip2.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxmask1.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxmask2.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxgw.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxdns1.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxdns2.BackColor = Color.FromArgb(255, 128, 255, 128);
                textBoxip1.Enabled = false;
                textBoxip2.Enabled = false;
                textBoxmask1.Enabled = false;
                textBoxmask2.Enabled = false;
                textBoxgw.Enabled = false;
                textBoxdns1.Enabled = false;
                textBoxdns2.Enabled = false;
                checkBox2IP.Enabled = false;
                checkBoxDHCP.Enabled = true;
            }

            if ((!IpClass.UseDhcp) && (IpClass.NicConnect))   //如果网卡是静态的且网卡起来了,变白色且可编辑
            {
                buttonnicenable.Text = "停用";
                buttonnicenable.ForeColor = Color.FromArgb(255, 0, 128, 0);

                textBoxip1.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxip2.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxmask1.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxmask2.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxgw.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxdns1.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxdns2.BackColor = Color.FromArgb(255, 255, 255, 255);
                textBoxip1.Enabled = true;
                textBoxip2.Enabled = true;
                textBoxmask1.Enabled = true;
                textBoxmask2.Enabled = true;
                textBoxgw.Enabled = true;
                textBoxdns1.Enabled = true;
                textBoxdns2.Enabled = true;
                checkBox2IP.Enabled = true;
                checkBoxDHCP.Enabled = true;
            }

        }
        public void ShowRoute()    //暂时没用到
        {
            ManagementClass isrouteClass = new ManagementClass("Win32_IP4RouteTable");
            ManagementObjectCollection routeColl = isrouteClass.GetInstances();
            foreach (ManagementObject mor in routeColl)
            {
                traceMessage.Items.Add("目的地址:" + mor["Destination"] + " 掩码:" + mor["Mask"] + " 下一跳:" + mor["NextHop"] + " 所在接口:" + mor["InterfaceIndex"] + " 跃点数:" + mor["Metric1"]);
                if ((mor["Destination"].ToString() == "0.0.0.0")) // && (mor["InterfaceIndex"].ToString() == mo["InterfaceIndex"].ToString()))
                {
                    try
                    {
                        traceMessage.Items.Add("正在删除网关为" + mor["NextHop"] + "的默认路由,所在接口: " + mor["InterfaceIndex"]);
                        mor.Delete(); //删除这条路由。
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("添加修改路由表无权限，请以管理员权限执行程序");
                    }
                }
            }
        }

        /// <summary>
        /// Process类执行DOS命令netsh
        /// </summary>
        private string RunCommand(string command)
        {
            string returnStr = null;
            //实例一个新的process类,启动一个新的进程
            Process p = new Process();
            //Process类有一个StartInfo属性
            p.StartInfo.FileName = "netsh.exe";  //设定程序名
            p.StartInfo.Arguments = command;            //设定程式执行参数
            p.StartInfo.Verb = "runas";
            p.StartInfo.UseShellExecute = false;        //关闭Shell的显示
            p.StartInfo.RedirectStandardInput = true;   //重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;  //重定向标准输出
            p.StartInfo.RedirectStandardError = true;   //重定向错误输出
            p.StartInfo.CreateNoWindow = true;          //设置不显示窗口
            p.Start();                                  //启动
            returnStr = p.StandardOutput.ReadToEnd();     //赋值
            p.Dispose();                                //释放资源
            return returnStr;        //从输出流取得命令执行结果
        }

        public void ReadConfig()    
        {
            FangAn.Items.Add("自动获取地址");
            FangAn.Items.Add("当前使用地址");
            FangAn.Items.Add("上次使用地址");

            if (!File.Exists("config.cfg"))
            {
                File.Create("config.cfg").Close();
            }

            StreamReader sr = File.OpenText("config.cfg");
            IpClass.configfile = sr.ReadToEnd();
            sr.Close();

            //去掉回车和换行符
            IpClass.configfile = (IpClass.configfile.Replace("\n", ""));
            IpClass.configfile = (IpClass.configfile.Replace("\r", ""));

            //每个方案用|隔开，每个方案的具体地IP用#隔开，用分隔符读取多个方案
            IpClass.fanganArray = IpClass.configfile.Split(new char[] { '|' });
            //MessageBox.Show(IpClass.fanganArray.Length.ToString());
            foreach (string i in IpClass.fanganArray)
            {
               // MessageBox.Show(i);
                traceMessage.Items.Add(i);
                IpClass.itemArray = i.Split(new char[] { '#' });
                foreach (string j in IpClass.itemArray)
                {
                   // MessageBox.Show(j);
                    traceMessage.Items.Add(j);
                    if (j.Contains("方案名字:") && (!j.Contains("当前使用地址")) && (!j.Contains("上次使用地址")) && (!j.Contains("自动获取地址")))
                    {
                        FangAn.Items.Add(j.Replace("方案名字:", ""));
                       // MessageBox.Show("包含"+j);
                    }
                }
                traceMessage.Items.Add("--------------------------------------------------------\r\n");
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        public void SelectFangAn()
        {
            if (FangAn.Text == "自动获取地址")
            {
                IpClass.UseDhcp = true;
                traceMessage.Items.Add("已改为网卡自动获取地址" + "\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return;
            }
            if (FangAn.Text == "当前使用地址")
            {
                SelectNetCard();
                traceMessage.Items.Add("已改为网卡当前使用地址" + "\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return;
            }
            if (FangAn.Text == "上次使用地址")
            {
                IpClass.UseDhcp = IpClass.lastUseDhcp;
                IpClass.Use2Ip = IpClass.lastUse2Ip;
                textBoxip1.Text = IpClass.lastArray[1];
                textBoxmask1.Text = IpClass.lastArray[2];
                textBoxgw.Text = IpClass.lastArray[3];
                textBoxdns1.Text = IpClass.lastArray[4];
                textBoxdns2.Text = IpClass.lastArray[5];
                textBoxip2.Text = IpClass.lastArray[6];
                textBoxmask2.Text = IpClass.lastArray[7];
                traceMessage.Items.Add("已改为网卡上次使用地址" + "\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return;
            }

            foreach (string i in IpClass.fanganArray)
            {
                IpClass.itemArray = i.Split(new char[] { '#' });
                if (IpClass.itemArray[0].Contains(FangAn.Text))
                {
                    IpClass.UseDhcp = false;
                    traceMessage.Items.Add("已选择"+IpClass.itemArray[0]+"\r\n");
                    traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                    textBoxip1.Text = IpClass.itemArray[1];
                    textBoxmask1.Text = IpClass.itemArray[2];
                    textBoxgw.Text = IpClass.itemArray[3];
                    textBoxdns1.Text = IpClass.itemArray[4];
                    textBoxdns2.Text = IpClass.itemArray[5];
                    textBoxip2.Text = IpClass.itemArray[6];
                    textBoxmask2.Text = IpClass.itemArray[7];
                    if ((textBoxip2.Text != "") && (textBoxmask2.Text != "")) IpClass.Use2Ip = true; else IpClass.Use2Ip = false;
                }
            }
        }

        public void Savelastip()
        {
            IpClass.lastUseDhcp = false;
            IpClass.lastUse2Ip = false;

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (!(adapter.Name == comboBoxnet.SelectedValue.ToString()))
                    continue;        //处理下拉列表,和前面读取的表项比较如果不匹配就继续匹配

                IPInterfaceProperties ip = adapter.GetIPProperties();
                IPv4InterfaceProperties ipstats = ip.GetIPv4Properties();
                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
                GatewayIPAddressInformationCollection gatewayIpAdds = ip.GatewayAddresses;
                IPAddressCollection dnsServers = ip.DnsAddresses;

                IpClass.lastUseDhcp = ipstats.IsDhcpEnabled;

                //处理IP和掩码,最多2组IPv4
                int index1 = 0;
                IpClass.lastArray[1] = "";
                IpClass.lastArray[2] = "";
                IpClass.lastArray[3] = "";
                IpClass.lastArray[4] = "";
                IpClass.lastArray[5] = "";
                IpClass.lastArray[6] = "";
                IpClass.lastArray[7] = "";
                foreach (UnicastIPAddressInformation ipadd in netIpAdds)
                {
                    if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //判断ipV4
                    {
                        index1++;   //多个ip处理
                        if (index1 == 1)
                        {
                            IpClass.lastArray[1] = ipadd.Address.ToString();
                            IpClass.lastArray[2] = ipadd.IPv4Mask.ToString();
                        }
                        if (index1 == 2)
                        {
                            IpClass.lastUse2Ip = true;
                            IpClass.lastArray[6] = ipadd.Address.ToString();
                            IpClass.lastArray[7] = ipadd.IPv4Mask.ToString();
                        }
                    }
                }

                //处理网关
                foreach (GatewayIPAddressInformation gateway in gatewayIpAdds)
                {
                    IpClass.lastArray[3] = gateway.Address.ToString();
                }

                //处理DNS服务器地址,最多2组
                int index2 = 0;
                foreach (IPAddress dns in dnsServers)
                {
                    if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //判断ipv4的dns
                    {
                        index2++;   //多个dns处理
                        if (index2 == 1) IpClass.lastArray[4] = dns.ToString();
                        if (index2 == 2) IpClass.lastArray[5] = dns.ToString();
                    }
                }
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }


        private void FangAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFangAn();
            ChangeUI();
        }



        private void FangAn_DoubleClick(object sender, EventArgs e)
        {
            SelectFangAn();
            SetNetworkAdapter2();
        }


        /// <summary>
        /// 鼠标左键和右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
                private void FangAn_MouseDown(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        MessageBox.Show("left");
                    }
                    else
                    {
                        MessageBox.Show("right");
                    }
                }
        */


        private void 应用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFangAn();
            SetNetworkAdapter2();
        }



        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.fangAnName.Text = this.FangAn.Text;
            f2.textBoxip1.Text = this.textBoxip1.Text;
            f2.textBoxmask1.Text = this.textBoxmask1.Text;
            f2.textBoxgw.Text = this.textBoxgw.Text;
            f2.textBoxdns1.Text = this.textBoxdns1.Text;
            f2.textBoxdns2.Text = this.textBoxdns2.Text;
            f2.textBoxip2.Text = this.textBoxip2.Text;
            f2.textBoxmask2.Text = this.textBoxmask2.Text;
            f2.Show() ; 
        }

        private void 参考ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是参考");
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是新建");




        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是删除");
        }

        private void Buttonreflash_Click(object sender, EventArgs e)
        {
            SelectNetCard();
            ChangeUI();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

public static class IpClass
{
    public static bool UseDhcp = false;
    public static bool Use2Ip = false;
    public static bool IpCheckOk = true;
    public static bool NiceEnable = true;
    public static bool NicConnect = true;
    public static bool lastUseDhcp = true;
    public static bool lastUse2Ip = true;

    public static string NicName = "";
    public static string NicDefaultName = "本地连接";
    public static string NicDescript = "";
    public static string NicMAC = "";
    public static string setip1 = "";
    public static string setmask1 = "";
    public static string setgw = "";
    public static string setdns1 = "";
    public static string setdns2 = "";
    public static string setip2 = "";
    public static string setmask2 = "";
    public static string lastgw = "";
    public static string[] fanganArray = null;
    public static string[] itemArray = null;
    public static string[] lastArray = { "", "", "", "", "", "", "", "" };
    public static string configfile= "";


}


