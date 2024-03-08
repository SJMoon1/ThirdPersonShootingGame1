using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatMecanimPlayer : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Animator _animator;

    public float moveSpeed = 3.0f;
    public float turnSpeed = 1000.0f;
    private float h, v, r;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveAndRotate();
        Sprint();
        StopJump();
        if (Input.GetKeyDown(KeyCode.Space) && v > 0)
        {
            _animator.SetTrigger("MoveJumpTrigger");
            rb.velocity = Vector3.up * 2.0f;
        }
    }

    private void StopJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && h == 0 && v == 0)
        {
            _animator.SetTrigger("StopJumpTrigger");
            rb.velocity = Vector3.up * 2.0f;
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            _animator.SetBool("IsSprint", true);
            moveSpeed = 8.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetBool("IsSprint", false);
            moveSpeed = 3.0f;
        }
    }

    private void MoveAndRotate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxisRaw("Mouse X");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);
        {
            _animator.SetFloat("posX", h, 0.1f, Time.deltaTime);
            _animator.SetFloat("posY", v, 0.1f, Time.deltaTime);
        }
        tr.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }
}
