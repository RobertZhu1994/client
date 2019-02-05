using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network : MonoBehaviour
{

    public static Network instance;

    [Header("Network Settings")]
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    public bool isConnected;

    public TcpClient PlayerSocket;
    public NetworkStream myStream;
    public StreamReader myReader;
    public StreamWriter myWriter;
    public int StuID;
    private byte[] asyncBuff;
    public bool shouldHandleData;
    private byte[] myBytes;
    string path;
    private string FileName;
    private string FolderName;

    private void Awake()
    {
        instance = this;
        path = Application.streamingAssetsPath;
    }

    // Use this for initialization
    void Start()
    {
        //FileStream fs = File.Open("", FileMode.Open);
        //fs.CopyToAsync(myStream);
        FolderName = "video/";
        FileName = FolderName + "video1.mp4";
        path = path + '/' + FileName;
        //StuID=
        //FolderName = "audio";
        //FolderName = "picture/";
        ConnectGameServer();
    }

    void ConnectGameServer()
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket.Connected || isConnected)
                return;
            PlayerSocket.Close();
            PlayerSocket = null;
        }

        PlayerSocket = new TcpClient();
        PlayerSocket.ReceiveBufferSize = 4096;
        PlayerSocket.SendBufferSize = 4096;
        PlayerSocket.NoDelay = false;
        Array.Resize(ref asyncBuff, 8192);
        PlayerSocket.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), PlayerSocket);
        isConnected = true;
        //MenuManager.instance._menu = MenuManager.Menu.Home;
    }

    void ConnectCallback(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            PlayerSocket.EndConnect(result);
            if (PlayerSocket.Connected == false)
            {
                isConnected = false;
                return;
            }
            else
            {
                PlayerSocket.NoDelay = true;

                myStream = PlayerSocket.GetStream();    //***********Very Important******
                //myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
                Upload();
            }
        }
    }

    private void Upload()
    {

        using (FileStream fs = new FileStream(path, FileMode.Open))
            //fs.CopyTo(myStream);
            CopyStream(fs, myStream);
        Debug.Log("You got disconnected from the Server.");
        PlayerSocket.Close();
        return;
    }

    public static long CopyStream(Stream source, Stream target)
    {
        const int bufSize = 0x1000;
        byte[] buf = new byte[bufSize];

        long totalBytes = 0;
        int bytesRead = 0;

        while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
        {
            target.Write(buf, 0, bytesRead);
            totalBytes += bytesRead;
        }
        return totalBytes;
    }
    /*
    private void Update()
    {
        if (shouldHandleData)
        {
            ClientHandleData.instance.HandleData(myBytes);
            shouldHandleData = false;
        }
    }
    
    void OnReceive(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket == null)
                return;

            int byteArray = myStream.EndRead(result);
            myBytes = null;
            Array.Resize(ref myBytes, byteArray);
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteArray);

            if (byteArray == 0)
            {
                Debug.Log("You got disconnected from the Server.");
                PlayerSocket.Close();
                return;
            }

            shouldHandleData = true;

            if (PlayerSocket == null)
                return;
            myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
        }
        
       }
       */

}
