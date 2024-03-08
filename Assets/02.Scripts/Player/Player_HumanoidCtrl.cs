using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HumanoidCtrl : MonoBehaviour
{
    
    [SerializeField]
    [Range(1.0f, 20.0f)] private float walkSpeed = 3.0f;
    [SerializeField]
    [Range(1.0f, 20.0f)] private float runSpeed = 6.0f;
    [SerializeField]
    [Range(1.0f, 1000.0f)] private float turnSpeed = 500.0f;

    private Animator animator;
/*    private CharacterController charCtrl;
    private Transform firePos;
    private GameObject bulletPref;*/

    // private Vector3 moveVelocity;

    private float moveSpeed;
    public bool isRun;

    void Start()
    {
        moveSpeed = walkSpeed;
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // charCtrl = GetComponent<CharacterController>();
        // firePos = transform.GetChild(0).GetChild(0).GetChild(0).transform;
        // bulletPref = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();

    }

    private void PlayerRotate()
    {
        float y = Input.GetAxisRaw("Mouse X");
        transform.Rotate(Vector3.up * y * turnSpeed * Time.deltaTime);
    }

    private void PlayerMove()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 moveDir = (Vector3.right * h) + (Vector3.forward * v);
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
/*        moveVelocity = new Vector3(h, 0f, v).normalized * moveSpeed * Time.deltaTime;
        moveVelocity = transform.TransformDirection(moveVelocity);
        charCtrl.Move(moveVelocity);*/
        animator.SetFloat("posX", h);
        animator.SetFloat("posY", v);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            animator.SetBool("isRun", true);
            isRun = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
            animator.SetBool("isRun", false);
            isRun = false;
        }
    }
}
