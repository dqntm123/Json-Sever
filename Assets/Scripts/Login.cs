using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using SimpleJSON;

public class Login : MonoBehaviour {

    public string verCheckURL = "";
	public string gameServerURL ="";
    public GameObject loginBTN;
    public GameObject popUpWindow;
    public UILabel popUpWindowText;

    void Start()
    {
        if (Application.internetReachability == 0)
        {
            popUpWindow.SetActive(true);
            popUpWindowText.GetComponent<UILabel>().text = "Could Not Connect Internet";
            Debug.Log("인터넷 연결 상태를 확인하세요");
        }
        else
        {
            StartCoroutine(VerCheck());
        }
    }

    IEnumerator VerCheck()
    {
        WWW www = new WWW(verCheckURL);
        yield return www;
        if(www.text == Application.version)
        {
            loginBTN.SetActive(true);
        }
        else
        {
            popUpWindow.SetActive(true);
            popUpWindowText.GetComponent<UILabel>().text = "Version is not Match";
        }
    }

    IEnumerator StartLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserId", PlayerPrefs.GetInt("UserID"));
        form.AddField("UUID", SystemInfo.deviceUniqueIdentifier);
        WWW www = new WWW(gameServerURL,form);
        yield return www;
        Debug.Log(www.text);
        SetMyGameData(www.text);
    }
    public void SetMyGameData(string data)
    {
       var gameData= JSON.Parse(data);

        Debug.Log(gameData["UserID"]);
        Debug.Log(gameData["UserNick"]);
        Debug.Log(gameData["UserGold"]);
        Debug.Log(gameData["UserCash"]);
        Debug.Log(gameData["UserScore"]);
    }

    public void OpenMarket()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.zabob.infinitydungeon2");
    }
    public void LoginBtn()
    {
        StartCoroutine(StartLogin());
    }
}
