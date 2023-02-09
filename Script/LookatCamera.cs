using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    Camera cameraToLookAt;
    float moveSpeed =1;
    void Start()
    {
        cameraToLookAt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    // Update is called once per frame

    void Update()
    {
        transform.LookAt(transform.position + cameraToLookAt.transform.rotation * Vector3.back,
        cameraToLookAt.transform.rotation * Vector3.up);

        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
    }
}