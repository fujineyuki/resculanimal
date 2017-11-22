using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScript : MonoBehaviour
{
    public float time = 91; //時間を数えるためのメンバ変数
    private AudioSource sound1;//ゲームオーバーのサウンドが入っていメンバ変数
    public ballScript BallScript;//ballscriptの情報が格納されているメンバ変数
    public GameObject exchangeButton;//交換のボタンの情報が入っているメンバ変数
    public GameObject gameoverText;//ゲームオーバーテキストのの情報が入っているメンバ変数
    public GameObject restartbutton;//やり直しのボタンの情報が入っているメンバ変数
    public GameObject Giveupbutton;//Giveupボタンの情報が入っているメンバ変数
    public GameObject timetext;//時間のテキストが入っている情報
    bool n = true;//判定のためのメンバ変数
    void Start()
    {
        restartbutton.SetActive(false);//restartbuttonを消している
        Giveupbutton.SetActive(false);
        gameoverText.SetActive(false);
        sound1 = GetComponent<AudioSource>();
    }

    void Update()
    {

        time -= Time.deltaTime;
        GetComponent<Text>().text = time.ToString("F0");
        if (time < 0)
        {
            if(n == true)
            {
                sound1.PlayOneShot(sound1.clip);
                n = false;
            }
            gameoverText.SetActive(true);
            exchangeButton.GetComponent<Button>().interactable = false;
            BallScript.isPlaying = false;
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("title1");
            }
            
        }
        if (time < 0) time = 0;
       
        if (Time.timeScale == 0)
        {
            restartbutton.SetActive(true);
            Giveupbutton.SetActive(true);
        }
    }
    public void stop()
    {
        if (Time.timeScale == 1)
        {
            exchangeButton.GetComponent<Button>().interactable = false;
            BallScript.isPlaying = false;
            Time.timeScale = 0;
        }
        else
        {
            restartbutton.SetActive(false);
            Giveupbutton.SetActive(false);
            exchangeButton.GetComponent<Button>().interactable = true;
            BallScript.isPlaying = true;
            Time.timeScale = 1;
        }

    }
    public void gameToMain()
    {
        exchangeButton.GetComponent<Button>().interactable = true;
        BallScript.isPlaying = true;
        Time.timeScale = 1;
        Application.LoadLevel("main");
    }
    public void giveup()
    {
        restartbutton.SetActive(false);
        Giveupbutton.SetActive(false);
        exchangeButton.GetComponent<Button>().interactable = true;
        BallScript.isPlaying = true;
        Time.timeScale = 1;
        Application.LoadLevel("title1");
    }
}