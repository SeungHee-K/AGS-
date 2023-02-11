using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float DMG = 100;
    public GameObject Particle;
    Vector3 dr;
    public float range = 3;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.name);
        if (other.gameObject.tag == "Ground")
        {
            if (Particle != null)
                dr = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Instantiate(Particle, dr, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, range);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (colliders[i].gameObject.TryGetComponent<Monster>(out Monster monster))
                    {
                        monster.RangeDamage(DMG);
                    }
                }
            }
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Particle != null)
                dr = new Vector3(this.transform.position.x, this.transform.position.y - 2f, this.transform.position.z);
            Instantiate(Particle, dr, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, range);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (colliders[i].gameObject.TryGetComponent<Monster>(out Monster monster))
                    {
                        monster.RangeDamage(DMG);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
