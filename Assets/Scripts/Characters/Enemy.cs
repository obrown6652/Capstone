using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 This class whole the basic function for all enemies that is:
 health, name, the loot that it will drop when dies, what happen when it is selected
     */
public class Enemy : NPC, IInteractable

{
    //Enemy health ui
    [SerializeField]
    private CanvasGroup healthGroup;

    //Enemy health ui text name
    [SerializeField]
    private Text Name;

    [SerializeField]
    private LootTable lootTable;

    SelectMarker marker;
    

    protected override void Start()
    {
        //at the start set all Enemy names to their health ui text
        Name.text = base.characterName;

        marker = GetComponent<SelectMarker>();
        base.Start();
    }

    protected  override void Update()
    {
        base.Update();
    }


    
    public override Transform Select()
    {
        marker.Activate();
        return base.Select();
    }


    //if enemy deselected make health ui invisble 
    public override void DeSelect()
    {
        marker.Deactivate();
        base.DeSelect();
    }

    public override void Interact()
    {
        Debug.Log("is alive:"+ isAlive );

        if (!isAlive)
        {
            //loot enemey
            Debug.Log("loot me");

            lootTable.ShowLoot();
           
        }
    }

}
