using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera_Practice : MonoBehaviour
{
    [SerializeField] private Transform camPivotTr;
    [SerializeField] private Transform camTr;
    [SerializeField] private Transform playerTr;

    [SerializeField, Range(1.0f, 100.0f)] private float camDist = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] private float camHeight = 5.0f;
    [SerializeField, Range(1.0f, 50.0f)] private float moveDamping = 20.0f;
    [SerializeField, Range(1.0f, 50.0f)] private float rotDamping = 20.0f;
    [SerializeField, Range(1.0f, 50.0f)] private float targetOffset = 1.0f;
    void Start()
    {
        camPivotTr= transform;
        camTr= Camera.main.transform;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void LateUpdate()
    {
        var camPos = playerTr.position + (Vector3.up * camHeight) - (Vector3.forward * camDist);
        camPivotTr.position = Vector3.Slerp(camPivotTr.position, camPos, moveDamping * Time.deltaTime);
        camPivotTr.rotation = Quaternion.Slerp(camPivotTr.rotation, playerTr.rotation, rotDamping * Time.deltaTime);
        camTr.LookAt(playerTr.position + (Vector3.up * targetOffset));
    }
}
