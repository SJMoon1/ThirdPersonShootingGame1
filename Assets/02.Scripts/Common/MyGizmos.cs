using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    [SerializeField] private Color _color = Color.red;
    [SerializeField, Range(0.001f, 0.1f)] private float _radius = 0.015f;
    void Start()
    {

    }
    private void OnDrawGizmos()     // ���̳� ������ �׷��ִ� ����Ƽ ���� �Լ� (�ݹ��Լ�)
    {
        Gizmos.color = _color;      // �÷�
        Gizmos.DrawSphere(transform.position, _radius);     // ����̳� ����(��ġ, �ݰ�)
    }
}
