using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualDamage : MonoBehaviour
{
    private bool _coroutineRunning;
    private BattleSimulator battleSim;

    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        battleSim = FindObjectOfType<BattleSimulator>();

        battleSim.OnHealthChanged += EnemyAnimations;
        battleSim.OnEnemyBeaten += EnemyDeath;
    }

    private void EnemyDeath()
    {
        _animator.Play("Death");
    }

    private void EnemyAnimations(Creature creature, sbyte healthChanged)
    {
        if (healthChanged < 0)
        {
            if (creature is Enemy)
            {
                _animator.Play("Shake");
            }
        }
        else if (healthChanged > 0)
        {
            if (creature is Enemy)
            {
                _animator.Play("Healed");
            }
        }

    }
}
