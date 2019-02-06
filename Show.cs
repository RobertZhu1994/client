using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class Show : MonoBehaviour {
    //***********Network Information*****////
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    Socket clientSocket;
    IPAddress ip;
    public string FolderName;
    public GameObject TextBox;

    private void Awake()
    {
        
        ip = IPAddress.Parse("127.0.0.1");
        FolderName = Application.streamingAssetsPath + "/book/";
        TextBox = GameObject.Find("Canvas/Text");
    }
    public void Request_C()
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
        String Msg = "Request_C:";
        /////*****************Sending Request the server***********************************////////
        int varSize = Msg.Length;
        byte[] SentSize = BitConverter.GetBytes(varSize);
        clientSocket.Send(SentSize, 4, SocketFlags.None);

        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(Msg);
        int stringsize = clientSocket.Send(myWriteBuffer);
        Debug.Log("Sent to server: " + Msg);


        /////*****************Receive Comment message from the server***********************************////////
        string path = FolderName + "Book.xml";
        FileStream fs;
        if (!File.Exists(path))
            fs = new FileStream(path, FileMode.Create);
        else
        {
            fs = new FileStream(path, FileMode.Create);
            Debug.Log("replaced");
        }
        var buffer = new byte[1024];
        int bytesRead;
        while ((bytesRead = clientSocket.Receive(buffer)) > 0)
        {
            fs.Write(buffer, 0, bytesRead);
            fs.Flush();
        }
        fs.Close();
        clientSocket.Close();
        Debug.Log("Successfully received from the cloud");
        TextBox.GetComponent<Text>().text = "";
        Display(path);
    }
    void Display(string path)
    {
        Book book;
        System.Xml.Serialization.XmlSerializer reader;

        /////***********************Read************************///////////
        reader = new System.Xml.Serialization.XmlSerializer(typeof(Book));
        System.IO.StreamReader file = new System.IO.StreamReader(path);
        book = (Book)reader.Deserialize(file);
        file.Close();
        
        
        TextBox.GetComponent<Text>().fontSize = 36;
        for (int i = 0; i < book.len; i++)
        {
            TextBox.GetComponent<Text>().text += ("Student "+book.StuId[i]+"Posted Comment:"+book.comments[i] + "\n");
        }

        //if (book.len == 0)
        //  return;

    }


}
