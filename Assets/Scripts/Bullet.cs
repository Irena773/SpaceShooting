using UnityEngine;
//弾のステータスについての処理
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
            //敵にあたったら弾を削除する
            Destroy(gameObject);
        }
    }

}
