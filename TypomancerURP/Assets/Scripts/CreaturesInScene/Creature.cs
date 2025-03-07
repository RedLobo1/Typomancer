using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    protected short maxHealth;
    protected short health;
    protected byte defence;
    protected EStatusEffect? StatusEffect = null;
    public float statusTimer;
    public byte damagecounter;

    private void Start()
    {
        health = maxHealth;
    }
    public void ChangeHealth(sbyte modifier)
    {
        if (modifier < 0)
            Debug.Log($"{modifier} damage afflicted");
        if (modifier > 0)
            Debug.Log($"{modifier} HP healed");
        this.health = (short)Mathf.Min(this.health + modifier, this.maxHealth);
    }

    public void ChangeDefence(byte modifier)
    {
        if (modifier > 0) Debug.Log($"{modifier} defence boost");
        defence = modifier; //defence is a one-time shield
    }

    public void SetStatusEffect(EStatusEffect statusEffect)
    {
        //Debug.Log($"{statusEffect} status applied");
        StatusEffect = statusEffect;
    }

    public short GetHealth()
    {
        return this.health;
    }
    public short GetMaxHealth()
    {
        return this.maxHealth;
    }
    public float GetDefence()
    {
        return defence;
    }
    public EStatusEffect? GetStatusEffect()
    {
        return StatusEffect;
    }
}



