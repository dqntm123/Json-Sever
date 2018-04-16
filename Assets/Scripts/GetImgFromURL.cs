using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetImgFromURL : MonoBehaviour {

    public string url = "http://dqntm123.cafe24.com/userpic/cuma.jpg";

    void Start ()
    {
        Debug.Log(Application.internetReachability);//인터넷이 연결되있는지 안되있는지 확인할수 있음
        StartCoroutine(GetIMG());
	}

    IEnumerator GetIMG()
    {
        WWW www = new WWW(url);
        yield return www;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
    }
}
