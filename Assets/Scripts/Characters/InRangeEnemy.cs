using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeEnemy : MonoBehaviour
{
    //Enemy health ui
    [SerializeField]
    private CanvasGroup healthGroup;

    //the enemey gameobject
    private Enemy enemyParent;

    private void Start()
    {
        enemyParent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player target trigger 
        if (collision.tag == "Player")
        {

            healthGroup.alpha = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if a player exit an enemy range show the enemy health ui
            healthGroup.alpha = 0; 
        }
    }


}
