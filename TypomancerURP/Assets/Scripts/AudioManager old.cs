using Cinemachine;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManagerOld : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerName;

    [SerializeField] private Slider EnemyAttackSlider;

    [SerializeField] private Animator CameraAnimator;

    [SerializeField] private Slider EnemyHealthSlider;
    [SerializeField] private Slider PlayerHealthSlider;

    [SerializeField] private Image Tab;
    [SerializeField] private Transform Word;
    // Start is called before the first frame update

    private BattleSimulator battleSim;
    private WordManager wordManager;

    void Start()
    {
        battleSim = FindObjectOfType<BattleSimulator>();
        wordManager = FindObjectOfType<WordManager>();


        //game end events
        //battleSim.OnEnemyBeaten += LogicWhenEnemyDefeated;
        //battleSim.OnGameOver += LogicWhenPlayerDefeated;
        //battleSim.OnPrizeLetterObtained += UpdatePlayerName;


        //stat events
        battleSim.OnHealthChanged += HealSound; //general health update for SE or stat boost Animation
        //battleSim.OnDefenceChanged += UIDefenceAnimation;
        //battleSim.OnStatusEffectAfflicted += UIStatusEffectAnimation;


        wordManager.OnWordchecked += WordFormedSE;

    }

    private void WordFormedSE(Color color, bool IsCorrect)
    {
        throw new NotImplementedException();
    }

    private void LogicWhenPlayerDefeated()
    {
        //play player death sound effect
    }

    private void LogicWhenEnemyDefeated()
    {
        //play enemy death
    }


    private void StatusEffectSound(Creature creature, EStatusEffect effect)
    {
        switch (effect)
        {
            case EStatusEffect.Blind: /*play blind sound*/ break; //pic
            case EStatusEffect.Sick: /*play blind sound*/ break; //ill
            case EStatusEffect.Stun: /*play blind sound*/ break; //eel
        }

    }

    private void HealSound(Creature creature, sbyte healthChanged)
    {
        if (healthChanged < 0)
        {
            //play attack sound
        }
        else if (healthChanged > 0)
        {
            //play heal sound
        }
    }

    private void DefenceSound(Creature creature, byte defenceChanged)
    {
        if (defenceChanged > 0)
        {
            //the defence sound
        }
    }
}
