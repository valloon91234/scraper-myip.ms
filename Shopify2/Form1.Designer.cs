namespace Shopify2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Parse = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_List = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_Email = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Merge = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Close = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStripButton_Test = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Parse,
            this.toolStripTextBox1,
            this.toolStripButton_List,
            this.toolStripTextBox2,
            this.toolStripButton_Email,
            this.toolStripButton_Merge,
            this.toolStripButton_Test,
            this.toolStripButton_Close});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(653, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Parse
            // 
            this.toolStripButton_Parse.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Parse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Parse.Image")));
            this.toolStripButton_Parse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Parse.Name = "toolStripButton_Parse";
            this.toolStripButton_Parse.Size = new System.Drawing.Size(78, 30);
            this.toolStripButton_Parse.Text = "&Parse";
            this.toolStripButton_Parse.Click += new System.EventHandler(this.toolStripButton_Parse_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(60, 33);
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripButton_List
            // 
            this.toolStripButton_List.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_List.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_List.Image")));
            this.toolStripButton_List.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_List.Name = "toolStripButton_List";
            this.toolStripButton_List.Size = new System.Drawing.Size(61, 30);
            this.toolStripButton_List.Text = "&List";
            this.toolStripButton_List.Click += new System.EventHandler(this.toolStripButton_List_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(60, 33);
            this.toolStripTextBox2.Text = "0";
            this.toolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripButton_Email
            // 
            this.toolStripButton_Email.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Email.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Email.Image")));
            this.toolStripButton_Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Email.Name = "toolStripButton_Email";
            this.toolStripButton_Email.Size = new System.Drawing.Size(79, 30);
            this.toolStripButton_Email.Text = "&Email";
            this.toolStripButton_Email.Click += new System.EventHandler(this.toolStripButton_Email_Click);
            // 
            // toolStripButton_Merge
            // 
            this.toolStripButton_Merge.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Merge.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Merge.Image")));
            this.toolStripButton_Merge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Merge.Name = "toolStripButton_Merge";
            this.toolStripButton_Merge.Size = new System.Drawing.Size(87, 30);
            this.toolStripButton_Merge.Text = "&Merge";
            this.toolStripButton_Merge.Click += new System.EventHandler(this.toolStripButton_Merge_Click);
            // 
            // toolStripButton_Close
            // 
            this.toolStripButton_Close.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Close.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Close.Image")));
            this.toolStripButton_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Close.Name = "toolStripButton_Close";
            this.toolStripButton_Close.Size = new System.Drawing.Size(77, 30);
            this.toolStripButton_Close.Text = "&Close";
            this.toolStripButton_Close.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 33);
            this.textBox1.MaxLength = 999999;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(653, 153);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // toolStripButton_Test
            // 
            this.toolStripButton_Test.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Test.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Test.Image")));
            this.toolStripButton_Test.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Test.Name = "toolStripButton_Test";
            this.toolStripButton_Test.Size = new System.Drawing.Size(65, 30);
            this.toolStripButton_Test.Text = "&Test";
            this.toolStripButton_Test.Click += new System.EventHandler(this.toolStripButton_Test_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 186);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Parse;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Email;
        private System.Windows.Forms.ToolStripButton toolStripButton_List;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Merge;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Close;
        private System.Windows.Forms.ToolStripButton toolStripButton_Test;
    }
}

