namespace WindowsWebBrowserTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.internalWebBrowser1 = new WindowsWebBrowserTest.InternalWebBrowser();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(548, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "This is a test ",
            "Test 2",
            "Europe",
            "AMERICA!!",
            "FREE EUROPE"});
            this.listBox1.Location = new System.Drawing.Point(652, 102);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 2;
            this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
            // 
            // internalWebBrowser1
            // 
            this.internalWebBrowser1.ExternalObject = null;
            this.internalWebBrowser1.Location = new System.Drawing.Point(37, 86);
            this.internalWebBrowser1.Name = "internalWebBrowser1";
            this.internalWebBrowser1.Size = new System.Drawing.Size(552, 492);
            this.internalWebBrowser1.TabIndex = 0;
            this.internalWebBrowser1.UseDragAndDrop = false;
            this.internalWebBrowser1.OnWBDragOver += new WindowsWebBrowserTest.WBDragOverEventHandler(this.internalWebBrowser1_OnWBDragOver);
            this.internalWebBrowser1.OnWBDragDrop += new WindowsWebBrowserTest.WBDropEventHandler(this.internalWebBrowser1_OnWBDragDrop);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 685);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.internalWebBrowser1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private InternalWebBrowser internalWebBrowser1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

