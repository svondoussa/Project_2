using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimator : MonoBehaviour
{

    public float spinSpeed = 100f;
    public float movementHeight = 0.005f;
    public float movementSpeed = 3.0f;
    public float scaleSpeed = 6f;
    public float scaleSize = 1f;


    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        this.transform.position += new Vector3(0f, movementHeight * Mathf.Sin(Time.time * movementSpeed), 0f);
        float scaleValue = scaleSize * Mathf.Sin(scaleSpeed * Time.time);
        this.transform.localScale += new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
