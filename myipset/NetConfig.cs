using System;
using System.Collections.Generic;
using System.Text;

namespace myipset
{
    public static class IpClass
    {
        public static bool UseDhcp = false;
        public static bool Use2Ip = false;
        public static bool lastUseDhcp = true;
        public static bool lastUse2Ip = true;
        public static bool IpCheckOk = true;
        public static bool NiceEnable = true;
        public static bool NicConnect = true;

        public static string NicName = "";
        public static string NicDefaultName = "以太网";
        public static string NicDescript = "";
        public static string NicMAC = "";
        public static string setip1 = "";
        public static string setmask1 = "";
        public static string setgw = "";
        public static string setdns1 = "";
        public static string setdns2 = "";
        public static string setip2 = "";
        public static string setmask2 = "";
        public static Dictionary<string, NetConfig> netConfigDict = null;
        public static string[] lastArray = { "", "", "", "", "", "", "", "" };
        public static string configfile = "";
    }

    public class NetConfig
    {
        public string Name = "";           // 方案名字
        public string IP1 = "";             // IP地址
        public string Mask1 = "";           // 掩码
        public string Gateway = "";        // 网关
        public string DNS1 = "";            // DNS
        public string DNS2 = "";          // 第二DNS
        public string IP2 = "";           // 第二IP地址
        public string Mask2 = "";         // 第二掩码

        public NetConfig(string config)
        {
            char[] sep = { '#' };
            string[] data = config.Trim().Split(sep);

            if (data.Length == 8)
            {
                Name = data[0].Replace("方案名字:", "");
                IP1 = data[1];
                Mask1 = data[2];
                Gateway = data[3];
                DNS1 = data[4];
                DNS2 = data[5];
                IP2 = data[6];
                Mask2 = data[7];
            }
            Console.WriteLine(data.Length);
        }

        public string Writebackfile()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("方案名字:" + Name + "#");
            strBuilder.Append(IP1 + "#");
            strBuilder.Append(Mask1 + "#");
            strBuilder.Append(Gateway + "#");
            strBuilder.Append(DNS1 + "#");
            strBuilder.Append(DNS2 + "#");
            strBuilder.Append(IP2 + "#");
            strBuilder.Append(Mask2 + "|");
            return strBuilder.ToString();
        }
    }
}