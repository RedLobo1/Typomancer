using System.Collections;
using UnityEngine;

public class FightEndMoveLetter : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float startDelay = 0.5f;

    private BattleSimulator battleSimulator;

    private void Start()
    {
        battleSimulator = FindObjectOfType<BattleSimulator>();
        if (battleSimulator != null)
        {
            battleSimulator.OnEnemyBeaten += MoveLetterToPlayer;
        }
        else
        {
            Debug.LogError("BattleSimulator not found in the scene!");
        }
    }

    private void MoveLetterToPlayer()
    {
        if (objectToMove == null || targetObject == null)
        {
            Debug.LogWarning("ObjectToMove or TargetObject is not assigned!");
            return;
        }

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(startDelay); // Wait before starting movement

        RectTransform rectTransform = objectToMove.GetComponent<RectTransform>();
        Vector3 startPosition = rectTransform.position;
        Vector3 targetPosition = targetObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            rectTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        rectTransform.position = targetPosition; // Ensure it reaches the exact position
        Destroy(objectToMove);

        // Activate all children of targetObject
        foreach (Transform child in targetObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
