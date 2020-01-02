using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Threading;

namespace UDP_Parctice
{
    public partial class Form1 : Form
    {
        //用来转换RGB565 To RGB888的宏定义
        public const ushort RGB565_MASK_RED = 0xF800;
        public const ushort RGB565_MASK_GREEN = 0x07E0;
        public const ushort RGB565_MASK_BLUE = 0x001F;

        //发送数据宏定义
        byte[] send_data = new byte[1];
        byte[] control_data = new byte[1];
        public const byte send_start = 0x01;
        public const byte restart = 0x02;
        public const byte send_over = 0x08;
        public const byte up = 0x0A;
        public const byte down = 0x0B;
        public const byte left = 0x0C;
        public const byte rightt = 0x0D;
        public const byte standd = 0x0E;


        //定义接收一帧图像的字节数组
        public byte[] picture_byte1 = new byte[153600];
        public byte[] picture_byte2 = new byte[153600];
        bool picture_flag = true;  //定义一个决定发送哪个字节数组的标志
        bool picture_success_flag = false;

        //创建发送和接收线程
        Thread threadUDPWatch = null;
        Thread threadUDPReceive2 = null;
        Thread threadUDPSend = null;
        Thread threadUDPSend2 = null;

        //创建socket套接字
        Socket socketUDP = null;
        Socket socketUDP2 = null;

        //创建网络连接模式
        string selectedMode;

        //发送数据线程挂起标志
        bool send1_flag = true;
        bool send2_flag = true;
        //复制字节数组索引
        uint line = 0; 

        //连接成功标志
        bool start_flag = false;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = Image.FromFile("D:\\Users\\HOWU\\Desktop\\IMG_7315.jpg");   
            TypeOfProtocolComboBox.Text = "UDP";            //获取本地IP及端口号，定义IPEndPoint endPoint变量，待后续与socket绑定
            IPAddress address = IPAddress.Parse(LocalIPAddressTextBox.Text.Trim());
            int portNum = int.Parse(LocalIPPortTextBox.Text.Trim());
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.1.105"), portNum);
            IPEndPoint endPoint2 = new IPEndPoint(IPAddress.Parse("192.168.1.105"), 6000);

            socketUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketUDP2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socketUDP.Bind(endPoint);
                socketUDP2.Bind(endPoint2);
            }
            catch (SocketException se)
            {
                MessageBox.Show("创建UDP服务器失败！", "错误");
                return;
            }
            threadUDPWatch = new Thread(RecMsg);
            threadUDPSend = new Thread(SendMsg);
            line = 0;
            threadUDPSend.Start();
            threadUDPWatch.Start();

            threadUDPSend2 = new Thread(SendMsg2);
            threadUDPReceive2 = new Thread(RecMsg2);
            threadUDPReceive2.Start();
            threadUDPSend2.Start();
            start_flag = true;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (start_flag)
            {
                send_data[0] = send_over;
               // threadUDPSend.Resume();
                threadUDPWatch.Abort();
                try
                {
                    if (threadUDPSend2.ThreadState == ThreadState.Suspended)
                        threadUDPSend2.Resume();
                    threadUDPSend2.Abort();
                }
                catch
                {
                    MessageBox.Show("threadUDPSend2", "错误");
                }
                try
                {
                    if (threadUDPSend.ThreadState == ThreadState.Suspended)
                        threadUDPSend.Resume();
                    threadUDPSend.Abort();
                }
                catch
                {
                    MessageBox.Show("threadUDPSend", "错误");
                }

                socketUDP.Close();
                threadUDPReceive2.Abort();
                socketUDP2.Close();
            }   
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ConnectButton.Text == "连接")
            {
                line = 0;
                ConnectButton.Text = "断开";
                send_data[0] = send_start;
                threadUDPSend.Resume();
                timer1.Start();
                    
            }
            else
            {
                if (ConnectButton.Text == "断开")
                {
                    ConnectButton.Text = "连接";
                    send_data[0] = send_over;
                    threadUDPSend.Resume();
                    timer1.Stop();
                }
            }   
        }


        //接收发送给本机IP对应端口的数据报
        private void RecMsg()
        {
            //这是一个线程，所以用死循环
            while (true)
            {
                //用来保存发送方的IP和端口号
                EndPoint RecPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[1280];
                int length = socketUDP.ReceiveFrom(buffer, ref RecPoint);
                if (picture_flag)
                {
                    buffer.CopyTo(picture_byte1, 1280 * line);
                }
                else
                {
                    buffer.CopyTo(picture_byte2, 1280 * line);
                }
                line++;
                if (line == 120)
                {
                    line = 0;
                    picture_flag = !picture_flag;
                    picture_success_flag = true;
                    Thread.Sleep(50);  //这里的时间应该大于定时器的间隔时间，要不然会出现视频画面的前面一小部分显示的是下一帧的画面
                }   
            }
        }

        private void RecMsg2()  //暂时未用到
        {
            //这是一个线程，所以用死循环
            while (true)
            {

            }
        }

        //向特定的主机的端口发送数据
        private void SendMsg()
        {
            //第一次调用线程，挂起
            if (send1_flag)
            {
                threadUDPSend.Suspend();
                send1_flag = false;
            }
            EndPoint point = new IPEndPoint(IPAddress.Parse("192.168.1.88"), 8088);
            while (true)
            {
                socketUDP.SendTo(send_data,  point);
                threadUDPSend.Suspend();
            }
        }

        private void SendMsg2()
        {
            //第一次调用线程，挂起
            if (send2_flag)
            {
                threadUDPSend2.Suspend();
                send2_flag = false;
            }
            EndPoint point = new IPEndPoint(IPAddress.Parse("192.168.1.88"), 7000);
            while (true)
            {
                socketUDP2.SendTo(control_data, point);
                threadUDPSend2.Suspend();
            }
        }

        private void ClearReciveDataButton_Click(object sender, EventArgs e)
        {
            PictureDataBox.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (picture_success_flag)
            {
                //将字节数组转换为图片
                if(picture_flag)
                    pictureBox1.Image = GetDataPicture(pictureBox1.Width, pictureBox1.Height, picture_byte1);
                else
                    pictureBox1.Image = GetDataPicture(pictureBox1.Width, pictureBox1.Height, picture_byte2);
               // threadUDPWatch.Suspend();
                picture_success_flag = false;
                return;
            }
            else
            {
                return;
            }
        }
