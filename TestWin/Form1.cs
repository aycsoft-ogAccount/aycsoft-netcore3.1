using aycsoft.application;
using aycsoft.iapplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UserIBLL userIBLL = new UserBLL(); 

        private void button1_Click(object sender, EventArgs e)
        {
            var list = userIBLL.GetAdminList().Result;
        }
    }
}
