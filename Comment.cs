using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class Comment : MonoBehaviour {
    //***********Network Information*****////
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    Socket clientSocket;
    IPAddress ip;

    private InputField input;
    public int StuId=0;
    public int Step=1;
    private void Awake()
    {
        input = GameObject.Find("InputFields").GetComponent<InputField>();
        ip = IPAddress.Parse("127.0.0.1");
    }
    public void GetInput(string recv)
    {
        Debug.Log("input string is" + recv);
        try
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(ip, 5500)); //配置服务器IP与端口
            Debug.Log("connect successfully");
        }
        catch
        {
            Debug.Log("failed");
            return;
        }
        
        String Msg = "M:" + StuId.ToString() + "_" + Step.ToString() + "_" + recv;
        Debug.Log("start sending comment: "+Msg);
        
        /////*****************Sending file Name String Size***********************************////////
        int varSize = Msg.Length;
        byte[] SentSize = BitConverter.GetBytes(varSize); 
        clientSocket.Send(SentSize, 4, SocketFlags.None);

        ////******************Sending file Name String String*********************************************/////////
        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(Msg);
        int stringsize = clientSocket.Send(myWriteBuffer);
        Console.WriteLine("string size=" + stringsize);
        Debug.Log("Sent to server: " + Msg);
        input.text = "";

    }

}
