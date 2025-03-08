namespace myipset
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.networklabel = new System.Windows.Forms.Label();
            this.comboBoxnet = new System.Windows.Forms.ComboBox();
            this.textBoxip1 = new System.Windows.Forms.TextBox();
            this.textBoxmask1 = new System.Windows.Forms.TextBox();
            this.textBoxgw = new System.Windows.Forms.TextBox();
            this.labelip = new System.Windows.Forms.Label();
            this.labelmask1 = new System.Windows.Forms.Label();
            this.labelgw = new System.Windows.Forms.Label();
            this.textBoxdns1 = new System.Windows.Forms.TextBox();
            this.textBoxdns2 = new System.Windows.Forms.TextBox();
            this.labeldns1 = new System.Windows.Forms.Label();
            this.labeldns2 = new System.Windows.Forms.Label();
            this.textBoxip2 = new System.Windows.Forms.TextBox();
            this.labelip2 = new System.Windows.Forms.Label();
            this.labelnetinfo = new System.Windows.Forms.Label();
            this.buttonreflash = new System.Windows.Forms.Button();
            this.buttonsaveconfig = new System.Windows.Forms.Button();
            this.buttonapply = new System.Windows.Forms.Button();
            this.textBoxmask2 = new System.Windows.Forms.TextBox();
            this.labelmask2 = new System.Windows.Forms.Label();
            this.checkBox2IP = new System.Windows.Forms.CheckBox();
            this.labelnicdes = new System.Windows.Forms.Label();
            this.buttonnicenable = new System.Windows.Forms.Button();
            this.checkBoxDHCP = new System.Windows.Forms.CheckBox();
            this.FangAn = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.应用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参考ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceMessage = new System.Windows.Forms.ListBox();
            this.buttonMAC_Random = new System.Windows.Forms.Button();
            this.textBoxMAC = new System.Windows.Forms.TextBox();
            this.buttonMAC_restore = new System.Windows.Forms.Button();
            this.button_showroute = new System.Windows.Forms.Button();
            this.buttonMAC_Self = new System.Windows.Forms.Button();
            this.buttonMTU_self = new System.Windows.Forms.Button();
            this.buttonMTU_restore = new System.Windows.Forms.Button();
            this.textBoxMTU = new System.Windows.Forms.TextBox();
            this.button_MTU = new System.Windows.Forms.Button();
            this.buttonChangeName = new System.Windows.Forms.Button();
            this.buttonHistoryPrev = new System.Windows.Forms.Button();
            this.buttonHistoryNext = new System.Windows.Forms.Button();
            this.buttonCallPing = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // networklabel
            // 
            this.networklabel.AutoSize = true;
            this.networklabel.Font = new System.Drawing.Font("宋体", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.networklabel.Location = new System.Drawing.Point(3, 26);
            this.networklabel.Name = "networklabel";
            this.networklabel.Size = new System.Drawing.Size(84, 16);
            this.networklabel.TabIndex = 1;
            this.networklabel.Text = "选择网卡:";
            // 
            // comboBoxnet
            // 
            this.comboBoxnet.BackColor = System.Drawing.SystemColors.Info;
            this.comboBoxnet.Font = new System.Drawing.Font("宋体", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxnet.Location = new System.Drawing.Point(102, 22);
            this.comboBoxnet.Name = "comboBoxnet";
            this.comboBoxnet.Size = new System.Drawing.Size(138, 24);
            this.comboBoxnet.TabIndex = 11;
            this.comboBoxnet.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNet_SelectedIndexChanged);
            // 
            // textBoxip1
            // 
            this.textBoxip1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxip1.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxip1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxip1.Location = new System.Drawing.Point(102, 192);
            this.textBoxip1.Name = "textBoxip1";
            this.textBoxip1.Size = new System.Drawing.Size(138, 24);
            this.textBoxip1.TabIndex = 1;
            // 
            // textBoxmask1
            // 
            this.textBoxmask1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxmask1.Location = new System.Drawing.Point(102, 222);
            this.textBoxmask1.Name = "textBoxmask1";
            this.textBoxmask1.Size = new System.Drawing.Size(138, 24);
            this.textBoxmask1.TabIndex = 2;
            // 
            // textBoxgw
            // 
            this.textBoxgw.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxgw.Location = new System.Drawing.Point(102, 250);
            this.textBoxgw.Name = "textBoxgw";
            this.textBoxgw.Size = new System.Drawing.Size(138, 24);
            this.textBoxgw.TabIndex = 3;
            // 
            // labelip
            // 
            this.labelip.AutoSize = true;
            this.labelip.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelip.Location = new System.Drawing.Point(3, 195);
            this.labelip.Name = "labelip";
            this.labelip.Size = new System.Drawing.Size(75, 15);
            this.labelip.TabIndex = 7;
            this.labelip.Text = "IP地址1:";
            // 
            // labelmask1
            // 
            this.labelmask1.AutoSize = true;
            this.labelmask1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmask1.Location = new System.Drawing.Point(3, 225);
            this.labelmask1.Name = "labelmask1";
            this.labelmask1.Size = new System.Drawing.Size(75, 15);
            this.labelmask1.TabIndex = 8;
            this.labelmask1.Text = "IP掩码1:";
            // 
            // labelgw
            // 
            this.labelgw.AutoSize = true;
            this.labelgw.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelgw.Location = new System.Drawing.Point(3, 253);
            this.labelgw.Name = "labelgw";
            this.labelgw.Size = new System.Drawing.Size(66, 15);
            this.labelgw.TabIndex = 9;
            this.labelgw.Text = "IP网关:";
            // 
            // textBoxdns1
            // 
            this.textBoxdns1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxdns1.Location = new System.Drawing.Point(102, 303);
            this.textBoxdns1.Name = "textBoxdns1";
            this.textBoxdns1.Size = new System.Drawing.Size(138, 24);
            this.textBoxdns1.TabIndex = 4;
            // 
            // textBoxdns2
            // 
            this.textBoxdns2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxdns2.Location = new System.Drawing.Point(102, 332);
            this.textBoxdns2.Name = "textBoxdns2";
            this.textBoxdns2.Size = new System.Drawing.Size(138, 24);
            this.textBoxdns2.TabIndex = 5;
            // 
            // labeldns1
            // 
            this.labeldns1.AutoSize = true;
            this.labeldns1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeldns1.Location = new System.Drawing.Point(3, 306);
            this.labeldns1.Name = "labeldns1";
            this.labeldns1.Size = new System.Drawing.Size(75, 15);
            this.labeldns1.TabIndex = 12;
            this.labeldns1.Text = "首选DNS:";
            // 
            // labeldns2
            // 
            this.labeldns2.AutoSize = true;
            this.labeldns2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeldns2.Location = new System.Drawing.Point(3, 336);
            this.labeldns2.Name = "labeldns2";
            this.labeldns2.Size = new System.Drawing.Size(75, 15);
            this.labeldns2.TabIndex = 13;
            this.labeldns2.Text = "备用DNS:";
            // 
            // textBoxip2
            // 
            this.textBoxip2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxip2.Location = new System.Drawing.Point(101, 411);
            this.textBoxip2.Name = "textBoxip2";
            this.textBoxip2.Size = new System.Drawing.Size(138, 24);
            this.textBoxip2.TabIndex = 6;
            // 
            // labelip2
            // 
            this.labelip2.AutoSize = true;
            this.labelip2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelip2.Location = new System.Drawing.Point(3, 420);
            this.labelip2.Name = "labelip2";
            this.labelip2.Size = new System.Drawing.Size(75, 15);
            this.labelip2.TabIndex = 15;
            this.labelip2.Text = "IP地址2:";
            // 
            // labelnetinfo
            // 
            this.labelnetinfo.AutoSize = true;
            this.labelnetinfo.Location = new System.Drawing.Point(460, 5);
            this.labelnetinfo.Name = "labelnetinfo";
            this.labelnetinfo.Size = new System.Drawing.Size(59, 12);
            this.labelnetinfo.TabIndex = 16;
            this.labelnetinfo.Text = "信息窗口:";
            // 
            // buttonreflash
            // 
            this.buttonreflash.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonreflash.Location = new System.Drawing.Point(6, 488);
            this.buttonreflash.Name = "buttonreflash";
            this.buttonreflash.Size = new System.Drawing.Size(114, 33);
            this.buttonreflash.TabIndex = 23;
            this.buttonreflash.Text = "刷新当前配置";
            this.buttonreflash.UseVisualStyleBackColor = true;
            this.buttonreflash.Click += new System.EventHandler(this.Buttonreflash_Click);
            // 
            // buttonsaveconfig
            // 
            this.buttonsaveconfig.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonsaveconfig.Location = new System.Drawing.Point(251, 126);
            this.buttonsaveconfig.Name = "buttonsaveconfig";
            this.buttonsaveconfig.Size = new System.Drawing.Size(92, 28);
            this.buttonsaveconfig.TabIndex = 23;
            this.buttonsaveconfig.Text = "保存配置";
            this.buttonsaveconfig.UseVisualStyleBackColor = true;
            this.buttonsaveconfig.Click += new System.EventHandler(this.Buttonsaveconfig_Click);
            // 
            // buttonapply
            // 
            this.buttonapply.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonapply.Location = new System.Drawing.Point(126, 488);
            this.buttonapply.Name = "buttonapply";
            this.buttonapply.Size = new System.Drawing.Size(116, 33);
            this.buttonapply.TabIndex = 23;
            this.buttonapply.Text = "应用当前设置";
            this.buttonapply.UseVisualStyleBackColor = true;
            this.buttonapply.Click += new System.EventHandler(this.Buttonapply_Click);
            // 
            // textBoxmask2
            // 
            this.textBoxmask2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxmask2.Location = new System.Drawing.Point(101, 441);
            this.textBoxmask2.Name = "textBoxmask2";
            this.textBoxmask2.Size = new System.Drawing.Size(138, 24);
            this.textBoxmask2.TabIndex = 7;
            // 
            // labelmask2
            // 
            this.labelmask2.AutoSize = true;
            this.labelmask2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmask2.Location = new System.Drawing.Point(3, 444);
            this.labelmask2.Name = "labelmask2";
            this.labelmask2.Size = new System.Drawing.Size(75, 15);
            this.labelmask2.TabIndex = 25;
            this.labelmask2.Text = "IP掩码2:";
            // 
            // checkBox2IP
            // 
            this.checkBox2IP.AutoSize = true;
            this.checkBox2IP.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2IP.Location = new System.Drawing.Point(6, 386);
            this.checkBox2IP.Name = "checkBox2IP";
            this.checkBox2IP.Size = new System.Drawing.Size(133, 19);
            this.checkBox2IP.TabIndex = 26;
            this.checkBox2IP.Text = "启用第二个IP:";
            this.checkBox2IP.UseVisualStyleBackColor = true;
            this.checkBox2IP.CheckedChanged += new System.EventHandler(this.CheckBox2IP_CheckedChanged);
            // 
            // labelnicdes
            // 
            this.labelnicdes.AutoSize = true;
            this.labelnicdes.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelnicdes.Location = new System.Drawing.Point(3, 2);
            this.labelnicdes.Name = "labelnicdes";
            this.labelnicdes.Size = new System.Drawing.Size(71, 15);
            this.labelnicdes.TabIndex = 27;
            this.labelnicdes.Text = "网卡描述";
            // 
            // buttonnicenable
            // 
            this.buttonnicenable.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonnicenable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonnicenable.Location = new System.Drawing.Point(251, 20);
            this.buttonnicenable.Name = "buttonnicenable";
            this.buttonnicenable.Size = new System.Drawing.Size(63, 28);
            this.buttonnicenable.TabIndex = 23;
            this.buttonnicenable.Text = "启禁";
            this.buttonnicenable.UseVisualStyleBackColor = false;
            this.buttonnicenable.Click += new System.EventHandler(this.Buttonnicenable_Click);
            // 
            // checkBoxDHCP
            // 
            this.checkBoxDHCP.AutoSize = true;
            this.checkBoxDHCP.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxDHCP.Location = new System.Drawing.Point(6, 167);
            this.checkBoxDHCP.Name = "checkBoxDHCP";
            this.checkBoxDHCP.Size = new System.Drawing.Size(149, 19);
            this.checkBoxDHCP.TabIndex = 29;
            this.checkBoxDHCP.Text = "自动获取IP地址:";
            this.checkBoxDHCP.UseVisualStyleBackColor = true;
            this.checkBoxDHCP.CheckedChanged += new System.EventHandler(this.CheckBoxDHCP_CheckedChanged);
            // 
            // FangAn
            // 
            this.FangAn.ContextMenuStrip = this.contextMenuStrip1;
            this.FangAn.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FangAn.ItemHeight = 14;
            this.FangAn.Location = new System.Drawing.Point(251, 160);
            this.FangAn.Name = "FangAn";
            this.FangAn.Size = new System.Drawing.Size(205, 368);
            this.FangAn.TabIndex = 30;
            this.FangAn.SelectedIndexChanged += new System.EventHandler(this.FangAn_SelectedIndexChanged);
            this.FangAn.DoubleClick += new System.EventHandler(this.FangAn_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.应用ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.参考ToolStripMenuItem,
            this.新建ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 114);
            // 
            // 应用ToolStripMenuItem
            // 
            this.应用ToolStripMenuItem.Name = "应用ToolStripMenuItem";
            this.应用ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.应用ToolStripMenuItem.Text = "应用";
            this.应用ToolStripMenuItem.Click += new System.EventHandler(this.应用ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 参考ToolStripMenuItem
            // 
            this.参考ToolStripMenuItem.Name = "参考ToolStripMenuItem";
            this.参考ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.参考ToolStripMenuItem.Text = "参考";
            this.参考ToolStripMenuItem.Click += new System.EventHandler(this.参考ToolStripMenuItem_Click);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // traceMessage
            // 
            this.traceMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.traceMessage.FormattingEnabled = true;
            this.traceMessage.HorizontalScrollbar = true;
            this.traceMessage.ItemHeight = 12;
            this.traceMessage.Location = new System.Drawing.Point(462, 20);
            this.traceMessage.Name = "traceMessage";
            this.traceMessage.Size = new System.Drawing.Size(436, 508);
            this.traceMessage.TabIndex = 31;
            // 
            // buttonMAC_Random
            // 
            this.buttonMAC_Random.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMAC_Random.Location = new System.Drawing.Point(2, 52);
            this.buttonMAC_Random.Name = "buttonMAC_Random";
            this.buttonMAC_Random.Size = new System.Drawing.Size(85, 28);
            this.buttonMAC_Random.TabIndex = 33;
            this.buttonMAC_Random.Text = "随机MAC";
            this.buttonMAC_Random.UseVisualStyleBackColor = true;
            this.buttonMAC_Random.Click += new System.EventHandler(this.ButtonMAC_change_Click);
            // 
            // textBoxMAC
            // 
            this.textBoxMAC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxMAC.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMAC.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMAC.Location = new System.Drawing.Point(102, 55);
            this.textBoxMAC.Name = "textBoxMAC";
            this.textBoxMAC.Size = new System.Drawing.Size(138, 24);
            this.textBoxMAC.TabIndex = 34;
            // 
            // buttonMAC_restore
            // 
            this.buttonMAC_restore.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMAC_restore.Location = new System.Drawing.Point(364, 52);
            this.buttonMAC_restore.Name = "buttonMAC_restore";
            this.buttonMAC_restore.Size = new System.Drawing.Size(92, 28);
            this.buttonMAC_restore.TabIndex = 35;
            this.buttonMAC_restore.Text = "恢复默认";
            this.buttonMAC_restore.UseVisualStyleBackColor = true;
            this.buttonMAC_restore.Click += new System.EventHandler(this.ButtonMAC_restore_Click);
            // 
            // button_showroute
            // 
            this.button_showroute.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_showroute.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_showroute.Location = new System.Drawing.Point(393, 21);
            this.button_showroute.Name = "button_showroute";
            this.button_showroute.Size = new System.Drawing.Size(63, 28);
            this.button_showroute.TabIndex = 36;
            this.button_showroute.Text = "查路由";
            this.button_showroute.UseVisualStyleBackColor = false;
            this.button_showroute.Click += new System.EventHandler(this.Button_showroute_Click);
            // 
            // buttonMAC_Self
            // 
            this.buttonMAC_Self.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMAC_Self.Location = new System.Drawing.Point(251, 52);
            this.buttonMAC_Self.Name = "buttonMAC_Self";
            this.buttonMAC_Self.Size = new System.Drawing.Size(92, 28);
            this.buttonMAC_Self.TabIndex = 35;
            this.buttonMAC_Self.Text = "手动MAC";
            this.buttonMAC_Self.UseVisualStyleBackColor = true;
            this.buttonMAC_Self.Click += new System.EventHandler(this.ButtonMAC_Self_Click);
            // 
            // buttonMTU_self
            // 
            this.buttonMTU_self.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMTU_self.Location = new System.Drawing.Point(251, 88);
            this.buttonMTU_self.Name = "buttonMTU_self";
            this.buttonMTU_self.Size = new System.Drawing.Size(92, 30);
            this.buttonMTU_self.TabIndex = 39;
            this.buttonMTU_self.Text = "手动MTU";
            this.buttonMTU_self.UseVisualStyleBackColor = true;
            this.buttonMTU_self.Click += new System.EventHandler(this.ButtonMTU_self_Click);
            // 
            // buttonMTU_restore
            // 
            this.buttonMTU_restore.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMTU_restore.Location = new System.Drawing.Point(364, 88);
            this.buttonMTU_restore.Name = "buttonMTU_restore";
            this.buttonMTU_restore.Size = new System.Drawing.Size(92, 30);
            this.buttonMTU_restore.TabIndex = 40;
            this.buttonMTU_restore.Text = "恢复默认";
            this.buttonMTU_restore.UseVisualStyleBackColor = true;
            this.buttonMTU_restore.Click += new System.EventHandler(this.ButtonMTU_restore_Click);
            // 
            // textBoxMTU
            // 
            this.textBoxMTU.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxMTU.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMTU.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMTU.Location = new System.Drawing.Point(101, 91);
            this.textBoxMTU.Name = "textBoxMTU";
            this.textBoxMTU.Size = new System.Drawing.Size(138, 24);
            this.textBoxMTU.TabIndex = 38;
            // 
            // button_MTU
            // 
            this.button_MTU.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_MTU.Location = new System.Drawing.Point(2, 89);
            this.button_MTU.Name = "button_MTU";
            this.button_MTU.Size = new System.Drawing.Size(85, 28);
            this.button_MTU.TabIndex = 37;
            this.button_MTU.Text = "当前MTU";
            this.button_MTU.UseVisualStyleBackColor = true;
            // 
            // buttonChangeName
            // 
            this.buttonChangeName.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonChangeName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonChangeName.Location = new System.Drawing.Point(322, 21);
            this.buttonChangeName.Name = "buttonChangeName";
            this.buttonChangeName.Size = new System.Drawing.Size(63, 28);
            this.buttonChangeName.TabIndex = 41;
            this.buttonChangeName.Text = "改名";
            this.buttonChangeName.UseVisualStyleBackColor = false;
            this.buttonChangeName.Click += new System.EventHandler(this.ButtonChangeName_Click);
            // 
            // buttonHistoryPrev
            // 
            this.buttonHistoryPrev.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonHistoryPrev.Location = new System.Drawing.Point(6, 126);
            this.buttonHistoryPrev.Name = "buttonHistoryPrev";
            this.buttonHistoryPrev.Size = new System.Drawing.Size(107, 28);
            this.buttonHistoryPrev.TabIndex = 42;
            this.buttonHistoryPrev.Text = "前一次IP";
            this.buttonHistoryPrev.UseVisualStyleBackColor = true;
            this.buttonHistoryPrev.Click += new System.EventHandler(this.ButtonHistoryPrev_Click);
            // 
            // buttonHistoryNext
            // 
            this.buttonHistoryNext.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonHistoryNext.Location = new System.Drawing.Point(132, 126);
            this.buttonHistoryNext.Name = "buttonHistoryNext";
            this.buttonHistoryNext.Size = new System.Drawing.Size(107, 28);
            this.buttonHistoryNext.TabIndex = 43;
            this.buttonHistoryNext.Text = "后一次IP";
            this.buttonHistoryNext.UseVisualStyleBackColor = true;
            this.buttonHistoryNext.Click += new System.EventHandler(this.ButtonHistoryNext_Click);
            // 
            // buttonCallPing
            // 
            this.buttonCallPing.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCallPing.ForeColor = System.Drawing.Color.Blue;
            this.buttonCallPing.Location = new System.Drawing.Point(364, 126);
            this.buttonCallPing.Name = "buttonCallPing";
            this.buttonCallPing.Size = new System.Drawing.Size(92, 28);
            this.buttonCallPing.TabIndex = 44;
            this.buttonCallPing.Text = "Cping工具";
            this.buttonCallPing.UseVisualStyleBackColor = true;
            this.buttonCallPing.Click += new System.EventHandler(this.ButtonCallPing_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonapply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(900, 531);
            this.Controls.Add(this.buttonCallPing);
            this.Controls.Add(this.buttonHistoryNext);
            this.Controls.Add(this.buttonHistoryPrev);
            this.Controls.Add(this.buttonChangeName);
            this.Controls.Add(this.buttonMTU_self);
            this.Controls.Add(this.buttonMTU_restore);
            this.Controls.Add(this.textBoxMTU);
            this.Controls.Add(this.button_MTU);
            this.Controls.Add(this.button_showroute);
            this.Controls.Add(this.buttonMAC_Self);
            this.Controls.Add(this.buttonMAC_restore);
            this.Controls.Add(this.textBoxMAC);
            this.Controls.Add(this.buttonMAC_Random);
            this.Controls.Add(this.traceMessage);
            this.Controls.Add(this.FangAn);
            this.Controls.Add(this.checkBoxDHCP);
            this.Controls.Add(this.labelnicdes);
            this.Controls.Add(this.checkBox2IP);
            this.Controls.Add(this.labelmask2);
            this.Controls.Add(this.textBoxmask2);
            this.Controls.Add(this.buttonapply);
            this.Controls.Add(this.buttonsaveconfig);
            this.Controls.Add(this.buttonnicenable);
            this.Controls.Add(this.buttonreflash);
            this.Controls.Add(this.labelnetinfo);
            this.Controls.Add(this.labelip2);
            this.Controls.Add(this.textBoxip2);
            this.Controls.Add(this.labeldns2);
            this.Controls.Add(this.labeldns1);
            this.Controls.Add(this.textBoxdns2);
            this.Controls.Add(this.textBoxdns1);
            this.Controls.Add(this.labelgw);
            this.Controls.Add(this.labelmask1);
            this.Controls.Add(this.labelip);
            this.Controls.Add(this.textBoxgw);
            this.Controls.Add(this.textBoxmask1);
            this.Controls.Add(this.textBoxip1);
            this.Controls.Add(this.comboBoxnet);
            this.Controls.Add(this.networklabel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "修改器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label networklabel;
        public System.Windows.Forms.TextBox textBoxmask1;
        private System.Windows.Forms.TextBox textBoxgw;
        private System.Windows.Forms.Label labelip;
        private System.Windows.Forms.Label labelmask1;
        private System.Windows.Forms.Label labelgw;
        private System.Windows.Forms.TextBox textBoxdns1;
        private System.Windows.Forms.TextBox textBoxdns2;
        private System.Windows.Forms.Label labeldns1;
        private System.Windows.Forms.Label labeldns2;
        private System.Windows.Forms.TextBox textBoxip2;
        private System.Windows.Forms.Label labelip2;
        private System.Windows.Forms.Label labelnetinfo;
        private System.Windows.Forms.Button buttonreflash;
        private System.Windows.Forms.Button buttonsaveconfig;
        private System.Windows.Forms.Button buttonapply;
        private System.Windows.Forms.TextBox textBoxmask2;
        private System.Windows.Forms.Label labelmask2;
        private System.Windows.Forms.CheckBox checkBox2IP;
        private System.Windows.Forms.Label labelnicdes;
        private System.Windows.Forms.Button buttonnicenable;
        private System.Windows.Forms.CheckBox checkBoxDHCP;
        private System.Windows.Forms.ListBox traceMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 应用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参考ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        public System.Windows.Forms.ListBox FangAn;
        public System.Windows.Forms.TextBox textBoxip1;
        private System.Windows.Forms.Button buttonMAC_Random;
        public System.Windows.Forms.TextBox textBoxMAC;
        private System.Windows.Forms.Button buttonMAC_restore;
        private System.Windows.Forms.Button button_showroute;
        public System.Windows.Forms.ComboBox comboBoxnet;
        private System.Windows.Forms.Button buttonMAC_Self;
        private System.Windows.Forms.Button buttonMTU_self;
        private System.Windows.Forms.Button buttonMTU_restore;
        public System.Windows.Forms.TextBox textBoxMTU;
        private System.Windows.Forms.Button button_MTU;
        private System.Windows.Forms.Button buttonChangeName;
        private System.Windows.Forms.Button buttonHistoryPrev;
        private System.Windows.Forms.Button buttonHistoryNext;
        private System.Windows.Forms.Button buttonCallPing;
    }
}

