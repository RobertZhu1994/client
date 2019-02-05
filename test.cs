using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{


    //Book book = new Book();
    //BookInstance.Book book = new BookInstance.Book();
    Book Book = new Book();
    private string FolderName;
    /*
    private void Awake()
    {
        FolderName = Application.streamingAssetsPath + "/video";
    }
    */
    public void ReadXML()
    {
        FolderName = Application.streamingAssetsPath + "/video";
        System.Xml.Serialization.XmlSerializer reader =new System.Xml.Serialization.XmlSerializer(typeof(Book));
        System.IO.StreamReader file = new System.IO.StreamReader(FolderName + "/stu.xml");
        Book overview = (Book)reader.Deserialize(file);
        file.Close();
        Debug.Log("test result");
        overview.print();
        

    }


}