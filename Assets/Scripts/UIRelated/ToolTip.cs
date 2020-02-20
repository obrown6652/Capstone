using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    public static ToolTip instance;

    public static ToolTip MyInstance
    { get { return instance; } }


    public Text name;

    public Text type;

    public Text stats;

    public Text description;

    public Image icon;

    public Text gold;

    public GameObject goldSection;


    public Item Item;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Create(Item item)
    {
        Item = item;
        name.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColor[item.MyQuality], item.MyTitle);
        type.text = "Need type";

        if (item is Armor)
        {
            stats.text = (item as Armor).GetStats();
            type.text = (item as Armor).MyArmorType.ToString();
        }

        description.text = item.GetDescription();
        icon.sprite = item.MyIcon;
        gold.text = item.MyCost+ "";

    }

    public void ActivateGoldSection() {
        goldSection.SetActive(true);
    }
    public void DeactivateGoldSection()
    {
        goldSection.SetActive(false);
    }



}
