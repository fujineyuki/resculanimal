using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class addScoreScript : MonoBehaviour {
    private int score = 0;

    void Start()
    {
        //初期スコア(0点)を表示
        GetComponent<Text>().text = "+" + score.ToString();
    }
    //ballScriptからSendMessageで呼ばれるスコア加算用メソッド
    public void AddPoint1(int score)
    {
        GetComponent<Text>().text = "+" + score.ToString();
    }
}
