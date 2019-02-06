using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System;
using System.Text;
using System.Linq;


public class Client : MonoBehaviour
{

    //public static Network instance;
    [Header("Network Settings")]
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    //public bool isConnected;
    //public TcpClient TCPClient;
    //public NetworkStream myStream;
    //public StreamReader myReader;
    //public StreamWriter myWriter;
    public int StuID;
    string path;
    private string FolderName;
    string[] fileName;
    Socket clientSocket;
    IPAddress ip;
    private void Awake()
    {
        StuID = 1;
        path = Application.streamingAssetsPath;
        FolderName = path + "/video";
        fileName = Array.FindAll(Directory.GetFiles(FolderName), x => !x.EndsWith(".meta"));
        Debug.Log(fileName.Length);
        ip = IPAddress.Parse("127.0.0.1");
        
        for (int i = 0; i < fileName.Length; i++)
        {
            string newfileName;
            if (fileName.Contains("_"))
            {
                newfileName = StuID.ToString() + "_" + Path.GetFileName(fileName[i]);
                string newfilePath = Path.GetDirectoryName(fileName[i]) + "/" + newfileName;
                System.IO.File.Move(fileName[i], newfilePath);
                fileName[i] = newfilePath;
            }
            //Debug.Log(fileName[i]);
        }
    }
    public void SendFile()
    {
         ConnectGameServer();
    }
    void ConnectGameServer()
    {
        for (int i = 0; i < fileName.Length; i++)
        {
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
            Debug.Log("start");

            /////*****************Sending file Name String Size***********************************////////
            int varSize = Path.GetFileName(fileName[i]).Length;
            byte[] SentSize = BitConverter.GetBytes(varSize);
            clientSocket.Send(SentSize,4,SocketFlags.None);

            ////******************Sending file Name String String*********************************************/////////
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(Path.GetFileName(fileName[i]));
            int stringsize=clientSocket.Send(myWriteBuffer);
            Console.WriteLine("string size=" + stringsize);
            Debug.Log("Sent to server：" + Path.GetFileName(fileName[i]));
            
            ////******************Sending File****************************************/////
            clientSocket.SendFile(fileName[i]);
            clientSocket.Close();
            
        }

    }

}

