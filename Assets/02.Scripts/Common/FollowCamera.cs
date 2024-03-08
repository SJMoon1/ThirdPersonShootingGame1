using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform camTr;
    [SerializeField] private Transform camPivotTr;
    [SerializeField] private Transform targetTr;
    [SerializeField]
    [Range(0f, 50.0f)] private float height = 3.0f;
    [SerializeField]
    [Range(0f, 50.0f)] private float dist = 2.0f;
    [SerializeField]
    [Range(0.00f, 2.0f)] private float moveDamping = 1.0f;
    [SerializeField]
    [Range(0.00f, 2.0f)] private float rotDamping = 1.0f;
    [SerializeField, Range(1.0f, 10.0f)] private float targetOffset = 2.0f;
    private void OnValidate()
    {
        camPivotTr = transform;
        camTr = transform.GetChild(0).transform;
        targetTr = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Awake()
    {

    }

    void Update()
    {
        if (targetTr == null && camPivotTr == null) return;
        RaycastHit hit;
        if (Physics.Raycast(camPivotTr.position, camTr.position - camPivotTr.position, out hit, 10f, ~(1 << LayerMask.NameToLayer("Player"))))
            camTr.localPosition = Vector3.back * hit.distance;
        else
            camTr.localPosition = Vector3.back * dist;
    }

    private void LateUpdate()
    {
        var cameraPos = targetTr.position - (targetTr.forward * dist) + (targetTr.up * height);
        camPivotTr.position = Vector3.Slerp(camPivotTr.position, cameraPos, Time.deltaTime * moveDamping);
        camPivotTr.rotation = Quaternion.Slerp(camPivotTr.rotation, targetTr.rotation, Time.deltaTime * rotDamping);
        camPivotTr.LookAt(targetTr.position + (Vector3.up * targetOffset));
    }
    private void OnDrawGizmos()     // 씬 화면에 라인(선) 이나 색깔을 넣어주는 함수
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetTr.position + (targetTr.up * targetOffset), 0.1f);
        Gizmos.DrawLine(targetTr.position + (targetTr.up * targetOffset), camPivotTr.position);
    }

}
