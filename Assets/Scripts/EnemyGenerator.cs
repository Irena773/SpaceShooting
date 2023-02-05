using UnityEngine;
using UnityEngine.Playables;
//敵の生成についての処理
public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ
    [SerializeField] private GameObject enemyPrefab;
    //敵の生成間隔
    private float INTERVAL = 5.0f;
    private float time = 0.0f;
    
    void Update()
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
