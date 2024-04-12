using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using static System.Net.WebRequestMethods;

public class WebRequests : MonoBehaviour
{
    [System.Serializable]
    public class ApiResponse
    {
        public bool success;
        public string message;
        public string UserID;
    }
    public static string url = "https://appendicular-conjun.000webhostapp.com/";
    public static IEnumerator Login(string username, string password, Action<ApiResponse> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "Login.php", form))
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

        using (UnityWebRequest www = UnityWebRequest.Post(url+"RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                callback(new ApiResponse { success = false, message = www.error });
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                callback(response);
            }
        }
    }

    public static IEnumerator GetItemsIDs(string userID, Action<String> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"GetItemsIDs.php", form))
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

        using (UnityWebRequest www = UnityWebRequest.Post(url+"GetItem.php", form))
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

    public static IEnumerator SellItem(string ID, string itemID,string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"SellItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                string response = www.downloadHandler.text;
                Debug.Log(response);   
            }
        }
    }
    public static IEnumerator GetItemIcon(string itemID, Action<Sprite> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"GetItemIcon.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                byte[] bytes = www.downloadHandler.data;

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(sprite);
            }
        }
    }
}
