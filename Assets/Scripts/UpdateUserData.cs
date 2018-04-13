using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUserData : MonoBehaviour {

    public GameManager gameManager;
    public string updateURL = "http://dqntm123.cafe24.com/gameserver/updateuser.php";
    
    public void UpdateToServer()//UpdateCorutine 코루틴을 호출하기위한 함수
    {
        StartCoroutine(UpdateCorutine());//UpdateCorutine을 실행
    }

    IEnumerator UpdateCorutine()
    {
        WWWForm form = new WWWForm();//클라이언트 데이터를 보내기위한 새로운 폼(WWWForm) 생성
        form.AddField("UserID", PlayerPrefs.GetInt("UserID"));//생성된 form에다 UserID에 key,클라이언트에 저장된 UserID에 value를 필드에 추가
        form.AddField("UserNick", PlayerPrefs.GetString("UserNick"));////생성된 form에다 UserNick에 key,클라이언트에 저장된 UserNick에 value를 필드에 추가
        form.AddField("UserGold",gameManager.gold);////생성된 form에다 UserGold에 key,클라이언트에 저장된 UserGold에 value를 필드에 추가
        form.AddField("UserCash",gameManager.cash);////생성된 form에다 UserCash에 key,클라이언트에 저장된 UserCash에 value를 필드에 추가
        form.AddField("UserScore",gameManager.score);////생성된 form에다 UserScore에 key,클라이언트에 저장된 UserScore에 value를 필드에 추가
        WWW www = new WWW(updateURL, form);//생성한 WWW클래스 안에 새로운 WWW클래스안에 따로 선언한 updateURL과 form을 인자값으로 집어넣는다.
        yield return www;//www가 반환될때까지 대기
        Debug.Log(www.text);
    }
}
