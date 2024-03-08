using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MariaCtrl : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Animator ani;

    private Transform tr;

    private Vector3 target = Vector3.zero;
    private Ray ray;
    private RaycastHit hit;

    private bool m_OneClick = false;
    private float m_ClickTimePrev;

    void Start()
    {
        tr = transform;
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        m_ClickTimePrev = Time.time;
    }

    void Update()
    {
        MoveWithMouse();
        MoveAnimation();
    }

    private void MoveAnimation()
    {
        Vector3 navVelocity = nav.velocity;
        Vector3 localVelocity = tr.InverseTransformDirection(navVelocity);
        float forwardSpeed = localVelocity.z;
        ani.SetFloat("forwardSpeed", forwardSpeed);
    }

    private void MoveWithMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.green);
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time - m_ClickTimePrev >= 0.3f)
            {
                m_OneClick = false;
                m_ClickTimePrev = Time.time;
            }

            if (!m_OneClick && Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
            {
                Vector3 target = hit.point;
                nav.destination = target;
                nav.speed = 3.0f;
                nav.isStopped = false;
                m_OneClick = true;
                m_ClickTimePrev = Time.time;
                print("Walk");
            }
            else if (m_OneClick && Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7) && (Time.time - m_ClickTimePrev) < 0.3f)
            {
                Vector3 target = hit.point;
                nav.destination = target;
                nav.speed = 6.0f;
                nav.isStopped = false;
                // m_OneClick = false;
                m_ClickTimePrev = Time.time;
                print("Run");
            }
        }
    }
}
