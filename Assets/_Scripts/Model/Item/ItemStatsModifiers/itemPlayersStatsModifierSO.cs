using UnityEngine;


public abstract class itemPlayersStatsModifierSO : ScriptableObject
{
    public abstract void affectCharacter(GameObject character, float val);
}
