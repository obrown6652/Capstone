using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//enum for declaring the quality of the item
public enum Quality { Normal, Superior, Epic, Legenndary }

public static class QualityColor 
{
    private static Dictionary<Quality, string> color = new Dictionary<Quality, string>()
    {
        {Quality.Normal, "#fbfbff"},
        {Quality.Superior, "#2a64a3"},
        {Quality.Epic, "#a32a8e"},
        {Quality.Legenndary, "#fbf236"}
    };

    public static Dictionary<Quality, string> MyColor {
        get
        {
            return color;
        }
    
    }
}
