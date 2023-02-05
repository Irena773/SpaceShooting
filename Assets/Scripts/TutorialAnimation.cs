using System.Collections;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
   
    private Animator animator;
    private Enemy enemy;
    //�A�j���[�V�����̍Đ�����
    private float deadWaitTime = 2.0f;
    //���S�������ǂ����̔���
    private bool isDied;
    public bool GetIsDied() { return isDied; }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //HP��0�ɂȂ����ꍇ�A���S�A�j���[�V�����ɂ���
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
