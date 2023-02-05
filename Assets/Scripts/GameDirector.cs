using System.Collections;
using TMPro;
using UnityEngine;

//ゲーム進行についての処理
public class GameDirector : MonoBehaviour
{
    public enum PlayState
    {
        Title,
        Ready,
        Play,
        Finish,
    }
    // 現在のステート
    public PlayState CurrentState;
    public PlayState GetPlayState() { return CurrentState; }
    public void SetPlayState(PlayState state) { CurrentState = state; }

    //カウントダウンスタートタイム
    [SerializeField] int countStartTime = 5;
    // カウントダウンの現在値.
    float currentCountDown = 0;
    
    //制限時間
    private float timelimit = 20;
    public float GetTimeLimit() { return timelimit; }
    public void SetTImeLimit(float value) { timelimit = value; }
    
    //プレイヤーのスコア
    private int score = 0;
    public int GetScore() { return score; }
    public void SetScore(int value) { score = value; }

    //ボーナス宇宙船の生成数
    const int BONUSSPACESHIP = 10;
    //ボーナス宇宙船の生成位置間隔
    const int POSINTERVAL = 20;

    private TextMeshPro TimeText;
    private TextMeshPro ScoreText;
    private TextMeshPro countdownText;
    private GameObject TitleUI;
    private GameObject CountDownUI;
    private GameObject RankingUI;
    [SerializeField] GameObject YSpaceShip;
    [SerializeField] GameObject BSpaceShip;
    [SerializeField] GameObject BonusSpaceShip;
    [SerializeField] GameObject TutorialEnemy;

    [SerializeField]private AudioClip BGM;
    private AudioSource BGMSource;

    void Start()
    {
        TitleUI = GameObject.Find("TitleUI").transform.Find("TitleImage").gameObject;
        CountDownUI = GameObject.Find("CountDownUI").transform.Find("CountDownText").gameObject;
        RankingUI = GameObject.Find("RankingUI").transform.Find("Ranking").gameObject;

        CurrentState = PlayState.Title;
        score = 0;
        this.TimeText  = GameObject.Find("Time").GetComponent<TextMeshPro>();
        this.ScoreText = GameObject.Find("Score").GetComponent<TextMeshPro>();
        this.countdownText = CountDownUI.GetComponent<TextMeshPro>();
        BGMSource = GetComponent<AudioSource>();

        YSpaceShip.SetActive(false);
        BSpaceShip.SetActive(false);
        
    }

    void Update()
    {       
       
        //カウントダウンの状態のときカウントダウンUIを表示する
        if (CurrentState == PlayState.Ready)
        {
            // 時間を引いていく.
            currentCountDown -= Time.deltaTime;
            int intNum = 0;
            // カウントダウン中.
            if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
            {
                intNum = (int)Mathf.Ceil(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else if (currentCountDown <= 0)
            {
                // 開始.
                intNum = 0;
                countdownText.text = "START!";
                StartCoroutine(HiddenCountDown(1.0f));
                SetPlayState(PlayState.Play);
            }
        }
        else if(CurrentState == PlayState.Play && 0 < timelimit)
        {
            //残り時間の表示
            timelimit -= Time.deltaTime;
            TimeText.text = timelimit.ToString("f0");
            //スコアの表示
            ScoreText.text = score.ToString();
        }else if(CurrentState == PlayState.Play && timelimit <= 0)
        {
            SetPlayState(PlayState.Finish);
        }
        else if(CurrentState == PlayState.Finish)
        {
            //ランキングの表示
            RankingUI.SetActive(true);
            //敵を消去する
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies) { Destroy(enemy); }
            //ランキングを非表示にする
            StartCoroutine("HiddenRanking");

            //初期化
            timelimit = 90;
            countStartTime = 5;
            BGMSource.Stop();
            HiddenSpaceShip();
            DestroyBonusSpaceShip();
            
            SetPlayState(PlayState.Title); 
            
        } 
    }

    public void ShowTitle()
    {   
        TutorialEnemy.SetActive(true);
        TitleUI.SetActive(true);
    }

    public IEnumerator HiddenTitle(float seconds)
    {
        currentCountDown = (float)countStartTime;
        yield return new WaitForSeconds(seconds);
        TitleUI.SetActive(false);
        CountDownUI.SetActive(true);
        SetPlayState(PlayState.Ready);
        
    }
    //一定時間後にカウントダウンUIを非表示にする
    public IEnumerator HiddenCountDown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CountDownUI.SetActive(false);
        //BGMの再生
        BGMSource.Play();
        SpawnSpaceShip();
        SpawnBonusSpaceShip();
    }
    //一定時間後にランキングを非表示にする
    public IEnumerator HiddenRanking()
    {
        
        yield return new WaitForSeconds(5.0f);
        RankingUI.SetActive(false);
        ShowTitle();

    }

    public void SpawnSpaceShip()
    {
        YSpaceShip.SetActive(true);
        BSpaceShip.SetActive(true);
    }

    public void HiddenSpaceShip()
    {
        YSpaceShip.SetActive(false);
        BSpaceShip.SetActive(false);
    }

    //ボーナス宇宙船を生成する
    public void SpawnBonusSpaceShip()
    {
        Vector3 ShipPos = new Vector3(100.0f,48.0f,900.0f);
        for(int i = 0; i<BONUSSPACESHIP; i++)
        {
            Instantiate(BonusSpaceShip, ShipPos, Quaternion.Euler(0, 180, 0));
            ShipPos.x -= POSINTERVAL;
        }
        
    }

    //ボーナス宇宙船を削除する
    public void DestroyBonusSpaceShip()
    {
        //敵を消去する
        GameObject[] bonusShips = GameObject.FindGameObjectsWithTag("BonusShip");
        foreach (GameObject ship in bonusShips) { Destroy(ship); }
    }
}
