using UnityEngine;
using Health;

[CreateAssetMenu(fileName = "itemPlayerHealthStatsModifierSO", menuName = "Scriptable Objects/ItemSO/itemModifiers/itemPlayerHealthStatsModifierSO")]
public class itemPlayerHealthStatsModifierSO : itemPlayersStatsModifierSO
{
    public override void affectCharacter(GameObject character, float val)
    {
        HealthController health = character.GetComponent<HealthController>();
        if(health != null)
            health.addHealth((int)val);
    }
}
