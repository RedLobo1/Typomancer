using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Creature
{
    public SO_Enemy SO_Enemy;


    private char prizeLetter;
    private float baseAttackCooldown;
    private Sprite sprite;

    private Dictionary<string, float> AvailableWords;

    // Start is called before the first frame update
    void Awake()
    {
        //this.GetComponent<SpriteRenderer>().sprite = SO_Enemy.enemySprite;
        this.sprite = SO_Enemy.enemySprite;
        this.name = SO_Enemy.name;
        this.prizeLetter = SO_Enemy.PrizeLetter;
        this.maxHealth = SO_Enemy.MaxHealth;
        this.health = SO_Enemy.MaxHealth;
        this.baseAttackCooldown = SO_Enemy.BasettackCooldown;
        this.AvailableWords = SO_Enemy.AvailableWords;
        this.StatusEffect = SO_Enemy.StatusEffect;

        this.GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetBaseAttackCooldown()
    {
        return baseAttackCooldown;
    }
    public string PickWord()
    {
        //var OddsTotal = SO_Enemy.AvailableWords.Values.Sum();
        float OddsTotal = this.AvailableWords.Values.Sum();

        Dictionary<string, float> normalizedData = SO_Enemy.AvailableWords.ToDictionary(kvp => kvp.Key, kvp => kvp.Value / OddsTotal);
        foreach (var word in SO_Enemy.AvailableWords)
        {
            if (word.Value > Random.Range(0, OddsTotal + 1))
            {
                return word.Key; // Return the key (string) associated with the selected probability
            }
            OddsTotal -= word.Value; // Reduce the random value by the probability of the current item
        }

        return ""; // Fallback if no valid item is chosen (shouldn't normally happen if the dictionary is valid)
    }

    public char GetPrizeLetter()
    {
        return prizeLetter;
    }
}
