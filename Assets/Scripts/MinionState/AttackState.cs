using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    private Minion parent;

    private float attackCooldown = 1;

    private float extraRange = 0.1f;

    public void Enter(Minion parent)
    {
        this.parent = parent;
        Physics2D.IgnoreCollision(parent.transform.Find("collision").GetComponent<Collider2D>(), parent.Mytarget_Enemy.Find("Collision").GetComponent<Collider2D>());

    }

    public void Exit()
    {

    }

    public void Update()
    {

        if (parent.MyAttackTime >= attackCooldown && !parent.MyIsAttack_Basic )
        {
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
        }
        Debug.Log("Attack State");
        if (parent.Mytarget_Enemy != null)
        {

            //if minion greater than enemy attack distance 
            if (Vector3.Distance(parent.transform.position, parent.Mytarget_Enemy.position) >= parent.MyAttackRange + extraRange && !parent.MyIsAttack_Basic)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack() {

        parent.MyIsAttack_Basic = true;

        parent.MyAnimator.SetBool("attack", parent.MyIsAttack_Basic );
        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(1).length);

        parent.MyIsAttack_Basic = false;
        parent.MyAnimator.SetBool("attack", parent.MyIsAttack_Basic);

    }
}
