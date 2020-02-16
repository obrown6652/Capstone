using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class is all the infomation the menu container holds
public class MinionMenuContainer : MonoBehaviour
{

  

    [SerializeField]
    private Text minionName;

    [SerializeField]
    private Text damage;

    [SerializeField]
    private Text level;

    [SerializeField]
    private Text requirements;

    [SerializeField]
    private Button upgradeBtn;

    public Text MyName
    {
        get
        {
            
            return minionName;
        }
        set
        {
            minionName = value;
        }
    }
    public Text MyDamage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public Text MyLevel
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public Text MyRequirements
    {
        get
        {
            return requirements;
        }
        set
        {
            requirements = value;
        }
    }

    public Button MyUpgradeBtn
    {
        get
        {
            return upgradeBtn;
        }
        set
        {
            upgradeBtn = value;
        }
    }



}
