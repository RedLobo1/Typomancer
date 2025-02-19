using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    protected short health;
    protected byte defence;
    protected EStatusEffect? StatusEffect = null;

    public void ChangeHealth(sbyte modifier)
    {
        if (modifier > 0) Debug.Log($"{modifier} damage afflicted");
        this.health += (short)modifier;
    }

    public void BoostDefence(byte modifier)
    {
        if(modifier > 0) Debug.Log($"{modifier} defence boost");
        defence += modifier;
    }

    public void AfflictStatusEffect(EStatusEffect statusEffect)
    {
        //Debug.Log($"{statusEffect} status applied");
        StatusEffect = statusEffect;
    }

    public short GetHealth()
    {
        return this.health;
    }
    public float GetDefence()
    {
        return defence;
    }
}



