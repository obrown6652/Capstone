using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState
{

    private Minion parent;


    public void Enter(Minion parent)
    {
        this.parent = parent;
    
    }

    public void Exit()
    {
        parent.MyDirection = Vector2.zero;
  
    }

    public void Update()
    {
        Debug.Log("Follow State");

        if ( parent.Mytarget_Enemy == null)
        {
            //find the player direction 
            parent.MyDirection = (parent.Mytarget_player.transform.position - parent.transform.position).normalized;


            //move the minion towards the player
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Mytarget_player.position, parent.MyMovementSpeed.MyMaxValue * Time.deltaTime);

            //
            if (Vector3.Distance(parent.transform.position, parent.Mytarget_player.position) <= parent.MyFollowRange )
            {
                parent.ChangeState(new IdleState());
            }

        }
        else if (parent.Mytarget_Enemy != null)
        {
            //find the enemy direction
            parent.MyDirection = (parent.Mytarget_Enemy.transform.position - parent.transform.position).normalized;

            //move the minion towards the enemy
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Mytarget_Enemy.position, parent.MyMovementSpeed.MyMaxValue * Time.deltaTime);

            //
            if (Vector3 .Distance(parent.transform.position, parent.Mytarget_Enemy.position ) < parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
     

    }
}
