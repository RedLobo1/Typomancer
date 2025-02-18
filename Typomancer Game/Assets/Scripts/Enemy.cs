using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private SO_Enemy SO_Enemy;
    private char prizeLetter;
    private byte health;
    private float baseAttackCooldown;

    public Dictionary<string, float> AvailableWords { get; private set; }
    public EStatusEffect? StatusEffect { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = SO_Enemy.enemySprite;
        this.name = SO_Enemy.name;
        this.prizeLetter = SO_Enemy.PrizeLetter;
        this.health = SO_Enemy.Health;
        this.baseAttackCooldown = SO_Enemy.BasettackCooldown;
        this.AvailableWords = SO_Enemy.AvailableWords;
        this.StatusEffect = SO_Enemy.StatusEffect; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
