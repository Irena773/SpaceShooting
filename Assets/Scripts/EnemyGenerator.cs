using UnityEngine;
using UnityEngine.Playables;
//�G�̐����ɂ��Ă̏���
public class EnemyGenerator : MonoBehaviour
{
    //�G�v���n�u
    [SerializeField] private GameObject enemyPrefab;
    //�G�̐����Ԋu
    private float INTERVAL = 5.0f;
    private float time = 0.0f;
    
    void Update()
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
