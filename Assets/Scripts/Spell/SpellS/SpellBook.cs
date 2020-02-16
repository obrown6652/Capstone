using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//All the references of the player ui slots 
public class SpellBook : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Spell spell;


    public string MyName
    { get { return name; } }

    public Spell MySpell
    { get { return spell; } }

    private bool hasRead;

    public bool GetHasRead { get { return hasRead; } set { hasRead = value; } }

    private void Start()
    {
        hasRead = false;
    }



    public void Interact()
    {
        hasRead = true;
        SpellInventory.MyInstance.addSpellInventory(spell);
        SpellInventory.MyInstance.addSpellUI(spell);
        StopInteract();
    }

    public void StopInteract()
    {
        if (hasRead)
        {
            Destroy(gameObject);
        }
      
    }
}
