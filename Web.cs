using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
public class Web : MonoBehaviour
{
    public int chooseMode = 0;
    public string message;
    public static int IsLogin;
    string accountState;
    public static string USER_NAME;
    public bool IsManager = false;
    public SwitchCanvas SC;
    
     void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://127.0.0.1/UnityPhp/getDate.php"));
         //StartCoroutine(Login("123","123321"));
        //StartCoroutine(Register("111","111"));

    }

    //UnityWebRequest.Get
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    
    //UnityWebRequest.POST
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);


        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                
                message = www.error;
                Debug.Log("MESSAGE: " + message);
            }
            else
            {
                
                message = www.downloadHandler.text;
                Debug.Log(message);
                Debug.Log("MESSAGE: "+ message);
                chooseMode = 1;//Login 
                IsLogin = 1;
                USER_NAME = username;
                accountState = File.ReadAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\state.txt");
                Debug.Log("AccountState: "+accountState);

                if(accountState == "success") SceneManager.LoadScene(5);
                if(accountState == "manager") {
                    
                    SC = FindObjectOfType<SwitchCanvas>();
                    IsManager = true;
                    SC.EnterManagerMode();
                }
              
             // SceneManager.LoadScene(5);
            }
           
        }
    }
    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                message = www.error;
                Debug.Log("MESSAGE: " + message );
            }
            else
            {
                message = www.downloadHandler.text;
                Debug.Log("MESSAGE: " + message );
                
                Debug.Log("END:");
                chooseMode = 2;//register
                USER_NAME = username;
                SceneManager.LoadScene(4);
            }
        }
    }
    public IEnumerator JsonToSQL(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/jsonToSql.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator SQLToJosn(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/SqlToJson.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator DeleteUser(string user)
    {
        WWWForm form = new WWWForm();
        form.AddField("User", user);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/Delete.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator UpdataInfor()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/UnityPhp/Updata_Infor.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
