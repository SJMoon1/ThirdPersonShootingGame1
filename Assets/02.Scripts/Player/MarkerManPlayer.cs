using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManPlayer : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    [SerializeField] Animator ani;

    float h, v, r;
    float moveSpeed;
    float turnSpeed;

    void Start()
    {
        tr= transform;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 3.0f;
        turnSpeed = 1000.0f;
    }

    void Update()
    {
        MoveAndRotate();
        if(Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.W))
        {
            moveSpeed = 8.0f;
            ani.SetTrigger("runTrigger");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 3.0f;
        }
    }

    private void MoveAndRotate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxisRaw("Mouse X");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        {
            ani.SetFloat("posX", h, 0.1f, Time.deltaTime);
            ani.SetFloat("posY", v, 0.1f, Time.deltaTime);
        }
        tr.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }
}
