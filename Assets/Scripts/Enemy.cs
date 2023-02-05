using UnityEngine;
//敵のステータスについての処理
public class Enemy : MonoBehaviour
{
    private int hp;
    public int GetHp() { return hp; }
    public void SetHp(int hp) { this.hp = hp; }
    private TutorialAnimation tutorialAnimation;
    private EnemyAnimation enemyAnimation;

    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    [SerializeField]GameDirector gameDirector;
    //死亡アニメーションが完了したかどうかのフラグ
    private bool isEnemyDied;
    
    //攻撃されたダメージ量
    const int DAMAGE = 100;
    //DELETETIME後にオブジェクトを消す
    const float DELETETIME = 5.0f;

    private void OnEnable()
    {
        
        if (gameObject.tag == "Enemy")
        {
            enemyAnimation = GetComponent<EnemyAnimation>();
            hp = 500;
        }
        else if (gameObject.tag == "TutorialEnemy")
        {
            tutorialAnimation = GetComponent<TutorialAnimation>();
            hp = 200;
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //HPが0以下で死亡アニメーションが完了したら敵を消す
        if (hp <= 0)
        { 

            if (gameObject.tag == "TutorialEnemy")
            {
                gameDirector.StartCoroutine(gameDirector.HiddenTitle(2.0f));
            }
            else
            {
                Destroy(gameObject, DELETETIME);
            }
        }
           
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Bullet") { 
            hp -= DAMAGE;
            //SEを鳴らす
            audioSource.PlayOneShot(audioClip);
        }
    }
}
