using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    public string Name;
    public char PrizeLetter; //text displayed when using the word
    public byte Health;
    public float attackCooldown; //Seconds until next attack --> could possible decrease over time? 
    public Dictionary<string, float> wordList; //Lop: 60%, Pod: 40%

    public EStatusEffect? StatusEffect = null;
    public Sprite enemySprite;
}