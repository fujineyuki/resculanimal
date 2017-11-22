using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ballScript : MonoBehaviour
{
    public ballScript BallScript;      //ballscriptの情報が格納されているメンバ変数

    public GameObject ballPrefab;      //ballprafabが格納されているメンバ変数
    public Sprite[] ballSprites;       //ballについている画像が格納されているメンバ変数
    static public string currentName;  //ballの名前に使われるメンバ変数
    private int count;                 //ballを消した回数を計算するメンバ変数
    private float counta;
    static public GameObject firstBall;//最初のballの情報が入るメンバ変数
    static public GameObject lastBall; //firstballの次につなげられたballの情報が入っているメンバ変数
    public GameObject scoreGUI;        //scoreの情報が入っているメンバ変数
    public GameObject countGUI;        //countの情報が入っているメンバ変数
    public GameObject exchangeButton;  //changebottanの情報が入っているメンバ変数
    public GameObject explosion;       //explosionの情報が入っているメンバ変数
    public GameObject highscoreGUI;    //highscoreの情報が入っているメンバ変数
    List<GameObject> removableBallList 
        = new List<GameObject>();      //removableBallListの中にはGameOverしか入らないlistのインスタンスが入っている
    private List<Vector3> linePoints 
        =new List<Vector3>();          //新しい座標を追加する時のしきい値.
    private float threshold = 0.1f;
    public bool isPlaying = true;      //ゲームの状況が入っているメンバ変数
    ballobjectScript BallobjectScript; //ballobjectScriptの情報が格納されてるクラス
    private float howLong;
    private LineRenderer lineRenderer;
    public float lineDrawSpeed;
    private int lineCount = 0;
    //LineRenderer lineRenderer;

    static public bool click;
    

    void Start()
    {
        BallobjectScript =
        GetComponent<ballobjectScript>();//ballobjectscriptを初期化している
        StartCoroutine(DropBall(60));    //60個のアニマルが落ちる処理が行われている
        //変更
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(.1f, .1f);
        //変更
        click = false;
        //lineRenderer = GetComponent<LineRenderer>();

    }



    void Update()
    {
        if (isPlaying)                                           //isplayingがtrueの時にtrueになる
        {
            if (Input.GetMouseButtonDown(0) && firstBall == null)//スクリーンから指を付けたときかつfirstBallがnullのときOnDragStratの処理が実行される
            {
                OnDragStart();                                   //OnDragStartメソッドを呼び出している
            }
            else if (Input.GetMouseButtonUp(0))                  //スクリーンから指を離したときOnDragEndメソッドの処理が実行される
            {
                OnDragEnd();                                    //OnDragEndメソッドを呼び出してる
            }
            else if (firstBall != null)                         //firstBallがnullの時OnDraggingメソッドの処理が実行される
            {
                OnDragging();                                   //OnDraggingメソッドを呼び出している
            }
        }
    }

    public void OnDragStart()                                   //クリックしたとき
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                                                                 //hitのポジションの位置をカメラから取得してる
        if (hit.collider != null)
                                                                 //hit.corriderがnullではないときに処理を実行する
        {
            GameObject hitObj = hit.collider.gameObject;        //hit.colliderの情報をHitObjに格納している
            string ballName = hitObj.name;                      //ballNameにHitObj.nameの文字列が入ってる
            if ((ballName.StartsWith("Ball")) || 
                (ballName.StartsWith("ball1")))                 //ballNameの先頭にballとついているときtrueになる
            {
              
      firstBall = hitObj;                                       //firstballにhitObjの情報が格納されている
                lastBall = hitObj;                              //lastballにhitObjの情報が格納されている
                currentName = hitObj.name;                      //curretNameにhitObjの名前が格納されている
                removableBallList = new List<GameObject>();     //removableBallListの中にはGameOverしか入らないlistのインスタンスが入っている
                PushToList(hitObj);                             //pustToListメソッドを呼び出している
            }
        }
    }
    public void OnDragging()                                   //クリック中
    {
        lineCount = removableBallList.Count;
        click = true;
        int remove_cnt = removableBallList.Count;              //押している間に繋いだ個数をremove_cntに入れる
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                                                              //hitのポジションの位置をカメラから取得してる
        if (hit.collider != null)                             //ballの判定
        {
           
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.name == currentName && lastBall != hitObj)
                                                               //同じballなのかを判定している
            {
                Vector3 pointA = hitObj.transform.position;
                Vector3 pointB = lastBall.transform.position;
                if (remove_cnt >= 2) {

                    

                    lineRenderer.SetPosition(0, hitObj.transform.position) ;
                    howLong = Vector3.Distance(hitObj.transform.position,lastBall.transform.position);
                    //変更
                    lineRenderer.SetVertexCount(20);
                    if (counta < howLong)
                    {
                        for (int i = 1; i <= remove_cnt; i++)
                        {
                            lineRenderer.SetPosition(i, hitObj.transform.position);
                            counta += .1f / lineDrawSpeed;
                            float x = Mathf.Lerp(i - 1, howLong, count);
                            lineRenderer.SetPosition(i - 1, removableBallList[i].transform.position);
                        }
                    }
                }
                float distance = Vector2.Distance(hitObj.transform.position, lastBall.transform.position);
                if (distance < 1.22f)
                {
                    if (remove_cnt <= 2)                       //格納２個以下
                    {
                        removableBallList[0].name = "hit";     //ツム２個以下の場合、一つ目のツムを指を戻した場合にヒットされないようにする
                    }
                    if(remove_cnt >= 3)                        //hitObj.nameにhitを入れ処理をfalseにして個数以上にカウントをさせなくしている
                    {
                        hitObj.name = "hit";                   //hitObjの名前をhitに変え個数以上のカウントをさせなくしている
                        lastBall.name = "hit";                 //hitObjの名前をhitに変え個数以上のカウントをさせなくしている
                    }
                    lastBall = hitObj;//hitObjの名前をhitに変え個数以上のカウントをさせなくしている
                    //変更
                    PushToList(hitObj);//pushToListメソッドを呼び出している
                }
            }
        }
        for (int i = 0; i < lineCount; i++)
        {
            lineRenderer.SetVertexCount(removableBallList.Count);
            lineRenderer.SetPosition(i, removableBallList[i].transform.position);
        }
    }


    public void OnDragEnd()                           //クリックを終えた時
    {
        click = false;

        int remove_cnt = removableBallList.Count;    //押している間に繋いだ個数をremove_cntに入れる
        if (remove_cnt <= 2)                         //格納２個以下
        {
            removableBallList[0].name = currentName; //指を話した場合、一つ目のツムの名前をもとに戻す(クリック中に２個以下の場合名前が変わるため)
        }
        linePoints.Clear();
        if (remove_cnt >= 3)                         //もしも、3個以上の時は、かっこの中を通る
        {
            for (int i = 0; i < remove_cnt; i++)    //選んだツムの数だけ、カッコの中の通る
            {
                Destroy(removableBallList[i]);      //選んだツムを消す
                Instantiate(explosion, removableBallList[i].transform.position, removableBallList[i].transform.rotation);//爆発の演出を付ける
                GetComponent<AudioSource>().Play();  //AudioSourceという変数の型を固定して音を入れている
            }
            StartCoroutine(DropBall(remove_cnt));   //消したツムの数分ツムが落ちてくる
            count = count + 1;                      //アニマルを逃がすたびにcountに１を入れる
            BallobjectScript.counta(count);         //countaメソッドを呼び出している(別のScriptでcountの計算とイベント用に使うスクリプトに移動する)
            BallobjectScript.haiten(remove_cnt);    //haitenメソッドを呼び出している(ツムを消したときのスコアの計算と個数のイベント用に使われている)
            countGUI.SendMessage("addcount", count);//countの個数を画面に表示するスクリプトに移動する
            BallobjectScript.highScorecount();      //highScoreCountメソッドを呼び出している(ハイスコアの表示と更新をしている)
        }
        else
        { 
            for (int i = 0; i < remove_cnt; i++)    //つないだ個数を分処理を実行している
            {
                BallobjectScript.ChangeColor(removableBallList[i], 1.0f);
                                                   //ランダムでアニマルを作っている

            }
        }

        lineCount = 1;
        lineRenderer.SetVertexCount(1);
        lineRenderer.SetPosition(1, Vector3.zero);

        firstBall = null;                         //firstBallにnullを入れ８１行目の処理をfalseにしている
        lastBall = null;                          //lastBallにnullを入れ８１行目の処理をfalseにしている
    }
    IEnumerator DropBall(int count)               //DropBallメソッド
        {
            if (count == 60)                      //countが60ある時に処理される
            {

                StartCoroutine("RestrictPush");   //RestrictPushメソッドを呼び出している
            }
            for (int i = 0; i < count; i++)
            {
                Vector2 pos = new Vector2(Random.Range(-2.0f, 2.0f), 7f);
                GameObject ball = Instantiate(ballPrefab, pos,
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;
                int spriteId = Random.Range(0, 5);
                ball.name = "Ball" + spriteId;
                SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
                spriteObj.sprite = ballSprites[spriteId];
                  yield return new WaitForSeconds(0.05f);
            }
        }
    
    IEnumerator RestrictPush()
    {
        exchangeButton.GetComponent<Button>().interactable = false;//changeボタンが押せなくなる
        yield return new WaitForSeconds(5.0f);                     //5秒経つまで待つ処理
        exchangeButton.GetComponent<Button>().interactable = true; //changeボタンが押せるようにする
    }
    void PushToList(GameObject obj)
    {
        removableBallList.Add(obj);　　　　　　　　　　　　　　　　　//removeableBallListにobjを入れている
        BallobjectScript.ChangeColor(obj, 0.5f);　　　　　　　　　　//ballの色を透明にしている
    }
}