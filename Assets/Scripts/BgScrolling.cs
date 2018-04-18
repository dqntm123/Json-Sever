using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScrolling : MonoBehaviour {

    public float scrollSpeed = 0.5F; //스크롤스피드.
    public Renderer rend; // 렌드라는 이름의 렌더러를 변수로 선언.

    void Start()
    {
        rend = GetComponent<Renderer>(); // 변수로 선언한 rend에다 렌더러컴포넌트를 가져와서 집어넣는다.
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed; // float offset에 Time.time * scrollSeepd 값을 집어넣는다.
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
