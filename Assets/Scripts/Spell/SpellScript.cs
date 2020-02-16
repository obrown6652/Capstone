using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class describle how the spells will function 
public class SpellScript : MonoBehaviour

{
    //the spell target
    public Transform Mytarget { get; protected set; }

    //damage
    protected int damage;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    public virtual void Initialized(Transform target, int damage)
    {
        this.Mytarget = target;
        this.damage = damage;
    }

    //spell and hitbox collision 
    protected  virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if a spell hit a the target hitbox 
        if (collision.tag == "HitBox")
        {
            //get the hitbox parent and take damage
           collision.GetComponentInParent<Enemy>().TakeDamage(damage);

            //play impact animation
          // GetComponent<Animator>().SetTrigger("impact");

        }
    }
}
