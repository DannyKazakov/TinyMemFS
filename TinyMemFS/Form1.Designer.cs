namespace TinyMemFS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileSystemOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayAllFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxOption1 = new System.Windows.Forms.TextBox();
            this.buttonComboBox1 = new System.Windows.Forms.Button();
            this.textBoxOption2_A = new System.Windows.Forms.TextBox();
            this.textBoxOption2_B = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelComboBox1 = new System.Windows.Forms.Label();
            this.labelComboBox2_A = new System.Windows.Forms.Label();
            this.labelComboBox2_B = new System.Windows.Forms.Label();
            this.buttonComboBox2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.labelFSSize = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(491, 45);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(533, 339);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 240;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileSystemOptionsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1052, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileSystemOptionsToolStripMenuItem
            // 
            this.fileSystemOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFileSystemToolStripMenuItem,
            this.loadFileSystemToolStripMenuItem});
            this.fileSystemOptionsToolStripMenuItem.Name = "fileSystemOptionsToolStripMenuItem";
            this.fileSystemOptionsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.fileSystemOptionsToolStripMenuItem.Text = "File System Options";
            // 
            // saveFileSystemToolStripMenuItem
            // 
            this.saveFileSystemToolStripMenuItem.Name = "saveFileSystemToolStripMenuItem";
            this.saveFileSystemToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.saveFileSystemToolStripMenuItem.Text = "Save File System";
            this.saveFileSystemToolStripMenuItem.Click += new System.EventHandler(this.saveFileSystemToolStripMenuItem_Click);
            // 
            // loadFileSystemToolStripMenuItem
            // 
            this.loadFileSystemToolStripMenuItem.Name = "loadFileSystemToolStripMenuItem";
            this.loadFileSystemToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.loadFileSystemToolStripMenuItem.Text = "Load File System";
            this.loadFileSystemToolStripMenuItem.Click += new System.EventHandler(this.loadFileSystemToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayAllFilesToolStripMenuItem,
            this.sortToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.optionsToolStripMenuItem.Text = "Display";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // displayAllFilesToolStripMenuItem
            // 
            this.displayAllFilesToolStripMenuItem.Name = "displayAllFilesToolStripMenuItem";
            this.displayAllFilesToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.displayAllFilesToolStripMenuItem.Text = "Display All Files";
            this.displayAllFilesToolStripMenuItem.Click += new System.EventHandler(this.displayAllFilesToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byNameToolStripMenuItem,
            this.byDateToolStripMenuItem,
            this.bySizeToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // byNameToolStripMenuItem
            // 
            this.byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            this.byNameToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.byNameToolStripMenuItem.Text = "By Name";
            this.byNameToolStripMenuItem.Click += new System.EventHandler(this.byNameToolStripMenuItem_Click);
            // 
            // byDateToolStripMenuItem
            // 
            this.byDateToolStripMenuItem.Name = "byDateToolStripMenuItem";
            this.byDateToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.byDateToolStripMenuItem.Text = "By Date";
            this.byDateToolStripMenuItem.Click += new System.EventHandler(this.byDateToolStripMenuItem_Click);
            // 
            // bySizeToolStripMenuItem
            // 
            this.bySizeToolStripMenuItem.Name = "bySizeToolStripMenuItem";
            this.bySizeToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.bySizeToolStripMenuItem.Text = "By Size";
            this.bySizeToolStripMenuItem.Click += new System.EventHandler(this.bySizeToolStripMenuItem_Click);
            // 
            // textBoxOption1
            // 
            this.textBoxOption1.Location = new System.Drawing.Point(129, 105);
            this.textBoxOption1.Name = "textBoxOption1";
            this.textBoxOption1.Size = new System.Drawing.Size(238, 27);
            this.textBoxOption1.TabIndex = 10;
            // 
            // buttonComboBox1
            // 
            this.buttonComboBox1.Enabled = false;
            this.buttonComboBox1.Location = new System.Drawing.Point(382, 105);
            this.buttonComboBox1.Name = "buttonComboBox1";
            this.buttonComboBox1.Size = new System.Drawing.Size(103, 29);
            this.buttonComboBox1.TabIndex = 13;
            this.buttonComboBox1.Text = "Remove";
            this.buttonComboBox1.UseVisualStyleBackColor = true;
            this.buttonComboBox1.Click += new System.EventHandler(this.buttonComboBox1_Click);
            // 
            // textBoxOption2_A
            // 
            this.textBoxOption2_A.Location = new System.Drawing.Point(139, 239);
            this.textBoxOption2_A.Name = "textBoxOption2_A";
            this.textBoxOption2_A.Size = new System.Drawing.Size(228, 27);
            this.textBoxOption2_A.TabIndex = 28;
            // 
            // textBoxOption2_B
            // 
            this.textBoxOption2_B.Location = new System.Drawing.Point(139, 276);
            this.textBoxOption2_B.Name = "textBoxOption2_B";
            this.textBoxOption2_B.Size = new System.Drawing.Size(228, 27);
            this.textBoxOption2_B.TabIndex = 29;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Remove",
            "Encrypt",
            "Decrypt",
            "Compress",
            "Uncompress"});
            this.comboBox1.Location = new System.Drawing.Point(12, 69);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 28);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.comboBox1_DropDownClosed);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Add",
            "Save",
            "Set Hidden",
            "Rename",
            "Copy",
            "Compare"});
            this.comboBox2.Location = new System.Drawing.Point(12, 191);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(151, 28);
            this.comboBox2.TabIndex = 31;
            this.comboBox2.DropDownClosed += new System.EventHandler(this.comboBox2_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Select Option:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Select Option:";
            // 
            // labelComboBox1
            // 
            this.labelComboBox1.AutoSize = true;
            this.labelComboBox1.Location = new System.Drawing.Point(38, 108);
            this.labelComboBox1.Name = "labelComboBox1";
            this.labelComboBox1.Size = new System.Drawing.Size(76, 20);
            this.labelComboBox1.TabIndex = 34;
            this.labelComboBox1.Text = "File Name";
            // 
            // labelComboBox2_A
            // 
            this.labelComboBox2_A.AutoSize = true;
            this.labelComboBox2_A.Location = new System.Drawing.Point(38, 239);
            this.labelComboBox2_A.Name = "labelComboBox2_A";
            this.labelComboBox2_A.Size = new System.Drawing.Size(76, 20);
            this.labelComboBox2_A.TabIndex = 35;
            this.labelComboBox2_A.Text = "File Name";
            // 
            // labelComboBox2_B
            // 
            this.labelComboBox2_B.AutoSize = true;
            this.labelComboBox2_B.Location = new System.Drawing.Point(38, 279);
            this.labelComboBox2_B.Name = "labelComboBox2_B";
            this.labelComboBox2_B.Size = new System.Drawing.Size(76, 20);
            this.labelComboBox2_B.TabIndex = 36;
            this.labelComboBox2_B.Text = "File Name";
            // 
            // buttonComboBox2
            // 
            this.buttonComboBox2.Enabled = false;
            this.buttonComboBox2.Location = new System.Drawing.Point(382, 257);
            this.buttonComboBox2.Name = "buttonComboBox2";
            this.buttonComboBox2.Size = new System.Drawing.Size(94, 29);
            this.buttonComboBox2.TabIndex = 37;
            this.buttonComboBox2.Text = "Remove";
            this.buttonComboBox2.UseVisualStyleBackColor = true;
            this.buttonComboBox2.Click += new System.EventHandler(this.buttonComboBox2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(507, 396);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 20);
            this.label6.TabIndex = 38;
            this.label6.Text = "Total FS Size(Bytes):";
            // 
            // labelFSSize
            // 
            this.labelFSSize.AutoSize = true;
            this.labelFSSize.Location = new System.Drawing.Point(653, 396);
            this.labelFSSize.Name = "labelFSSize";
            this.labelFSSize.Size = new System.Drawing.Size(17, 20);
            this.labelFSSize.TabIndex = 39;
            this.labelFSSize.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(38, 309);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 126);
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1052, 460);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelFSSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonComboBox2);
            this.Controls.Add(this.labelComboBox2_B);
            this.Controls.Add(this.labelComboBox2_A);
            this.Controls.Add(this.labelComboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBoxOption2_B);
            this.Controls.Add(this.textBoxOption2_A);
            this.Controls.Add(this.buttonComboBox1);
            this.Controls.Add(this.textBoxOption1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1070, 507);
            this.MinimumSize = new System.Drawing.Size(1070, 507);
            this.Name = "Form1";
            this.Text = "TinyMemFS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileSystemOptionsToolStripMenuItem;
        private ToolStripMenuItem saveFileSystemToolStripMenuItem;
        private ToolStripMenuItem loadFileSystemToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem displayAllFilesToolStripMenuItem;
        private ToolStripMenuItem sortToolStripMenuItem;
        private ToolStripMenuItem byNameToolStripMenuItem;
        private ToolStripMenuItem byDateToolStripMenuItem;
        private ToolStripMenuItem bySizeToolStripMenuItem;
        private TextBox textBoxOption1;
        private Button buttonComboBox1;
        private TextBox textBoxOption2_A;
        private TextBox textBoxOption2_B;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label1;
        private Label label2;
        private Label labelComboBox1;
        private Label labelComboBox2_A;
        private Label labelComboBox2_B;
        private Button buttonComboBox2;
        private Label label6;
        private Label labelFSSize;
        private ColumnHeader columnHeader1;
        private PictureBox pictureBox1;
    }
}