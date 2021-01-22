using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shangweiji
{
    public partial class 上位机之王 : Form
    {
        public 上位机之王()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void 我老婆叫沈佳佳_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 1; i < 20; i++)
            {
                comboBox1.Items.Add("COM" + i.ToString());
            }
            comboBox2.Items.Add("600");
            comboBox2.Items.Add("1200");
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("115200");
            comboBox2.Items.Add("230400");
            comboBox2.Items.Add("460800");
            comboBox1.Text = "COM1";
            comboBox2.Text = "9600";

            /*****************非常重要************************/

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//必须手动添加事件处理程序
            button3.Enabled = true;
            button4.Enabled = false;
            button2.Enabled = true;
            button1.Enabled = true;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                string str = serialPort1.ReadExisting();//字符串方式读
                textBox2.AppendText(str);//添加内容
        }
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[4];
            byte[] Data1 = new byte[1];
            byte[] Data2 = new byte[1];
            uint highWei;
            uint lowWei;
            uint number;
            Data[0] = Convert.ToByte(118);
            number = Convert.ToUInt16(textBox1.Text.Substring(0, textBox1.Text.Length));
            highWei = number / 256;
            lowWei = number % 256;
            Data[1] = Convert.ToByte(highWei);
            Data[2] = Convert.ToByte(lowWei);
            serialPort1.Write(Data, 0, 1);
            Task.Delay(1000);
            serialPort1.Write(Data, 1, 2);
            //serialPort1.Write(Data1, 0, 1);
            // serialPort1.Write(Data2, 0, 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[1];
            Data[0] = Convert.ToByte(68);
            serialPort1.Write(Data, 0, 1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();
                button3.Enabled = false;
                button4.Enabled = true;
            }
            catch
            {
                MessageBox.Show("端口打开错误", "错误");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            button4.Enabled = false;
            button3.Enabled = true;
        }
    }
}
