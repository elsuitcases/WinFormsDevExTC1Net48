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

using WinFormsDevExTC1Net48.Library;

namespace WinFormsTeeChart1Net48
{
    public partial class Form1 : Form
    {
        private Socket skClient1 = null;
        private Socket skClient2 = null;
        private Socket skClient3 = null;



        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += Form1_FormClosing;

            btnDisconnectSession1.Enabled = false;
            btnDisconnectSession2.Enabled = false;
            btnDisconnectSession3.Enabled = false;
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
            skClient1 = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            await skClient1.ConnectAsync(Common.TEECHART_NET_SERVER_IP, Common.TEECHART_NET_SERVER_PORT);

            btnConnectSession1.Enabled = false;
            btnDisconnectSession1.Enabled = true;
            lblMessageSession1.Text = "연결됨";

            await Task.Factory.StartNew((async () =>
            {
                while ((this.skClient1 != null) && (this.skClient1.Connected))
                {
                    string msg = "";

                    while (msg.IndexOf(Common.TEECHART_NET_EOM) <= -1)
                    {
                        byte[] buffer = new byte[Common.TEECHART_NET_BUFFER_SIZE];
                        int bytesRead = await skClient1.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        msg = msg + Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    }
                    
                    string[] strNumbers = msg.Split(new string[1] { Common.TEECHART_NET_EOM }, StringSplitOptions.RemoveEmptyEntries);
                    int[] numbers = new int[strNumbers.Length];

                    for (int i = 0; i < strNumbers.Length; i++)
                    {
                        numbers[i] = int.Parse(strNumbers[i]);
                    }

                    Invoke(new Action(() =>
                    {
                        foreach (int i in numbers)
                        {
                            tChart1[0].Add(i);
                            tChart1.Page.Next();
                            lblDataSession1.Text = i.ToString();
                        }
                    }));
                }
            }));
        }

        private void btnDisconnectSession1_Click(object sender, EventArgs e)
        {
            if (skClient1 != null)
            {
                skClient1.Disconnect(false);
                skClient1.Dispose();
                skClient1 = null;
            }
            
            btnConnectSession1.Enabled = true;
            btnDisconnectSession1.Enabled = false;
            lblMessageSession1.Text = "연결 종료";
        }

        private async void btnConnectSession2_Click(object sender, EventArgs e)
        {
            skClient2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await skClient2.ConnectAsync(Common.TEECHART_NET_SERVER_IP, Common.TEECHART_NET_SERVER_PORT);

            btnConnectSession2.Enabled = false;
            btnDisconnectSession2.Enabled = true;
            lblMessageSession2.Text = "연결됨";

            await Task.Factory.StartNew((async () =>
            {
                while ((this.skClient2 != null) && (this.skClient2.Connected))
                {
                    string msg = "";

                    while (msg.IndexOf(Common.TEECHART_NET_EOM) <= -1)
                    {
                        byte[] buffer = new byte[Common.TEECHART_NET_BUFFER_SIZE];
                        int bytesRead = await skClient2.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        msg = msg + Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    }

                    string[] strNumbers = msg.Split(new string[1] { Common.TEECHART_NET_EOM }, StringSplitOptions.RemoveEmptyEntries);
                    int[] numbers = new int[strNumbers.Length];

                    for (int i = 0; i < strNumbers.Length; i++)
                    {
                        numbers[i] = int.Parse(strNumbers[i]);
                    }

                    Invoke(new Action(() =>
                    {
                        foreach (int i in numbers)
                        {
                            tChart1[1].Add(i);
                            tChart1.Page.Next();
                            lblDataSession2.Text = i.ToString();
                        }
                    }));
                }
            }));
        }

        private void btnDisconnectSession2_Click(object sender, EventArgs e)
        {
            if (skClient2 != null)
            {
                skClient2.Disconnect(false);
                skClient2.Dispose();
                skClient2 = null;
            }

            btnConnectSession2.Enabled = true;
            btnDisconnectSession2.Enabled = false;
            lblMessageSession2.Text = "연결 종료";
        }

        private async void btnConnectSession3_Click(object sender, EventArgs e)
        {
            skClient3 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await skClient3.ConnectAsync(Common.TEECHART_NET_SERVER_IP, Common.TEECHART_NET_SERVER_PORT);

            btnConnectSession3.Enabled = false;
            btnDisconnectSession3.Enabled = true;
            lblMessageSession3.Text = "연결됨";

            await Task.Factory.StartNew((async () =>
            {
                while ((this.skClient3 != null) && (this.skClient3.Connected))
                {
                    string msg = "";

                    while (msg.IndexOf(Common.TEECHART_NET_EOM) <= -1)
                    {
                        byte[] buffer = new byte[Common.TEECHART_NET_BUFFER_SIZE];
                        int bytesRead = await skClient3.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        msg = msg + Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    }

                    string[] strNumbers = msg.Split(new string[1] { Common.TEECHART_NET_EOM }, StringSplitOptions.RemoveEmptyEntries);
                    int[] numbers = new int[strNumbers.Length];

                    for (int i = 0; i < strNumbers.Length; i++)
                    {
                        numbers[i] = int.Parse(strNumbers[i]);
                    }

                    Invoke(new Action(() =>
                    {
                        foreach (int i in numbers)
                        {
                            tChart1[2].Add(i);
                            tChart1.Page.Next();
                            lblDataSession3.Text = i.ToString();
                        }
                    }));
                }
            }));
        }

        private void btnDisconnectSession3_Click(object sender, EventArgs e)
        {
            if (skClient3 != null)
            {
                skClient3.Disconnect(false);
                skClient3.Dispose();
                skClient3 = null;
            }

            btnConnectSession3.Enabled = true;
            btnDisconnectSession3.Enabled = false;
            lblMessageSession3.Text = "연결 종료";
        }
    }
}
