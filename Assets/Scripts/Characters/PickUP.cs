using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 This class is for items that can be pick up in the game world
     */
public class PickUP : MonoBehaviour, IInteractable,IDescribable
{
    [SerializeField]
    Item item;

    private SpriteRenderer spriteRenderer;

    RaycastHit2D hit;

    public void Start()
    {
        //get the parent gameobject
        spriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //set the image to the item icon
        spriteRenderer.sprite = item.MyIcon;

    }
 

    public string GetDescription()
    {
        string text = item.GetDescription();
        string text2 = "\n Price: 5g";

        return text+ text2; ;
    }

    public void Interact()
    {
        Debug.Log("activate interact");
        InventoryScript.MyInstance.AddItem((Item)Instantiate(item));
        Destroy(this.gameObject);

        StopInteract();
    }

 

    public void OnMouseOver()
    {
       
       UIManager.MyInstance.ShowTooltip(Camera.main.WorldToScreenPoint(transform.position), new Vector2(0, 1), item);
        ToolTip.MyInstance.ActivateGoldSection();
    }
    public void OnMouseExit()
    {
        UIManager.MyInstance.HideTooltip();

    }

  
    public void StopInteract()
    {
        UIManager.MyInstance.HideTooltip();
    }



    
}
