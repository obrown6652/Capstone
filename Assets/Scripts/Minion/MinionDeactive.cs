using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDeactive : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy" )
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.collider);
            
        }
    }
}
