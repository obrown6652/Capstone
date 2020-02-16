using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Spell", menuName = "Spells/Projectile", order = 1)]
public class GenericSpellProjectile : Spell
{
    [SerializeField]
    private SpellType spellType = SpellType.PROJECTILE;
    
      [SerializeField]
      private GameObject spellPrefab;


      public GameObject MySpellPrefab
      {
          get
          {
              return spellPrefab;
          }
          set
          {
              spellPrefab = value;
          }
      }


      [SerializeField]
      private int speed;
      public int MySpeed
      {
          get
          {
              return speed;
          }
          set
          {
              speed = value;
          }
      }

        
      

    [SerializeField]
    string parameter;

    public override  IEnumerator Ability() {


        if (Player.MyInstance.MyTarget != null)
        {
            //subtract the spell magic cost
            Player.MyInstance.MyMana.MyCurrentValue = Player.MyInstance.MyMana.MyCurrentValue - MagicCost;

            //activate the projectile animation
            Player.MyInstance.MyAnimator.SetBool(parameter, true);

            //amount of seconds to wait for the spell to complete
            yield return new WaitForSeconds(MyCastTime);

            //instantiate the spell prefab from the player exit point
            SpellScriptProjectile s = Instantiate(MySpellPrefab, Player.MyInstance.MyexitPoints[Player.MyInstance.MyexitIndex].position, Quaternion.identity).GetComponent<SpellScriptProjectile>();

            //initialized the spell target to the player target and get the spell damage
            s.Initialized(Player.MyInstance.MyTarget, MyDamage, MySpeed);

            //destory the pref
            
            
           

            //deativate the projectile animation
            Player.MyInstance.MyAnimator.SetBool(parameter, false);

        }

        Player.MyInstance.StopAttack();


    }

 
}
