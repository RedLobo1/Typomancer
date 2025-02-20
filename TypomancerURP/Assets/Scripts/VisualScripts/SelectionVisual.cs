using System;
using UnityEngine;

public class SelectionVisual : MonoBehaviour
{
    [SerializeField] public GameObject[] Selectables;
    [SerializeField] public GameObject Pointer;
    [SerializeField] private GameObject _pointer2;
    private int _currentSelected = 1;

    public event Action OnChangedSelection;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentSelected = (_currentSelected + 1) % Selectables.Length;
            SelectBox();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentSelected = (_currentSelected - 1 + Selectables.Length) % Selectables.Length;
            SelectBox();
        }
    }
    private void SelectBox()
    {
        for (int i = 0; i < Selectables.Length; i++)
        {
            if (Selectables[i] != null)
            {
                if (i == _currentSelected)
                {

                    Pointer.transform.position = Selectables[i].transform.position;
                    Pointer.transform.rotation = Selectables[i].transform.rotation;

                    OnChangedSelection?.Invoke();
                }


            }
        }
    }
}
