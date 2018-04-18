using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using SimpleJSON;
using UnityEngine.SceneManagement;//씬매니저 클래스를 사용하기위한 선언
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Login : MonoBehaviour {

    public string verCheckURL = "";//버젼체크를 위한 서버URL
	public string gameServerURL ="";//게임서버 접속을 위한 URL
    public GameObject loginBTN;//로그인버튼
    public GameObject popUpWindow;//팝업윈도우
    public UILabel popUpWindowText;//팝업윈도우 내 텍스트
    public UILabel logText;
    void Start()
    {
        if (Application.internetReachability == 0)//만약 어플의 인터넷연결상태가 되어있지않다면..
        {
            popUpWindow.SetActive(true);//팝업윈도우 게임오브젝트 활성화
            popUpWindowText.GetComponent<UILabel>().text = "Could Not Connect Internet";//팝업윈도우 내 텍스트ㅇ
            Debug.Log("인터넷 연결 상태를 확인하세요");
        }
        else//인터넷이 연결이 되어 있다면...
        {
            StartCoroutine(VerCheck());//버젼체크의 코루틴 호출
        }
    }

    IEnumerator VerCheck()//버젼을 체크하기 위한 코루틴
    {
        WWW www = new WWW(verCheckURL);//http 접속을 위한 새로운 WWW 클래스 생성
        yield return www;//www가 반환될때까지 잠시대기
        if(www.text == Application.version)//www에서 반환된 텍스트가 Application.version과 같다면
        {
            loginBTN.SetActive(true);//로그인버튼 오브젝트 활성화
        }
        else//www에서 반환된 텍스트가 Application.version과 다르다면
        {
            popUpWindow.SetActive(true);//팝업윈도우 오브젝트 활성화
            popUpWindowText.GetComponent<UILabel>().text = "Version is not Match";//팝업윈도우 내 텍스트 전환
        }
    }

    IEnumerator StartLogin()//로그인 과정을위한 코루틴
    {
        WWWForm form = new WWWForm();//클라이언트 데이터를 보내기위한 새로운 폼(WWWForm) 생성
        form.AddField("UserID", PlayerPrefs.GetInt("UserID"));//생성된 form에다가 key갑과 value값을 추가한다
        form.AddField("UUID", SystemInfo.deviceUniqueIdentifier);//생성된 form에다가 key갑과 value값을 추가한다
        WWW www = new WWW(gameServerURL,form);//생성한 WWW클래스 안에 새로운 WWW클래스안에 따로 선언한 updateURL과 form을 인자값으로 집어넣는다.
        yield return www;//www가 반환될때까지 잠시대기
        //Debug.Log(www.text);
        SetMyGameData(www.text);//www에서 반환된 텍스트를 SetMyGameData()의 인자로 호출
        loginBTN.SetActive(false);//로그인버튼 오브젝트 비활성화
        SceneManager.LoadScene(1);//1번의 씬을 호출
    }
    public void SetMyGameData(string data)//www에서 받아온 JSON srting 데이터를 Parse하여 PlayerPrefabs에 저장하는 함수
    {
       var gameData= JSON.Parse(data);//www에서 받아온 JSON srting 데이터를 var로 선언
        PlayerPrefs.SetInt("UserID", int.Parse(gameData["UserID"]));//JSON 내에 UserID란 key를 가진 value를 int로 변환하여 저장
        PlayerPrefs.SetString("UserNick", gameData["UserNick"]);//JSON 내에 UserNick란 key를 가진 value를 sting으로 저장
        PlayerPrefs.SetInt("UserGold", int.Parse(gameData["UserGold"]));//JSON 내에 UserGold란 key를 가진 value를 int로 변환하여 저장
        PlayerPrefs.SetInt("UserCash", int.Parse(gameData["UserCash"]));//JSON 내에 UserCash란 key를 가진 value를 int로 변환하여 저장
        PlayerPrefs.SetInt("UserScore", int.Parse(gameData["UserScore"]));//JSON 내에 UserScore란 key를 가진 value를 int로 변환하여 저장

        //user[0].GetComponent<UILabel>().text = "ID   " + int.Parse(gameData["UserID"]).ToString();
        //user[1].GetComponent<UILabel>().text = "Name  " + gameData["UserNick"].ToString();
        //user[2].GetComponent<UILabel>().text = "Gold  " + int.Parse(gameData["UserGold"]).ToString();
        //user[3].GetComponent<UILabel>().text = "Cash  " + int.Parse(gameData["UserCash"]).ToString();
        //user[4].GetComponent<UILabel>().text = "Score  " + int.Parse(gameData["UserScore"]).ToString();
        //Debug.Log(gameData["UserID"].GetType());
        //Debug.Log(gameData["UserNick"].GetType());
        //Debug.Log(gameData["UserGold"].GetType());
        //Debug.Log(gameData["UserCash"].GetType());
        //Debug.Log(gameData["UserScore"].GetType());
    }

    public void OpenMarket()//마켓링크를 열기위한 함수
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.zabob.infinitydungeon2");
    }
    public void LoginBtn()//로그인 버튼이 눌렸을때 StartLogin 코루틴을 호출하기 위한 함수
    {
        //StartCoroutine(StartLogin());
        StartCoroutine(GoogleLogin());
    }
    IEnumerator GoogleLogin()
    {
        yield return null;
#if UNITY_EDITOR
        Debug.Log("Editor 환경입니다!!!!!!!!");
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        PlayGamesPlatform.Instance.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("로그인성공!!");
                logText.text = "Login thank you" + Social.localUser.id;
            }
            else
            {
                Debug.Log("로그인실패!!");
                logText.text = "Login Faild";
            }
        });
#endif
    }
}
