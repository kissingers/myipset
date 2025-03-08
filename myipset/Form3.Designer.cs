namespace myipset
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.DataGridViewCping = new System.Windows.Forms.DataGridView();
            this.textBoxCping = new System.Windows.Forms.TextBox();
            this.buttonStartPing = new System.Windows.Forms.Button();
            this.DataGridViewMAC = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10ms = new System.Windows.Forms.Label();
            this.label100ms = new System.Windows.Forms.Label();
            this.label1000ms = new System.Windows.Forms.Label();
            this.labelLG1000ms = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSaveMac = new System.Windows.Forms.Button();
            this.labelDenyPing = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMAC)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewCping
            // 
            this.DataGridViewCping.AllowUserToAddRows = false;
            this.DataGridViewCping.AllowUserToDeleteRows = false;
            this.DataGridViewCping.AllowUserToResizeColumns = false;
            this.DataGridViewCping.AllowUserToResizeRows = false;
            this.DataGridViewCping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewCping.ColumnHeadersVisible = false;
            this.DataGridViewCping.Location = new System.Drawing.Point(11, 45);
            this.DataGridViewCping.Name = "DataGridViewCping";
            this.DataGridViewCping.RowHeadersVisible = false;
            this.DataGridViewCping.RowHeadersWidth = 51;
            this.DataGridViewCping.RowTemplate.Height = 27;
            this.DataGridViewCping.Size = new System.Drawing.Size(643, 437);
            this.DataGridViewCping.TabIndex = 0;
            // 
            // textBoxCping
            // 
            this.textBoxCping.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxCping.BackColor = System.Drawing.Color.White;
            this.textBoxCping.Font = new System.Drawing.Font("宋体", 10.64348F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCping.Location = new System.Drawing.Point(183, 8);
            this.textBoxCping.Name = "textBoxCping";
            this.textBoxCping.Size = new System.Drawing.Size(167, 28);
            this.textBoxCping.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxCping, "C类群ping，第四位无用，仅供参考");
            // 
            // buttonStartPing
            // 
            this.buttonStartPing.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStartPing.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStartPing.ForeColor = System.Drawing.Color.Blue;
            this.buttonStartPing.Location = new System.Drawing.Point(11, 6);
            this.buttonStartPing.Name = "buttonStartPing";
            this.buttonStartPing.Size = new System.Drawing.Size(165, 33);
            this.buttonStartPing.TabIndex = 43;
            this.buttonStartPing.Text = "开始群ping";
            this.buttonStartPing.UseVisualStyleBackColor = false;
            this.buttonStartPing.Click += new System.EventHandler(this.ButtonStartPing_Click);
            // 
            // DataGridViewMAC
            // 
            this.DataGridViewMAC.AllowUserToAddRows = false;
            this.DataGridViewMAC.AllowUserToDeleteRows = false;
            this.DataGridViewMAC.AllowUserToResizeColumns = false;
            this.DataGridViewMAC.AllowUserToResizeRows = false;
            this.DataGridViewMAC.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataGridViewMAC.ColumnHeadersHeight = 29;
            this.DataGridViewMAC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.MAC});
            this.DataGridViewMAC.Location = new System.Drawing.Point(666, 45);
            this.DataGridViewMAC.Name = "DataGridViewMAC";
            this.DataGridViewMAC.RowHeadersVisible = false;
            this.DataGridViewMAC.RowHeadersWidth = 51;
            this.DataGridViewMAC.RowTemplate.Height = 27;
            this.DataGridViewMAC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridViewMAC.Size = new System.Drawing.Size(305, 437);
            this.DataGridViewMAC.TabIndex = 44;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.MinimumWidth = 130;
            this.IP.Name = "IP";
            this.IP.Width = 130;
            // 
            // MAC
            // 
            this.MAC.HeaderText = "MAC";
            this.MAC.MinimumWidth = 150;
            this.MAC.Name = "MAC";
            this.MAC.Width = 150;
            // 
            // label10ms
            // 
            this.label10ms.AutoSize = true;
            this.label10ms.BackColor = System.Drawing.Color.Lime;
            this.label10ms.Location = new System.Drawing.Point(367, 15);
            this.label10ms.Name = "label10ms";
            this.label10ms.Size = new System.Drawing.Size(47, 15);
            this.label10ms.TabIndex = 45;
            this.label10ms.Text = "<10ms";
            // 
            // label100ms
            // 
            this.label100ms.AutoSize = true;
            this.label100ms.BackColor = System.Drawing.Color.Yellow;
            this.label100ms.Location = new System.Drawing.Point(430, 15);
            this.label100ms.Name = "label100ms";
            this.label100ms.Size = new System.Drawing.Size(55, 15);
            this.label100ms.TabIndex = 46;
            this.label100ms.Text = "<100ms";
            // 
            // label1000ms
            // 
            this.label1000ms.AutoSize = true;
            this.label1000ms.BackColor = System.Drawing.Color.Orange;
            this.label1000ms.Location = new System.Drawing.Point(503, 15);
            this.label1000ms.Name = "label1000ms";
            this.label1000ms.Size = new System.Drawing.Size(63, 15);
            this.label1000ms.TabIndex = 47;
            this.label1000ms.Text = "<1000ms";
            // 
            // labelLG1000ms
            // 
            this.labelLG1000ms.AutoSize = true;
            this.labelLG1000ms.BackColor = System.Drawing.Color.OrangeRed;
            this.labelLG1000ms.Location = new System.Drawing.Point(591, 15);
            this.labelLG1000ms.Name = "labelLG1000ms";
            this.labelLG1000ms.Size = new System.Drawing.Size(63, 15);
            this.labelLG1000ms.TabIndex = 48;
            this.labelLG1000ms.Text = ">1000ms";
            // 
            // buttonSaveMac
            // 
            this.buttonSaveMac.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSaveMac.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveMac.ForeColor = System.Drawing.Color.Blue;
            this.buttonSaveMac.Location = new System.Drawing.Point(808, 6);
            this.buttonSaveMac.Name = "buttonSaveMac";
            this.buttonSaveMac.Size = new System.Drawing.Size(163, 33);
            this.buttonSaveMac.TabIndex = 49;
            this.buttonSaveMac.Text = "保存IP-MAC地址";
            this.buttonSaveMac.UseVisualStyleBackColor = false;
            this.buttonSaveMac.Click += new System.EventHandler(this.ButtonSaveMac_Click);
            // 
            // labelDenyPing
            // 
            this.labelDenyPing.AutoSize = true;
            this.labelDenyPing.BackColor = System.Drawing.Color.Fuchsia;
            this.labelDenyPing.Location = new System.Drawing.Point(679, 15);
            this.labelDenyPing.Name = "labelDenyPing";
            this.labelDenyPing.Size = new System.Drawing.Size(99, 15);
            this.labelDenyPing.TabIndex = 50;
            this.labelDenyPing.Text = "同网段禁ping";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(981, 491);
            this.Controls.Add(this.labelDenyPing);
            this.Controls.Add(this.buttonSaveMac);
            this.Controls.Add(this.labelLG1000ms);
            this.Controls.Add(this.label1000ms);
            this.Controls.Add(this.label100ms);
            this.Controls.Add(this.label10ms);
            this.Controls.Add(this.DataGridViewMAC);
            this.Controls.Add(this.buttonStartPing);
            this.Controls.Add(this.textBoxCping);
            this.Controls.Add(this.DataGridViewCping);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "群ping工具";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMAC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridViewCping;
        public System.Windows.Forms.TextBox textBoxCping;
        private System.Windows.Forms.Button buttonStartPing;
        private System.Windows.Forms.DataGridView DataGridViewMAC;
        private System.Windows.Forms.Label label10ms;
        private System.Windows.Forms.Label label100ms;
        private System.Windows.Forms.Label label1000ms;
        private System.Windows.Forms.Label labelLG1000ms;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAC;
        private System.Windows.Forms.Button buttonSaveMac;
        private System.Windows.Forms.Label labelDenyPing;
    }
}