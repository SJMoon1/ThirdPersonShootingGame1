using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] private GameObject spark;

    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private AudioSource source;

    string bulletTag = "BULLET";
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Sounds/bullet_hit_metal_enemy_4");
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(bulletTag))
        {
            Destroy(col.gameObject);
            source.PlayOneShot(clip, 1.0f);
                         // ���� ��ġ�� hitPos�� �Ѱ��ش�
            Vector3 hitPos = col.contacts[0].point;
            Vector3 _normal = col.contacts[0].normal;   // �߻��� ����
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
            var spk = Instantiate(spark, hitPos, rot);
            Destroy(spk, 2.5f);
        }
    }
}
