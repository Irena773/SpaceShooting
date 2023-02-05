using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��ʂ̉F���D���ڋ߂���ʂ̓G���X�|�[�����鏈��
public class BonusSpaceShip : MonoBehaviour
{
    private GameObject Player;
    //�v���C���[�̈ʒu
    private Vector3 playerPos;
    //�F���D�̈ʒu
    private Vector3 bonusShipPos;
    //�v���C���[�ƉF���D�̋���
    private float distance;
    //�F���D�̃X�s�[�h
    private float speed;
    //��������
    private float timeLimit;
    //�F���D�̈ړ�����
    private float moveTime;
    //�{�[�i�X�^�C��
    const float BONUSTIME = 10.0f;
    private bool isEnabled;
    [SerializeField]AudioClip ShipSE;
    AudioSource audioSource;
    void Start()
    {
        bonusShipPos = this.transform.position;
        playerPos = GameObject.Find("Player").transform.position;
        //�v���C���[�̏�����O�܂ňړ����Ăق����̂ő���
        playerPos.x = bonusShipPos.x;
        playerPos.y = bonusShipPos.y;
        playerPos.z += 30;
        
        //�v���C���[�ƉF���D�̋������Z�o����
        distance = Vector3.Distance(bonusShipPos, playerPos);
        //�������Ԃ��擾����
        timeLimit = GameObject.Find("GameDirector").GetComponent<GameDirector>().GetTimeLimit();
        
        //�ړ����Ԃ��Z�o����
        moveTime = timeLimit - BONUSTIME;
        //�X�e�[�W�Ɏc��10�b�̎��_�Ńv���C���[�̂���Ƃ���ɓ�������܂ł̑������Z�o����
        speed = distance / moveTime;

        audioSource = GetComponent<AudioSource>();
        isEnabled = false;
    }

    void Update()
    {
        //�v���C���[�̈ʒu�Ɍ������Ĉړ�����
        transform.position = Vector3.MoveTowards(
          transform.position,//�X�^�[�g�ʒu
          playerPos,
          speed * Time.deltaTime);

    }
}
