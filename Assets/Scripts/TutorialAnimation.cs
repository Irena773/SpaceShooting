using System.Collections;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
   
    private Animator animator;
    private Enemy enemy;
    //アニメーションの再生時間
    private float deadWaitTime = 2.0f;
    //死亡したかどうかの判定
    private bool isDied;
    public bool GetIsDied() { return isDied; }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //HPが0になった場合、死亡アニメーションにする
        if (enemy.GetHp() <= 0)
        { 
            Dead();
        }
    }

    public void Dead()
    {
        animator.SetTrigger("IsDied");
        StartCoroutine("DeadTimer");
    }

    // 死亡してから数秒間待つ処理
    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(deadWaitTime);
        gameObject.SetActive(false);
    }

    //弾にあたった場合、ダメージのアニメーションにする
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("IsAttacked");
        }
    }
}
