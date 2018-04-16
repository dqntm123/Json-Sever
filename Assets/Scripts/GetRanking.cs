using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using SimpleJSON;

public class GetRanking : MonoBehaviour {


    public string rankURL = "http://dqntm123.cafe24.com/gameserver/getranking.php";
    
    void Start ()
    {
        StartCoroutine(GetRankingData());
	}
	
	IEnumerator GetRankingData()
    {
        WWW www = new WWW(rankURL);
        yield return www;

        var data = JSON.Parse(www.text);
        for (int i = 0; i < data.Count; i++)
        {
            GetComponent<GridContentsManager>().MakeContents(data[i]["UserNick"], data[i]["UserScore"], i , data[i]["UserPic"]);
            //Debug.Log(data[i]["UserNick"]);
            //Debug.Log(data[i]["UserScore"]);
            //Debug.Log(data[i]["UserPic"]);
        }
    }
}