#if false
        private void timer1_Tick(object sender, EventArgs e)
        {
            //PictureDataBox.Text = recData;
            //timer1.Stop();

            if (str_src != null && str_src.Contains(str_begin))
            {
                //str_src += str.Substring(12);
                //一直等到结束标志符
                if (str_src.Contains(str_end))
                {
                    line = 0;
                    //获取一帧数据
                    string picture_str = str_src.Substring(str_src.IndexOf(str_begin) + 12, str_src.IndexOf(str_end) - 12);

                    //如果不足一帧数据，将后面的数据进行补零处理
                    if (picture_str.Length < 307200)
                        picture_str = picture_str.PadRight(307200, '0');
                    else if (picture_str.Length > 307200)
                        picture_str = picture_str.Substring(0, 307200);

                    //在全局变量中移除已经取得的一帧数据
                    str_src = str_src.Remove(str_src.IndexOf(str_begin), (str_src.IndexOf(str_end) - str_src.IndexOf(str_begin) + 12));
                    //textBox3.AppendText(picture_str);
                    //将字符串转换为16进制字节数组
                    //声明一个字节数组，其长度等于字符串长度的一半。
                    //byte[] buffer = new byte[picture_str.Length / 2];
                    Hex16StringToByte(picture_str);
                    //将字节数组转换为图片
                    pictureBox1.Image = GetDataPicture(pictureBox1.Width, pictureBox1.Height, picture_byte);
                    //将原来的数据清空
                    str_src = "";
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
#endif
        private static void rgb565_2_rgb24(byte[] rgb24, ushort rgb565)
        {
            //extract RGB   
            rgb24[2] = (byte)((rgb565 & RGB565_MASK_RED) >> 11);
            rgb24[1] = (byte)((rgb565 & RGB565_MASK_GREEN) >> 5);
            rgb24[0] = (byte)((rgb565 & RGB565_MASK_BLUE));

            //amplify the image   
            rgb24[2] <<= 3;
            rgb24[1] <<= 2;
            rgb24[0] <<= 3;

            //data = (uint)(rgb24[2] << 16 + rgb24[1] << 8 + rgb24[0]);
            //return data;
        }

        public Bitmap GetDataPicture(int w, int h, byte[] data)
        {
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //定义一个像素颜色变量
            Color c;
            //定义RGB565转变为RGB888的字节数组
            byte[] rgb24 = new byte[3];
            //这是像素位置变量
            int j = 0;
            for (int i = 0; i < data.Length; i += 2)
            {
                //将高低8位合并为半字
                ushort RGB565_Temp = (ushort)data[i];
                RGB565_Temp = (ushort)((RGB565_Temp << 8) | data[i + 1]);
                //进行转换
                rgb565_2_rgb24(rgb24, RGB565_Temp);
                //设置像素
                c = Color.FromArgb(rgb24[2], rgb24[1], rgb24[0]);
                //将像素写进图片
                pic.SetPixel(j % w, j / w, c);
                j++;
            }

            return pic;
        }

        public void Hex16StringToByte(string _hex16String)
        {
            //声明一个字节数组，其长度等于字符串长度的一半。
            //byte[] buffer = new byte[_hex16String.Length / 2];
            for (int i = 0; i < picture_byte1.Length; i++)
            {
                //为字节数组的元素赋值。
                picture_byte1[i] = Convert.ToByte((_hex16String.Substring(i * 2, 2)), 16);
            }
            //返回字节数组。
            //return buffer;
        }

        //发送前进命令
        private void button1_Click(object sender, EventArgs e)
        {
            control_data[0] = up;
            threadUDPSend2.Resume();
            PictureDataBox.AppendText("up\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control_data[0] = left;
            threadUDPSend2.Resume();
            PictureDataBox.AppendText("left\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control_data[0] = down;
            threadUDPSend2.Resume();
            PictureDataBox.AppendText("down\r\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control_data[0] = rightt;
            threadUDPSend2.Resume();
            PictureDataBox.AppendText("right\r\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            control_data[0] = standd;
            threadUDPSend2.Resume();
            PictureDataBox.AppendText("stand\r\n");
        }

        private void restart_button_Click(object sender, EventArgs e)
        {
            send_data[0] = restart;
            threadUDPSend.Resume();
            ConnectButton.Text = "连接";
            PictureDataBox.AppendText("restart\r\n");
        }


    }
}
