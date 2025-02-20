using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceEveryLetterIsShownProceed : MonoBehaviour
{
    [SerializeField] private SelectionVisual _selectionVisual;
    [SerializeField] private float maxSelectionDistance = 10f;
    [SerializeField] private GameObject completionIndicator; // Object to show when all are activated

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSelection();
        }
    }

    private void CheckSelection()
    {
        GameObject pointer = _selectionVisual.Pointer;
        GameObject[] selectables = _selectionVisual.Selectables;
        bool allActivated = true;

        foreach (GameObject selectable in selectables)
        {
            if (selectable != null)
            {
                float distance = Vector3.Distance(pointer.transform.position, selectable.transform.position);
                if (distance <= maxSelectionDistance)
                {
                    ActivateChildren(selectable);
                }

                // Check if all children are activated
                if (!AreAllChildrenActive(selectable))
                {
                    allActivated = false;
                }
            }
        }

        // Show completion indicator if all are activated
        if (allActivated)
        {
            completionIndicator.SetActive(true);
        }
    }

    private void ActivateChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private bool AreAllChildrenActive(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}


