using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridContentsManager : MonoBehaviour {

    public GameObject contents;
    public int makeLimit;
    void Start ()
    {
        //MakeContents(makeLimit);
	}


    public void MakeContents(string name,int score,int i,string picURL)
    {
        gameObject.GetComponent<UIGrid>().enabled = true;
        GameObject obj = Instantiate(contents) as GameObject;
        obj.name = "Contents" + i;
        obj.transform.parent = gameObject.transform;
        obj.transform.localScale= new Vector3(1, 1, 1);
        obj.GetComponent<RankContents>().rankingNum.text = (i + 1).ToString();
        obj.GetComponent<RankContents>().userName.text = name;
        obj.GetComponent<RankContents>().userScore.text = score.ToString();
        obj.GetComponentInChildren<GetUserIMG>().url = picURL;
    }
}
