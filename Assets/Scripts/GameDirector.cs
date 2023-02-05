using System.Collections;
using TMPro;
using UnityEngine;

//�Q�[���i�s�ɂ��Ă̏���
public class GameDirector : MonoBehaviour
{
    public enum PlayState
    {
        Title,
        Ready,
        Play,
        Finish,
    }
    // ���݂̃X�e�[�g
    public PlayState CurrentState;
    public PlayState GetPlayState() { return CurrentState; }
    public void SetPlayState(PlayState state) { CurrentState = state; }

    //�J�E���g�_�E���X�^�[�g�^�C��
    [SerializeField] int countStartTime = 5;
    // �J�E���g�_�E���̌��ݒl.
    float currentCountDown = 0;
    
    //��������
    private float timelimit = 20;
    public float GetTimeLimit() { return timelimit; }
    public void SetTImeLimit(float value) { timelimit = value; }
    
    //�v���C���[�̃X�R�A
    private int score = 0;
    public int GetScore() { return score; }
    public void SetScore(int value) { score = value; }

    //�{�[�i�X�F���D�̐�����
    const int BONUSSPACESHIP = 10;
    //�{�[�i�X�F���D�̐����ʒu�Ԋu
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
       
        //�J�E���g�_�E���̏�Ԃ̂Ƃ��J�E���g�_�E��UI��\������
        if (CurrentState == PlayState.Ready)
        {
            // ���Ԃ������Ă���.
            currentCountDown -= Time.deltaTime;
            int intNum = 0;
            // �J�E���g�_�E����.
            if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
            {
                intNum = (int)Mathf.Ceil(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else if (currentCountDown <= 0)
            {
                // �J�n.
                intNum = 0;
                countdownText.text = "START!";
                StartCoroutine(HiddenCountDown(1.0f));
                SetPlayState(PlayState.Play);
            }
        }
        else if(CurrentState == PlayState.Play && 0 < timelimit)
        {
            //�c�莞�Ԃ̕\��
            timelimit -= Time.deltaTime;
            TimeText.text = timelimit.ToString("f0");
            //�X�R�A�̕\��
            ScoreText.text = score.ToString();
        }else if(CurrentState == PlayState.Play && timelimit <= 0)
        {
            SetPlayState(PlayState.Finish);
        }
        else if(CurrentState == PlayState.Finish)
        {
            //�����L���O�̕\��
            RankingUI.SetActive(true);
            //�G����������
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies) { Destroy(enemy); }
            //�����L���O���\���ɂ���
            StartCoroutine("HiddenRanking");

            //������
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
    //��莞�Ԍ�ɃJ�E���g�_�E��UI���\���ɂ���
    public IEnumerator HiddenCountDown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CountDownUI.SetActive(false);
        //BGM�̍Đ�
        BGMSource.Play();
        SpawnSpaceShip();
        SpawnBonusSpaceShip();
    }
    //��莞�Ԍ�Ƀ����L���O���\���ɂ���
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

    //�{�[�i�X�F���D�𐶐�����
    public void SpawnBonusSpaceShip()
    {
        Vector3 ShipPos = new Vector3(100.0f,48.0f,900.0f);
        for(int i = 0; i<BONUSSPACESHIP; i++)
        {
            Instantiate(BonusSpaceShip, ShipPos, Quaternion.Euler(0, 180, 0));
            ShipPos.x -= POSINTERVAL;
        }
        
    }

    //�{�[�i�X�F���D���폜����
    public void DestroyBonusSpaceShip()
    {
        //�G����������
        GameObject[] bonusShips = GameObject.FindGameObjectsWithTag("BonusShip");
        foreach (GameObject ship in bonusShips) { Destroy(ship); }
    }
}
