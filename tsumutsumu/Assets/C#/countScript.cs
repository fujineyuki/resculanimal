using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countScript : MonoBehaviour
{
    private int count = 0;

    void Start()
    {
        //初期スコア(0点)を表示
        GetComponent<Text>().text = count.ToString();
    }
    //ballScriptからSendMessageで呼ばれるスコア加算用メソッド
    public void addcount(int count)
    {
        GetComponent<Text>().text = count.ToString();
    }
}