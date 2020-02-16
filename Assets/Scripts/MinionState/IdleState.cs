using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    private Minion parent;

    public void Enter(Minion parent)
    {
        this.parent = parent;
            
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        Debug.Log("Idle State");

        //Change into state if the player is close
        //if we have a player target
        if (parent.Mytarget_player != null)
        {
            //if minion greater than player distance 
            if (Vector3.Distance(parent.transform.position, parent.Mytarget_player.position) > parent.MyFollowRange)
            {
                parent.ChangeState(new FollowState());
            }
           
        }
    }
}
