using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testdelegte
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //public event Action<int, string> onclick;
        //protected virtual bool clickx(int num, string value)
        //{
        //    Action<int, string> handler = onclick;
        //    if (handler != null)
        //    {
        //        handler(num, value);
        //        return true;
        //    }
        //    return false;
        //}
        public event EventHandler<ClickEventArgs> onclick;
        protected virtual void clickx(int num, string value)
        {
            var handler = onclick;
            if (handler != null)
            {
                ClickEventArgs args = new ClickEventArgs();
                args.Num = num;
                args.Value = value;
                handler(this,args/* new ClickEventArgs(num, value)*/);
                //handler(this,args/* new ClickEventArgs(num, value)*/);
            }
        }
        public class ClickEventArgs :EventArgs 
        {
            public int Num { set; get; }
            public string Value { set; get; }
            //public ClickEventArgs(int num, string value)
            //{
            //    Num = num;
            //    Value = value;
            //}
        }   
        
        private void button1_Click(object sender, EventArgs e)
        {
            int num = -1;
            string value = string.Empty;
            if (onclick != null && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                value = textBox2.Text;
                num = Convert.ToInt32(textBox1.Text);
                clickx(num, value);
            }

        }
    }
}

                // Check if the text boxes are not empty
                // Call the clickx method and pass the values
                //    if (clickx(num, value))
                //    {
                //        this.Close();
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Please enter both values.");
                //}