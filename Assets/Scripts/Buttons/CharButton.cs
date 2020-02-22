using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor equippedArmor;

    [SerializeField]
    private Image icon;

    public Armor MyEquippedArmor
    {
        get
        {
            return equippedArmor;
        }
        set
        {
            equippedArmor = value;
        }

    }
    [SerializeField]
    private GearSocket gearSocket;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp =(Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }

                UIManager.MyInstance.RefreashTooltip(tmp);
            }
            else if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedArmor);
                CharacterPanel.MyInstance.MySlectedButton = this;
                 icon.color = Color.grey;
                
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();

       
        if (equippedArmor != null)
        {
            if (equippedArmor !=  armor)
            {
          
               armor.MySlot.AddItem(equippedArmor);

            }
            
            UIManager.MyInstance.RefreashTooltip(equippedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        this.equippedArmor = armor;// a reference to the equipped armor
        this.equippedArmor.MyCharButton = this;

        if (HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }
        if (gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Equip(equippedArmor.MyAnimationClips);
        }
       

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, equippedArmor, new Vector2(1.7f,0));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;

        if (gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Dequip();
        }
        equippedArmor.MyCharButton = null;
        equippedArmor = null;
    }
}
