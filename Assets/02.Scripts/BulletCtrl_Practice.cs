using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl_Practice : MonoBehaviour
{
    [SerializeField, Range(1.0f, 10000.0f)] private float bulletSpeed = 1000.0f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * bulletSpeed);
    }
}
