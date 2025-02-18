using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "ScriptableObjects/Word")]
public class SO_Word : ScriptableObject
{
    public string Word;
    public string Phrase; //text displayed when using the word
    public byte attackModifier;
    public byte DefenceModifier;
    public sbyte HealthModifier;
    public Color TextColor;
    public EStatusEffect StatusEffect;
    public Sprite enemySprite;
}