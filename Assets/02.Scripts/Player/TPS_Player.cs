using UnityEngine;

public class TPS_Player : MonoBehaviour
{
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider csCollider;
    [SerializeField] Animation ani;

    public float moveSpeed;
    public float turnSpeed;
    float h, v;
    public bool isRun = false;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        csCollider = GetComponent<CapsuleCollider>();
        moveSpeed = 5.0f;
        turnSpeed = 500.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMove();
        PlayerAnimation();
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 10.0f;
            ani.CrossFade("SprintF", 0.3f);
            isRun = true;
        }
        else
        {
            moveSpeed = 5.0f;
            isRun = false;
        }
    }

    private void PlayerAnimation()
    {
        if (Input.GetKey(KeyCode.D))
            ani.CrossFade("RunR", 0.3f);
        else if (Input.GetKey(KeyCode.A))
            ani.CrossFade("RunL", 0.3f);
        if (Input.GetKey(KeyCode.W))
            ani.CrossFade("RunF", 0.3f);
        else if (Input.GetKey(KeyCode.S))
            ani.CrossFade("RunB", 0.3f);
        if (v == 0 && h == 0)
        {
            ani.CrossFade("Idle", 0.3f);
        }
        // 직전 동작과 지금 동작 사이를 0.3초 동안 블랜드 한다
    }

    void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime);
    }
}
