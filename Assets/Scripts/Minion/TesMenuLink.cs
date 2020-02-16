using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesMenuLink : MonoBehaviour
{
    Bag bag;

    // Update is called once per frame
    void Update()
    {
        //check if minion and it's container is added to the list
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("x is pressed");
            foreach (KeyValuePair<Minion, MinionMenuContainer> item in MinionMenu.MyInstance.MyMainMenu)
            {
                Debug.Log(item.Key.MyCharacterName );
            }
        }

        //check to see if an item is in player inventory
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("z is pressed");
            foreach (SlotScript slot in BagScript.MyInstance.MySlots)
            {

                try
                {
                    if (slot.MyItem.MyTitle == "Steel Sword")
                    {
                        Debug.Log("found");
                    }
                    else
                    {
                        Debug.Log(slot.MyItem.MyTitle);
                    }
                }
                catch (System.Exception)
                {
                    Debug.Log("Nullll");
                    continue;
                }

            }
           
        }
    }


}
