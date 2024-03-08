using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    // Atribute: 주석은 사람간의 소통(설명, 참고사항 기입)을 위해 사용하는 것이라면 Atribute는 사람과 컴퓨터(에디터)와의 상호작용이다
    [Header("Player Move")]
    [SerializeField] Transform tr;
    public float moveSpeed;
    public float turnSpeed;
    [SerializeField] private float h = 0f, v = 0f;
    [Header("Player Rotation")]
    public float xSensitivity = 1600.0f;     // 마우스 x회전 감도
    public float ySensitivity = 1600.0f;     // 마우스 y회전 감도
    public float yMinLimit = -45.0f;        // x회전을 제한(최소값)
    public float yMaxLimit = 45.0f;         // x회전을 제한(최대값)
    public float xMinLimit = 360.0f;
    public float xMaxLimit = 360.0f;
    public float xRot = 0f;
    public float yRot = 0f;
    [Header("Jump")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce = 8.0f;    // 점프력
    private bool isJumping = false; // 점프중인지 아닌지 판단
    void Start()
    {
        moveSpeed = 5.0f;
        turnSpeed = 90.0f;
    }
    private void OnCollisionEnter(Collision col)
    {
        isJumping = false;
    }
    private void OnCollisionExit(Collision col)
    {
        isJumping = true;
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();
        FastSpeed();
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
            isJumping = true;
        }
    }

    private void Jump()
    {
        if (isJumping) return;
        rb.velocity = Vector3.up * jumpForce;
        // velocity: 힘과 방향
        Debug.Log("Jump");
    }

    void FastSpeed()
    {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 13.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5.0f;
        }
    }

    private void PlayerRotate()
    {
        xRot += Input.GetAxisRaw("Mouse X") * xSensitivity * Time.deltaTime;
        yRot += Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yMinLimit, yMaxLimit);
        tr.localEulerAngles = new Vector3(-yRot, xRot, 0f);
    }

    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
    }
}
