using UnityEngine;

public class SelectionVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] _selectables;
    [SerializeField] private GameObject _pointer;
    private int _currentSelected = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentSelected = (_currentSelected + 1) % _selectables.Length;
            SelectBox();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentSelected = (_currentSelected - 1 + _selectables.Length) % _selectables.Length;
            SelectBox();
        }
    }
    private void SelectBox()
    {
        for (int i = 0; i < _selectables.Length; i++)
        {
            if (_selectables[i] != null)
            {
                if (i == _currentSelected)
                {

                    _pointer.transform.position = _selectables[i].transform.position;
                }


            }
        }
    }
}
