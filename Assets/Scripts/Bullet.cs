using UnityEngine;
//�e�̃X�e�[�^�X�ɂ��Ă̏���
public class Bullet : MonoBehaviour
{
    private int score;
    const int ADDSCORE = 100;
    const float DELETETIME = 1.5f;
    private GameDirector gameDirector;

    void Start()
    {
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        score = gameDirector.GetScore();
    }

    private void Update()
    {
        Destroy(gameObject,DELETETIME);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            score += ADDSCORE;
            gameDirector.SetScore(score);
            //�G�ɂ���������e���폜����
            Destroy(gameObject);
        }
    }

}
