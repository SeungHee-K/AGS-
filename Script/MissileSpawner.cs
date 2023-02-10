using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missile;
    public GameObject Image;

    public int MissileCount = 10;
    public int coolTime = 60;
    public Slider Slider;

    private bool missilebool =false;

    private void Start()
    {
        missilebool = Datamanager.instance.nowPlayer.missile;
        Slider.maxValue = coolTime;
        Slider.value = 0;
        if (!missilebool) { Image.SetActive(false); }
    }

    private void Update()
    {
        if (!missilebool) { return; }
        if (Slider.value >0)
        {
            Slider.value -= Time.deltaTime;
        } 
    }


    public void Missilespawn()
    {
        if (!missilebool) { return; }
        if (Slider.value != 0) { return;}
        for (int i = 0; i < MissileCount; i++)
        {
            float RandomX = Random.Range(-15f, 15f);
            float RandomY = Random.Range(10f, 20f);
            float RandomZ = Random.Range(-15f, 15f);


            if (RandomX < 3 && RandomX > -3f && RandomZ < 3f && RandomZ > -3f)
            {
                if (RandomX > 0)
                {
                    RandomX = Random.Range(8f, 12f);
                }
                else
                {
                    RandomX = Random.Range(-12f, -8f);
                }
                if (RandomZ > 0)
                {
                    RandomZ = Random.Range(8f, 12f);
                }
                else
                {
                    RandomZ = Random.Range(-12f, -8f);
                }
            }

            Instantiate(missile, new Vector3(this.transform.position.x + RandomX, this.transform.position.y + RandomY, this.transform.position.z + RandomZ), Quaternion.Euler(90,0,0));
        }
        Slider.value = coolTime;
    }

}
