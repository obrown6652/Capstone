using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/*
 This class contain all the functionality that relate to any Non Playable character that is:
 what happen when select and deslect, incteract and stop interacting
  */


 
public class NPC : Character, IInteractable
{

    public string talkToNode = "";

    [Header("Optional")]
    public YarnProgram scriptToLoad;


    protected override void Start()
    {
      

        if (scriptToLoad != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(scriptToLoad);
        }

        base.Start();
    }


    //Deselect is marked as virtual so we can override it in the subclasses
    public virtual void DeSelect()
    {
        //when the npc is deslect deactivate the marker 
    }

    //Select is marked as virtual so we can override it in the subclasses
    public virtual Transform Select()
    {
        //when the npc is select activate the marker 
        return hitBox;
    }
    public virtual void Interact()
    {
    }

    public virtual void StopInteract()
    {
        
    }
}
