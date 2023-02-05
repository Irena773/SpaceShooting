using System.Collections;
using UnityEngine;
//敵のアニメーションについての処理
public class EnemyAnimation : MonoBehaviour
{
    private CharacterController enemyController;
    private Animator animator;

    //アニメーションの再生時間
    private float deadWaitTime = 3;
    private Enemy enemy;
    //　目的地
     Vector3 DESTINATION;
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //目的地についたかどうかの判定
    private bool arrived;
    //死亡したかどうかの判定
    private bool isDied;
    public bool GetIsDied() { return isDied; }

    private GameObject Player;
    private Vector3 PlayerPos;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        velocity = Vector3.zero;
        arrived = false;
        Player = GameObject.Find("Player");
        PlayerPos = Player.transform.position;
        DESTINATION = PlayerPos;
        
    }

    void Update()
    {       
        //宙に浮いている場合、ゆっくり地面に落下する
        if (!arrived && !enemyController.isGrounded)
        {
            Drop();
        }
        //地面についてる場合、プレイヤーに向かって歩く
        if (!arrived && enemyController.isGrounded)
        {
            Walk();
        }
        //HPが0になった場合、死亡アニメーションにする
        if (enemy.GetHp() <= 0) 
        {
            Dead();       
        }
    }

    public void Drop()
    {
        velocity.y += 5.0f * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);
    }

    public void Walk()
    {
        velocity = Vector3.zero;
        animator.SetFloat("Speed", 2.0f);
        direction = (DESTINATION - transform.position).normalized;
        transform.LookAt(new Vector3(DESTINATION.x, transform.position.y, DESTINATION.z));
        velocity = direction * walkSpeed;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

        //　目的地に到着したかどうかの判定
        if (Vector3.Distance(transform.position, DESTINATION) <= 3.7f)
        {
            arrived = true;
            animator.SetFloat("Speed", 0.0f);
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
