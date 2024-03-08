using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BarrelCtrl : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private GameObject explosionSpark;

    CameraShake cameraShake;
    CameraShake1 cameraShake1;

    public int hitCount = 0;
    private string bulletTag = "BULLET";

    void Start()
    {
        source = GetComponent<AudioSource>();
        explosionClip = Resources.Load<AudioClip>("Sounds/grenade_exp2");
        explosionSpark = Resources.Load<GameObject>("Effects/ExplosionFx");
        cameraShake = Camera.main.GetComponent<CameraShake>();
        cameraShake1 = Camera.main.GetComponent<CameraShake1>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag(bulletTag))
        {
            if(++hitCount >= 3)
            {
                ExplosionBarrel();
            }

        }    
    }
    void ExplosionBarrel()
    {
        GameObject exp = Instantiate(explosionSpark, transform.position, Quaternion.identity);
        Destroy(exp, 0.5f);
        source.PlayOneShot(explosionClip);
        Collider[] Cols = Physics.OverlapSphere(transform.position, 20f, 1<<8);
                       // �ڱ��ڽ� ��ġ���� 20�ٹ��� �ݶ��̴��� Cols�� �迭 ��´�
        foreach(Collider col in Cols)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.mass = 1.0f;
                rb.AddExplosionForce(1000f, transform.position, 20f, 800f);
                // ������ٵ��� �����Լ�(���ķ�, ��ġ, �ݰ�, ���� �ڱ�ġ�� ��)
            }
        }
        // cameraShake.ExplosionShake();
        // cameraShake1.TurnOn();
    }
}
