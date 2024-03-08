using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    // Atribute: �ּ��� ������� ����(����, ������� ����)�� ���� ����ϴ� ���̶�� Atribute�� ����� ��ǻ��(������)���� ��ȣ�ۿ��̴�
    [Header("Player Move")]
    [SerializeField] Transform tr;
    public float moveSpeed;
    public float turnSpeed;
    [SerializeField] private float h = 0f, v = 0f;
    [Header("Player Rotation")]
    public float xSensitivity = 1600.0f;     // ���콺 xȸ�� ����
    public float ySensitivity = 1600.0f;     // ���콺 yȸ�� ����
    public float yMinLimit = -45.0f;        // xȸ���� ����(�ּҰ�)
    public float yMaxLimit = 45.0f;         // xȸ���� ����(�ִ밪)
    public float xMinLimit = 360.0f;
    public float xMaxLimit = 360.0f;
    public float xRot = 0f;
    public float yRot = 0f;
    [Header("Jump")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce = 8.0f;    // ������
    private bool isJumping = false; // ���������� �ƴ��� �Ǵ�
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
        // velocity: ���� ����
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
