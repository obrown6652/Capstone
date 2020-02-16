using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class contain the different type of spells the player can use
public class SpellInventory : MonoBehaviour
{
    private static SpellInventory instance;

    public static SpellInventory MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellInventory>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Spell[] spells;

    //start off with player knowing 2 spells 
    private int spellKnow= 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get count of the number of spells the player know
       // SpellbookCounter.MyInstance.MyCount = spellKnow;
    }

    //add the spell to the spell inventory 
    public void addSpellInventory(Spell s) {
        spellKnow++;

        //add 1 to the spellbook counter
        SpellbookCounter.MyInstance.addCount();
        GoldCounter.MyInstance.addCount(10);
        spells[spellKnow] = s;
    }

    //update the spellinventory ui
    public void addSpellUI(Spell s) {
       
        SpellSlots.MyInstance.mySlots[spellKnow].MySpellName = s.MyName;

        SpellSlots.MyInstance.mySlots[spellKnow].MySpellDescription = s.MyDescription;

        SpellSlots.MyInstance.myImage[spellKnow].sprite = s.MyIcon;
        SpellSlots.MyInstance.myImage[spellKnow].color = Color.white;
        
        
    }
    public Spell CastSpell(string spellName)
    {
       
       Spell spell = Array.Find(spells, x => x.MyName == spellName);
       

        //return the spell that we cast
        return spell;
    }

    public Spell GetSpell(string spellName)
    {
      Spell spell =  Array.Find(spells, x=> x.MyName == spellName);

        return spell;
    }
}
