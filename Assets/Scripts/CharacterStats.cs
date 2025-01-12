using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public bool showDevStats = false;
    [Header("Player Panel Prefabs")]
    public GameObject playerPanelPrefab;
    public GameObject playerPanelContent;

    [Header("Player checks")]
    public bool isDagda;
    public bool isMio;
    public bool isKing;

    [Header("Main Players")]
    public GameObject DagdaPlayer;
    public GameObject MioPlayer;
    public GameObject KingPlayer;

    [Header("Max Stats")]

    public int[] mainplayerMaxStats = { 0, 0, 0, 0 }; //HP, Mana, XP, Level
    public int[] mainplayerCurrentStats = { 0, 0, 0, 1 }; //HP, Mana, XP, Level
    public int[] mainplayerOptionStats = { 0, 0, 0, 0, 0, 0 }; //Strength, Dexterity, Condition, Intelligence, Wisdom, Charisma

    public int[] dagdaStats = { 350, 250, 100, 1 }; //HP, Mana, XP, Level
    public int[] mioStats = { 1377, 3500, 5345, 12 };
    int[] kingStats = { 850, 500, 101, 25 };

    public int[] dagdaOptionStats = { 7, 5, 2, 11, 6, 9 }; //Strength, Dexterity, Condition, Intelligence, Wisdom, Charisma
    public int[] mioOptionStats = { 350, 780, 240, 888, 945, 725 };
    int[] kingOptionStats = { 10542, 4535, 5345, 5345, 4535, 7875 };

    [Header("Current Stats")]
    int[] currentDagdaStats = { 350, 50, 1, 1 };
    int[] currentMioStats = { 1377, 3500, 3400, 12 };
    int[] currentKingStats = { 825, 241, 56 };

    [Header("Player Classes")]
    string dagdaClass = "Warrior";
    string mioClass = "Nightelf";
    string kingClass = "Paladin";

    [Header("Player Main Stats Text")]
    public Text GUI_HP;
    public Text GUI_Mana;
    public Text MENU_XP_Text;
    public Text GUI_XP_Text;
    public Text GUI_LVL_Text;
    public Text Menu_LVL_Text;
    public Text Menu_LVL_Title;

    [Header("Player Side Stats Text")]
    public Text Strength;
    public Text Dexterity;
    public Text Condition;
    public Text Intelligence;
    public Text Wisdom;
    public Text Charisma;

    [Header("Player Max Stats Text")]
    public Text MaxHP;
    public Text MaxMana;
    public Text MaxXP;

    [Header("Level Mulitplyer")]
    public int LevelMultiplyer;

    [Header("Player Stats Sliders")]
    public Image GUI_HP_Slider;
    public Image GUI_Mana_Slider;
    public Image Menu_Slider_XP;
    public Image GUI_Slider_XP;
    [Header("Main Script Reference")]
    public MainScript mainScript;
    public Image playerRing;
    public Sprite[] allRingSprites;

    public Text devStats;

    private void Awake()
    {
        mainScript = FindObjectOfType<MainScript>();
        //addCharacterToPlayerPanel("Mio");
        MENU_XP_Text = FindAnyObjectByType<PlayerStatsXPMenuText>().gameObject.GetComponent<Text>();
        Menu_LVL_Text = FindAnyObjectByType<PlayerStatsLevelMenuText>().gameObject.GetComponent<Text>();
        Menu_Slider_XP = FindAnyObjectByType<PlayerStatsXPMenuSlider>().gameObject.GetComponent<Image>();
        Menu_LVL_Title = FindAnyObjectByType<PlayerStatsLevelMenuTitle>().gameObject.GetComponent<Text>();
        if (FindAnyObjectByType<TextStatsDev>() != null)
        { devStats = FindAnyObjectByType<TextStatsDev>().gameObject.GetComponentInChildren<Text>(); }
       
        /*GUI_Slider_XP = FindAnyObjectByType<PlayerStatsXPSlider>().gameObject.GetComponent<Image>();
        GUI_LVL_Text = FindAnyObjectByType<PlayerStatsLevelText>().gameObject.GetComponent<Text>();
        */
       
        if (devStats != null && showDevStats == true)
        {
            StartCoroutine(CheckDevStats());
            if (devStats != null)
            { devStats.gameObject.SetActive(true); }
            
        }
        else {
            if (devStats != null)
            {
                devStats.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator CheckDevStats()
    {
        devStats.text =
            "Current player:" + mainScript.player.name
            + "\n" +
            "HP: " + mainplayerCurrentStats[0] + " Mana: " + mainplayerCurrentStats[1] + " XP: " + mainplayerCurrentStats[2] + " Lvl: " + mainplayerCurrentStats[3]
             + "\n" +
            "STR: " + mainplayerOptionStats[0] + " DEX: " + mainplayerOptionStats[1] + " CON: " + mainplayerOptionStats[2] + " INT: " + mainplayerOptionStats[3] + " WHI: " + mainplayerOptionStats[4] + " CAR: " + mainplayerOptionStats[5]
            + "\n" +
            "Dagda:"
            + "\n" +
           "HP: " + currentDagdaStats[0] + " Mana: " + currentDagdaStats[1] + " XP: " + currentDagdaStats[2] + " Lvl: " + currentDagdaStats[3]
            + "\n" +
            "STR: " + dagdaOptionStats[0] + " DEX: " + dagdaOptionStats[1] + " CON: " + dagdaOptionStats[2] + " INT: " + dagdaOptionStats[3] + " WHI: " + dagdaOptionStats[4] + " CAR: " + dagdaOptionStats[5]
            + "\n" +
             "Mio:"
            + "\n" +
            "HP: " + currentMioStats[0] + " Mana: " + currentMioStats[1] + " XP: " + currentMioStats[2] + " Lvl: " + currentMioStats[3]
            + "\n" +
            "STR: " + mioOptionStats[0] + " DEX: " + mioOptionStats[1] + " CON: " + mioOptionStats[2] + " INT: " + mioOptionStats[3] + " WHI: " + mioOptionStats[4] + " CAR: " + mioOptionStats[5];
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(CheckDevStats());
    }

    public void getDamage(int _damage, string _playerName)

    {
        mainplayerCurrentStats[0] -= _damage;
        GUI_HP.text = mainplayerCurrentStats[0].ToString() + "/" + mainplayerMaxStats[0] + " HP";
        GUI_HP_Slider.fillAmount = (1f / mainplayerCurrentStats[0]) * mainplayerMaxStats[0];
        
        switch (_playerName)
        {
            case "Dagda":
                currentDagdaStats = mainplayerCurrentStats;
                dagdaStats = mainplayerMaxStats;
                break;
            case "Mio":
                currentMioStats = mainplayerCurrentStats;
                mioStats = mainplayerMaxStats;
                break;
            default:
                break;
        }

       
    }
    public void addManaToPlayer(int _mana)
    {
        currentDagdaStats[1] += _mana;
      
        if (currentDagdaStats[1] < dagdaStats[1])
        {
            GUI_Mana.text = currentDagdaStats[1].ToString() + "/" + dagdaStats[1].ToString() + " Mana";
            GUI_Mana_Slider.fillAmount = (1f / dagdaStats[1]) * currentDagdaStats[1];
        }
        else if (currentDagdaStats[1] > dagdaStats[1])
        {
            currentDagdaStats[1] = dagdaStats[1];
            GUI_Mana.text = currentDagdaStats[1].ToString() + "/" + dagdaStats[1].ToString() + " Mana";
            GUI_Mana_Slider.fillAmount = 1f;
        }
    }

    public void addXPToPlayer(int _xp)
    {
                mainplayerCurrentStats[2] += _xp;
        if (mainplayerCurrentStats[2] < mainplayerMaxStats[2])
        {
            GUI_XP_Text.text = mainplayerCurrentStats[2].ToString() + "/" + mainplayerMaxStats[2].ToString() + " XP";
            MENU_XP_Text.text = mainplayerCurrentStats[2].ToString() + "/" + mainplayerMaxStats[2].ToString() + " XP";

            Menu_Slider_XP.fillAmount = (1f / mainplayerMaxStats[2]) * mainplayerCurrentStats[2];
            GUI_Slider_XP.fillAmount = (1f / mainplayerMaxStats[2]) * mainplayerCurrentStats[2];
        }

        else
        {
            mainplayerMaxStats[3] += 1 ;

            GUI_LVL_Text.text = "Lvl " + mainplayerMaxStats[3];
            Menu_LVL_Text.text = "Lvl " + mainplayerMaxStats[3];
            Menu_LVL_Title.text = "Lvl " + mainplayerMaxStats[3] + " Rogue";

            mainScript.StartCoroutine(mainScript.levelUpPlayer());

            mainplayerMaxStats[2] = (int)Mathf.Round(mainplayerMaxStats[2] * 5f);
            mainplayerCurrentStats[2] += _xp;
            mainplayerMaxStats[1] += 50;
            addManaToPlayer(15);
            GUI_XP_Text.text = mainplayerCurrentStats[2].ToString() + "/" + mainplayerMaxStats[2].ToString() + " XP";
            MENU_XP_Text.text = mainplayerCurrentStats[2].ToString() + "/" + mainplayerMaxStats[2].ToString() + " XP";
            Menu_Slider_XP.fillAmount = (1f / mainplayerMaxStats[2]) * mainplayerCurrentStats[2];
            GUI_Slider_XP.fillAmount = (1f / mainplayerMaxStats[2]) * mainplayerCurrentStats[2];

        }
    }

    public void UpdateStats(string _statname, int _statvalue)
    {

        switch (_statname)
        {
            case "Strength":
                dagdaOptionStats[0] += _statvalue;
                Strength.text = dagdaOptionStats[0].ToString();
                StartCoroutine(colorUpdatedStats(Strength));
                break;
            case "Dexterity":
                dagdaOptionStats[1] += _statvalue;
                Dexterity.text = dagdaOptionStats[1].ToString();
                StartCoroutine(colorUpdatedStats(Dexterity));
                break;
            case "Condition":
                dagdaOptionStats[2] += _statvalue;
                Condition.text = dagdaOptionStats[2].ToString();
                StartCoroutine(colorUpdatedStats(Condition));
                break;
            case "Intelligence":
                dagdaOptionStats[3] += _statvalue;
                Intelligence.text = dagdaOptionStats[3].ToString();
                StartCoroutine(colorUpdatedStats(Intelligence));
                break;
            case "Wisdom":
                dagdaOptionStats[4] += _statvalue;
                Wisdom.text = dagdaOptionStats[4].ToString();
                StartCoroutine(colorUpdatedStats(Wisdom));
                break;
            case "Charisma":
                dagdaOptionStats[5] += _statvalue;
                Charisma.text = dagdaOptionStats[5].ToString();
                StartCoroutine(colorUpdatedStats(Charisma));
                break;
            default:
                break;
        }

    }

    IEnumerator colorUpdatedStats(Text statText)
    {
        statText.color = Color.green;
        yield return new WaitForSecondsRealtime(6f);
        statText.color = Color.white;

    }
    void setupPlayerForPanel(bool _playerCheck, string _addedPlayerName, GameObject _addedPlayer)
    {

        mainScript.playerMenuPanel.SetActive(true);

        GUI_HP = _addedPlayer.GetComponentInChildren<PlayerStatsHealthText>().gameObject.GetComponent<Text>();
        GUI_HP_Slider = _addedPlayer.GetComponentInChildren<PlayerStatsHealthSlider>().gameObject.GetComponent<Image>();
        GUI_Mana = _addedPlayer.GetComponentInChildren<PlayerStatsManaText>().gameObject.GetComponent<Text>();
        GUI_Mana_Slider = _addedPlayer.GetComponentInChildren<PlayerStatsManaSlider>().gameObject.GetComponent<Image>();
        GUI_XP_Text = _addedPlayer.GetComponentInChildren<PlayerStatsXPText>().gameObject.GetComponent<Text>();
        GUI_Slider_XP = _addedPlayer.GetComponentInChildren<PanelStatsXPSlider>().gameObject.GetComponent<Image>();
        GUI_LVL_Text = _addedPlayer.GetComponentInChildren<PlayerStatsLevelText>().gameObject.GetComponent<Text>();
      
        
        playerRing = _addedPlayer.GetComponentInChildren<PlayerRingType>().gameObject.GetComponent<Image>();
  
    
       

        Strength = FindObjectOfType<StatsStrength>().gameObject.GetComponent<Text>();
        Dexterity = FindObjectOfType<StatsDexerity>().gameObject.GetComponent<Text>();
        Condition = FindObjectOfType<StatsCondition>().gameObject.GetComponent<Text>();
        Intelligence = FindObjectOfType<StatsIntelligence>().gameObject.GetComponent<Text>();
        Wisdom = FindObjectOfType<StatsWisdom>().gameObject.GetComponent<Text>();
        Charisma = FindObjectOfType<StatsCharisma>().gameObject.GetComponent<Text>();



        //_addedPlayer.GetComponentInChildren<PlayerAvaterKing>().gameObject.SetActive(false);

        switch (_addedPlayerName)
        {
            case "Dagda":
                _addedPlayer.GetComponentInChildren<PlayerAvaterMio>().gameObject.SetActive(false);
                _addedPlayer.GetComponentInChildren<PlayerAvaterDagda>().gameObject.SetActive(true);
                _addedPlayer.GetComponentInChildren<PlayerAvaterDagda>().gameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (mainScript.isDagda == true)
                    {
                        mainScript.togglePlayerMenu();
                    }
                    else
                    {
                        mainScript.ChangePlayer(_addedPlayerName);
                    }
                });
                mainScript.playerMenuPanel.SetActive(false);
                mainScript.isShowingMainMenu = false;
                break;

            case "Mio":
                _addedPlayer.GetComponentInChildren<PlayerAvaterDagda>().gameObject.SetActive(false);
                _addedPlayer.GetComponentInChildren<PlayerAvaterMio>().gameObject.SetActive(true);
                _addedPlayer.GetComponentInChildren<PlayerAvaterMio>().gameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if (mainScript.isMio == true)
                    {
                        mainScript.togglePlayerMenu();
                    }
                    else
                    {
                        mainScript.ChangePlayer(_addedPlayerName);
                    }
                });
                mainScript.playerMenuPanel.SetActive(false);
                mainScript.isShowingMainMenu = false;
                break;
            default:
                break;
        }

    }


    public void addCharacterToPlayerPanel(string _characterName, bool setCharcter)
    {

        switch (_characterName)
        {
            case "Dagda":
                DagdaPlayer = Instantiate(playerPanelPrefab, playerPanelContent.transform.position, Quaternion.identity, playerPanelContent.transform);
                Debug.Log("addCharacterToPlayerPanel" + isDagda + _characterName + DagdaPlayer);
               

                setupPlayerForPanel(isDagda, _characterName, DagdaPlayer);
                setCharacterStats(_characterName);
                break;

            case "Mio":
                MioPlayer = Instantiate(playerPanelPrefab, playerPanelContent.transform.position, Quaternion.identity, playerPanelContent.transform);
                setupPlayerForPanel(isMio, _characterName, MioPlayer);
                setCharacterStats(_characterName); 
              
                break;

            case "King":
                KingPlayer = Instantiate(playerPanelPrefab, playerPanelContent.transform.position, Quaternion.identity, playerPanelContent.transform);
                setupPlayerForPanel(isKing, _characterName, KingPlayer);
                setCharacterStats(_characterName);
                break;

            default:
                return;
        }


    }

    void addCharacterStats(GameObject _playerToAddStats, int[] _maxStats, int[] _currentStats, int[] _optionStats)
    {


        //GUI HP
        GUI_HP = _playerToAddStats.GetComponentInChildren<PlayerStatsHealthText>().gameObject.GetComponent<Text>();
        GUI_HP.text = _currentStats[0].ToString() + "/" + _maxStats[0].ToString() + " HP";

        GUI_HP_Slider = _playerToAddStats.GetComponentInChildren<PlayerStatsHealthSlider>().gameObject.GetComponent<Image>();
        GUI_HP_Slider.fillAmount = (1f / _maxStats[0])* _currentStats[0];

        //GUI Mana
        GUI_Mana = _playerToAddStats.GetComponentInChildren<PlayerStatsManaText>().gameObject.GetComponent<Text>();
        GUI_Mana.text = _currentStats[1].ToString() + "/" + _maxStats[1].ToString() + " Mana";

        GUI_Mana_Slider = _playerToAddStats.GetComponentInChildren<PlayerStatsManaSlider>().gameObject.GetComponent<Image>();
        GUI_Mana_Slider.fillAmount = (1f / _maxStats[1]) * _currentStats[1];

        //GUI XP
        GUI_XP_Text = _playerToAddStats.GetComponentInChildren<PlayerStatsXPText>().gameObject.GetComponent<Text>();
        GUI_XP_Text.text = _currentStats[2].ToString() + "/" + _maxStats[2].ToString() + " XP";

        GUI_Slider_XP = _playerToAddStats.GetComponentInChildren<PanelStatsXPSlider>().gameObject.GetComponent<Image>();
        GUI_Slider_XP.fillAmount = (1f / _maxStats[2]) * _currentStats[2];

        GUI_LVL_Text = _playerToAddStats.GetComponentInChildren<PlayerStatsLevelText>().gameObject.GetComponent<Text>();
        GUI_LVL_Text.text = "Lvl " + _maxStats[3];

        playerRing = _playerToAddStats.GetComponentInChildren<PlayerRingType>().gameObject.GetComponent<Image>();

        //Menu XP and Level
        MENU_XP_Text.text = _currentStats[2].ToString() + "/" + _maxStats[2].ToString() + " XP";
        Menu_Slider_XP.fillAmount = (1f / _maxStats[2]) * _currentStats[2];

        Menu_LVL_Text.text = "Lvl " + _maxStats[3];
        Menu_LVL_Title.text = "Lvl " + _maxStats[3] + " Rogue";

        //Menu Options
        Strength.text = _optionStats[0].ToString();
        Dexterity.text = _optionStats[1].ToString();
        Condition.text = _optionStats[2].ToString();
        Intelligence.text = _optionStats[3].ToString();
        Wisdom.text = _optionStats[4].ToString();
        Charisma.text = _optionStats[5].ToString();

  
       
      
    
     


    }
    public void setCharacterStats(string _characterName)
    {
        switch (_characterName)
        {
            case "Dagda":
                mainplayerMaxStats = dagdaStats;
                mainplayerOptionStats = dagdaOptionStats;
                mainplayerCurrentStats = currentDagdaStats;
                addCharacterStats(DagdaPlayer, mainplayerMaxStats, mainplayerCurrentStats, mainplayerOptionStats);
                break;

            case "Mio":
                mainplayerMaxStats = mioStats;
                mainplayerOptionStats = mioOptionStats;
                mainplayerCurrentStats = currentMioStats;
                addCharacterStats(MioPlayer, mainplayerMaxStats, mainplayerCurrentStats, mainplayerOptionStats);
                break;

            case "King":
                mainplayerMaxStats = kingStats;
                mainplayerOptionStats = kingOptionStats;
                mainplayerCurrentStats = currentKingStats;
                
                break;

            default:
                return;
        }

        //addCharacterStats(mainScript.player, mainplayerMaxStats, mainplayerCurrentStats, mainplayerOptionStats);
    }
  
    public void saveCharacterStats(string _passedCharacterName){

        switch (_passedCharacterName)
        {
            case "Dagda":
                dagdaStats = mainplayerMaxStats;
                dagdaOptionStats = mainplayerOptionStats;
                currentDagdaStats = mainplayerCurrentStats;
                break;

            case "Mio":
                mioStats = mainplayerMaxStats;
                mioOptionStats = mainplayerOptionStats;
                currentMioStats = mainplayerCurrentStats;
                break;

            case "King":
                break;

            default:
                return;
        }

    }

    public void saveAllCharacterStats()
    {
        saveCharacterStats("Dagda");
        saveCharacterStats("Mio");
    }

}
