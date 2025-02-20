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


    [SerializeField] private Transform _messagePanel;

    private float messageLogTimer;


    [Header("Player effects")]


    [SerializeField] private GameObject _playerDead;

    [SerializeField] private GameObject _playerShield;
    [SerializeField] private GameObject _playerPoison;
    [SerializeField] private GameObject _playerStun;
    [SerializeField] private GameObject _playerBlind;

    [Header("Enemy effects")]

    [SerializeField] private GameObject _enemyShield;
    [SerializeField] private GameObject _enemyPoison;
    [SerializeField] private GameObject _enemyStun;
    [SerializeField] private GameObject _enemyBlind;


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
        battleSim.OnDefenceChanged += UIDefenceAnimation;
        battleSim.OnStatusEffectAfflicted += UIStatusEffectAnimation;


        battleSim.OnEnemyCooldownTimePassed += UpdateEnemyAttackTimer;

        battleSim.OnEnemyStatUpdate += UpdateEnemyHealth; //only HP visible in UI so it sends HP and Max HP only
        battleSim.OnPlayerStatUpdate += UpdatePlayerHealth; //only HP visible in UI so it sends HP and Max HP only

        battleSim.OnEnemyWordPicked += UpdateEnemyWord;


        battleSim.OnAddToMessageLog += WriteToLog;

        wordManager.OnWordchecked += UIActivateTab;

    }
    //var childGameObject = WordPanels[i].GetChild(0).gameObject;
    //GameObject newText = Instantiate(childGameObject, WordPanels[i]);
    //newText.GetComponent<TMP_Text>().text = word.name.ToUpper();
    //                newText.SetActive(true);
    private void WriteToLog(string message)
    {
        var childGameObject = _messagePanel.GetChild(0).gameObject;
        GameObject newMessage = Instantiate(childGameObject, _messagePanel);
        newMessage.GetComponent<TMP_Text>().text = message;
        newMessage.SetActive(true);
        _messagePanel.gameObject.SetActive(true);
        if (_messagePanel.transform.childCount > 6)
        {
            Transform child = _messagePanel.GetChild(1);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    private void RemoveFromLog()
    {
        if (_messagePanel.childCount > 1)
        {
            var child = _messagePanel.GetChild(1);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        else _messagePanel.gameObject.SetActive(false);

    }

    private void LogicWhenPlayerDefeated()
    {
        StartCoroutine(RestartLevelTimer());
    }

    IEnumerator RestartLevelTimer()
    {
        _playerDead.SetActive(true);
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
            case EStatusEffect.Blind:


                //if (creature is Player)
                //{
                //    _playerBlind.SetActive(true);
                //    CameraAnimator.Play("Damage");
                //}
                //if (creature is Enemy)
                //{
                //    _enemyBlind.SetActive(false);
                //    CameraAnimator.Play("Attack");
                //}

                break; //pic

            case EStatusEffect.Sick:

                //if (creature is Player)
                //{
                //    _playerPoison.SetActive(true);
                //    CameraAnimator.Play("Damage");

                //}
                //if (creature is Enemy)
                //{
                //    _enemyPoison.SetActive(false);
                //    CameraAnimator.Play("Attack");

                //}


                break; //ill

            case EStatusEffect.Stun:

                //if (creature is Player)
                //{
                //    _playerStun.SetActive(true);
                //    CameraAnimator.Play("Damage");
                //}
                //if (creature is Enemy)
                //{
                //    _enemyStun.SetActive(false);
                //    CameraAnimator.Play("Attack");
                //}


                break; //eel

            case EStatusEffect.None:


                //if (creature is Player)
                //{
                //    _playerBlind.SetActive(false);
                //    _playerPoison.SetActive(false);
                //    _playerStun.SetActive(false);

                //}
                //if (creature is Enemy)
                //{
                //    _enemyBlind.SetActive(false);
                //    _enemyPoison.SetActive(false);
                //    _enemyStun.SetActive(false);
                //}

                break;

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
            //if (creature is Player)
            //{
            //    CameraAnimator.Play("Damage");
            //}
            //if (creature is Enemy)
            //{
            //    CameraAnimator.Play("Attack");
            //}
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
            //if (creature is Player)
            //{
            //    _playerShield.SetActive(false);
            //    CameraAnimator.Play("Damage");
            //}
            //if (creature is Enemy)
            //{
            //    _enemyShield.SetActive(false);
            //    CameraAnimator.Play("Attack");
            //}
        }

        else if (defenceChanged > 0)
        {
            //if (creature is Player)
            //{
            //    _playerShield.SetActive(true);
            //}
            //if (creature is Enemy)
            //{
            //    _enemyShield.SetActive(true);
            //}
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
            if (LetterSlot != null) //here
            {
                LetterSlot.GetChild(0).GetComponent<TMP_Text>().text = attack.ToCharArray()[index].ToString();
                index++;
            }
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
        messageLogTimer += Time.deltaTime;
        if (messageLogTimer >= 4)
        {
            RemoveFromLog();
            messageLogTimer = 0;
        }
    }
}
