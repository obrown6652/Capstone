using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactiveui : MonoBehaviour
{
    private static Interactiveui instance;

    public static Interactiveui MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Interactiveui>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private Text interactbtn;

    [SerializeField]
    private string interactText = "Q";


    public Text Myinteractbtn { get { return interactbtn; } set { interactbtn = value; } }
    private void Awake()
    {

        gameObject.SetActive(false);
        interactbtn.text = interactText;
    }

  


    public void Activate() {
       gameObject.SetActive(true);
    }

    public void DeActivate() {
       gameObject.SetActive(false);
    }
}
