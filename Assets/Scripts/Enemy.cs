using UnityEngine;
//�G�̃X�e�[�^�X�ɂ��Ă̏���
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
    //���S�A�j���[�V�����������������ǂ����̃t���O
    private bool isEnemyDied;
    
    //�U�����ꂽ�_���[�W��
    const int DAMAGE = 100;
    //DELETETIME��ɃI�u�W�F�N�g������
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
        //HP��0�ȉ��Ŏ��S�A�j���[�V����������������G������
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
            //SE��炷
            audioSource.PlayOneShot(audioClip);
        }
    }
}
