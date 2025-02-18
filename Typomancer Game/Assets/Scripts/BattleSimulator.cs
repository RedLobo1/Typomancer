using UnityEngine;

public class BattleSimulator : MonoBehaviour
{
    UIWordSelector userInput;

    private Enemy enemy;
    void Start()
    {
        userInput = FindObjectOfType<UIWordSelector>();
        userInput.OnMove += MoveByPlayer;

        enemy = FindObjectOfType<Enemy>();
    }

    private void MoveByPlayer(SO_Word wordData)
    {
        enemy.ChangeHealth((sbyte)((sbyte)-wordData.attackModifier + (sbyte)wordData.HealthModifier));


    }

    // Update is called once per frame
    void Update()
    {

    }
}
