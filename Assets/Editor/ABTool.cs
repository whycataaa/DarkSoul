using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;



public static class ABTool
{
    private static readonly string ABCompareTextName="ABCompareInfo.txt";


    [MenuItem("ABTool/CreateABCompareText")]
    public static void CreateABCompareFile()
    {
        DirectoryInfo directory=Directory.CreateDirectory(Application.dataPath+"/Resources/PC/");

        FileInfo[] fileInfos=directory.GetFiles();

        string abCompareInfo="";

        foreach(FileInfo fileInfo in fileInfos)
        {
            if(fileInfo.Extension=="")
            {
                abCompareInfo+=fileInfo.Name+" "+fileInfo.Length+" "+GetMD5(fileInfo.FullName);
                abCompareInfo+="|";
            }
        }
        

        File.WriteAllText(Application.dataPath+"/Resources/PC/"+ABCompareTextName,abCompareInfo);
        AssetDatabase.Refresh();
    }

    [MenuItem("ABTool/UpLoadABAndABCompareText")]
    public static void UpLoadABAndABCompareText()
    {
        DirectoryInfo directory=Directory.CreateDirectory(Application.dataPath+"/Resources/PC/");
        FileInfo[] fileInfos=directory.GetFiles();
        foreach(FileInfo fileInfo in fileInfos)
        {
            if(fileInfo.Extension==""||fileInfo.Name==ABCompareTextName)
            {
                //上传
            }
        }
    }

    private static void FtpUpLoad(string filePath,string fileName)
    {
        FtpWebRequest ftpWebRequest=FtpWebRequest.Create(new Uri(""+fileName)) as FtpWebRequest;

        NetworkCredential credential=new NetworkCredential("name","password");

        ftpWebRequest.Credentials=credential;

        ftpWebRequest.Proxy=null;

        ftpWebRequest.KeepAlive=false;

        ftpWebRequest.Method=WebRequestMethods.Ftp.UploadFile;

        ftpWebRequest.UseBinary=true;

        Stream upLoadStream=ftpWebRequest.GetRequestStream();

        using(FileStream file=File.OpenRead(filePath))
        {
            byte[] bytes=new byte[2048];
            int contentLength=file.Read(bytes,0,bytes.Length);
        }
    
    }
    /// <summary>
    /// 根据文件路径得到MD5码
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>16字节MD5码</returns>
    public static string GetMD5(string filePath)
    {
        using (FileStream file=new FileStream(filePath,FileMode.Open))
        {
            MD5 md5=new MD5CryptoServiceProvider();
            byte[] md5Info=md5.ComputeHash(file);

            file.Close();

            StringBuilder sb=new StringBuilder();

            for(int i=0;i<md5Info.Length;i++)
            {
                sb.Append(md5Info[i].ToString("x2"));
            }

            return sb.ToString();
        }

    }
}
