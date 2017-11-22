using UnityEngine;
using System.Collections;

public class ExchangeScript : MonoBehaviour
{
    public ballScript BallScript;
    private float time;
    private int d = 1;
    void Start()
    {

        time = 0;
    }
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Exchange()
    {
        
        //配列に「respawn」タグのついているオブジェクトを全て格納
        GameObject[] ball = GameObject.FindGameObjectsWithTag("Respawn");
        if (time >= 5)
        {
            foreach (GameObject obs in ball)
            {
                Destroy(obs);
            }
            BallScript.SendMessage("DropBall", 60);
            time = 0;
            GetComponent<AudioSource>().Play();
        }
        
    }
}