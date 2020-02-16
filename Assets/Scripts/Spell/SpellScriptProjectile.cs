using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class describle how the projectile spells will function 
public class SpellScriptProjectile : SpellScript

{

    private Rigidbody2D myRigidbody;

    //the spell movement speed
    private float speed;

    //time to destory projectile prefab
    float time = 5f;


    protected override void Start()
    {
        //get the spell rigid body
        myRigidbody = GetComponent<Rigidbody2D>();

        base.Start();

    }

   

    private void FixedUpdate()
    {
        if (Mytarget != null)
        {
            //calculate the spells direction
            Vector2 direction = Mytarget.position - transform.position;

            //moves the spell by using the rigid boyd
            myRigidbody.velocity = direction.normalized * speed;

            //calculates the rotation angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //rotates teh spell towards the target
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    //
    public  void Initialized(Transform target, int damage, int speed)
    {

        this.Mytarget = target;
        this.damage = damage;
        this.speed = speed;

        StartCoroutine(DestoryProjectile(this.gameObject, time));

       
    }
    //spell and hitbox collision 
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //if a spell hit a the target hitbox 
        if (collision.tag == "HitBox" && collision.transform == Mytarget)
        {
            speed = 0;

            //get the hitbox parent and take damage
            collision.GetComponentInParent<Enemy>().TakeDamage(damage);

            //play impact animation
            GetComponent<Animator>().SetTrigger("impact");

            //stop moving the spell
            myRigidbody.velocity = Vector2.zero;

            Mytarget = null;
        }
    }

    //destroy projectile projectile after a set time
    public IEnumerator DestoryProjectile(GameObject obj, float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(obj);

    }
}
