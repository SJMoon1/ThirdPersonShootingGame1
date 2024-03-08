using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInMiniMap : MonoBehaviour
{
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        _image.enabled = true;
        StartCoroutine(BlinkPoint());
    }

    void Update()
    {

    }
    IEnumerator BlinkPoint()
    {
        _image.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _image.enabled = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BlinkPoint());
    }
}
