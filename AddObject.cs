using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

public class DllTest
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOpenFileName1([In, Out] OpenFileName ofn)

    {
        return GetOpenFileName(ofn);
    }
}
public class AddObject : MonoBehaviour
{
    // Start is called before the first frame update
    public string FileName;
    public GameObject dd;
    public void onDropdown(){
        dd.SetActive(true);
    }
    public void Click(int folder)
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);

        //三菱(*.gxw)\0*.gxw\0西?子(*.mwp)\0*.mwp\0All Files\0*.*\0\0  
        ofn.filter = "All Files\0*.*\0\0";

        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;

        ofn.initialDir =UnityEngine.Application.dataPath;//默?路?  

        ofn.title = "Open Project";

        ofn.defExt = "JPG";//?示文件的?型  
                           //注意 一下?目不一定要全? 但是0x00000008?不要缺少  
        ofn.flags=0x00080000|0x00001000|0x00000800|0x00000200|0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

        if (DllTest.GetOpenFileName(ofn) )
        {

            // StartCoroutine(WaitLoad(ofn.file));//加??片到panle  

            Debug.Log("Selected file with full path: {0}"+ofn.file);
            string fileName = ofn.fileTitle;
            FileName = fileName;
            string sourcePath = ofn.file;
            string targetPath = @"C:\Users\HUANG HUNG CHIN\UnityPrject\3d_Object\Assets\Resources\"+Fclass(folder);
            
            // Use Path class to manipulate file and directory paths.
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            Debug.Log("in add");
            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            System.IO.Directory.CreateDirectory(targetPath);


            System.IO.File.Copy(sourcePath, destFile, true);
            Debug.Log(ofn.file);
            Debug.Log(ofn.fileTitle);
            Debug.Log("FileName" + FileName);
        }
    }
    public string Fclass(int i){
        string s = "Other";
        switch (i)
        {
            case 0:
                s = "Other";
                break;
            case 1:
                s = "Sofa";
                break;
            case 2:
                s = "Table";
                break;
            case 3:
                s = "Chair";
                break;
            case 4:
                s = "Desk";
                break;
            case 5:
                s = "Cabinet";
                break;
            case 6:
                s = "Bed";
                break;
            case 7:
                s = "Other";
                break;
            
            default:
                break; 
        }
        return s;
    }
    
}



