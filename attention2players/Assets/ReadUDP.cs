using System.Collections;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
public class ReadUDP : MonoBehaviour
 {

	 // port
	 public int portLocal1 = 8000;
	 public int portLocal2 = 8001;

	// UDP client objects
	 UdpClient client1;
	 UdpClient client2;

	// Receiving thread
	Thread receiveThread;
	private bool onReceive = true;

	private float timeshift = 0;
	public string data1hex;
	public string data2hex;
	public float data1float;
	public float data2float;
	Encoding utf8 = Encoding.UTF8;
	void Start () 
	{
		init();
	}

	private void init()
	{
		print("UDP object init");

		// Create local client
		client1 = new UdpClient(portLocal1);
		client2 = new UdpClient(portLocal2);

		// Create new thread for reception of incoming data
		receiveThread = new Thread (
			new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		print("ReadUDP is listening");
	}

	// Receive data, update received packages
	private void ReceiveData()
	{
		print("ReceiveData func");
		do
		{
			try
			{
				IPEndPoint IP8000 = new IPEndPoint(IPAddress.Any, portLocal1);
				IPEndPoint IP8001 = new IPEndPoint(IPAddress.Any, portLocal2);
				print("Pass IP");

				byte[] data1 = client1.Receive(ref IP8000);
				byte[] data2 = client2.Receive(ref IP8001);
				print("Pass receiving data");

				data1hex = BitConverter.ToString(data1, 0); // hex string
				data2hex = BitConverter.ToString(data2, 0); // hex string
				string data1utf8 = utf8.GetString(data1);	// dec string
				string data2utf8 = utf8.GetString(data2);	// dec string
				data1float = float.Parse(data1utf8); 		// float
				data2float = float.Parse(data2utf8);		// float
				print("Data1hex : " + data1hex);
				print("Data1dec : " + data1utf8);
				print("Data1f : " + data1float);
				print("Data2hex : " + data2hex);
				print("Data2dec : " + data2utf8);
				print("Data2f : " + data2float);

				print("Pass bit converter");
			}
			catch(Exception err)
			{
				print(err.ToString());
			}
		}while(onReceive);
	}
	void OnDisable()
	{
		onReceive = false;
		if(receiveThread != null)
			receiveThread.Abort();
		client1.Close();
		client2.Close();
		print("Close all");
	}
}
