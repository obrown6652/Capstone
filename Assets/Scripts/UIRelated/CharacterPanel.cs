using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }

   

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private CharButton outfit, mainhand, accessory1, accessory2, accessory3;

    public CharButton MySlectedButton { get; set; }

    public CharButton MyMainhand
    {
        get
        {
            return mainhand;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    public  void EpuipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Outfit:
                outfit.EquipArmor(armor);
                break;
            case ArmorType.MainHand:
                mainhand.EquipArmor(armor);
                break;
            case ArmorType.Accessory://equip to empthy armor slot if there's not switch the 1st slot
                accessory1.EquipArmor(armor);
                break;
            default:
                break;
        }

    }
}
