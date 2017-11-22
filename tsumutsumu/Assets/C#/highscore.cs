using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class highscore : MonoBehaviour
{
    private int highScorem;
    void Start()
    {
        highScorem = PlayerPrefs.GetInt("highScoreKey", 0);
        GetComponent<Text>().text = "" + highScorem.ToString();

    }
    public void AddPoint(int Highscore)
    {

        if (highScorem < Highscore)
        {

            GetComponent<Text>().text = "" + Highscore.ToString();
            PlayerPrefs.SetInt("highScoreKey", Highscore);
            // キーと値を保存
            PlayerPrefs.Save();
        }
    }
}