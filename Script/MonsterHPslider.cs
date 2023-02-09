using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MonsterHPslider : MonoBehaviour
{
    Monster monster;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        monster = this.transform.parent.parent.GetComponent<Monster>();
        slider.maxValue = monster.monsterHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = monster.monsterHP;
    }
}
