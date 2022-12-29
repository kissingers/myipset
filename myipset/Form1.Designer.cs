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
            this.buttonsave = new System.Windows.Forms.Button();
            this.buttonapply = new System.Windows.Forms.Button();
            this.textBoxmask2 = new System.Windows.Forms.TextBox();
            this.labelmask2 = new System.Windows.Forms.Label();
            this.checkBox2IP = new System.Windows.Forms.CheckBox();
            this.labelnicdes = new System.Windows.Forms.Label();
            this.buttonnicenable = new System.Windows.Forms.Button();
            this.buttonnicmac = new System.Windows.Forms.Button();
            this.checkBoxDHCP = new System.Windows.Forms.CheckBox();
            this.FangAn = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.应用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参考ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceMessage = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // networklabel
            // 
            this.networklabel.AutoSize = true;
            this.networklabel.Font = new System.Drawing.Font("宋体", 11.89565F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.networklabel.Location = new System.Drawing.Point(-2, 9);
            this.networklabel.Name = "networklabel";
            this.networklabel.Size = new System.Drawing.Size(100, 19);
            this.networklabel.TabIndex = 1;
            this.networklabel.Text = "选择网卡:";
            // 
            // comboBoxnet
            // 
            this.comboBoxnet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxnet.BackColor = System.Drawing.SystemColors.Info;
            this.comboBoxnet.Font = new System.Drawing.Font("宋体", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxnet.Location = new System.Drawing.Point(101, 6);
            this.comboBoxnet.Name = "comboBoxnet";
            this.comboBoxnet.Size = new System.Drawing.Size(180, 27);
            this.comboBoxnet.TabIndex = 11;
            this.comboBoxnet.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNet_SelectedIndexChanged);
            // 
            // textBoxip1
            // 
            this.textBoxip1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxip1.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxip1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxip1.Location = new System.Drawing.Point(104, 140);
            this.textBoxip1.Name = "textBoxip1";
            this.textBoxip1.Size = new System.Drawing.Size(188, 27);
            this.textBoxip1.TabIndex = 1;
            // 
            // textBoxmask1
            // 
            this.textBoxmask1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxmask1.Location = new System.Drawing.Point(104, 175);
            this.textBoxmask1.Name = "textBoxmask1";
            this.textBoxmask1.Size = new System.Drawing.Size(188, 27);
            this.textBoxmask1.TabIndex = 2;
            // 
            // textBoxgw
            // 
            this.textBoxgw.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxgw.Location = new System.Drawing.Point(104, 210);
            this.textBoxgw.Name = "textBoxgw";
            this.textBoxgw.Size = new System.Drawing.Size(188, 27);
            this.textBoxgw.TabIndex = 3;
            // 
            // labelip
            // 
            this.labelip.AutoSize = true;
            this.labelip.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelip.Location = new System.Drawing.Point(6, 143);
            this.labelip.Name = "labelip";
            this.labelip.Size = new System.Drawing.Size(86, 18);
            this.labelip.TabIndex = 7;
            this.labelip.Text = "IP地址1:";
            // 
            // labelmask1
            // 
            this.labelmask1.AutoSize = true;
            this.labelmask1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmask1.Location = new System.Drawing.Point(6, 175);
            this.labelmask1.Name = "labelmask1";
            this.labelmask1.Size = new System.Drawing.Size(86, 18);
            this.labelmask1.TabIndex = 8;
            this.labelmask1.Text = "IP掩码1:";
            // 
            // labelgw
            // 
            this.labelgw.AutoSize = true;
            this.labelgw.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelgw.Location = new System.Drawing.Point(6, 213);
            this.labelgw.Name = "labelgw";
            this.labelgw.Size = new System.Drawing.Size(76, 18);
            this.labelgw.TabIndex = 9;
            this.labelgw.Text = "IP网关:";
            // 
            // textBoxdns1
            // 
            this.textBoxdns1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxdns1.Location = new System.Drawing.Point(104, 277);
            this.textBoxdns1.Name = "textBoxdns1";
            this.textBoxdns1.Size = new System.Drawing.Size(188, 27);
            this.textBoxdns1.TabIndex = 4;
            // 
            // textBoxdns2
            // 
            this.textBoxdns2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxdns2.Location = new System.Drawing.Point(104, 312);
            this.textBoxdns2.Name = "textBoxdns2";
            this.textBoxdns2.Size = new System.Drawing.Size(188, 27);
            this.textBoxdns2.TabIndex = 5;
            // 
            // labeldns1
            // 
            this.labeldns1.AutoSize = true;
            this.labeldns1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeldns1.Location = new System.Drawing.Point(6, 280);
            this.labeldns1.Name = "labeldns1";
            this.labeldns1.Size = new System.Drawing.Size(86, 18);
            this.labeldns1.TabIndex = 12;
            this.labeldns1.Text = "首选DNS:";
            // 
            // labeldns2
            // 
            this.labeldns2.AutoSize = true;
            this.labeldns2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeldns2.Location = new System.Drawing.Point(6, 315);
            this.labeldns2.Name = "labeldns2";
            this.labeldns2.Size = new System.Drawing.Size(86, 18);
            this.labeldns2.TabIndex = 13;
            this.labeldns2.Text = "备用DNS:";
            // 
            // textBoxip2
            // 
            this.textBoxip2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxip2.Location = new System.Drawing.Point(104, 388);
            this.textBoxip2.Name = "textBoxip2";
            this.textBoxip2.Size = new System.Drawing.Size(188, 27);
            this.textBoxip2.TabIndex = 6;
            // 
            // labelip2
            // 
            this.labelip2.AutoSize = true;
            this.labelip2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelip2.Location = new System.Drawing.Point(7, 390);
            this.labelip2.Name = "labelip2";
            this.labelip2.Size = new System.Drawing.Size(86, 18);
            this.labelip2.TabIndex = 15;
            this.labelip2.Text = "IP地址2:";
            // 
            // labelnetinfo
            // 
            this.labelnetinfo.AutoSize = true;
            this.labelnetinfo.Location = new System.Drawing.Point(559, 9);
            this.labelnetinfo.Name = "labelnetinfo";
            this.labelnetinfo.Size = new System.Drawing.Size(105, 15);
            this.labelnetinfo.TabIndex = 16;
            this.labelnetinfo.Text = "所有网卡信息:";
            // 
            // buttonreflash
            // 
            this.buttonreflash.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonreflash.Location = new System.Drawing.Point(9, 504);
            this.buttonreflash.Name = "buttonreflash";
            this.buttonreflash.Size = new System.Drawing.Size(136, 29);
            this.buttonreflash.TabIndex = 23;
            this.buttonreflash.Text = "刷新当前配置";
            this.buttonreflash.UseVisualStyleBackColor = true;
            this.buttonreflash.Click += new System.EventHandler(this.Buttonreflash_Click);
            // 
            // buttonsave
            // 
            this.buttonsave.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonsave.Location = new System.Drawing.Point(156, 550);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(136, 30);
            this.buttonsave.TabIndex = 23;
            this.buttonsave.Text = "保存配置方案";
            this.buttonsave.UseVisualStyleBackColor = true;
            // 
            // buttonapply
            // 
            this.buttonapply.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonapply.Location = new System.Drawing.Point(156, 504);
            this.buttonapply.Name = "buttonapply";
            this.buttonapply.Size = new System.Drawing.Size(136, 30);
            this.buttonapply.TabIndex = 23;
            this.buttonapply.Text = "应用当前设置";
            this.buttonapply.UseVisualStyleBackColor = true;
            this.buttonapply.Click += new System.EventHandler(this.Buttonapply_Click);
            // 
            // textBoxmask2
            // 
            this.textBoxmask2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxmask2.Location = new System.Drawing.Point(104, 421);
            this.textBoxmask2.Name = "textBoxmask2";
            this.textBoxmask2.Size = new System.Drawing.Size(188, 27);
            this.textBoxmask2.TabIndex = 7;
            // 
            // labelmask2
            // 
            this.labelmask2.AutoSize = true;
            this.labelmask2.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelmask2.Location = new System.Drawing.Point(6, 421);
            this.labelmask2.Name = "labelmask2";
            this.labelmask2.Size = new System.Drawing.Size(86, 18);
            this.labelmask2.TabIndex = 25;
            this.labelmask2.Text = "IP掩码2:";
            // 
            // checkBox2IP
            // 
            this.checkBox2IP.AutoSize = true;
            this.checkBox2IP.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2IP.Location = new System.Drawing.Point(9, 360);
            this.checkBox2IP.Name = "checkBox2IP";
            this.checkBox2IP.Size = new System.Drawing.Size(154, 22);
            this.checkBox2IP.TabIndex = 26;
            this.checkBox2IP.Text = "启用第二个IP:";
            this.checkBox2IP.UseVisualStyleBackColor = true;
            this.checkBox2IP.CheckedChanged += new System.EventHandler(this.CheckBox2IP_CheckedChanged);
            // 
            // labelnicdes
            // 
            this.labelnicdes.AutoSize = true;
            this.labelnicdes.Location = new System.Drawing.Point(12, 41);
            this.labelnicdes.Name = "labelnicdes";
            this.labelnicdes.Size = new System.Drawing.Size(67, 15);
            this.labelnicdes.TabIndex = 27;
            this.labelnicdes.Text = "网卡描述";
            // 
            // buttonnicenable
            // 
            this.buttonnicenable.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonnicenable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonnicenable.Location = new System.Drawing.Point(287, 4);
            this.buttonnicenable.Name = "buttonnicenable";
            this.buttonnicenable.Size = new System.Drawing.Size(70, 29);
            this.buttonnicenable.TabIndex = 23;
            this.buttonnicenable.Text = "默认";
            this.buttonnicenable.UseVisualStyleBackColor = false;
            this.buttonnicenable.Click += new System.EventHandler(this.Buttonnicenable_Click);
            // 
            // buttonnicmac
            // 
            this.buttonnicmac.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonnicmac.Location = new System.Drawing.Point(363, 4);
            this.buttonnicmac.Name = "buttonnicmac";
            this.buttonnicmac.Size = new System.Drawing.Size(70, 29);
            this.buttonnicmac.TabIndex = 28;
            this.buttonnicmac.Text = "改MAC";
            this.buttonnicmac.UseVisualStyleBackColor = true;
            // 
            // checkBoxDHCP
            // 
            this.checkBoxDHCP.AutoSize = true;
            this.checkBoxDHCP.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxDHCP.Location = new System.Drawing.Point(9, 112);
            this.checkBoxDHCP.Name = "checkBoxDHCP";
            this.checkBoxDHCP.Size = new System.Drawing.Size(173, 22);
            this.checkBoxDHCP.TabIndex = 29;
            this.checkBoxDHCP.Text = "自动获取IP地址:";
            this.checkBoxDHCP.UseVisualStyleBackColor = true;
            this.checkBoxDHCP.CheckedChanged += new System.EventHandler(this.CheckBoxDHCP_CheckedChanged);
            // 
            // FangAn
            // 
            this.FangAn.ContextMenuStrip = this.contextMenuStrip1;
            this.FangAn.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FangAn.HorizontalScrollbar = true;
            this.FangAn.ItemHeight = 17;
            this.FangAn.Location = new System.Drawing.Point(298, 140);
            this.FangAn.Name = "FangAn";
            this.FangAn.Size = new System.Drawing.Size(148, 310);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 114);
            // 
            // 应用ToolStripMenuItem
            // 
            this.应用ToolStripMenuItem.Name = "应用ToolStripMenuItem";
            this.应用ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.应用ToolStripMenuItem.Text = "应用";
            this.应用ToolStripMenuItem.Click += new System.EventHandler(this.应用ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 参考ToolStripMenuItem
            // 
            this.参考ToolStripMenuItem.Name = "参考ToolStripMenuItem";
            this.参考ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.参考ToolStripMenuItem.Text = "参考";
            this.参考ToolStripMenuItem.Click += new System.EventHandler(this.参考ToolStripMenuItem_Click);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
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
            this.traceMessage.ItemHeight = 14;
            this.traceMessage.Location = new System.Drawing.Point(562, 41);
            this.traceMessage.Name = "traceMessage";
            this.traceMessage.Size = new System.Drawing.Size(393, 550);
            this.traceMessage.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(295, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 18);
            this.label1.TabIndex = 32;
            this.label1.Text = "系统及保存方案:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonapply;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(967, 602);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.traceMessage);
            this.Controls.Add(this.FangAn);
            this.Controls.Add(this.checkBoxDHCP);
            this.Controls.Add(this.buttonnicmac);
            this.Controls.Add(this.labelnicdes);
            this.Controls.Add(this.checkBox2IP);
            this.Controls.Add(this.labelmask2);
            this.Controls.Add(this.textBoxmask2);
            this.Controls.Add(this.buttonapply);
            this.Controls.Add(this.buttonsave);
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
        private System.Windows.Forms.ComboBox comboBoxnet;
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
        private System.Windows.Forms.Button buttonsave;
        private System.Windows.Forms.Button buttonapply;
        private System.Windows.Forms.TextBox textBoxmask2;
        private System.Windows.Forms.Label labelmask2;
        private System.Windows.Forms.CheckBox checkBox2IP;
        private System.Windows.Forms.Label labelnicdes;
        private System.Windows.Forms.Button buttonnicenable;
        private System.Windows.Forms.Button buttonnicmac;
        private System.Windows.Forms.CheckBox checkBoxDHCP;
        private System.Windows.Forms.ListBox traceMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 应用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参考ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox FangAn;
        public System.Windows.Forms.TextBox textBoxip1;
    }
}

