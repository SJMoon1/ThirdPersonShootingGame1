using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardCtrl : MonoBehaviour
{
    [SerializeField] NavMeshAgent nav;
    [SerializeField] Transform tr;
    [SerializeField] Animator animator;
    private Ray ray;                        // ����
    private RaycastHit hit;                 // � Object�� ������ �¾Ҵ��� ����
    private Vector3 target = Vector3.zero;
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {      
        MouseMove();

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            nav.isStopped = true;
            animator.SetTrigger("isSkill");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            nav.isStopped = true;
            animator.SetTrigger("isAttack");
        }
        UpdateAnimator();
    }

    private void MouseMove()
    {       // ī�޶󿡼� ���콺������ �������� ������ ������ ����
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.green);
        // ���콺 ��Ŭ��
        if (m_IsOneClick == true && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {

            Debug.Log("One Click");

            m_IsOneClick = false;

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (!m_IsOneClick)

            {

                m_Timer = Time.time;
                m_IsOneClick = true;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
                {                  //������ �¾Ҵٸ�  �÷ξ 
                    target = hit.point; //������ ���� ��ġ�� target�� ����
                    nav.destination = target;
                    nav.speed = 2;
                    nav.isStopped = false;

                }

            }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {

                Debug.Log("Double Click");
                m_IsOneClick = false;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
                {                  //������ �¾Ҵٸ�  �÷ξ 
                    target = hit.point; //������ ���� ��ġ�� target�� ����
                    nav.destination = target;
                    nav.speed = 6f;
                    nav.isStopped = false;

                }

            }
        }
    }

    void UpdateAnimator()
    {
        Vector3 _velocity = nav.velocity;
                                // ������ǥ�� ������ǥ�� ��ȯ
        Vector3 localVelocity = tr.InverseTransformDirection(_velocity);
        float _speed = localVelocity.z; // z�� ���ǵ带 ����
        animator.SetFloat("ForwardSpeed", _speed);
    }
}
