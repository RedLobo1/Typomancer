using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualDamage : MonoBehaviour
{
    private bool _coroutineRunning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_coroutineRunning) return;
            StartCoroutine(DamageTaking());
        }

    }
    IEnumerator DamageTaking()
    {

        GetComponent<Animator>().Play("Shake");
        _coroutineRunning = true;
        yield return new WaitForSeconds(0.5f);
        _coroutineRunning = false;

    }
}
