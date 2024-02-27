using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static UserInfo instance;
    
    public string UserID { get; private set; }
    string UserName;
    string UserPassword;
    string Level;
    string Coins;

    private void Awake()
    {
        instance = this;
    }
    public void SetInfo(string name, string password)
    {
        UserName = name;
        UserPassword = password;
    }
    public void SetID(string id)
    {
        UserID = id;
    }

}
