using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler ,IPointerExitHandler
{
    [SerializeField]
    private string spellName;

    [SerializeField]
    private string Description;

    [SerializeField]
    private Text DescriptionTxt;

    public void Start()
    {
        
        DescriptionTxt.text = "No spell to preview.";

    }

   

    public string MySpellName { get { return spellName; } set { spellName = value; } }
    public string MySpellDescription { get { return Description; } set { Description = value; } }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //pick up spell
            HandScript.MyInstance.TakeMoveable(SpellInventory.MyInstance.GetSpell(spellName));
          
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionTxt.text = "No spell to preview.";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
   

        if (Description == "")
        {
            DescriptionTxt.text = "No spell to preview.";
        }
        else
        {
            //set description
            DescriptionTxt.text = Description;
        }
    }
}
