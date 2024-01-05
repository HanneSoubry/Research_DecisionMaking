using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class FileWriter : MonoBehaviour
{
    private StreamWriter streamWriter = null;
    bool isFileOpen = false;

    // Singleton
    public static FileWriter instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void NewFile(string fileName)
    {
        if(isFileOpen)
        {
            CloseFile();
        }

        streamWriter = new StreamWriter(fileName);
        isFileOpen = true;
    }

    public void WriteToFile(string text)
    {
        if(isFileOpen)
        {
            streamWriter.Write(text);
        }
    }

    public void WriteLineToFile(string text)
    {
        if (isFileOpen)
        {
            streamWriter.WriteLine(text);
        }
    }

    public void CloseFile()
    {
        if(isFileOpen)
        {
            streamWriter.Close();
        }
    }

    public void OnApplicationQuit()
    {
        if(isFileOpen)
        {
            CloseFile();
        }
    }
}
