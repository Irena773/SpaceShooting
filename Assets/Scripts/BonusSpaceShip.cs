using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//大量の宇宙船が接近し大量の敵がスポーンする処理
public class BonusSpaceShip : MonoBehaviour
{
    private GameObject Player;
    //プレイヤーの位置
    private Vector3 playerPos;
    //宇宙船の位置
    private Vector3 bonusShipPos;
    //プレイヤーと宇宙船の距離
    private float distance;
    //宇宙船のスピード
    private float speed;
    //制限時間
    private float timeLimit;
    //宇宙船の移動時間
    private float moveTime;
    //ボーナスタイム
    const float BONUSTIME = 10.0f;
    private bool isEnabled;
    [SerializeField]AudioClip ShipSE;
    AudioSource audioSource;
    void Start()
    {
        bonusShipPos = this.transform.position;
        playerPos = GameObject.Find("Player").transform.position;
        //プレイヤーの少し手前まで移動してほしいので足す
        playerPos.x = bonusShipPos.x;
        playerPos.y = bonusShipPos.y;
        playerPos.z += 30;
        
        //プレイヤーと宇宙船の距離を算出する
        distance = Vector3.Distance(bonusShipPos, playerPos);
        //制限時間を取得する
        timeLimit = GameObject.Find("GameDirector").GetComponent<GameDirector>().GetTimeLimit();
        
        //移動時間を算出する
        moveTime = timeLimit - BONUSTIME;
        //ステージに残り10秒の時点でプレイヤーのいるところに到着するまでの速さを算出する
        speed = distance / moveTime;

        audioSource = GetComponent<AudioSource>();
        isEnabled = false;
    }

    void Update()
    {
        //プレイヤーの位置に向かって移動する
        transform.position = Vector3.MoveTowards(
          transform.position,//スタート位置
          playerPos,
          speed * Time.deltaTime);

    }
}
