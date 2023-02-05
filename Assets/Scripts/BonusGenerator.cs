using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    //�G�v���n�u
    [SerializeField] private GameObject enemyPrefab;
    //�v���C���[�̈ʒu
    private Vector3 playerPos;
    //�G�̐����Ԋu
    private float INTERVAL = 5.0f;
    private float time = 0.0f;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, playerPos) <= 100.0f)
        {
            time += Time.deltaTime;
            if (INTERVAL < time)
            {
                //TODO: �X�|�[���ʒu�̂���
                Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y - 10.0f, transform.position.z), Quaternion.identity);

                time = 0f;
            }
        }
    }
}
