using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace WindowsWebBrowserTest {
    public partial class Form1 : Form {
        private IHTMLElement internalelement = null;

        public Form1() {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {
                this.internalWebBrowser1.WebBrowser.Navigate(this.textBox1.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_MouseDown(object sender, MouseEventArgs e) {
            this.listBox1.DoDragDrop(this.listBox1.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void internalWebBrowser1_OnWBDragDrop(object sender, WBDropEventArgs e) {
            e.dropeffect = DROPEFFECT.COPY;
            if (e.pt != null) {

                IHTMLElement x = this.internalWebBrowser1.ElementFromPoint(e.pt.X, e.pt.Y);

                if (x != null && this.listBox1.SelectedIndex > -1) {
                    if (x is IHTMLInputTextElement) {
                        (x as mshtml.HTMLInputTextElement).value = this.listBox1.SelectedItem.ToString();
                    }
                }
            }
            e.handled = true;
        }

        private void internalWebBrowser1_OnWBDragOver(object sender, WBDragOverEventArgs e) {
            e.dropeffect = DROPEFFECT.COPY;            

            IHTMLElement x = this.internalWebBrowser1.ElementFromPoint(e.pt.X, e.pt.Y);  
            
            /// lot's of flickering here - but who cares - it can be 
            /// fixed in production code [as we know which elements to ignore!]
            if (x != null) {                
                if (internalelement != null) {
                    internalelement.style.setAttribute("border", "", 0);
                    internalelement = null;
                }
                else {
                    internalelement = x;
                    x.style.setAttribute("border", "2px dotted green", 0);                    
                }
            }

            e.handled = true;
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.textBox1.Text = "http://www.google.com";
            this.internalWebBrowser1.WebBrowser.Navigate("http://www.google.com");
        }
    }
}
