using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float smoothing = 5;
    private Vector3 offset;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCam = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCam, smoothing * Time.deltaTime);

    }
}
