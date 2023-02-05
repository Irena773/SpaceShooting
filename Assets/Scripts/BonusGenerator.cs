using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    //敵プレハブ
    [SerializeField] private GameObject enemyPrefab;
    //プレイヤーの位置
    private Vector3 playerPos;
    //敵の生成間隔
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
                //TODO: スポーン位置のずれ
                Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y - 10.0f, transform.position.z), Quaternion.identity);

                time = 0f;
            }
        }
    }
}
