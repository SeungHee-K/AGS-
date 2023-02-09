using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// 몬스터 타겟팅 이미지 생성 => 사용 XXX

public class Target_Manager : MonoBehaviour
{
    public GameObject Indicator;
    ARRaycastManager ARraycast;

    void Start()
    {
        Indicator.SetActive(true);

        ARraycast = this.GetComponent<ARRaycastManager>();
    }

    void Update() {}

    private void Targeting()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // 화면 정중앙

        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if(ARraycast.Raycast(screenSize, hits, TrackableType.AllTypes))
        {
            Indicator.GetComponent<MeshRenderer>().material.color = Color.red;

            Indicator.transform.position = hits[0].pose.position;
            Indicator.transform.rotation = hits[0].pose.rotation;
        }

        else
        {
            Indicator.GetComponent<MeshRenderer>().material.color = Color.white;
        }

    }
}

