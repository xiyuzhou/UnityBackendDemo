using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    Action<String> _createItemsCallback;
    void Start()
    {
        _createItemsCallback = (JSONArrayString) =>
        {
            StartCoroutine(createItemsRoutine(JSONArrayString));
        };
        CreatItems();
    }

    public void CreatItems()
    {
        StartCoroutine(WebRequests.GetItemsIDs(UserInfo.instance.UserID, _createItemsCallback));
    }

    IEnumerator createItemsRoutine(string jsonArrayString)
    {
        Debug.Log("1");
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string itemID = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(WebRequests.GetItem(itemID, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject item = Instantiate(Resources.Load("Prefab/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            item.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];
        }


        yield return null;
    }

}
