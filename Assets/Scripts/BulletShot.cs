using System;
using System.Collections;

using UnityEngine;

//弾の発射についての処理
public class BulletShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject chargeBulletPrefab;
    const int CHARGEBULLET = 3;
    private float bulletSpeed;
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;


    void Start()
    {
        if (gameObject.tag == "ChargeBullet") bulletSpeed = 400;
        else bulletSpeed = 800;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)|| OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            //弾を発射する
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            //SEを鳴らす
            audioSource.PlayOneShot(audioClip);
        }
        //左中指トリガー入力
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            //左のコントローラーを振動させる
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);
            StartCoroutine("ChargeUp");
        }
        //離したとき
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            //コントローラーを振動を止める
            OVRInput.SetControllerVibration(0, 0);
            StopCoroutine("ChargeUp");
            for(int i = 0; i< CHARGEBULLET; i++)
            {
                //弾を発射する
                float random1 = UnityEngine.Random.Range(50f, -50f);
                float random2 = UnityEngine.Random.Range(50f, -50f);
                float random3 = UnityEngine.Random.Range(50f, -50f);
                Vector3 force = new Vector3(random1, random2, random3);
                GameObject bullet = (GameObject)Instantiate(chargeBulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(force);
                bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            }
            
            //SEを鳴らす
            audioSource.PlayOneShot(audioClip);
        }
        //右中指トリガー入力
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            //右のコントローラーを振動させる
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);
            StartCoroutine("ChargeUp");
        }
        //離したとき
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            //コントローラーを振動を止める
            OVRInput.SetControllerVibration(0, 0);
            StopCoroutine("ChargeUp");
            for (int i = 0; i < CHARGEBULLET; i++)
            {
                //弾を発射する
                float random1 = UnityEngine.Random.Range(50f, -50f);
                float random2 = UnityEngine.Random.Range(50f, -50f);
                float random3 = UnityEngine.Random.Range(50f, -50f);
                Vector3 force = new Vector3(random1, random2, random3);
                GameObject bullet = (GameObject)Instantiate(chargeBulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(force);
                bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            }

            //SEを鳴らす
            audioSource.PlayOneShot(audioClip);
        }
    }

    IEnumerator ChargeUp()
    {
        while (true)
        {
            bulletSpeed += 0.03f;

            if(1.0f <= bulletSpeed) yield break;
            yield return new WaitForSeconds(0.01f);
        }
    }


}
