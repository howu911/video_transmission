using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDP测试_客户端
{
    public partial class ClientForm2 : Form
    {
        public ClientForm2()
        {
            InitializeComponent();
        }
        static Socket client;
        Thread t;
        Thread t2;
        string recv;
        private void btnSend_Click(object sender, EventArgs e)
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("192.168.48.1"), 6001);
            string msg = txtSend.Text;
            client.SendTo(Encoding.UTF8.GetBytes(msg), point);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtReciv.Text = recv;
        }

        private void ClientForm2_Load(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text)));

            t = new Thread(sendMsg);
            //t.Start();

            t2 = new Thread(ReciveMsg);
            t2.Start();

            timer1.Start();
        }
        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("192.168.48.1"), 6001);
            while (true)
            {
                string msg = txtSend.Text;
                client.SendTo(Encoding.Default.GetBytes(msg), point);
            }
        }
        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
                int length = client.ReceiveFrom(buffer, ref point);//接收数据报
                recv += Encoding.UTF8.GetString(buffer, 0, length);
                recv += "\r\n";
                //txtReciv.Text += recv;
            }
        }

        private void ClientForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            t.Abort();
            t2.Abort();
        }
    }
}
