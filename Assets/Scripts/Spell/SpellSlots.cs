using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlots : MonoBehaviour
{

    private static SpellSlots instance;

    public static SpellSlots MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellSlots>();
            }
            return instance;
        }
    }


    [SerializeField]
    private SpellButton[] slots;
    [SerializeField]
    private Image[] image;

    

    public SpellButton[] mySlots { get { return slots; } }
    public Image[] myImage { get { return image; } set { image = value; } }

}
