using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake1 : MonoBehaviour
{
    bool isShake = false;
    Transform camPivotTr;

    float shakeTime;
    float duration = 1.0f;

    Vector3 originPos;
    Quaternion originRot;

    void Start()
    {
        camPivotTr = transform;   
    }

    void Update()
    {
        
        if(isShake)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            camPivotTr.position = shakePos * 0.2f;

            Vector3 shakeRot = new Vector3(0f, 0f, Mathf.PerlinNoise(Time.time * 0.5f, 0f));
            camPivotTr.rotation = Quaternion.Euler(shakeRot);

            if(Time.time - shakeTime > duration)
            {
                isShake = false;
                camPivotTr.position = originPos;
                camPivotTr.rotation = originRot;
            }
        }
    }
    public void TurnOn()
    {
        originPos = camPivotTr.position;
        originRot = camPivotTr.rotation;
        shakeTime = Time.time;
        if (!isShake)
            isShake = true;
    }
}
