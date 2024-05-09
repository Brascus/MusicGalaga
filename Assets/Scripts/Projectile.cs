using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        
        if (collision.tag == "Boundary" )
        Destroy(this.gameObject);

        if (this.gameObject.tag == "Laser" && collision.gameObject.tag == "Invader" || collision.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(this.gameObject);
        }


    }
}
