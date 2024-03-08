using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform camPivotTr;
    float shakeTime = 0f;
    public bool isShake = false;

    void Start()
    {

    }

    void Update()
    {
        if(isShake)
        {
            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);
            transform.position += new Vector3(x, y, 0f);
            if(Time.time - shakeTime > 1.0f)
                isShake = false;
        }
    }
    public void ExplosionShake()
    {
        if(!isShake)
            isShake = true;
        shakeTime = Time.time;

    }
}
