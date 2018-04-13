using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int gold;
    public int cash;
    public int score;
    public UILabel userNick;
    public UILabel userGold;
    public UILabel userCash;
    public UILabel userScore;

    void Start()
    {
        gold = PlayerPrefs.GetInt("UserGold");
        cash = PlayerPrefs.GetInt("UserCash");
        score = PlayerPrefs.GetInt("UserScore");
        userNick.text = PlayerPrefs.GetString("UserNick");
        userGold.text = "Gold : " + gold.ToString("#,###,###0");
        userCash.text = "Cash : " + cash.ToString("#,###0");
        userScore.text = "Score : " + score.ToString("#,###,###0");
    }

    void Update()
    {
        if (gold < 0)
        {
            gold = 0;
            userGold.text = "Gold : " + gold.ToString("#,###,###0");
        }
        if (cash < 0)
        {
            cash = 0;
            userCash.text = "Cash : " + cash.ToString("#,###0");
        }
        if (score < 0)
        {
            score = 0;
            userScore.text = "Score : " + score.ToString("#,###,###0");
        }
        userNick.text = PlayerPrefs.GetString("UserNick");
        userGold.text = "Gold : " + gold.ToString("#,###,###0");
        userCash.text = "Cash : " + cash.ToString("#,###0");
        userScore.text = "Score : " + score.ToString("#,###,###0");
    }
	public void UpGold()
    {
        gold += 10;
        userGold.text = "Gold : " + gold.ToString("#,###,###0");
    }
	public void DownGold()
    {
        gold -= 10;
        userGold.text = "Gold : " + gold.ToString("#,###,###0");
    }
    public void UpCash()
    {
        cash += 1;
        userCash.text = "Cash : " + cash.ToString("#,###0");
    }
    public void DownCash()
    {
        cash -= 1;
        userCash.text = "Cash : " + cash.ToString("#,###0");
    }
    public void UpScore()
    {
        score += 100;
        userScore.text = "Score : " + score.ToString("#,###,###0");
    }
    public void DownScore()
    {
        score -= 100;
        userScore.text = "Score : " + score.ToString("#,###,###0");
    }
}
