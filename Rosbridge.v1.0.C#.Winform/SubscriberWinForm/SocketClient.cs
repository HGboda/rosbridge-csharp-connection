
//Houssem Dellai
//houssem.dellai@live.com
//This app is based on a simple code from SharpDevelop.

using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using Newtonsoft.Json;
//using RosBridgeDotNet;
using System.Text.RegularExpressions;

namespace SubscriberWinForm
{
	/// <summary>
	/// Description of SocketClient.	
	/// </summary>
	public class SocketClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.RichTextBox richTextRxMessage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxConnectStatus;
		private System.Windows.Forms.RichTextBox richTextTxName;
		private System.Windows.Forms.Button buttonSendMessage;
		private System.Windows.Forms.Button buttonClose;
		
		byte[] m_dataBuffer = new byte [10];
		IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
        private Label label6;
        private Label label7;
        private RichTextBox richTextBoxHistoryResponses;
        private Label label8;
        private RichTextBox richTextBoxHistroryRequests;
        private Button button1;
        private Button button2;
        private Button button3;
		public Socket m_clientSocket;
		
		public SocketClient()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			textBoxIP.Text = GetIP();
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketClient());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.richTextTxName = new System.Windows.Forms.RichTextBox();
            this.textBoxConnectStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextRxMessage = new System.Windows.Forms.RichTextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBoxHistoryResponses = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBoxHistroryRequests = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(400, 421);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(104, 24);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(8, 184);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(152, 24);
            this.buttonSendMessage.TabIndex = 14;
            this.buttonSendMessage.Text = "Send Request";
            this.buttonSendMessage.Click += new System.EventHandler(this.ButtonSendMessageClick);
            // 
            // richTextTxName
            // 
            this.richTextTxName.Location = new System.Drawing.Point(8, 131);
            this.richTextTxName.Name = "richTextTxName";
            this.richTextTxName.Size = new System.Drawing.Size(152, 29);
            this.richTextTxName.TabIndex = 2;
            this.richTextTxName.Text = "";
            // 
            // textBoxConnectStatus
            // 
            this.textBoxConnectStatus.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxConnectStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConnectStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBoxConnectStatus.Location = new System.Drawing.Point(128, 429);
            this.textBoxConnectStatus.Name = "textBoxConnectStatus";
            this.textBoxConnectStatus.ReadOnly = true;
            this.textBoxConnectStatus.Size = new System.Drawing.Size(240, 13);
            this.textBoxConnectStatus.TabIndex = 10;
            this.textBoxConnectStatus.Text = "Not Connected";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Message To Server";
            // 
            // richTextRxMessage
            // 
            this.richTextRxMessage.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextRxMessage.Location = new System.Drawing.Point(331, 80);
            this.richTextRxMessage.Name = "richTextRxMessage";
            this.richTextRxMessage.ReadOnly = true;
            this.richTextRxMessage.Size = new System.Drawing.Size(233, 128);
            this.richTextRxMessage.TabIndex = 1;
            this.richTextRxMessage.Text = "";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(112, 31);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxPort.TabIndex = 6;
            this.textBoxPort.Text = "8000";
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.Yellow;
            this.buttonConnect.Location = new System.Drawing.Point(344, 8);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 48);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect To Server";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 429);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Connection Status";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(112, 8);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(152, 20);
            this.textBoxIP.TabIndex = 3;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.BackColor = System.Drawing.Color.Red;
            this.buttonDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Yellow;
            this.buttonDisconnect.Location = new System.Drawing.Point(432, 8);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 48);
            this.buttonDisconnect.TabIndex = 15;
            this.buttonDisconnect.Text = "Disconnet From Server";
            this.buttonDisconnect.UseVisualStyleBackColor = false;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP Address";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Port";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(328, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Message From Server";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "Name of the Requested Event";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(331, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(233, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "History of Responses From Broker";
            // 
            // richTextBoxHistoryResponses
            // 
            this.richTextBoxHistoryResponses.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBoxHistoryResponses.Location = new System.Drawing.Point(331, 257);
            this.richTextBoxHistoryResponses.Name = "richTextBoxHistoryResponses";
            this.richTextBoxHistoryResponses.ReadOnly = true;
            this.richTextBoxHistoryResponses.Size = new System.Drawing.Size(233, 128);
            this.richTextBoxHistoryResponses.TabIndex = 17;
            this.richTextBoxHistoryResponses.Text = "";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 19);
            this.label8.TabIndex = 20;
            this.label8.Text = "History of Requests to Broker";
            // 
            // richTextBoxHistroryRequests
            // 
            this.richTextBoxHistroryRequests.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBoxHistroryRequests.Location = new System.Drawing.Point(8, 257);
            this.richTextBoxHistroryRequests.Name = "richTextBoxHistroryRequests";
            this.richTextBoxHistroryRequests.ReadOnly = true;
            this.richTextBoxHistroryRequests.Size = new System.Drawing.Size(211, 128);
            this.richTextBoxHistroryRequests.TabIndex = 19;
            this.richTextBoxHistroryRequests.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(171, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 43);
            this.button1.TabIndex = 14;
            this.button1.Text = "Subscr";
            this.button1.Click += new System.EventHandler(this.ButtonSendSubscribeMessage);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 43);
            this.button2.TabIndex = 14;
            this.button2.Text = "Pub";
            this.button2.Click += new System.EventHandler(this.ButtonSendPublishMessage);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button3.Location = new System.Drawing.Point(243, 109);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 43);
            this.button3.TabIndex = 14;
            this.button3.Text = "UnSubscr";
            this.button3.Click += new System.EventHandler(this.ButtonSendUnSubscribeMessage);
            // 
            // SocketClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(631, 492);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.richTextBoxHistroryRequests);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBoxHistoryResponses);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxConnectStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.richTextTxName);
            this.Controls.Add(this.richTextRxMessage);
            this.Name = "SocketClient";
            this.Text = "Subscriber";
            this.Load += new System.EventHandler(this.SocketClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		void ButtonCloseClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close ();
				m_clientSocket = null;
			}		
			Close();
		}
		
		void ButtonConnectClick(object sender, System.EventArgs e)
		{
			// See if we have text on the IP and Port text fields
			if(textBoxIP.Text == "" || textBoxPort.Text == ""){
				MessageBox.Show("IP Address and Port Number are required to connect to the Server\n");
				return;
			}
			try
			{
                UpdateControls(false);
                m_clientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				
				// Cet the remote IP address
                IPAddress ip = IPAddress.Parse(textBoxIP.Text);
                int iPortNo = System.Convert.ToInt16(textBoxPort.Text);
				// Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
				// Connect to the remote host
                m_clientSocket.Connect(ipEnd);
				if(m_clientSocket.Connected) {
					
					UpdateControls(true);
					//Wait for data asynchronously 
					WaitForData();
				}
                m_clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                byte[] handShake = Encoding.UTF8.GetBytes("raw\r\n\r\n");
                m_clientSocket.Send(handShake);
			}
			catch(SocketException se)
			{
				string str;
				str = "\nConnection failed, is the server running?\n" + se.Message;
				MessageBox.Show (str);
				UpdateControls(false);
			}		
		}			

        /// <summary>
        /// Sends the Object to the Broker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void ButtonSendMessageClick(object sender, System.EventArgs e)
		{
			try
			{
                //save the request in history.
                richTextBoxHistroryRequests.Text += "\n \\\\\\\\\\\\\\ \n Request : " + richTextTxName.Text;

                Object objData = SerializeEventData(new EventData(richTextTxName.Text)) + "ENDOFMESSAGE";
				byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString ());
				if(m_clientSocket != null)
                {
                    m_clientSocket.Send(new byte[] { 0 });    // \x00
					m_clientSocket.Send(byData);
                    m_clientSocket.Send(new byte[] { 255 });    // \xff
				}
            }
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}	
		}

        /// <summary>
        /// Serialize a given EventData.
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        private string SerializeEventData(EventData eventData)
        {
            StreamWriter stWriter = null;
            XmlSerializer xmlSerializer;
            string buffer;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(EventData));
                MemoryStream memStream = new MemoryStream();
                stWriter = new StreamWriter(memStream);
                System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
                xs.Add("", "");
                
                xmlSerializer.Serialize(stWriter, eventData, xs);
                buffer = Encoding.UTF8.GetString(memStream.GetBuffer());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (stWriter != null)
                    stWriter.Close();
            }
            return buffer;
        }

		public void WaitForData()
		{
			try
			{
				if  ( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.thisSocket = m_clientSocket;
				// Start listening to the data asynchronously
				m_result = m_clientSocket.BeginReceive (theSocPkt.dataBuffer,
				                                        0, theSocPkt.dataBuffer.Length,
				                                        SocketFlags.None, 
				                                        m_pfnCallBack, 
				                                        theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}

		}
		public class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1];
		}

        /// <summary>
        /// When receiving data from the server(Broker).
        /// </summary>
        /// <param name="asyn"></param>
		public  void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
				SocketPacket theSockId = (SocketPacket)asyn.AsyncState ;
				int iRx  = theSockId.thisSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);
                try
                {
                    szData = Regex.Replace(szData, @"[^\u0000-\u007F]", string.Empty);
                    System.Diagnostics.Debug.WriteLine(szData.ToString());
                    //richTextRxMessage.Text = richTextRxMessage.Text + szData[2];
                }
                catch (Exception e){
                    //MessageBox.Show(e.ToString());
                }
                //If we finished receiving the response (EventData object) from the Broker
                //then we will recover it
                /*if (richTextRxMessage.Text.Contains("ENDOFMESSAGE"))
                {
                    //Remove the flag "ENDOFMESSAGE"
                    String pureResponse = richTextRxMessage.Text
                        .Remove(richTextRxMessage.Text.IndexOf("ENDOFMESSAGE"), 12);
                    RetrieveReceivedEventData(pureResponse);

                    richTextBoxHistoryResponses.Text += "\n \\\\\\\\\\\\ \n Response : " + pureResponse;
                    richTextRxMessage.Text = "";
                }*/

				WaitForData();
			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}

        private void RetrieveReceivedEventData(string p)
        {
            EventData receivedEventData = DeserializeReceivedEventData(p);
            
            //richTextBoxHistoryResponses.Text += "\n //////////////////////// \n"
            //        + "\n Request : " + receivedEventData.Name
            //        + "\n Request : " + receivedEventData.Details;
        }

        /// <summary>
        /// Deserialize a given EventData Object.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private EventData DeserializeReceivedEventData(string p)
        {
            EventData eventData = new EventData();
            //Deserialize the received Event
            XmlSerializer xmlSerializer;
            MemoryStream memStream = null;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(EventData));
                byte[] bytes = new byte[p.Length];
                Encoding.UTF8.GetBytes(p, 0, p.Length, bytes, 0);
                memStream = new MemoryStream(bytes);
                object objectFromXml = xmlSerializer.Deserialize(memStream);
                eventData = (EventData)objectFromXml;
            }
            catch (Exception Ex)
            {
                //throw Ex;
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (memStream != null)
                    memStream.Close();
                
            }
            return eventData;
        }
	
		private void UpdateControls( bool connected ) 
		{
			buttonConnect.Enabled = !connected;
			buttonDisconnect.Enabled = connected;
			string connectStatus = connected? "Connected" : "Not Connected";
			textBoxConnectStatus.Text = connectStatus;
		}

		void ButtonDisconnectClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close();
				m_clientSocket = null;
				UpdateControls(false);
			}
		}
	   //----------------------------------------------------	
	   // This is a helper function used (for convenience) to 
	   // get the IP address of the local machine
   	   //----------------------------------------------------
   	   String GetIP()
	   {	   
	   		String strHostName = Dns.GetHostName();
		
		   	// Find host by name
		   	IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
		
		   	// Grab the first IP addresses
		   	String IPStr = "";
		   	foreach(IPAddress ipaddress in iphostentry.AddressList){
		        IPStr = ipaddress.ToString();
		   		return IPStr;
		   	}
		   	return IPStr;
       }

       private void SocketClient_Load(object sender, EventArgs e)
       {
           textBoxIP.Text = "10.2.230.124";
           textBoxPort.Text = "9090";
       }
       private void ButtonSendSubscribeMessage(object sender, EventArgs e)
       {
           try
           {
               object[] poseSubscribe = { "/turtle1/pose", -1 };
               RosBridgeDotNet.RosBridgeDotNet.SubscribeMessage m = new RosBridgeDotNet.RosBridgeDotNet.SubscribeMessage("/rosbridge/subscribe", poseSubscribe);
               string needToSend = JsonConvert.SerializeObject(m);
               richTextBoxHistroryRequests.Text += "\n \\\\\\\\\\\\\\ \n Request : " + needToSend; //save the request in history.
               //Object objData = SerializeEventData(new EventData(richTextTxName.Text));
               //byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString());
               if (m_clientSocket != null)
               {
                   m_clientSocket.Send(new byte[] { 0 });    // \x00
                   m_clientSocket.Send(Encoding.UTF8.GetBytes(needToSend));
                   m_clientSocket.Send(new byte[] { 255 });    // \xff
               }
           }
           catch (SocketException se)
           {
               MessageBox.Show(se.Message);
           }
       }
       private void ButtonSendPublishMessage(object sender, EventArgs e)
       {
           try
           {
               string topic = "/turtle1/command_velocity";
               string msgtype = "turtlesim/Velocity";
               RosBridgeDotNet.RosBridgeDotNet.TurtleSim turtleGo1 = new RosBridgeDotNet.RosBridgeDotNet.TurtleSim(20, 20);
               RosBridgeDotNet.RosBridgeDotNet.PublishMessage m = new RosBridgeDotNet.RosBridgeDotNet.PublishMessage(topic, msgtype, turtleGo1);
               string needToSend = JsonConvert.SerializeObject(m);
               richTextBoxHistroryRequests.Text += "\n \\\\\\\\\\\\\\ \n Request : " + needToSend; //save the request in history.
               //Object objData = SerializeEventData(new EventData(richTextTxName.Text));
               //byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString());
               if (m_clientSocket != null)
               {
                   m_clientSocket.Send(new byte[] { 0 });    // \x00
                   m_clientSocket.Send(Encoding.UTF8.GetBytes(needToSend));
                   m_clientSocket.Send(new byte[] { 255 });    // \xff
               }
           }
           catch (SocketException se)
           {
               MessageBox.Show(se.Message);
           }	
       }

       private void ButtonSendUnSubscribeMessage(object sender, EventArgs e)
       {
           try
           {
               object[] poseSubscribe = { "/turtle1/pose", -1 };
               RosBridgeDotNet.RosBridgeDotNet.SubscribeMessage u = new RosBridgeDotNet.RosBridgeDotNet.SubscribeMessage("/rosbridge/unsubscribe", poseSubscribe);
               string needToSend = JsonConvert.SerializeObject(u);
               richTextBoxHistroryRequests.Text += "\n \\\\\\\\\\\\\\ \n Request : " + needToSend; //save the request in history.
               //Object objData = SerializeEventData(new EventData(richTextTxName.Text));
               //byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString());
               if (m_clientSocket != null)
               {
                   m_clientSocket.Send(new byte[] { 0 });    // \x00
                   m_clientSocket.Send(Encoding.UTF8.GetBytes(needToSend));
                   m_clientSocket.Send(new byte[] { 255 });    // \xff
               }
           }
           catch (SocketException se)
           {
               MessageBox.Show(se.Message);
           }
       }

	}
}
