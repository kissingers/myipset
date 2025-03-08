using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myipset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            IpClass.netConfigDict = new Dictionary<string, NetConfig>();
            StartPosition = FormStartPosition.CenterScreen;        //程序居中
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowAdapterInfo();
            NetWorkList();
            ReadConfig();
        }

        private void ComboBoxNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectNetCard();
            ChangeUI();
        }

        private void Buttonapply_Click(object sender, EventArgs e)
        {
            SetNetworkAdapter();
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

        // 网卡列表,这个方法只显示真的的物理网卡列表
        public void NetWorkList()
        {
            //netWorkList.Clear();
            string qry = "SELECT * FROM MSFT_NetAdapter WHERE Virtual=False";
            ManagementScope scope = new ManagementScope(@"\\.\ROOT\StandardCimv2");
            ObjectQuery query = new ObjectQuery(qry);
            ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection moc = mos.Get();
            List<string> netWorkList = new List<string>();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {
                netWorkList.Add(mo["Name"]?.ToString());
                uint ConnectState = Convert.ToUInt32(mo["MediaConnectState"] ?? 0);
                if (ConnectState == 1)    //网卡连接状态0未知  1已连接 2断开
                {
                    IpClass.NicDefaultName = mo["Name"]?.ToString();
                }
            }
            comboBoxnet.DataSource = netWorkList;
            comboBoxnet.SelectedItem = IpClass.NicDefaultName;    //默认选取预定义网卡,最后点亮的物理网卡匹配优先,如果都没有,就默认第一个.
        }

        //显示网卡信息
        public void ShowAdapterInfo()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            traceMessage.Items.Add("主机名字：" + Dns.GetHostName());
            traceMessage.Items.Add("网卡个数：" + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                //显示网络适配器描述信息、名称、类型、速度、MAC 地址
                index++;
                traceMessage.Items.Add("--------------------第" + index + "个适配器信息--------------------");
                traceMessage.Items.Add("网卡名字：" + adapter.Name);
                traceMessage.Items.Add("网卡描述：" + adapter.Description);
                traceMessage.Items.Add("网卡标识：" + adapter.Id);
                traceMessage.Items.Add("网卡类型：" + adapter.NetworkInterfaceType);
                traceMessage.Items.Add("点亮情况：" + adapter.OperationalStatus);
                traceMessage.Items.Add("网卡地址：" + adapter.GetPhysicalAddress());
                traceMessage.Items.Add("网卡速度：" + adapter.Speed / 1000 / 1000 + "MB");

                IPInterfaceProperties ip = adapter.GetIPProperties();

                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    traceMessage.Items.Add("网卡类型：有线网卡");
                    traceMessage.Items.Add("自动获取：" + ip.GetIPv4Properties().IsDhcpEnabled);
                    traceMessage.Items.Add("IPV4MTU：" + ip.GetIPv4Properties().Mtu);
                    traceMessage.Items.Add("IPV6MTU：" + ip.GetIPv6Properties().Mtu);
                }

                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    traceMessage.Items.Add("网卡类型：无线网卡");
                    traceMessage.Items.Add("自动获取：" + ip.GetIPv4Properties().IsDhcpEnabled);
                    traceMessage.Items.Add("IPV4MTU：" + ip.GetIPv4Properties().Mtu);
                    traceMessage.Items.Add("IPV6MTU：" + ip.GetIPv6Properties().Mtu);
                }

                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
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
            traceMessage.Items.Add("-------------------适配器信息输出结束---------------------");
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        // 选择网卡下拉列表时候显示对应的网卡
        public void SelectNetCard()
        {
            if (comboBoxnet.SelectedValue == null)
            {
                MessageBox.Show("请选择网卡下拉列表重新刷新，不存在网卡名: " + comboBoxnet.Text);
                return;
            }

            IpClass.NiceEnable = false;
            IpClass.UseDhcp = false;
            IpClass.NicConnect = false;
            IpClass.Use2Ip = false;

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Name != comboBoxnet.SelectedValue.ToString())
                    continue;        //处理下拉列表,和前面读取的表项比较如果不匹配就继续匹配

                labelnicdes.Text = adapter.Description;
                IpClass.NiceEnable = true;    //匹配成功说明网卡起来了,读取网卡信息

                IPInterfaceProperties ip = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
                GatewayIPAddressInformationCollection gatewayIpAdds = ip.GatewayAddresses;
                IPAddressCollection dnsServers = ip.DnsAddresses;

                IpClass.NicName = adapter.Name;                 //如果匹配先保存网卡名字和描述到ip临时表
                IpClass.NicDescript = adapter.Description;
                textBoxMAC.Text = adapter.GetPhysicalAddress().ToString();
                textBoxMTU.Text = ip.GetIPv4Properties().Mtu.ToString();
                IpClass.UseDhcp = ip.GetIPv4Properties().IsDhcpEnabled;
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    IpClass.NicConnect = true;
                }

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

        // 设置网卡ip地址
        public bool SetNetworkAdapter()
        {
            //检查合格保存当前网卡状态，以备可以回退一次
            Savelastip();

            //如果是地址是自动获取的,上面已经修改为dhcp模式了,完成任务直接结束
            if (IpClass.UseDhcp)
            {
                traceMessage.Items.Add("netsh interface ip set address name=\"" + IpClass.NicName + "\" source=dhcp");
                traceMessage.Items.Add("netsh interface ip set dns name=\"" + IpClass.NicName + "\" source=dhcp");
                RunNetshCommand("interface ip set address name=\"" + IpClass.NicName + "\" source=dhcp");
                RunNetshCommand("interface ip set dns name=\"" + IpClass.NicName + "\" source=dhcp");
                traceMessage.Items.Add("-------------修改网卡动态获取地址结束---------------\r\n");
                SelectNetCard();
                ChangeUI();
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return true;
            }

            //网卡不是dhcp则检查是否激活,不激活直接退出
            if (!IpClass.NicConnect)
            {
                MessageBox.Show("当前网卡未激活，请激活网卡后再设置静态IP！");
                return false;
            }

            //不是动态则检查IP是否合法，不合法直接退出
            if (!Checkinput())
            {
                traceMessage.Items.Add("-----------需要修改的IP不符合规范,更改IP不成功-----------\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return false;
            }

            //处理第一组IP掩码和网关,有变化才改变,避免不必要的更改IP导致网络暂时中断
            if (IpClass.lastUseDhcp || IpClass.setip1 != IpClass.lastArray[1] || IpClass.setmask1 != IpClass.lastArray[2] || IpClass.setgw != IpClass.lastArray[3])
            {
                //如果ip、掩码和网关都不为空,则设置ip地址和子网掩码和网关
                if (!string.IsNullOrEmpty(IpClass.setip1) && !string.IsNullOrEmpty(IpClass.setmask1) && !string.IsNullOrEmpty(IpClass.setgw))
                {
                    traceMessage.Items.Add("netsh interface ipv4 set address \"" + IpClass.NicName + "\" static " + IpClass.setip1 + " " + IpClass.setmask1 + " " + IpClass.setgw);
                    RunNetshCommand("interface ipv4 set address \"" + IpClass.NicName + "\" static " + IpClass.setip1 + " " + IpClass.setmask1 + " " + IpClass.setgw);
                }

                //如果ip和掩码都不为空，但是没网关，则设置ip地址和子网掩码
                if (!string.IsNullOrEmpty(IpClass.setip1) && !string.IsNullOrEmpty(IpClass.setmask1) && string.IsNullOrEmpty(IpClass.setgw))
                {
                    traceMessage.Items.Add("netsh interface ipv4 set address \"" + IpClass.NicName + "\" static " + IpClass.setip1 + " " + IpClass.setmask1);
                    RunNetshCommand("interface ipv4 set address \"" + IpClass.NicName + "\" static " + IpClass.setip1 + " " + IpClass.setmask1);
                }
            }

            //处理第二组IP掩码
            if (IpClass.Use2Ip)
            {
                if (IpClass.lastUseDhcp || !IpClass.lastUse2Ip || IpClass.setip2 != IpClass.lastArray[6] || IpClass.setmask2 != IpClass.lastArray[7])
                {
                    //如果有第二个IP和掩码且不为空，则加入第二个IP和掩码
                    if ((IpClass.Use2Ip) && !string.IsNullOrEmpty(IpClass.setip2) && !string.IsNullOrEmpty(IpClass.setmask2))
                    {
                        traceMessage.Items.Add("netsh interface ipv4 add address \"" + IpClass.NicName + "\" " + IpClass.setip2 + " " + IpClass.setmask2);
                        RunNetshCommand("interface ipv4 add address \"" + IpClass.NicName + "\" " + IpClass.setip2 + " " + IpClass.setmask2);
                    }
                }
            }
            else
            {
                if (IpClass.lastUse2Ip)
                {
                    //如果有第二个IP和掩码且不为空，则加入第二个IP和掩码

                    traceMessage.Items.Add("netsh interface ipv4 delete address \"" + IpClass.NicName + "\" " + IpClass.lastArray[6]);
                    RunNetshCommand("interface ipv4 delete address \"" + IpClass.NicName + "\" " + IpClass.lastArray[6]);
                }
            }

            //处理DNS
            if (IpClass.lastUseDhcp || IpClass.setdns1 != IpClass.lastArray[4] || IpClass.setdns2 != IpClass.lastArray[5])
            {
                //如果任意一个DNS非空,那么设置DNS
                if (!string.IsNullOrEmpty(IpClass.setdns1))
                {
                    traceMessage.Items.Add("netsh interface ipv4 set dns \"" + IpClass.NicName + "\" static " + IpClass.setdns1 + " register=primary");
                    RunNetshCommand("interface ipv4 set dns \"" + IpClass.NicName + "\" static " + IpClass.setdns1 + " register=primary");
                }
                else
                {
                    traceMessage.Items.Add("netsh interface ipv4 delete dns \"" + IpClass.NicName + "\" all");
                    RunNetshCommand("interface ipv4 delete dns \"" + IpClass.NicName + "\" all");
                }

                if (!string.IsNullOrEmpty(IpClass.setdns2))
                {
                    string DNS2Command = $"interface ipv4 add dns \"{IpClass.NicName}\" {IpClass.setdns2}";
                    traceMessage.Items.Add("netsh " + DNS2Command);
                    RunNetshCommand(DNS2Command);
                }
            }
            traceMessage.Items.Add("---------------修改网卡结束-----------------\r\n");
            SelectNetCard();
            ChangeUI();
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            return true;
        }

        public bool CheckIP(string ip)
        {
            // 尝试解析IP地址
            if (!IPAddress.TryParse(ip, out IPAddress address))
            {
                traceMessage.Items.Add("无效的IP地址：" + ip);
                return false;
            }
            // 仅接受IPv4地址
            if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                traceMessage.Items.Add("无效的IPv4地址：" + ip);
                return false;
            }
            // 判断第一段必须在1到223之间
            byte[] bytes = address.GetAddressBytes();
            if (bytes[0] < 1 || bytes[0] > 223)
            {
                traceMessage.Items.Add("IP地址首段必须在1到223之间：" + ip);
                return false;
            }
            traceMessage.Items.Add("这是合法的IP网关DNS地址：" + ip);
            return true;
        }

        // 验证子网掩码正确性,最后一个1后面应该是全0
        public bool CheckMask(string mask)
        {
            // 尝试解析子网掩码
            if (!IPAddress.TryParse(mask, out IPAddress subnet))
            {
                traceMessage.Items.Add("无效的网络掩码：" + mask);
                return false;
            }
            // 仅接受IPv4掩码
            if (subnet.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                traceMessage.Items.Add("无效的IPv4网络掩码：" + mask);
                return false;
            }

            byte[] bytes = subnet.GetAddressBytes();
            // 将4字节转为一个32位整数（大端序）
            uint maskValue = ((uint)bytes[0] << 24) | ((uint)bytes[1] << 16) | ((uint)bytes[2] << 8) | ((uint)bytes[3]);
            if (maskValue == 0)
            {
                traceMessage.Items.Add("无效的网络掩码，掩码不能全为0：" + mask);
                return false;
            }
            // 检查掩码连续性：从最高位开始连续为1，后面必须全为0
            bool foundZero = false;
            for (int i = 31; i >= 0; i--)
            {
                if ((maskValue & (1u << i)) != 0)
                {
                    if (foundZero)
                    {
                        traceMessage.Items.Add("无效的网络掩码，掩码中1和0不连续：" + mask);
                        return false;
                    }
                }
                else
                {
                    foundZero = true;
                }
            }
            traceMessage.Items.Add("这是合法的网络掩码 地址：" + mask);
            return true;
        }

        // 获得网络地址
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

        // 把条框中显示的保存到ip临时表中,并返回是否成功审核
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

            if (string.IsNullOrEmpty(textBoxdns1.Text))
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

            if (string.IsNullOrEmpty(textBoxdns2.Text))
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
                if (string.IsNullOrEmpty(textBoxip2.Text))
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

                if (string.IsNullOrEmpty(textBoxmask2.Text))
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
            if (!string.IsNullOrEmpty(IpClass.setgw) && (!string.IsNullOrEmpty(IpClass.setip1) || !string.IsNullOrEmpty(IpClass.setip2)))
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

        public void SetMACAddress(string newMac)
        {
            //用此方法获取注册表内物理网卡的ID
            string DeviceId = "";
            string netState = "SELECT * From Win32_NetworkAdapter  where PhysicalAdapter=1";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection.Cast<ManagementObject>())
            {
                if (manage["NetConnectionID"].ToString() == comboBoxnet.SelectedValue.ToString())  //直接用列表名匹配
                {
                    DeviceId = int.Parse(manage["DeviceId"].ToString()).ToString("D4");
                    //MessageBox.Show(DeviceId);
                }
            }
            if (DeviceId == "")
            {
                return;
            }

            //所有网卡物理信息所在位置
            RegistryKey NetaddaptRegistry = Registry.LocalMachine.OpenSubKey("SYSTEM")
                .OpenSubKey("CurrentControlSet")
                .OpenSubKey("Control")
                .OpenSubKey("Class")
                .OpenSubKey("{4D36E972-E325-11CE-BFC1-08002bE10318}")
                .OpenSubKey(DeviceId, true);
            //MessageBox.Show("新的MAC地址为: "+ newMac);
            if (string.IsNullOrEmpty(newMac))
            {
                NetaddaptRegistry.DeleteValue("NetworkAddress");
            }
            else
            {
                NetaddaptRegistry.SetValue("NetworkAddress", newMac);
            }
            NetaddaptRegistry.Close();
        }

        // 生成随机MAC地址
        public string CreateNewMacAddress()
        {
            int min = 0;
            int max = 15;
            string MAC = "AA";
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                MAC += rand.Next(min, max).ToString("X");
            }
            //MessageBox.Show("新的随机MAC地址为: " + MAC);
            return MAC;
        }

        //检查mac地址
        public bool CheckMacAddress(string MAC)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]$)";
            if (System.Text.RegularExpressions.Regex.IsMatch(MAC, pattrn))
                return true;
            else
                return false;
        }

        // 禁用网卡
        public static bool DisableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Disable", null);
                return true;
            }
            catch { return false; }
        }

        // 启用网卡
        public static bool EnableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Enable", null);
                return true;
            }
            catch { return false; }
        }

        // 网卡状态
        public static bool NetWorkState(string netWorkName)
        {
            string netState = "SELECT * From Win32_NetworkAdapter  where PhysicalAdapter=1";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection.Cast<ManagementObject>())
            {
                if (manage["NetConnectionID"].ToString() == netWorkName)
                {
                    return true;
                }
            }
            return false;
        }

        // 得到指定网卡
        public static ManagementObject NetWork(string networkname)
        {
            string netState = "SELECT * From Win32_NetworkAdapter  where PhysicalAdapter=1";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject manage in collection.Cast<ManagementObject>())
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
                    { MessageBox.Show("开启网卡失败!"); }
                    else
                    { MessageBox.Show("开启网卡成功!"); }
                }
                else { MessageBox.Show("网卡己开启!"); }

            if (buttonnicenable.Text == "停用")
                if (NetWorkState(comboBoxnet.SelectedValue.ToString()))
                {
                    if (!DisableNetWork(NetWork(comboBoxnet.SelectedValue.ToString())))
                    { MessageBox.Show("禁用网卡失败!"); }
                    else
                    { MessageBox.Show("禁用网卡成功!"); }
                }
                else
                { MessageBox.Show("网卡己禁用!"); }
            Thread.Sleep(1000);
            SelectNetCard();
            Thread.Sleep(1000);
            ChangeUI();
        }

        public void ChangeUI()
        {
            IpClass.HistoryCurrentIndex = -1;
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

        //显示当前路由表
        public void ShowRoute()
        {
            traceMessage.Items.Add("-------------------------------------------------------");
            traceMessage.Items.Add("目的地址\t\t掩码\t\t下一跳\t\t接口\t代价");
            ManagementClass isrouteClass = new ManagementClass("Win32_IP4RouteTable");
            ManagementObjectCollection routeColl = isrouteClass.GetInstances();
            foreach (ManagementObject mor in routeColl.Cast<ManagementObject>())
            {
                string routemessage = mor["Destination"] + "\t";
                if (mor["Destination"].ToString().Length < 8)
                    routemessage += "\t";

                routemessage += mor["Mask"] + "\t";
                if (mor["Mask"].ToString().Length < 8)
                    routemessage += "\t";

                routemessage += mor["NextHop"] + "\t";
                if (mor["NextHop"].ToString().Length < 8)
                    routemessage += "\t";

                routemessage += mor["InterfaceIndex"] + "\t" + mor["Metric1"];
                traceMessage.Items.Add(routemessage);
            }
            traceMessage.Items.Add("-------------------------------------------------------");
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        private void RunNetshCommand(string command)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "netsh.exe";
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
                else
                {
                    Console.WriteLine("Output: " + output);
                }
            }
        }

        //读取配置文件 config.cfg 然后生成一个配置方案的下拉集合
        public void ReadConfig()
        {
            FangAn.Items.Clear();

            if (!File.Exists("config.cfg"))
            {
                File.Create("config.cfg").Close();
            }

            using (StreamReader sr = File.OpenText("config.cfg"))
            {
                IpClass.configfile = sr.ReadToEnd();
            }

            //去掉回车和换行符
            IpClass.configfile = IpClass.configfile.Replace("\n", "").Replace("\r", "");

            //每个方案用|隔开，每个方案的具体地IP用#隔开，用分隔符读取多个方案
            string[] configArray = IpClass.configfile.Split('|');
            foreach (string config in configArray)
            {
                if (config.Length > 0)
                {
                    NetConfig nc = new NetConfig(config);
                    IpClass.netConfigDict.Add(nc.Name, nc);
                    traceMessage.Items.Add($"========== 方案:{nc.Name} ==========");
                    traceMessage.Items.Add($"IP地址\t\t{(nc.IP1 == "" ? "无" : nc.IP1)}");
                    traceMessage.Items.Add($"IP掩码\t\t{(nc.Mask1 != "" ? nc.Mask1 : "无")}");
                    traceMessage.Items.Add($"IP网关\t\t{(nc.Gateway != "" ? nc.Gateway : "无")}");
                    traceMessage.Items.Add($"首选DNS\t\t{(nc.DNS1 != "" ? nc.DNS1 : "无")}");
                    traceMessage.Items.Add($"备选DNS\t\t{(nc.DNS2 != "" ? nc.DNS2 : "无")}");
                    traceMessage.Items.Add($"IP地址2\t\t{(nc.IP2 != "" ? nc.IP2 : "无")}");
                    traceMessage.Items.Add($"IP掩码2\t\t{(nc.Mask2 != "" ? nc.Mask2 : "无")}");
                    FangAn.Items.Add(nc.Name);
                }
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        public void SaveConfig()
        {
            FileStream fs = new FileStream("config.cfg", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            foreach (NetConfig config in IpClass.netConfigDict.Values)
            {
                string saveString = config.Writebackfile();
                traceMessage.Items.Add("写入\t\t" + saveString);
                sw.WriteLine(saveString);
            }
            sw.Close();
            traceMessage.Items.Add("已保存配置方案");
        }

        public void SelectFangAn()
        {
            string name = FangAn.Text;

            // 原有方案选择处理代码
            if (!string.IsNullOrEmpty(name))
            {
                NetConfig config = IpClass.netConfigDict[name];
                IpClass.UseDhcp = false;
                traceMessage.Items.Add("已选择" + config.Name + "\r\n");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                textBoxip1.Text = config.IP1;
                textBoxmask1.Text = config.Mask1;
                textBoxgw.Text = config.Gateway;
                textBoxdns1.Text = config.DNS1;
                textBoxdns2.Text = config.DNS2;
                textBoxip2.Text = config.IP2;
                textBoxmask2.Text = config.Mask2;
                if (!string.IsNullOrEmpty(textBoxip2.Text) && !string.IsNullOrEmpty(textBoxmask2.Text))
                    IpClass.Use2Ip = true;
                else
                    IpClass.Use2Ip = false;
            }
        }

        private void DisplayHistoryRecord(string[] record)
        {
            checkBoxDHCP.Checked = false;
            textBoxip1.Text = record[1];
            textBoxmask1.Text = record[2];
            textBoxgw.Text = record[3];
            textBoxdns1.Text = record[4];
            textBoxdns2.Text = record[5];
            textBoxip2.Text = record[6];
            textBoxmask2.Text = record[7];

            // 根据记录中的第二IP启用标记显示或隐藏第二IP的文本框
            bool use2Ip = record[8] == "true";
            if (use2Ip)
            {
                textBoxip2.Show();
                textBoxmask2.Show();
                labelip2.Show();
                labelmask2.Show();
                checkBox2IP.Checked = true;
            }
            else
            {
                textBoxip2.Hide();
                textBoxmask2.Hide();
                labelip2.Hide();
                labelmask2.Hide();
                checkBox2IP.Checked = false;
            }

            // 将记录显示为黄色高亮
            Color highlightColor = Color.Yellow;
            textBoxip1.BackColor = highlightColor;
            textBoxmask1.BackColor = highlightColor;
            textBoxgw.BackColor = highlightColor;
            textBoxdns1.BackColor = highlightColor;
            textBoxdns2.BackColor = highlightColor;
            textBoxip2.BackColor = highlightColor;
            textBoxmask2.BackColor = highlightColor;
        }

        public void UpdateFanganList()
        {
            FangAn.Items.Clear();
            foreach (NetConfig config in IpClass.netConfigDict.Values)
            {
                FangAn.Items.Add(config.Name);
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
                    continue;
                IPInterfaceProperties ip = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection netIpAdds = ip.UnicastAddresses;
                GatewayIPAddressInformationCollection gatewayIpAdds = ip.GatewayAddresses;
                IPAddressCollection dnsServers = ip.DnsAddresses;

                IpClass.lastUseDhcp = ip.GetIPv4Properties().IsDhcpEnabled;

                int index1 = 0;
                Array.Clear(IpClass.lastArray, 0, IpClass.lastArray.Length);
                foreach (UnicastIPAddressInformation ipadd in netIpAdds)
                {
                    if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        index1++;
                        if (index1 == 1)
                        {
                            IpClass.lastUse2Ip = false;
                            IpClass.lastArray[1] = ipadd.Address.ToString();
                            IpClass.lastArray[2] = ipadd.IPv4Mask.ToString();
                        }
                        if (index1 == 2)
                        {
                            IpClass.lastUse2Ip = true;
                            IpClass.lastArray[6] = ipadd.Address.ToString();
                            IpClass.lastArray[7] = ipadd.IPv4Mask.ToString();
                            IpClass.lastArray[8] = IpClass.Use2Ip ? "true" : "false";
                        }
                    }
                }

                foreach (GatewayIPAddressInformation gateway in gatewayIpAdds)
                {
                    IpClass.lastArray[3] = gateway.Address.ToString();
                }

                int index2 = 0;
                foreach (IPAddress dns in dnsServers)
                {
                    if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        index2++;
                        if (index2 == 1) IpClass.lastArray[4] = dns.ToString();
                        if (index2 == 2) IpClass.lastArray[5] = dns.ToString();
                    }
                }
            }
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;

            // 保存本次记录到历史记录中
            string[] currentRecord = new string[IpClass.lastArray.Length];
            Array.Copy(IpClass.lastArray, currentRecord, IpClass.lastArray.Length);
            IpClass.HistoryRecords.Add(currentRecord);
            // 重置历史记录索引为默认 –1
            IpClass.HistoryCurrentIndex = -1;
        }

        private void FangAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = FangAn.Text;
            if (string.IsNullOrEmpty(name) || name == "自动获取地址" || name == "当前使用地址")
            {
                return;
            }
            if (name == "上次使用地址")
            {
                traceMessage.Items.Add("========== 请参上次使用的地址: ==========");
                if (!string.IsNullOrEmpty(IpClass.lastArray[1])) traceMessage.Items.Add("IP1 地址\t\t" + IpClass.lastArray[1]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[2])) traceMessage.Items.Add("IP1 掩码\t\t" + IpClass.lastArray[2]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[3])) traceMessage.Items.Add("网关地址 \t\t" + IpClass.lastArray[3]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[4])) traceMessage.Items.Add("DNS1地址\t\t" + IpClass.lastArray[4]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[5])) traceMessage.Items.Add("DNS2地址\t\t" + IpClass.lastArray[5]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[6])) traceMessage.Items.Add("IP2 地址\t\t" + IpClass.lastArray[6]);
                if (!string.IsNullOrEmpty(IpClass.lastArray[7])) traceMessage.Items.Add("IP2 掩码\t\t" + IpClass.lastArray[7]);
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                return;
            }
            NetConfig config = IpClass.netConfigDict[name];
            traceMessage.Items.Add("========== 请参考方案:" + FangAn.Text + "的配置 ==========");
            if (!string.IsNullOrEmpty(config.IP1)) traceMessage.Items.Add("IP1 地址\t\t" + config.IP1);
            if (!string.IsNullOrEmpty(config.Mask1)) traceMessage.Items.Add("IP1 掩码\t\t" + config.Mask1);
            if (!string.IsNullOrEmpty(config.Gateway)) traceMessage.Items.Add("网关地址 \t\t" + config.Gateway);
            if (!string.IsNullOrEmpty(config.DNS1)) traceMessage.Items.Add("DNS1地址\t\t" + config.DNS1);
            if (!string.IsNullOrEmpty(config.DNS2)) traceMessage.Items.Add("DNS2地址\t\t" + config.DNS2);
            if (!string.IsNullOrEmpty(config.IP2)) traceMessage.Items.Add("IP2 地址\t\t" + config.IP2);
            if (!string.IsNullOrEmpty(config.Mask2)) traceMessage.Items.Add("IP2 掩码\t\t" + config.Mask2);
            traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
        }

        private void FangAn_DoubleClick(object sender, EventArgs e)
        {
            SelectFangAn();
            SetNetworkAdapter();
            ChangeUI();
        }

        private void 应用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFangAn();
            SetNetworkAdapter();
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = FangAn.Text;
            if (string.IsNullOrEmpty(name) || !IpClass.netConfigDict.ContainsKey(name))
            {
                return;
            }
            NetConfig config = IpClass.netConfigDict[name];
            Form2 f2 = new Form2(config)
            { Owner = this };

            //增加可编辑的下拉列表
            foreach (NetConfig cfg in IpClass.netConfigDict.Values)
            { f2.fangAnName.Items.Add(cfg.Name); }

            f2.fangAnName.Text = FangAn.Text;
            f2.textBoxip1.Text = textBoxip1.Text;
            f2.textBoxmask1.Text = textBoxmask1.Text;
            f2.textBoxgw.Text = textBoxgw.Text;
            f2.textBoxdns1.Text = textBoxdns1.Text;
            f2.textBoxdns2.Text = textBoxdns2.Text;
            f2.textBoxip2.Text = textBoxip2.Text;
            f2.textBoxmask2.Text = textBoxmask2.Text;
            f2.Show();
        }

        private void 参考ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = FangAn.Text;
            if (!string.IsNullOrEmpty(name) && name != "自动获取地址" && name != "当前使用地址" && name != "上次使用地址")
            {
                NetConfig config = IpClass.netConfigDict[name];
                textBoxip1.Text = config.IP1;
                textBoxmask1.Text = config.Mask1;
                textBoxgw.Text = config.Gateway;
                textBoxdns1.Text = config.DNS1;
                textBoxdns2.Text = config.DNS2;
                textBoxip2.Text = config.IP2;
                textBoxmask2.Text = config.Mask2;
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dat = DateTime.Now.ToString();
            NetConfig config = new NetConfig("方案名字:方案" + dat + "#######");
            IpClass.netConfigDict.Add("方案" + dat, config);
            FangAn.Items.Add("方案" + dat);
            FangAn.SetSelected(FangAn.Items.Count - 1, true);

            Form2 f2 = new Form2(config)
            { Owner = this };

            f2.fangAnName.Text = FangAn.Text;
            f2.textBoxip1.Text = textBoxip1.Text;
            f2.textBoxmask1.Text = textBoxmask1.Text;
            f2.textBoxgw.Text = textBoxgw.Text;
            f2.textBoxdns1.Text = textBoxdns1.Text;
            f2.textBoxdns2.Text = textBoxdns2.Text;
            f2.textBoxip2.Text = textBoxip2.Text;
            f2.textBoxmask2.Text = textBoxmask2.Text;
            f2.Show();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = FangAn.Text;
            if (!string.IsNullOrEmpty(name))
            {
                FangAn.Items.Remove(name);
                IpClass.netConfigDict.Remove(name);
                SaveConfig();
            }
        }

        private void Buttonreflash_Click(object sender, EventArgs e)
        {
            SelectNetCard();
            ChangeUI();
        }

        // 保存配置方案
        private void Buttonsaveconfig_Click(object sender, EventArgs e)
        {
            string name = FangAn.Text;
            if (!string.IsNullOrEmpty(name))
            {
                NetConfig config = IpClass.netConfigDict[name];
                config.IP1 = textBoxip1.Text;
                config.Mask1 = textBoxmask1.Text;
                config.Gateway = textBoxgw.Text;
                config.DNS1 = textBoxdns1.Text;
                config.DNS2 = textBoxdns2.Text;
                config.IP2 = textBoxip2.Text;
                config.Mask2 = textBoxmask2.Text;
            }
            SaveConfig();
        }

        private void Button_showroute_Click(object sender, EventArgs e)
        {
            ShowRoute();
        }

        private async void ButtonMAC_change_Click(object sender, EventArgs e)
        {
            if (ConfirmMacChange())
            {
                await ChangeMacAddressAsync(CreateNewMacAddress());
            }
        }

        private async void ButtonMAC_restore_Click(object sender, EventArgs e)
        {
            if (ConfirmMacChange())
            {
                await ChangeMacAddressAsync("");
            }
        }

        private async void ButtonMAC_Self_Click(object sender, EventArgs e)
        {
            if (!CheckMacAddress(textBoxMAC.Text))
            {
                MessageBox.Show("输入的MAC地址不合法，本次更改无效");
                return;
            }
            if (ConfirmMacChange())
            {
                await ChangeMacAddressAsync(textBoxMAC.Text);
            }
        }

        private bool ConfirmMacChange()
        {
            DialogResult result = MessageBox.Show("网络将断开，IP可能会改变，不重启可能不生效，确认更改？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return result == DialogResult.Yes;
        }

        private async Task ChangeMacAddressAsync(string newMac)
        {
            SetMACAddress(newMac);
            DisableNetWork(NetWork(comboBoxnet.SelectedValue.ToString()));
            EnableNetWork(NetWork(comboBoxnet.SelectedValue.ToString()));
            await Task.Delay(9000);     //等待9秒差不多可以重启网卡,并刷新DHCP的IP
            SelectNetCard();
            ChangeUI();
        }

        private void ButtonMTU_self_Click(object sender, EventArgs e)
        {
            // 尝试将文本转换为整数
            if (int.TryParse(textBoxMTU.Text, out int mtuValue))
            {
                // 检查 MTU 值是否在 64 到 9600 之间
                if (mtuValue >= 64 && mtuValue <= 9600)
                {
                    // 设置 IPv4 MTU
                    string ipv4Command = $"interface ipv4 set subinterface \"{IpClass.NicName}\" mtu={mtuValue} store=persistent";
                    traceMessage.Items.Add("netsh " + ipv4Command);
                    RunNetshCommand(ipv4Command);

                    // 设置 IPv6 MTU
                    string ipv6Command = $"interface ipv6 set subinterface \"{IpClass.NicName}\" mtu={mtuValue} store=persistent";
                    traceMessage.Items.Add("netsh " + ipv6Command);
                    RunNetshCommand(ipv6Command);
                    SelectNetCard();
                    ChangeUI();
                }
                else
                {
                    MessageBox.Show("MTU 值必须在 64 到 9600 之间。");
                }
            }
            else
            {
                MessageBox.Show("请输入一个有效的整数作为 MTU 值。");
            }
        }

        private void ButtonMTU_restore_Click(object sender, EventArgs e)
        {
            string ipv4Command = $"interface ipv4 set subinterface \"{IpClass.NicName}\" mtu=1500 store=persistent";
            traceMessage.Items.Add("netsh " + ipv4Command);
            RunNetshCommand(ipv4Command);

            // 设置 IPv6 MTU
            string ipv6Command = $"interface ipv6 set subinterface \"{IpClass.NicName}\" mtu=1500 store=persistent";
            traceMessage.Items.Add("netsh " + ipv6Command);
            RunNetshCommand(ipv6Command);
            SelectNetCard();
            ChangeUI();
        }

        private void ButtonChangeName_Click(object sender, EventArgs e)
        {
            string ChangeNameCommand = $"interface set interface name=\"{IpClass.NicName}\" newname=\"{comboBoxnet.Text}\"";
            traceMessage.Items.Add("netsh " + ChangeNameCommand);
            RunNetshCommand(ChangeNameCommand);
            NetWorkList();
        }

        private void ButtonHistoryPrev_Click(object sender, EventArgs e)
        {
            if (IpClass.HistoryRecords.Count == 0)
            {
                MessageBox.Show("还没有历史记录");
                return;
            }
            // 如果处于默认状态，则设置为最老的历史记录
            if (IpClass.HistoryCurrentIndex == -1)
            {
                IpClass.HistoryCurrentIndex = IpClass.HistoryRecords.Count - 1;
                DisplayHistoryRecord(IpClass.HistoryRecords[IpClass.HistoryCurrentIndex]);
                traceMessage.Items.Add("显示前一条历史记录");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            }
            else if (IpClass.HistoryCurrentIndex > 0)
            {
                IpClass.HistoryCurrentIndex--;
                DisplayHistoryRecord(IpClass.HistoryRecords[IpClass.HistoryCurrentIndex]);
                traceMessage.Items.Add("显示前一条历史记录");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("没有更多历史记录了");
            }
        }

        private void ButtonHistoryNext_Click(object sender, EventArgs e)
        {
            if (IpClass.HistoryRecords.Count == 0)
            {
                MessageBox.Show("还没有历史记录");
                return;
            }
            // 如果当前为默认状态，则提示已经是最新实际IP状态
            if (IpClass.HistoryCurrentIndex == -1)
            {
                MessageBox.Show("已是实际IP状态");
            }
            else if (IpClass.HistoryCurrentIndex < IpClass.HistoryRecords.Count - 1)
            {
                IpClass.HistoryCurrentIndex++;
                DisplayHistoryRecord(IpClass.HistoryRecords[IpClass.HistoryCurrentIndex]);
                traceMessage.Items.Add("显示后一条历史记录");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
            }
            else if (IpClass.HistoryCurrentIndex == IpClass.HistoryRecords.Count - 1)
            {
                // 已经到最新历史记录，再点击则刷新为当前实际IP状态
                IpClass.HistoryCurrentIndex = -1;  // 恢复到默认状态
                SelectNetCard();
                ChangeUI();
                traceMessage.Items.Add("已刷新到当前实际IP状态");
                traceMessage.SelectedIndex = traceMessage.Items.Count - 1;
                MessageBox.Show("已是实际IP状态");
            }
        }

        private void ButtonCallPing_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3
            {
                Owner = this // 'this' should be an instance of Form1
            };
            form3.Show();
        }
    }
}