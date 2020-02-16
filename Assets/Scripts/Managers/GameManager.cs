using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Player player;

    private NPC currentTarget;

   

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
      //  Debug.Log(LayerMask.NameToLayer("TransparentFX"));
    }

    //control what happen when click on something
    private void ClickTarget()
    {
        //right click on a target and if it's not an ui
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            //the mouse click position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            //if the click is not null
            if (hit.collider != null && (hit.collider.tag == "Enemy" ))
            {
                //and if target isn't null deselet it 
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                //set the current target
                currentTarget = hit.collider.GetComponent<NPC>();

                
                
                //set the player target to new target
                player.MyTarget = currentTarget.Select();
     
            }
            else if (hit.collider != null && hit.collider.tag == "Minion")
            {
                hit.collider.transform.parent.gameObject.GetComponent<Minion>().Interact();
            }
            //if the click is null
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                //don't have a target anymore
                currentTarget = null;
                player.MyTarget = null;
            }
           
        }
        else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           
            //the mouse click position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null && (hit.collider.tag == "Enemy" || hit.collider.tag == "PickUp" || hit.collider.tag == "Spellbook" || hit.collider.tag == "Interactable" || hit.collider.tag == "Chest"))
            {
                player.Interact();

                if (hit.collider.GetComponent<NPC>() != null)
                {
                    hit.collider.GetComponent<NPC>().Interact();
                }
                if (hit.collider.tag == "Spellbook")
                {
                    hit.collider.GetComponent<SpellBook>().GetHasRead = true;
                }
                
            }

        }
       
    }
}
