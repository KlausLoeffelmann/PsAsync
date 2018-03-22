using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiHost
{
    public partial class frmWebApiStatusForm : Form
    {
        private ServiceController myServiceController;
        public frmWebApiStatusForm()
        {
            InitializeComponent();
        }

        private void btnStartWebApi_Click(object sender, EventArgs e)
        {
            if (myServiceController is null)
            {
                myServiceController = new ServiceController();
                myServiceController.Start(this.txtLog);
                txtLog.WriteLine($"WebAPI STARTED AT {DateTime.Now.TimeOfDay.ToString()}");
                btnStartWebApi.Text = "Stop";
            }
            else
            {
                if (myServiceController.HasStarted)
                {
                    myServiceController.Stop();
                    txtLog.WriteLine($"WebAPI STOPPED AT {DateTime.Now.TimeOfDay.ToString()}");
                    btnStartWebApi.Text = "Start";
                }
            }
        }
    }
}
