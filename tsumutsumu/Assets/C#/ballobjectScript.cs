using UnityEngine;

public class ballobjectScript : MonoBehaviour {
    public GameObject explosion1;
    public GameObject explosion2;
    public ballScript BallScript;
    private int point = 100;
    private int points = 0;
    private int pointx = 0;
    private int Score;
    private int addScore;
    public GameObject highscoreGUI;
    public GameObject scoreGUI;
    public GameObject countGUI;
    public GameObject addScoreGUI;
    private int e = 0;
    public void counta(int count)
    {
        if (count % 5 == 0 && count != 0)
        {
            Score = Score + ((count / 5) * 10000);
            pointx += Score;
            scoreGUI.SendMessage("AddPoint", Score);
        }
    }
    public void haiten(int remove_cnt)
    {
        Score = ((point + (25 * remove_cnt)) * remove_cnt);//ポイントを計算している
        points = points + Score;//イベント発生のための計算
        pointx = pointx + Score;
        addScore = Score;
        scoreGUI.SendMessage("AddPoint", Score);//ポイントの計算をしたものを画面に出しているもの
        addScoreGUI.SendMessage("AddPoint1", addScore);
        if (remove_cnt >= 8 )
        {
            e += 1;
            GameObject[] ball = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (GameObject obs in ball)
            {
                Destroy(obs);
                Instantiate(explosion1, obs.transform.position, obs.transform.rotation);
                Instantiate(explosion2, transform.position, transform.rotation);
                GetComponent<AudioSource>().Play();
            }
            
            if (remove_cnt > 9)
            {
                pointx = pointx + 50000 + 5000 * (remove_cnt % 10);
                scoreGUI.SendMessage("AddPoint", (50000 + 5000 * (remove_cnt % 10)));
                addScoreGUI.SendMessage("AddPoint1", (addScore + (50000 + 5000 * (remove_cnt % 10))));
            }
           
            scoreGUI.SendMessage("AddPoint", 50000);
            addScoreGUI.SendMessage("AddPoint1", (addScore + 50000));
            pointx = pointx + 50000;
            BallScript.SendMessage("DropBall", 60 - remove_cnt);
            points = 0;
        }
    }
    public void highScorecount()
    {
        int highscore = pointx;
        highscoreGUI.SendMessage("AddPoint", highscore);
    }
    public void ChangeColor(GameObject obj, float transparency)
    {
        SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>();
        ballTexture.color = new Color(ballTexture.color.r, ballTexture.color.g, ballTexture.color.b, transparency);
        
    }
}
