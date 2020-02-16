using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script control what happen when player in area
public class InRangeMinion : MonoBehaviour
{
    

    //determin if minion is in enemey range or not
    private bool minionInRage = false;

    //temporaly hold the enemey who's in the minion range
    private Enemy TempEnemy;

    //the minion gameobject
    private Minion minionParent;
    //determin if the minion found the plaeyr or not 
    private bool foundPlayer = false;



    
    private void Start()
    {
        
        minionParent = GetComponentInParent<Minion>();
       
    }

    public void Update()
    {

        //check to see if gameobject is a minion if so if minion target is null decided if it should go to the player or attack enemey
        if (minionParent != null)
        {

            if (minionParent.Mytarget_Enemy == null )
            {
            

                //if the minion is in rage of the enemy and the player than attack the enemy
                if (minionInRage && foundPlayer)
                {
                    minionParent.Mytarget_Enemy = TempEnemy.transform;
                }
               
            }
        }
       
       
    }

    
    public void OnTriggerStay2D(Collider2D collision)
    {
        //if the enemy is in the minion range
        if (collision.tag =="Enemy")
        {
            if (minionParent != null)
            {
                minionInRage = true;
            }
        }

        //if the minion is in the player range 
        if (collision.tag == "Player")
        {
            if (minionParent != null)
            {
                foundPlayer = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
      
        //if the enemy target trigger
        if (collision.tag == "Enemy")
        {
            //if the gameobject is a minion
            if (minionParent != null)
            {

                //if enemy enter minion range go to the enemey
                TempEnemy = collision.GetComponent<Enemy>();    //temportarly get the enmey that enter ther minion range
                minionParent.Mytarget_Enemy = collision.transform;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if player exit minion range set foundPlayer to false
            if (minionParent != null)
            {
                foundPlayer = false;
            }

            //if the minion is out of the player range start the find the player methond
            if (minionParent != null)
            {
            
                StartCoroutine(FindPlayer());

            }
        }
        //if minion exit enemy range set  minioninrage to false
        if (collision.tag == "Enemy")
        {
            if (minionParent != null)
            {
                minionInRage = false;
            }
        }
    }

    public IEnumerator FindPlayer() {
       yield return new WaitForSeconds(3);

       minionParent.Mytarget_Enemy = null;
        
      

    }
}
