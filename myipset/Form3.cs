using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;  // 新增引用

namespace myipset
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        // 在类中添加字段
        private CancellationTokenSource _cts;

        // 添加SendARP导入，用于获取MAC地址
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint destIp, uint srcIp, byte[] macAddr, ref uint phyAddrLen);

        private void Form3_Load(object sender, EventArgs e)
        {
            // 复制 textBoxip1 到 textBoxCping
            textBoxCping.Text = ((Form1)Owner).textBoxip1.Text;
            // 初始化dataGridViewCping
            InitDataGridViewCping();
        }

        // 初始化dataGridViewCping
        private void InitDataGridViewCping()
        {
            DataGridViewCping.ColumnCount = 16; // 设置16列
            DataGridViewCping.RowCount = 16;    // 16行共256个单元格

            // 设置列宽等
            foreach (DataGridViewColumn col in DataGridViewCping.Columns)
            {
                col.Width = 40;
            }
            // 填充单元格文本和背景色置浅灰色
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    int ipSuffix = row * 16 + col;
                    DataGridViewCping.Rows[row].Cells[col].Value = ipSuffix.ToString();
                    DataGridViewCping.Rows[row].Cells[col].Style.BackColor = Color.LightGray;
                    DataGridViewCping.Rows[row].Cells[col].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private List<Tuple<IPAddress, IPAddress>> GetLocalIPv4Addresses()
        {
            List<Tuple<IPAddress, IPAddress>> addresses = new List<Tuple<IPAddress, IPAddress>>();
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        addresses.Add(new Tuple<IPAddress, IPAddress>(ip.Address, ip.IPv4Mask));
                    }
                }
            }
            return addresses;
        }

        private bool IsInSameSubnet(IPAddress address, IPAddress subnetAddress, IPAddress subnetMask)
        {
            byte[] addressBytes = address.GetAddressBytes();
            byte[] subnetAddressBytes = subnetAddress.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (addressBytes.Length != subnetAddressBytes.Length || addressBytes.Length != subnetMaskBytes.Length)
            {
                return false;
            }

            for (int i = 0; i < addressBytes.Length; i++)
            {
                if ((addressBytes[i] & subnetMaskBytes[i]) != (subnetAddressBytes[i] & subnetMaskBytes[i]))
                {
                    return false;
                }
            }
            return true;
        }

        // 根据目标IP获取MAC地址
        private string GetMacAddress(string ipAddress)
        {
            try
            {
                uint destIp = BitConverter.ToUInt32(IPAddress.Parse(ipAddress).GetAddressBytes(), 0);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;
                int result = SendARP(destIp, 0, macAddr, ref macAddrLen);
                if (result != 0)
                {
                    return "";
                }
                return string.Join(":", macAddr.Select(b => b.ToString("X2")));
            }
            catch
            {
                return "";
            }
        }

        private async void ButtonStartPing_Click(object sender, EventArgs e)
        {
            // 创建新的 CancellationTokenSource，每次点击时重建
            _cts = new CancellationTokenSource();

            // 清空 DataGridViewMAC（假定已存在）
            DataGridViewMAC.Rows.Clear();

            // 填充单元格文本和背景色置浅灰色
            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    int ipSuffix = row * 16 + col;
                    DataGridViewCping.Rows[row].Cells[col].Value = ipSuffix.ToString();
                    DataGridViewCping.Rows[row].Cells[col].Style.BackColor = Color.LightGray;
                    DataGridViewCping.Rows[row].Cells[col].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            string ipText = textBoxCping.Text.Trim();
            if (!IPAddress.TryParse(ipText, out IPAddress baseIp))
            {
                MessageBox.Show("无效的IP地址！");
                return;
            }
            // 提取前三个octet，形成 /24 网段前缀
            string[] parts = baseIp.ToString().Split('.');
            if (parts.Length != 4)
            {
                MessageBox.Show("IP地址格式错误！");
                return;
            }
            string prefix = $"{parts[0]}.{parts[1]}.{parts[2]}";  // /24前缀

            // 仅一次判断，检查baseIp是否与本机任意IPv4地址在同一网段
            bool sameNetwork = false;
            var localAddresses = GetLocalIPv4Addresses();
            foreach (var addr in localAddresses)
            {
                if (IsInSameSubnet(baseIp, addr.Item1, addr.Item2))
                {
                    sameNetwork = true;
                    break;
                }
            }

            // 可增加标记提示同网段信息
            if (sameNetwork)
            {
                textBoxCping.BackColor = Color.LightGreen;
                toolTip1.SetToolTip(textBoxCping, "和本机同网段");
            }
            else
            {
                textBoxCping.BackColor = Color.LightSalmon;
                toolTip1.SetToolTip(textBoxCping, "和本机不同网段");
            }

            buttonStartPing.Text = "正在群ping...";
            buttonStartPing.BackColor = Color.Crimson;
            buttonStartPing.Enabled = false;

            // 分批次 Ping，每次 32 个地址，分 8 次完成
            for (int batch = 0; batch < 8; batch++)
            {
                List<Task> pingTasks = new List<Task>();
                for (int i = 0; i < 32; i++)
                {
                    int ipSuffix = batch * 32 + i;
                    string targetIp = $"{prefix}.{ipSuffix}";
                    pingTasks.Add(PingAndUpdateUI(targetIp, ipSuffix, sameNetwork, _cts.Token));
                }
                try
                {
                    await Task.WhenAll(pingTasks);
                }
                catch (OperationCanceledException)
                {
                    // 任务被取消，不再执行后续逻辑
                    return;
                }
            }

            // 如果是同网段群ping，添加本机arp表中存在但未探测到的IP对应的MAC
            if (sameNetwork)
            {
                var arpEntries = GetArpTableEntries();
                foreach (var entry in arpEntries)
                {
                    // 检查该ARP表记录的IP是否属于当前 /24 网段
                    if (entry.Key.StartsWith(prefix + "."))
                    {
                        // 检查是否已存在 DataGridViewMAC 中
                        bool exists = false;
                        foreach (DataGridViewRow row in DataGridViewMAC.Rows)
                        {
                            if (row.Cells[0].Value?.ToString() == entry.Key)
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            DataGridViewMAC.Rows.Add(entry.Key, entry.Value);
                            // 修改 DataGridViewCping 中对应单元格背景色为粉红色
                            string[] ipParts = entry.Key.Split('.');
                            if (ipParts.Length == 4 && int.TryParse(ipParts[3], out int ipSuffix))
                            {
                                int rowIndex = ipSuffix / 16;
                                int colIndex = ipSuffix % 16;
                                DataGridViewCping.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Fuchsia;
                            }
                        }
                    }
                }
            }

            buttonStartPing.BackColor = SystemColors.Control;
            buttonStartPing.Text = "开始群ping";
            buttonStartPing.Enabled = true;
        }

        private async Task PingAndUpdateUI(string targetIp, int ipSuffix, bool sameNetwork, CancellationToken token)
        {
            // 检查取消请求
            token.ThrowIfCancellationRequested();

            Ping pingSender = new Ping();
            PingReply reply = null;
            try
            {
                reply = await pingSender.SendPingAsync(targetIp, 1200);
            }
            catch
            {
                // 出错当作超时处理
            }

            // 根据结果设置颜色
            Color cellColor = Color.LightGray; // 默认不通
            bool online = false;
            long delay = 0;
            if (reply != null && reply.Status == IPStatus.Success)
            {
                online = true;
                delay = reply.RoundtripTime;
                if (delay < 10)
                    cellColor = Color.Lime;
                else if (delay < 100)
                    cellColor = Color.Yellow;
                else if (delay < 1000)
                    cellColor = Color.Orange;
                else
                    cellColor = Color.OrangeRed;
            }

            // 更新 UI 前检查是否取消，并采用 Invoke 确保在 UI 线程上执行
            if (!token.IsCancellationRequested)
            {
                int row = ipSuffix / 16, col = ipSuffix % 16;
                DataGridViewCping.Invoke((MethodInvoker)(() =>
                {
                    var cell = DataGridViewCping.Rows[row].Cells[col];
                    cell.Style.BackColor = cellColor;
                    // 设置提示文字：在线时显示延迟，超时则提示"超时"
                    cell.ToolTipText = online ? delay.ToString() + " ms" : "ping超时";
                }));

                if (online)
                {
                    string mac = sameNetwork ? GetMacAddress(targetIp) : "";
                    DataGridViewMAC.Invoke((MethodInvoker)(() =>
                    {
                        DataGridViewMAC.Rows.Add(targetIp, mac);
                    }));
                }
            }
        }

        /// <summary>
        /// 运行arp -a命令并解析输出，返回一个以IP为键，MAC为值的字典
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetArpTableEntries()
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("arp", "-a")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                    {
                        // 如果该行包含MAC地址分隔符'-'
                        if (line.Contains("-"))
                        {
                            var tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length >= 2)
                            {
                                string ip = tokens[0];
                                string mac = tokens[1].Replace('-', ':').ToUpper();
                                // 排除全FF的MAC地址
                                if (mac == "FF:FF:FF:FF:FF:FF")
                                    continue;

                                entries[ip] = mac;
                            }
                        }
                    }
                }
            }
            catch
            {
                // 错误直接返回空字典
            }
            return entries;
        }

        private void ButtonSaveMac_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*";
                sfd.Title = "请选择保存位置";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataGridViewRow row in DataGridViewMAC.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string ip = row.Cells[0].Value?.ToString() ?? "";
                            string mac = row.Cells[1].Value?.ToString() ?? "";
                            sb.AppendLine($"{ip},{mac}");
                        }
                    }
                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("保存成功！");
                }
            }
        }

        // 没有引用,但是可以解决关闭时候ping线程还没结束的情况下跳出的错误
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 关闭窗口时取消所有后台任务
            _cts?.Cancel();
            base.OnFormClosing(e);
        }
    }
}
