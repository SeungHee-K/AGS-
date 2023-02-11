using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject Grenade;
    public float power = 20;

    public int coolTime = 10;
    public Slider Slider;
   

    private void Start()
    {
        Slider.maxValue = coolTime;
        Slider.value = 0;
    }

    private void Update()
    {
 
        if (Slider.value > 0)
        {
            Slider.value -= Time.deltaTime;
        }
    }

    public void GrenadeCreate()
    {
        if (Slider.value != 0) { return; }
        GameObject Grenade_=  Instantiate(Grenade,this.transform.position, Quaternion.Euler(0,-90,0));
        Rigidbody rb= Grenade_.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward* power,ForceMode.Impulse);
        Slider.value = coolTime;
    }
}
