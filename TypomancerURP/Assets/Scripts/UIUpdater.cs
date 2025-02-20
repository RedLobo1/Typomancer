using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
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
        battleSim.OnEnemyBeaten += LogicWhenEnemyDefeated;
        battleSim.OnGameOver += LogicWhenPlayerDefeated;
        battleSim.OnPrizeLetterObtained += UpdatePlayerName;


        //stat events
        battleSim.OnHealthChanged += UIHealthAnimation; //general health update for SE or stat boost Animation
        //battleSim.OnDefenceChanged += UIDefenceAnimation;
        //battleSim.OnStatusEffectAfflicted += UIStatusEffectAnimation;


        battleSim.OnEnemyCooldownTimePassed += UpdateEnemyAttackTimer;

        battleSim.OnEnemyStatUpdate += UpdateEnemyHealth; //only HP visible in UI so it sends HP and Max HP only
        battleSim.OnPlayerStatUpdate += UpdatePlayerHealth; //only HP visible in UI so it sends HP and Max HP only

        battleSim.OnEnemyWordPicked += UpdateEnemyWord;

        wordManager.OnWordchecked += UIActivateTab;

    }
    private void LogicWhenPlayerDefeated()
    {
        StartCoroutine(RestartLevelTimer());
    }

    IEnumerator RestartLevelTimer()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current level
    }

    private void LogicWhenEnemyDefeated()
    {
        StartCoroutine(NextLevelTimer());
    }

    IEnumerator NextLevelTimer()
    {
        yield return new WaitForSeconds(3f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Prevents loading out-of-range scene
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels!"); // You can handle game completion here
        }
    }

    private void UIActivateTab(Color color, bool canSubmit)
    {
        Tab.color = color;
        Tab.gameObject.SetActive(canSubmit);
    }

    private void UIStatusEffectAnimation(Creature creature, EStatusEffect effect)
    {
        switch (effect)
        {
            case EStatusEffect.Blind: break; //pic
            case EStatusEffect.Sick: break; //ill
            case EStatusEffect.Stun: break; //eel
        }
        if (creature is Player)
        {

        }
        else if (creature is Enemy)
        {

        }
    }

    private void UIHealthAnimation(Creature creature, sbyte healthChanged)
    {
        if (healthChanged < 0)
        {
            if (creature is Player)
            {
                CameraAnimator.Play("Damage");
            }
            if (creature is Enemy)
            {
                CameraAnimator.Play("Attack");
            }
        }
        else if (healthChanged > 0)
        {
            //the stat increased check
        }
        if (creature is Player)
        {

        }
        else if (creature is Enemy)
        {

        }
    }

    private void UIDefenceAnimation(Creature creature, byte defenceChanged)
    {
        if (defenceChanged == 0)
        {
            //the defence got lifted
        }
        else if (defenceChanged > 0)
        {
            //the stat increased
        }
        if (creature is Player)
        {

        }
        else if (creature is Player)
        {

        }
    }

    private void UpdatePlayerHealth(object sender, CreatureUIStatUpdate e)
    {
        float healthPercentage = (float)e.Health / (float)e.MaxHealth;

        PlayerHealthSlider.value = 1 - healthPercentage;
    }

    private void UpdateEnemyHealth(object sender, CreatureUIStatUpdate e)
    {
        float healthPercentage = (float)e.Health / (float)e.MaxHealth;

        EnemyHealthSlider.value = 1 - healthPercentage;
    }

    private void UpdateEnemyAttackTimer(float percentage)
    {
        EnemyAttackSlider.value = percentage;
    }

    private void UpdateEnemyWord(string attack)
    {
        attack.ToCharArray();
        var index = 0;
        foreach (Transform LetterSlot in Word.transform)
        {
            LetterSlot.GetChild(0).GetComponent<TMP_Text>().text = attack.ToCharArray()[index].ToString();
            index++;
        }
    }

    private void UpdatePlayerName(char prizeLetter)
    {
        foreach (Transform letterbox in PlayerName.transform)
        {
            foreach (Transform letter in letterbox)
            {
                TMP_Text LetterTxt = letter.GetComponent<TMP_Text>();
                if (LetterTxt.text.ToLower() == prizeLetter.ToString().ToLower())
                {
                    letter.gameObject.SetActive(true);
                }
            }
        }
    }
    public void UpdatePlayerName(LettersOwned lettersOwned)
    {
        foreach (Transform letterbox in PlayerName.transform)
        {
            foreach (Transform letter in letterbox)
            {
                TMP_Text LetterTxt = letter.GetComponent<TMP_Text>();
                if (lettersOwned.letters.Contains(LetterTxt.text.ToCharArray()[0]))
                {
                    letter.gameObject.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
