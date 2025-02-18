using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    public string Name;
    public char PrizeLetter; //text displayed when using the word
    public byte Health;
    public float BasettackCooldown; //Seconds until next attack --> could possible decrease over time? 
    public StringByteDictionary wordList;

    public EStatusEffect? StatusEffect = null;
    public Sprite enemySprite;

    public Dictionary<string, float> AvailableWords;

    private void OnEnable()
    {
        // Initialize AvailableWords with the dictionary created from wordList.
        if (wordList != null)
        {
            AvailableWords = wordList.ToDictionary();
        }
    }

}