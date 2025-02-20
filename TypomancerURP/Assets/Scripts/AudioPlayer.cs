using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private bool FindAllObjectsOnReload = false;
    [SerializeField] private bool QuitPressingStartSelect = true;

    //list of objects
    private CharacterController character;
    [SerializeField] private GameObject PlayerName;
    [SerializeField] private Slider EnemyAttackSlider;
    [SerializeField] private Animator CameraAnimator;
    [SerializeField] private Slider EnemyHealthSlider;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private Image Tab;
    [SerializeField] private Transform Word;

    private BattleSimulator battleSim;
    private WordManager wordManager;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindAllObjectsOfType();
        PlayAmbiance();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectsOfType();
    }

    private void Update()
    {

        if (QuitPressingStartSelect)
            if (Input.GetButton("JoystickButton6") && Input.GetButton("JoystickButton7"))
                Application.Quit();
    }
    private void FindAllObjectsOfType()
    {
        //character = GameObject.FindObjectOfType<CharacterController>();

        battleSim = FindObjectOfType<BattleSimulator>();
        wordManager = FindObjectOfType<WordManager>();
        battleSim.OnHealthChanged += HealSound; //general health update for SE or stat boost Animation
        wordManager.OnWordchecked += WordFormedSE;
        //foreach (var button in buttons)
        //    button.OnButtonPressed += PlayOnButtonPressed;
    }

    private void PlayOnNextLevel()
    {
        AudioManager.Instance.Play("NextLevel");
    }
    private void PlayAmbiance()
    {
        AudioManager.Instance.Play("Ambiance");
        AudioManager.Instance.Play("Music");
    }
    private void WordFormedSE(Color color, bool IsCorrect)
    {
        AudioManager.Instance.Play("Spell");
    }
    private void HealSound(Creature creature, sbyte healthChanged)
    {
        if (healthChanged < 0)
            AudioManager.Instance.Play("Attack");
        else if (healthChanged > 0)
            AudioManager.Instance.Play("Heal");

    }
    private void DefenceSound(Creature creature, byte defenceChanged)
    {
        if (defenceChanged > 0)
        {
            AudioManager.Instance.Play("Defence");
        }
    }
}
