using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemCounter: MonoBehaviour
{
    [SerializeField]
    private int startCount;

    private  int count;

    [SerializeField]
    private Text text;

    private bool updateText;


    protected void Awake()
    {
        count = startCount;
        text.text = count.ToString();
        updateText = true;
    }

    protected void Update()
    {
        // text.text = count.ToString();
        Format();
    }
    public  void addCount() {
        count++;
        updateText = true;
    }
    public void addCount(int amount)
    {
        count += amount;
        updateText = true;
    }
    public void subtractCount(int amount)
    {
        count -= amount;
        updateText = true;
    }

    public  int MyCount {
        get {
            return count;

        }
    }

    private void Format()
    {
       
          //  string countS = count.ToString();
            string hundredsPlace;
            string thosoundPlace;

        if (count >= 1000 )
        {
            if (updateText)
            {
                hundredsPlace = "," + text.text[text.text.Length - 3] + text.text[text.text.Length - 2] + text.text[text.text.Length - 1];
                text.text = count.ToString().Substring(0, (count.ToString().Length ) - 3) + hundredsPlace;

                if (count>= 1000000 )
                {
                    thosoundPlace = "," + text.text[text.text.Length - 7] + text.text[text.text.Length - 6] + text.text[text.text.Length - 5];
                    text.text = count.ToString().Substring(0, (count.ToString().Length) - 6)+ thosoundPlace + hundredsPlace;
                }
                updateText = false; 
            }
         
        }
        else
        {
            text.text = count.ToString();

        }

    }

}
