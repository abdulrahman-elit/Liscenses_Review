using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liscenses;

namespace Licenses
{
    public partial class Form1: Form
    {
            public People person = new People();
            public Users user = new Users();
            //public Drivers driver = new Drivers();
        public Form1()
        {
            InitializeComponent();


            this.BackColor = Color.Black;

            // Handle the Load event to customize the MDI client area
            this.Load += MdiParentForm_Load;
        }
        private void MdiParentForm_Load(object sender, EventArgs e)
        {
            // Find the MdiClient control and change its background color
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    mdiClient.BackColor = Color.Black; 
                    // Set the client area background color
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            user?.Hide();
            //driver?.Hide();
            person.MdiParent=this;
            person.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //if(person)

            person?.Hide();
            //driver?.Hide();
            user.MdiParent = this;
            user.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            person?.Hide();
            user?.Hide();
            //driver.MdiParent = this;
            //driver.Show();
        }
    }
}
