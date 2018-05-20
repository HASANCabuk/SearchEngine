namespace SearchEngine
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betweenUnivercitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rawFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betweenUnivercitiesPagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawFrequecyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ınverseTermFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(598, 20);
            this.textBox1.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.SearchButton.Location = new System.Drawing.Point(636, 19);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(282, 48);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(705, 422);
            this.webBrowser1.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(264, 433);
            this.listBox1.TabIndex = 4;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analysisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1011, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.betweenUnivercitiesToolStripMenuItem,
            this.betweenUnivercitiesPagesToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // betweenUnivercitiesToolStripMenuItem
            // 
            this.betweenUnivercitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.rawFrequencyToolStripMenuItem});
            this.betweenUnivercitiesToolStripMenuItem.Name = "betweenUnivercitiesToolStripMenuItem";
            this.betweenUnivercitiesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.betweenUnivercitiesToolStripMenuItem.Text = "Between Univercities";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItem1.Text = "Inverse Term Frequency";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // rawFrequencyToolStripMenuItem
            // 
            this.rawFrequencyToolStripMenuItem.Name = "rawFrequencyToolStripMenuItem";
            this.rawFrequencyToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.rawFrequencyToolStripMenuItem.Text = "Raw Frequency";
            this.rawFrequencyToolStripMenuItem.Click += new System.EventHandler(this.rawFrequencyToolStripMenuItem_Click);
            // 
            // betweenUnivercitiesPagesToolStripMenuItem
            // 
            this.betweenUnivercitiesPagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawFrequecyToolStripMenuItem,
            this.ınverseTermFrequencyToolStripMenuItem});
            this.betweenUnivercitiesPagesToolStripMenuItem.Name = "betweenUnivercitiesPagesToolStripMenuItem";
            this.betweenUnivercitiesPagesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.betweenUnivercitiesPagesToolStripMenuItem.Text = "Between Univercities Pages";
            // 
            // rawFrequecyToolStripMenuItem
            // 
            this.rawFrequecyToolStripMenuItem.Name = "rawFrequecyToolStripMenuItem";
            this.rawFrequecyToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.rawFrequecyToolStripMenuItem.Text = "Raw Frequency";
            this.rawFrequecyToolStripMenuItem.Click += new System.EventHandler(this.rawFrequecyToolStripMenuItem_Click);
            // 
            // ınverseTermFrequencyToolStripMenuItem
            // 
            this.ınverseTermFrequencyToolStripMenuItem.Name = "ınverseTermFrequencyToolStripMenuItem";
            this.ınverseTermFrequencyToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.ınverseTermFrequencyToolStripMenuItem.Text = "Inverse Term Frequency";
            this.ınverseTermFrequencyToolStripMenuItem.Click += new System.EventHandler(this.ınverseTermFrequencyToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 482);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "GYBY";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem betweenUnivercitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rawFrequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem betweenUnivercitiesPagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawFrequecyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ınverseTermFrequencyToolStripMenuItem;
    }
}

