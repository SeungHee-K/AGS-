using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barret : MonoBehaviour
{
    public GameObject UIImage;
    void Start()
    {
        Destroy(gameObject,10f); // 드럼통 10초 후 삭제
    }
    
    public void Death()
    {
        Instantiate(UIImage, this.transform.position,Quaternion.identity);                      
        
        Destroy(gameObject);
    }
}
