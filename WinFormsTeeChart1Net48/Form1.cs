using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Steema.TeeChart.Styles;

namespace WinFormsTeeChart1Net48
{
    public partial class Form1 : Form
    {
        private TcpClient client1 = null;
        private TcpClient client2 = null;
        private TcpClient client3 = null;



        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDisconnectSession1_Click(this, e);
            btnDisconnectSession2_Click(this, e);
            btnDisconnectSession3_Click(this, e);
        }

        private void tChart1_DoubleClick(object sender, EventArgs e)
        {
            tChart1.ShowEditor();
        }

        private async void btnConnectSession1_Click(object sender, EventArgs e)
        {
            btnConnectSession1.Enabled = false;
            btnDisconnectSession1.Enabled = true;

            client1 = new TcpClient();
            await client1.ConnectAsync("127.0.0.1", 8800);

            lblMessageSession1.Text = "연결됨";

            using (NetworkStream ns = client1.GetStream())
            {
                while ((client1 != null) && (ns != null))
                {
                    try
                    {
                        byte[] buffer = new byte[4];
                        int bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                        int i = int.Parse(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                        tChart1[0].Add(i);
                        tChart1.Page.Next();
                        lblDataSession1.Text = i.ToString();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void btnDisconnectSession1_Click(object sender, EventArgs e)
        {
            if (client1 != null)
            {
                client1.Close();
                client1.Dispose();
                client1 = null;
            }
            
            btnConnectSession1.Enabled = true;
            btnDisconnectSession1.Enabled = false;
            lblMessageSession1.Text = "연결 종료";
        }

        private async void btnConnectSession2_Click(object sender, EventArgs e)
        {
            btnConnectSession2.Enabled = false;
            btnDisconnectSession2.Enabled = true;

            client2 = new TcpClient();
            await client2.ConnectAsync("127.0.0.1", 8800);

            lblMessageSession2.Text = "연결됨";

            using (NetworkStream ns = client2.GetStream())
            {
                while ((client2 != null) && (ns != null))
                {
                    try
                    {
                        byte[] buffer = new byte[4];
                        int bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                        int i = int.Parse(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                        tChart1[1].Add(i);
                        tChart1.Page.Next();
                        lblDataSession2.Text = i.ToString();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void btnDisconnectSession2_Click(object sender, EventArgs e)
        {
            if (client2 != null)
            {
                client2.Close();
                client2.Dispose();
                client2 = null;
            }

            btnConnectSession2.Enabled = true;
            btnDisconnectSession2.Enabled = false;
            lblMessageSession2.Text = "연결 종료";
        }

        private async void btnConnectSession3_Click(object sender, EventArgs e)
        {
            btnConnectSession3.Enabled = false;
            btnDisconnectSession3.Enabled = true;

            client3 = new TcpClient();
            await client3.ConnectAsync("127.0.0.1", 8800);

            lblMessageSession3.Text = "연결됨";

            using (NetworkStream ns = client3.GetStream())
            {
                while ((client3 != null) && (ns != null))
                {
                    try
                    {
                        byte[] buffer = new byte[4];
                        int bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                        int i = int.Parse(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                        tChart1[2].Add(i);
                        tChart1.Page.Next();
                        lblDataSession3.Text = i.ToString();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void btnDisconnectSession3_Click(object sender, EventArgs e)
        {
            if (client3 != null)
            {
                client3.Close();
                client3.Dispose();
                client3 = null;
            }

            btnConnectSession3.Enabled = true;
            btnDisconnectSession3.Enabled = false;
            lblMessageSession3.Text = "연결 종료";
        }
    }
}
