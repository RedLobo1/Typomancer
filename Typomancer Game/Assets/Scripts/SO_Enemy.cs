using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class SO_Enemy : ScriptableObject
{
    

    public string Name;
    public char PrizeLetter; //text displayed when using the word
    public byte Health;
    public float attackCooldown; //Seconds until next attack --> could possible decrease over time? 
    public StringByteDictionary wordList;

    public EStatusEffect? StatusEffect = null;
    public Sprite enemySprite;

    private Dictionary<string, byte> _wordList;
    public Dictionary<string, byte> WordList
    {
        get
        {
            if (_wordList == null)
            {
                _wordList = wordList.ToDictionary();
            }
            return _wordList;
        }
    }
}