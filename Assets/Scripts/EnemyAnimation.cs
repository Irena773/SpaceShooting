using System.Collections;
using UnityEngine;
//�G�̃A�j���[�V�����ɂ��Ă̏���
public class EnemyAnimation : MonoBehaviour
{
    private CharacterController enemyController;
    private Animator animator;

    //�A�j���[�V�����̍Đ�����
    private float deadWaitTime = 3;
    private Enemy enemy;
    //�@�ړI�n
     Vector3 DESTINATION;
    [SerializeField]
    private float walkSpeed = 1.0f;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
    //�ړI�n�ɂ������ǂ����̔���
    private bool arrived;
    //���S�������ǂ����̔���
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
        //���ɕ����Ă���ꍇ�A�������n�ʂɗ�������
        if (!arrived && !enemyController.isGrounded)
        {
            Drop();
        }
        //�n�ʂɂ��Ă�ꍇ�A�v���C���[�Ɍ������ĕ���
        if (!arrived && enemyController.isGrounded)
        {
            Walk();
        }
        //HP��0�ɂȂ����ꍇ�A���S�A�j���[�V�����ɂ���
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

        //�@�ړI�n�ɓ����������ǂ����̔���
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

    // ���S���Ă��琔�b�ԑ҂���
    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(deadWaitTime);
        gameObject.SetActive(false);
    }

    //�e�ɂ��������ꍇ�A�_���[�W�̃A�j���[�V�����ɂ���
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("IsAttacked");
        }
    }
}
