using System;
using System.Collections;

using UnityEngine;

//�e�̔��˂ɂ��Ă̏���
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
            //�e�𔭎˂���
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            //SE��炷
            audioSource.PlayOneShot(audioClip);
        }
        //�����w�g���K�[����
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            //���̃R���g���[���[��U��������
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);
            StartCoroutine("ChargeUp");
        }
        //�������Ƃ�
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            //�R���g���[���[��U�����~�߂�
            OVRInput.SetControllerVibration(0, 0);
            StopCoroutine("ChargeUp");
            for(int i = 0; i< CHARGEBULLET; i++)
            {
                //�e�𔭎˂���
                float random1 = UnityEngine.Random.Range(50f, -50f);
                float random2 = UnityEngine.Random.Range(50f, -50f);
                float random3 = UnityEngine.Random.Range(50f, -50f);
                Vector3 force = new Vector3(random1, random2, random3);
                GameObject bullet = (GameObject)Instantiate(chargeBulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(force);
                bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            }
            
            //SE��炷
            audioSource.PlayOneShot(audioClip);
        }
        //�E���w�g���K�[����
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            //�E�̃R���g���[���[��U��������
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);
            StartCoroutine("ChargeUp");
        }
        //�������Ƃ�
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            //�R���g���[���[��U�����~�߂�
            OVRInput.SetControllerVibration(0, 0);
            StopCoroutine("ChargeUp");
            for (int i = 0; i < CHARGEBULLET; i++)
            {
                //�e�𔭎˂���
                float random1 = UnityEngine.Random.Range(50f, -50f);
                float random2 = UnityEngine.Random.Range(50f, -50f);
                float random3 = UnityEngine.Random.Range(50f, -50f);
                Vector3 force = new Vector3(random1, random2, random3);
                GameObject bullet = (GameObject)Instantiate(chargeBulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(force);
                bulletRigidbody.AddForce(transform.forward * bulletSpeed);
            }

            //SE��炷
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
