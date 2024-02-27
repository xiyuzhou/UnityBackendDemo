using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WebRequests : MonoBehaviour
{
    [System.Serializable]
    public class ApiResponse
    {
        public bool success;
        public string message;
        public string UserID;
    }

    public static IEnumerator Login(string username, string password, Action<ApiResponse> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                callback(new ApiResponse { success = false, message = www.error });
            }
            else
            {
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                UserInfo.instance.SetInfo(username, password);
                UserInfo.instance.SetID(response.UserID);
                callback(response);
            }
        }
    }

    public static IEnumerator RegisterUser(string username, string password, Action<ApiResponse> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                callback(new ApiResponse { success = false, message = www.error });
            }
            else
            {
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                callback(response);
            }
        }
    }

    public static IEnumerator GetItemsIDs(string userID, Action<String> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/GetItemsIDs.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                callback(www.error);
            }
            else
            {
                //ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                string response = www.downloadHandler.text;

                callback(response);
            }
        }
    }
    public static IEnumerator GetItem(string itemID, Action<String> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/GetItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                callback(www.error);
            }
            else
            {
                //ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                string response = www.downloadHandler.text;

                callback(response);
            }
        }
    }
}
