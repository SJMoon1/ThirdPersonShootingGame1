using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;

    public float speed = 2500.0f;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        rb.AddForce(tr.forward * speed);
        Destroy(gameObject, 3.0f);
    }
}
