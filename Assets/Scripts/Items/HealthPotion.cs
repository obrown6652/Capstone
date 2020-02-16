using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion/Health", order = 1)]
public class HealthPotion : Potions
{
    [SerializeField]
    private int health;

    private PotionType potionType = PotionType.Health;

    public override void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {

            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }
      
    }

    public override string GetDescription()
    {

        return base.GetDescription()+ string.Format("\nRestore {0} health", health);
    }
}
