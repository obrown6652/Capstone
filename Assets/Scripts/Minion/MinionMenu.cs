using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls the minion menu by creating the container,
// a dictionary that link all minion to it's container
public class MinionMenu : MonoBehaviour
{
    private static MinionMenu instance;

    public static MinionMenu MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MinionMenu>();
            }
            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject container;


    [SerializeField]
    private Transform body;


    //dictionary that link minion to it's container
    private Dictionary<Minion, MinionMenuContainer> mainMenu = new Dictionary<Minion, MinionMenuContainer>();

    public Dictionary<Minion, MinionMenuContainer> MyMainMenu {
        get {
            return mainMenu;
        }
    }

    private bool canUpgrade;

    protected void Start()
    {
        canUpgrade = true;
    }

    protected void Update()
    {
       
    }

    public MinionMenuContainer createContainer(Minion p, string name, float damage, int level,string requirements)
    {
        MinionMenuContainer mm = Instantiate(container,body).GetComponent<MinionMenuContainer>();
        mm.MyName.text = name;
        mm.MyDamage.text = damage + "";
        mm.MyLevel.text = level+ "";
        mm.MyRequirements.text = ItemCollections.MyInstance.weapons["00000001"];     //just for debug
        mm.MyUpgradeBtn.onClick.AddListener(delegate { upgrade(p, mm); });
        return mm;
 
        
    }

   
    //upgrade minion when button is click
    public void upgrade(Minion p, MinionMenuContainer mc) {

        if (canUpgrade && ItemInInventory(mc.MyRequirements.text))
        {
            p.MyDamage.MyCurrentValue += 5;
            p.MyDamage.MyMaxValue += 5;
            mc.MyDamage.text = p.MyDamage.MyMaxValue + "";

            p.MyLevel += 1;
            mc.MyLevel.text = p.MyLevel + "";
        }
    }

    //check if item in playe inventory
    public bool ItemInInventory(string itemName)
    {
        foreach (SlotScript slot in BagScript.MyInstance.MySlots)
        {

            try
            {
                if (slot.MyItem.MyTitle == itemName)
                {
                    Debug.Log("found");
                    return true;
                }
           
            }
            catch (System.Exception)
            {
                Debug.Log("Nullll");
                continue;
            }

        }
        return false;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
