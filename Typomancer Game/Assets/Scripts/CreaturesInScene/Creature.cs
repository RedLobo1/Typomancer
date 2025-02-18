using UnityEngine;

public class Creature : MonoBehaviour
{
    protected short health;
    protected byte defence;
    protected EStatusEffect? StatusEffect = null;

    public void ChangeHealth(sbyte modifier)
    {
        this.health += (short)modifier;
    }

    public void BoostDefence(byte modifier)
    {
        defence += modifier;
    }

    public void AfflictStatusEffect(EStatusEffect statusEffect)
    {
        StatusEffect = statusEffect;
    }
}



