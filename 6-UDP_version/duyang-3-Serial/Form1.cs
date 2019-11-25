using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;


namespace duyang_3_Serial
{
    public partial class Form1 : Form
    {
        //用来转换RGB565 To RGB888的宏定义
        public const ushort RGB565_MASK_RED = 0xF800;
        public const ushort RGB565_MASK_GREEN = 0x07E0;
        public const ushort RGB565_MASK_BLUE = 0x001F;

        //定义接收一帧图像的字符串
        public string str_src;

        //定义接收一帧图像的字节数组
        public byte[] picture_byte = new byte[153600];
        //public byte[] RGB888 = new byte[3];

        //定义一帧图像的起始和结束标志
        public const string str_begin = "FF11FF11FF11";
        public const string str_end = "EE22EE22EE22";


        public Form1()
        {
            InitializeComponent();
            serialPort1.Encoding = Encoding.GetEncoding("GB2312");
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                comboBox1.Items.Add("COM" + i.ToString());
            }
            comboBox1.Text = "COM11";
            comboBox2.Text = "115200";

            /* 初始化串口事件，C#没有自带 */
          //  serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.AppendFormat
                
                if (!radioButton4.Checked)
                {
                    string str = serialPort1.ReadExisting();
                    str_src += str;
                    
                    textBox1.AppendText(str);
                }
                else
                {
                    //原来的方式是以为一个字节就会产生一个中断，所以每次读取一个字节
                    //但Windows系统并不是这样子，而是有缓冲区
#if false
                    byte data = (byte)serialPort1.ReadByte();
                    string str = Convert.ToString(data, 16).ToUpper();
                    textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
#endif
                    byte[] data = new byte[serialPort1.BytesToRead];
                    serialPort1.Read(data, 0, data.Length);
                    foreach (byte Member in data)
                    {
                        string str = Convert.ToString(Member, 16).ToUpper();
                        textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        //打开串口函数
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;
                picture_time.Enabled = true;
                picture_time.Start();
            }
            catch { 
                MessageBox.Show("端口错误，请检查串口", "错误");
            }
        }
        //关闭串口
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close(); //关闭串口
                button1.Enabled = true;
                button2.Enabled = false;
                picture_time.Enabled = false;
                picture_time.Stop();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)  //发送数据按钮
        {
            byte[] data = new byte[1];
            if (serialPort1.IsOpen && textBox2.Text != "")  //判断串口是否打开，并且发送框内有字符
            {
                if (!radioButton1.Checked)  //如果选择的是字符
                {
                    try
                    {
                        serialPort1.WriteLine(textBox2.Text);  //发数据
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("串口数据发送错误", "错误！");
                        serialPort1.Close();
                        button1.Enabled = true;
                        button2.Enabled = false;
                    }
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < (textBox2.Text.Length - textBox2.Text.Length % 2) / 2; ++i)
                        {
                            data[0] = Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16);
                            serialPort1.Write(data, 0, 1);
                        }
                        if (textBox2.Text.Length % 2 != 0)
                        {
                            data[0] = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - 1, 1), 16);
                            serialPort1.Write(data, 0, 1);
                        }              
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("串口数据发送错误", "错误！");
                        serialPort1.Close();
                        button1.Enabled = true;
                        button2.Enabled = false;
                    }
                }
            }
        }
        private static void rgb565_2_rgb24(byte[] rgb24, ushort rgb565)
        {
            //uint data;
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
#if true
        public Bitmap GetDataPicture(int w, int h,byte[] data)
        {
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //定义一个像素颜色变量
            Color c;
            //定义RGB565转变为RGB888的字节数组
            byte[] rgb24 = new byte[3];
            //这是像素位置变量
            int j = 0;
            for (int i = 0; i < data.Length; i+=2)
            {
                //将高低8位合并为半字
                ushort RGB565_Temp = (ushort)data[i];
                RGB565_Temp = (ushort)((RGB565_Temp << 8) | data[i + 1]); 
                //进行转换
                rgb565_2_rgb24(rgb24, RGB565_Temp);
                //设置像素
                c = Color.FromArgb(rgb24[2], rgb24[1], rgb24[0]);
                //c = Color.FromArgb(0x00, 0x00, 0x00);
                //将像素写进图片
                pic.SetPixel(j % w, j / w, c);
                j++;
            }

            return pic;
        }
#endif

#if false
        /* 写这个函数是为了验证pictureBox是否能显示一副RGB888的图片
        ** 验证OK
        */
        public Bitmap GetDataPicture(int w, int h, byte[] data)
        {
            int i = 0, j = 0;
            //定义一个像素颜色变量
            Color c;
            //定义一个BMP格式的图片
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //打开bitmao图片
            Bitmap bmp_test = new Bitmap(@"D:\\Users\\HOWU\\Desktop\\RGB888_Test.bmp");
            for (i = 0; i < 320; i++)
            {
                for (j = 0; j < 240; j++)
                {
                    Color pixelcolor = bmp_test.GetPixel(i, j);
                    //设置像素
                    c = Color.FromArgb(pixelcolor.R, pixelcolor.G, pixelcolor.B);
                    pic.SetPixel(i, j, c);
                }
            }

            return pic;
        }
#endif


        private void picture_time_Tick(object sender, EventArgs e)
        {
            if (str_src != null && str_src.Contains(str_begin))
            {
                //str_src += str.Substring(12);
                //一直等到结束标志符
                if (str_src.Contains(str_end))
                {
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
                    return;
                }
            }
            else {
                return;
            }
        }

        //将16进制字符串转变为16进制字节数组
        public void Hex16StringToByte(string _hex16String)
        {
            //声明一个字节数组，其长度等于字符串长度的一半。
            //byte[] buffer = new byte[_hex16String.Length / 2];
            for (int i = 0; i < picture_byte.Length; i++)
            {
                //为字节数组的元素赋值。
                picture_byte[i] = Convert.ToByte((_hex16String.Substring(i * 2, 2)), 16);
            }
            //返回字节数组。
            //return buffer;
        }

    }
}
