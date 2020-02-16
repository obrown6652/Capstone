using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that whole all the wepons in the game

public class ItemCollections : MonoBehaviour
{
    private static ItemCollections instance;

    public static ItemCollections MyInstance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemCollections>();
            }
            return instance;
        }
    }

    [Header("Weapon collection")]
    [SerializeField]
    private Item[] weapon;



    [Header("Outfit collection")]
    [SerializeField]
    private Item[] outfit;

    [Header("Potions collection")]
    [SerializeField]
    private Item[] potion;

    private string name;
    private int damage;
    
    public Dictionary<string, string> weapons = new Dictionary<string, string>();
    public Dictionary<string, string> outfits = new Dictionary<string, string>();
    public Dictionary<string, string> potions = new Dictionary<string, string>();

    public void Awake()
    {

        generateId();
    }

    public string MyName { get { return name; } }
    public int MyDamage { get { return damage; } }

    /*      
 *      Steel sword -  00000001
 *      black sword -  00000002
 *      undead sword - 00000003
 *      wooden staff - 00000004
 *      undead staff - 00000005
 *      power staff -  00000006
 *      
 *      black robe -  0000000a
 *      blue robe -   0000000b
 *      red robe -    0000000c
 *      yellow robe - 0000000d
 *      
 *      health potion - 000000aa
 */
    private void generateId()
    {
        weapons.Add("00000001",weapon[0].MyTitle);
        weapons.Add("00000002", weapon[1].MyTitle);
        weapons.Add("00000003", weapon[2].MyTitle);
        weapons.Add("00000004",weapon[3].MyTitle);
        weapons.Add("00000005", weapon[4].MyTitle);
        weapons.Add("00000006", weapon[5].MyTitle);

        outfits.Add("0000000a", outfit[0].MyTitle);
        outfits.Add("0000000b", outfit[1].MyTitle);
        outfits.Add("0000000c", outfit[2].MyTitle);
        outfits.Add("0000000d", outfit[3].MyTitle);

        
    }

}
