using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            KlarupHalBooking.Server.TCPServer tCPServer = new KlarupHalBooking.Server.TCPServer(65432);
        }

        protected override void OnStop()
        {
            this.Dispose();
        }
    }
}
