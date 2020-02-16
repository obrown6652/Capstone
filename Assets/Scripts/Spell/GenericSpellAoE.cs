using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Spell", menuName = "Spells/AOE", order = 1)]
public class GenericSpellAoE : Spell
{
    [SerializeField]
    private SpellType spellType = SpellType.AOE;

    [SerializeField]
    string parameter;

    [SerializeField]
  




    public override IEnumerator Ability()
    {

        Player.MyInstance.MyAnimator.SetTrigger("aoe");
        yield return new WaitForSeconds(MyCastTime);
   

        //get script on the gamebject and set the target and damage
        SpellScriptAreaOfEffect s = Player.MyInstance.MyAoE.GetComponent<SpellScriptAreaOfEffect>();
        s.Initialized(Player.MyInstance.MyTarget, MyDamage);

        //subtract the spell magic cost
        Player.MyInstance.MyMana.MyCurrentValue = Player.MyInstance.MyMana.MyCurrentValue - MagicCost;
        //activate the ring of death animation
       Player.MyInstance.MyAnimator.SetTrigger(parameter);

        //amount of seconds to wait for the spell to complete
        yield return new WaitForSeconds(MyCastTime);

 

        Player.MyInstance.StopAttack();
    }
    
}
