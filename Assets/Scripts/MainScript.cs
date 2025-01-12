using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MainScript : MonoBehaviour
{
    public Slider storyLineSlider;
    [Header("Mission Checks")]
    public bool Mission1 = false;
    public bool Mission1MiniBoss = false;
    [Header("Main Settings")]
    public bool quickStart = true;
    public bool teleportForFight = true;
    public GameObject[] enemyTargetObject;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public GameObject virtualCam;
    public CinemachineVirtualCamera vcam;
    CinemachineFreeLook follower;
    float CamSize;
    public float zoomOutSpeed = 0.002f;

    [Header("Rîngs")]
    public bool ringWater;
    public bool ringFire;
    public bool ringLight;
    public bool ringVoid;
    public bool ringGround;
    public bool ringAir;

    [Header("Main Player Settings")]
    public GameObject player;
    CharacterStats charStats;
    public PanelInventory charInventory;
    public Vector2 positionBeforeFight;
    public Vector2 positionCameraBeforeInside;
    public Vector2 positionBeforeInside;

    [Header("Enemy Target")]

    public GameObject prefab_Item;
    public GameObject prefab_Wolf;
    public GameObject prefab_Gnoll;
    public GameObject prefab_Goblin;
    public GameObject prefab_Troll;
    public GameObject prefab_Werewolf;
    public GameObject fx_impact;
    public GameObject fx_spawning;
    public GameObject fx_levelup;

    [Header("Mouse Settings")]
    public float clickOffsetX = 0.01f;
    public float clickOffsetY = 0.01f;
    public Vector2 walkOffset = new Vector2(0, 0.01f);
    public Vector3 mousePosWorld;
    public Vector3 mousePosWorld2D;
    public Vector3 mousePos;
    public Vector2 targetPos;
    public float speed = 0.1f;
    public float followingRadius = 20f;

    [Header("Character check")]
    public bool isMio = true;
    public bool isDagda = false;
    public bool isKing = false;
    public string characterName = "Mio";

    [Header("Main Player Objects")]
    public GameObject playerMio;
    public GameObject playerDagda;
    public GameObject playerKing;
    public GameObject playerAshadi;
    public GameObject playerYuiDienerin;

    [Header("Avatar Images")]
    public GameObject mainAvaterMio;
    public GameObject mainAvaterDagda;
    public GameObject mainAvaterKing;

    [Header("Condition checks")]
    public bool isMoving;
    public bool isAttacking;
    public bool isMovingToFight;
    public bool isInFight;
    public bool isShowingDialog;
    public bool isZooming;
    public bool isFlyingAway;
    public bool isLevelingUp;
    public bool isIntroScene = false;
    public bool gameHasStarted = false;
    public bool isShowingPlayerMenu = false;
    
    [Header("Animator Settings")]
    public Animator playerAnimator;
    Vector3 attackFXOffset = new Vector3(3f, 0, 0);

    [Header("Condition checks")]
    public int currentSpriteIndex = 0;

    [Header("Items")]
    public GameObject redStone;
    public int itemNumber = 0;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject dialogPanel;
    public GameObject playerPanel;
    public GameObject fightPanel;
    public GameObject fightPanelAttacks;
    public GameObject playerMenuPanel;
      public GameObject ringsWorldPanel;

    [Header("World Panels")]
    public GameObject worldPanelDamage;
    public GameObject worldPanelXP;
    public GameObject worldPanelHealth;
    public Vector3 worldPanelOffset = new Vector3(0f, -5f, 0f);

    [Header("Condition checks")]
    public string dialogMessage;


    [Header("World Positions")]
    public Transform flyPos1;

    [Header("Text Objects")]
    public Text ItemNumberTxt;
    public Text DialogText;

    [Header("All Backgrounds")]
    public GameObject[] introBackgrounds;
    public GameObject[] testBackgrounds;
    public GameObject[] prologBackgrounds;
    public GameObject[] battleBackgrounds;
    public GameObject[] startBackgrounds;
    public GameObject[] castleBackgrounds;
    public GameObject[] m1sceneBackgrounds;

    [Header("Prolog Text")]
    public int prologTextCounter = 0;
    public int mioTextCounter = 0;
    public int kingTextCounter = 0;

    [Header("World Story Objects")]
    public GameObject door;
    public GameObject ruins;

    [Header("Speaking with Character Checks")]
    public bool isSpeakingWithMio = false;
    public bool isSpeakingWithKing = false;

    [Header("Following Character Checks")]
    public bool MioIsNearDagda = false;
    public bool MioFolowingPlayer = false;
    public bool AshadiFolowingPlayer = false;
    public bool AshadiFolowingKing = false;
    public bool DagdaFolowingPlayer = false;

    public bool hasPlayedIntroScene = false;
    public AudioControllCenter audioCenter;
    public int mission1EnemyCounter = 3;

    public Transform[] allSpawnPointsM1;
    public Transform[] allSpawnPointsM1MiniBoss;
    public GameObject MiniBossSpawnPointHolder;
    public Transform[] allEnemySpawnPoints;
    public Transform[] allEnemyTestSpawnPoints;
    public Transform[] allItemSpawnPoints;
    public int ringSpriteIndex;

    public Sprite[] allAvatarSprites;
    public Image avaterSpriteHolder;
    public string[] chosenEnemyAttacks;
    public int chosenEnemyAttackCounter;
    public bool hasAllItems = false;
    public bool isShowingMainMenu = false;
    public bool activated_TestLevel = false;
    public int bookNumber = 1;
    public BoxCollider2D colliderTestLevel;
    public Vector3 dagdaSpawnPos;
    public GameObject currentEnemyTarget;
    public int testLevelEnemyCounter = 0;
    public int testLevelWaveCounter = 0;
    public bool activated_TestWave = false;
    public GameObject testBook;
    MainDialog mainDia;
    GameObject _resumeButton;
    MainStory mainStory;
    MainFightSystem mainFightSystem;
    AllItems allItems;
    public GameObject generated_Item;
    GameObject[] allMapEnemies = new GameObject[25];

    public GameObject rightFoot;
    public GameObject rightLeg;
    public GameObject rightUpperLeg;

    public GameObject rightRunningFoot;
    public GameObject rightRunningLeg;
    public GameObject rightRunningUpperLeg;

    public GameObject rightFootMio;
    public GameObject rightLegMio;
    public GameObject rightUpperLegMio;

    public GameObject rightRunningFootMio;
    public GameObject rightRunningLegMio;
    public GameObject rightRunningUpperLegMio;

    MainUpdateLoop mainUpdateLoop;
    /******************************************************************/
    /*____________________________DEV CHEATS__________________________*/
    /******************************************************************/

    public void addAllItemsToInventory()
    {
        if (hasAllItems == false)
        { charInventory.addAllItemsToInventory(); hasAllItems = true; }
        else
        { charInventory.removeAllItemsFromInventory(); hasAllItems = false; }
   

    }
    public void togglePlayerMenu()
    {
        if (isShowingPlayerMenu == false)
        {

            playerMenuPanel.SetActive(true);
            isShowingPlayerMenu = true;
        }
        else
        {
            playerMenuPanel.SetActive(false);
            isShowingPlayerMenu = false;
        }
    }
    public void toggleMainMenu()
    {
        if (isShowingMainMenu == false)
        {
          
            mainMenuPanel.GetComponent<Image>().color = new Color(85f, 85f, 130f, 255f);
            foreach (Button _menuButton in mainMenuPanel.GetComponentsInChildren<Button>())
            {
                _menuButton.gameObject.SetActive(false);
            }
          
            _resumeButton.SetActive(true);

            mainMenuPanel.SetActive(true); isShowingMainMenu = true; }
        else
        {  mainMenuPanel.SetActive(false); isShowingMainMenu = false; }
    }
    public void toggleShowTestLevel()
    {
        if (activated_TestLevel == false)
        { activated_TestLevel = true; }
        else
        { activated_TestLevel = false; }
    }

    /******************************************************************/
    /*__________________________START FUNCTION________________________*/
    /******************************************************************/

  
    public void quickBoot()
    {
        mainMenuPanel.SetActive(false);
        quickStart = true;
        StartGame();
    
    }
    void Start()
    {
        testBook.SetActive(false);
        mainStory = FindObjectOfType<MainStory>();
        allItems = FindObjectOfType<AllItems>();
        //Deactivate resume Button
        _resumeButton = mainMenuPanel.GetComponentInChildren<ResumeBTN>().gameObject;
        _resumeButton.SetActive(false);
        mainMenuPanel.SetActive(false);

        //Set Dialog
        mainDia = FindObjectOfType<MainDialog>();

        //Set Audio Center
        audioCenter = FindObjectOfType<AudioControllCenter>();

        //Set Char Stats
        charStats = FindObjectOfType<CharacterStats>();

        //Set Panel Inventory
        charInventory = FindObjectOfType<PanelInventory>();

        //Set Player Animator
        playerAnimator = player.GetComponent<Animator>();

        //Set virutual Cam
        vcam = virtualCam.GetComponent<CinemachineVirtualCamera>();

        mainUpdateLoop = FindObjectOfType<MainUpdateLoop>();

        //Set world damge/xp/health panels
        worldPanelDamage = FindObjectOfType<PanelDamage>().gameObject;
        worldPanelDamage.SetActive(false);
        worldPanelXP = FindObjectOfType<PanelXP>().gameObject;
        worldPanelXP.SetActive(false);
        worldPanelHealth = FindObjectOfType<PanelHealth>().gameObject;
        worldPanelHealth.SetActive(false);

        //Set camsize
        CamSize = vcam.m_Lens.OrthographicSize;

        //Set cam damping
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;

        //Set spawn position
        dagdaSpawnPos = player.transform.position;

        //Check quickstart
        if (quickStart)
        {
            StartGame();
        }
        else
        {
            StartCoroutine(IntroScene());
        }

        //Spawn Map Enemies
        for (int i = 0; i < allEnemySpawnPoints.Length; i++)
        {
            GameObject enemyM1Scene = Instantiate(prefab_Wolf, allEnemySpawnPoints[i].transform.position, Quaternion.identity);
            int enemyMaxHealth = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyMaxHealth;
            int enemyCurrentHealth = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyCurrentHealth;
            int enemyAttackStrength = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyAttackStrength;
            allMapEnemies[i] = enemyM1Scene;
            enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyMaxHealth = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyMaxHealth + (i * 1);
            enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyCurrentHealth = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyCurrentHealth + (i * 1);
            enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyAttackStrength = enemyM1Scene.GetComponentInChildren<EnemyStats>().enemyAttackStrength + (i * 1);

            if (i == 0)
            { enemyM1Scene.GetComponent<SpriteRenderer>().flipX = true; }
        }

    }

    /******************************************************************/
    /*____________________LOADING & MAIN GAME MENU____________________*/
    /******************************************************************/


   

    IEnumerator IntroScene()
    {
      
        //Turn off panels
        dialogPanel.SetActive(false);
        playerPanel.SetActive(false);
        playerMenuPanel.SetActive(false);

        //Set cam size to 10f
        vcam.m_Lens.OrthographicSize = 10f;

        //Set start logo background
        /*
       vcam.Follow = introBackgrounds[0].transform;

     yield return new WaitForSecondsRealtime(6f);
       */
        //Set main menu background
        vcam.Follow = introBackgrounds[1].transform;
        yield return new WaitForSecondsRealtime(0.1f);
        //Turn on main menu panel
        mainMenuPanel.SetActive(true);

  }

  /******************************************************************/
        /*________________________CHOOSE RINGS MENU______________________*/
        /******************************************************************/
        public void ChooseCharacter()
    {
        //Play button sound
        audioCenter.setSound(0);

        //Set choose rings background
        vcam.Follow = introBackgrounds[2].transform;

        //Turn off main menu panel
        mainMenuPanel.SetActive(false);

        //Show choose ring dialog
        StartCoroutine(DisplayText(DialogText, "The creators voice: 'Choose wisely...''"));
        checkDialogAvatar("Witch");
    }

    /******************************************************************/
    /*_______________________RING CHOOSE SCENE________________________*/
    /******************************************************************/


    IEnumerator RingsScene(string _ringType)
    {
        ringsWorldPanel.SetActive(false);
        SkipText(dialogPanel);

        switch (_ringType)
        {
            case "Water":
                StartCoroutine(DisplayText(DialogText, "You have chosen the water element. Stay with the flow of things."));
                ringWater = true;
                ringSpriteIndex = 0;
                break;
            case "Fire":
                StartCoroutine(DisplayText(DialogText, "Fire is youre element of choise. Keep that fire burning hot."));
                ringFire = true;
                ringSpriteIndex = 1;
                break;
            case "Light":
                StartCoroutine(DisplayText(DialogText, "Shine bright and open your eyes to be blinded by the light.'"));
                ringLight = true;
                ringSpriteIndex = 2;
                break;
            case "Void":
                StartCoroutine(DisplayText(DialogText, "Chaos, entropy and the void is your element. Be wise and navigate."));
                ringVoid = true;
                ringSpriteIndex = 3;
                break;
            case "Ground":
                StartCoroutine(DisplayText(DialogText, "Staying grounded is a good choice. Stable souls go far in this world."));
                ringGround = true;
                ringSpriteIndex = 4;
                break;
            case "Air":
                StartCoroutine(DisplayText(DialogText, "Be the uplifting that becomes the way of life in the clouds. You are everywhere and nowhere to be seen."));
                ringAir = true;
                ringSpriteIndex = 5;
                break;
            default:
                break;
        }
        audioCenter.setSound(0);
        audioCenter.setTrack(0);
        yield return new WaitForSecondsRealtime(5f);
        audioCenter.setSound(8);
        StartGame();
    }


    /******************************************************************/
    /*_______________________INTRO STORY SCENE________________________*/
    /******************************************************************/
    IEnumerator StartGameScene()
    {
        charStats.addCharacterToPlayerPanel("Dagda", true);
        if (!quickStart)
        {
            isFlyingAway = true;

            SkipText(dialogPanel);

            isIntroScene= true;

            vcam.Follow = prologBackgrounds[prologTextCounter].gameObject.transform;
            StartCoroutine(DisplayText(DialogText, mainDia.allPrologDialog[prologTextCounter]));
        }
        else
        { 
          
            beginGame("Dagda");
            yield return new WaitForSecondsRealtime(12f);
            setCamDampening(3f);
        }
    }

    IEnumerator StartGameSceneAfterIntro()
    {
            yield return new WaitForSecondsRealtime(2f);

            beginGame("Dagda");

            yield return new WaitForSecondsRealtime(4f);

            setCamDampening(3f);
     }

    /******************************************************************/
    /*________________________LAUNCH IN GAME__________________________*/
    /******************************************************************/
    public void StartGame()
    {

        SkipText(dialogPanel);
        StartCoroutine(StartGameScene());
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 3f, -10f);
    }
    public void StartGameWithRing(string _passedRingType)
    {
        StartCoroutine(RingsScene(_passedRingType));

    }

    public void beginGame(string _playerName)
    {
        SkipText(dialogPanel);
        if (activated_TestLevel == false)
        {

            StartCoroutine(StartZoomOut());
             vcam.Follow = startBackgrounds[0].transform;
          StartCoroutine(DisplayText(DialogText, characterName + " was awake and opened the door. He said his goodbyes and left the security of his home to travel vast lands on a journey to meet King Rado."));
        }
        else
        {
            setCamDistanceInsideWorld();
            player.transform.position = testBackgrounds[0].transform.position;
            vcam.Follow = testBackgrounds[0].transform;
            testBook.SetActive(true);

        }
        isFlyingAway = false;
      
        ChangePlayer(_playerName);
      
        gameHasStarted = true;
        audioCenter.setSound(8);
        StartCoroutine(SpawnItems());

    }

    IEnumerator StartZoomOut()
    {
        vcam.m_Lens.OrthographicSize = 2.4f;
        yield return new WaitForSecondsRealtime(3f);
        vcam.m_Lens.OrthographicSize = 4.4f;
        yield return new WaitForSecondsRealtime(2f);
        vcam.m_Lens.OrthographicSize = 6.4f;
        yield return new WaitForSecondsRealtime(1f);
        vcam.m_Lens.OrthographicSize = 8.4f;
        yield return new WaitForSecondsRealtime(1f);
        setCamDistanceOutsideWorld();
    }
    IEnumerator SpawnItems()
    {
        //Spawn Map Enemies
        for (int i = 0; i < allItemSpawnPoints.Length; i++)
        {
            GameObject itemM1Scene = Instantiate(prefab_Item, allItemSpawnPoints[i].transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(6f);
        }
    }
    public void SpawnSingleItem(Vector3 _itemSpawnPoint)
    {
        generated_Item = Instantiate(prefab_Item, _itemSpawnPoint, Quaternion.identity);
        int randomItemIndex = Random.Range(0, 28);
        generated_Item.GetComponentInChildren<SpriteRenderer>().sprite = allItems.allItemSprites[randomItemIndex];
        if (activated_TestLevel == false)
        {
            generated_Item.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        }
        else
        {
            generated_Item.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    /******************************************************************/
    /*XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*/
    /******************************************************************/
    /******************************************************************/
    //HELPER FUNTCIONS
    /******************************************************************/
    /******************************************************************/
    /*XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*/
    /******************************************************************/


    /******************************************************************/
    /*_________________________HELPER ROUTINES_______________________*/
    /******************************************************************/


    public void setCamDistanceOutsideWorld()
    {
        speed = 0.2f;
        vcam.m_Lens.OrthographicSize = 9.4f; //10.5f
    }
    public void setCamDistanceInsideWorld()
    {
        vcam.m_Lens.OrthographicSize = 30f;
        speed = 0.75f;
    }
    public void setCamDistanceNear()
    {
        vcam.m_Lens.OrthographicSize = 8.5f;
    }
    public void setCamDampening(float _camDampeningValue)
    {
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = _camDampeningValue;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = _camDampeningValue;
    }
    public IEnumerator RemoveXPPanel()
    {
        yield return new WaitForSecondsRealtime(1.3f);
        worldPanelXP.SetActive(false);

    }
    public IEnumerator resetCamDamping(int _time)
    {
        yield return new WaitForSecondsRealtime(_time);
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;

    }

    public IEnumerator levelUpPlayer()
    {
       
       

        if (activated_TestLevel)
        { fx_levelup.transform.localScale = new Vector3(15f, 15f, 15f); }
        else
        { fx_levelup.transform.localScale = new Vector3(5f, 5f, 5f); }


        isLevelingUp = true;
        fx_levelup.GetComponent<Animator>().SetBool("isSpawning", true);
        StartCoroutine(DisplayText(DialogText, "Leveled up!"));
        yield return new WaitForSecondsRealtime(2f);
        fx_levelup.GetComponent<Animator>().SetBool("isSpawning", false);
        isLevelingUp = false;

    }



    /******************************************************************/
    /*_____________________SWITCH PLAYER CHARACTER____________________*/
    /******************************************************************/
    public void ChangePlayer(string _name)

    {
      
     
        isMoving = false;
        playerAnimator.SetBool("isWalking", false);

        //StopAllCoroutines();
        switch (_name)
        {
            case "Dagda":
                if (DagdaFolowingPlayer == true)
                { DagdaFolowingPlayer = false; MioFolowingPlayer = true; }

                isDagda = true;
                isMio = false;
                isKing = false;

                characterName = "Dagda";

                player = playerDagda;

                charStats.setCharacterStats("Dagda");

                vcam.Follow = playerDagda.transform;

                playerAnimator = player.GetComponent<Animator>();

                mainAvaterMio.SetActive(false);
                mainAvaterKing.SetActive(false);
                mainAvaterDagda.SetActive(true);
              
              
                break;
            case "Mio":

                if (MioFolowingPlayer == true)
                { MioFolowingPlayer = false; DagdaFolowingPlayer = true; }

                isMio = true;
                isDagda = false;
                isKing = false;
               
                player = playerMio;

                characterName = "Mio";

                charStats.setCharacterStats("Mio");

                vcam.Follow = playerMio.transform;
                playerAnimator = playerMio.GetComponent<Animator>();
                mainAvaterMio.SetActive(true);
                mainAvaterKing.SetActive(false);
                mainAvaterDagda.SetActive(false);
               
                //charInventory.addToInventory("Redstone");
               
                break;
            case "King":
                isKing = true;
                isDagda = false;
                isMio = false;
                player = playerKing;
                characterName = "King";
                vcam.Follow = playerKing.transform;
                playerAnimator = player.GetComponent<Animator>();
                mainAvaterMio.SetActive(false);
                mainAvaterKing.SetActive(true);
                mainAvaterDagda.SetActive(false);
                charStats.setCharacterStats("King");
                break;

            default:
                return;
        }


    }

    /******************************************************************/
    /*____________________DISPLAY TEXT TO TEXTBOXES___________________*/
    /******************************************************************/

    public IEnumerator DisplayText(Text _textBox, string _textMessage)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        isShowingDialog = true;
        playerPanel.SetActive(false);
        dialogMessage = _textMessage;
        _textBox.transform.parent.gameObject.SetActive(true);
        _textBox.text = "";

        foreach (char c in dialogMessage)
        {
            if (isShowingDialog)
            {
                _textBox.text += c;
                yield return new WaitForSecondsRealtime(0.03f);
            }
            else
            {
                _textBox.text = "";
            }
        }

    }

    public void checkDialogAvatar(string _avaterCharacterName)
    {
        switch (_avaterCharacterName)
        {
            case "Witch":
                avaterSpriteHolder.sprite = allAvatarSprites[0];
                break;
            case "Dagda":
                avaterSpriteHolder.sprite = allAvatarSprites[1];
                break;
            case "Mio":
                avaterSpriteHolder.sprite = allAvatarSprites[2];
                break;
            case "King":
                avaterSpriteHolder.sprite = allAvatarSprites[3];
                break;
            case "Ashadi":
                avaterSpriteHolder.sprite = allAvatarSprites[4];
                break;
            case "Dienerin":
                avaterSpriteHolder.sprite = allAvatarSprites[5];
                break;
            case "Enemy":
                avaterSpriteHolder.sprite = allAvatarSprites[6];
                break;
            default:
                break;
        }
    }
    /******************************************************************/
    /*____________________SKIP TEXT FROM TEXTBOXES___________________*/
    /******************************************************************/

    public void SkipText(GameObject _textBox)
    {
        audioCenter.setSound(0);
        dialogMessage = "";
        isShowingDialog = false;
        _textBox.GetComponentInChildren<Text>().text = "";
        _textBox.SetActive(false);
        if (gameHasStarted)
        { playerPanel.SetActive(true); }

        
        if(prologTextCounter == 0 && isIntroScene == true)
        {
            prologTextCounter++;
            vcam.Follow = prologBackgrounds[prologTextCounter].gameObject.transform;
            StartCoroutine(DisplayText(DialogText, mainDia.allPrologDialog[prologTextCounter]));
         
        }
        else if (prologTextCounter == 1 && isIntroScene == true)
        {
            isIntroScene = false;
            StartCoroutine(StartGameSceneAfterIntro());
        }

        if (isSpeakingWithMio && mioTextCounter < 1)
        {
            mioTextCounter++;
            StartCoroutine(DisplayText(DialogText, mainDia.allMioIntroDialog[mioTextCounter]));



        }
        else if (mioTextCounter >= 1 && isSpeakingWithKing == false && hasPlayedIntroScene == false)
        {
            isSpeakingWithMio = false;
            mainStory.StartCoroutine(mainStory.MioScene());
            hasPlayedIntroScene = true;
        }

        if (isSpeakingWithKing && kingTextCounter < 1)
        {
            kingTextCounter++;
            StartCoroutine(DisplayText(DialogText, mainDia.allKingIntroDialog[kingTextCounter]));


        }
        else if (kingTextCounter >= 2)
        {
            kingTextCounter++;
            isSpeakingWithKing = false;
            //StartCoroutine(MioScene());
        }



    }
    public void M1MiniBossScene()
    {
        chosenEnemyAttacks = new string[1];
        enemyTargetObject = new GameObject[1];
        allSpawnPointsM1MiniBoss = MiniBossSpawnPointHolder.GetComponentsInChildren<Transform>();

        for (int i = 0; i < 5; i++)
        {
            if (i < 5)
            {
                GameObject enemyM1Scene = Instantiate(prefab_Goblin, allSpawnPointsM1MiniBoss[i + 1].transform.position, Quaternion.identity);
                enemyM1Scene.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (i > 3)
            {
                GameObject enemyM1Scene = Instantiate(prefab_Troll, allSpawnPointsM1MiniBoss[i + 1].transform.position, Quaternion.identity);
                enemyM1Scene.GetComponent<SpriteRenderer>().flipX = true;

            }
        }
        Mission1MiniBoss = true;
    }


    /******************************************************************/
    /*______________________FOLLOW PLAYER CHARACTER___________________*/
    /******************************************************************/

    public void FolowPlayer(string _name)

    {
        isMoving = false;
        playerAnimator.SetBool("isWalking", false);
        //StopAllCoroutines();
        switch (_name)
        {
            case "Dagda":
                isDagda = true;
                isMio = false;
                isKing = false;
                characterName = "Dagda";
                player = playerDagda;
                vcam.Follow = playerDagda.transform;
                playerAnimator = player.GetComponent<Animator>();
                mainAvaterMio.SetActive(false);
                mainAvaterKing.SetActive(false);
                mainAvaterDagda.SetActive(true);
                //charStats.setCharacterStats("Dagda");
                break;
            case "Mio":
                isMio = true;
                isDagda = false;
                isKing = false;
                player = playerMio;
                characterName = "Mio";
                vcam.Follow = playerMio.transform;
                playerAnimator = player.GetComponent<Animator>();
                mainAvaterMio.SetActive(true);
                mainAvaterKing.SetActive(false);
                mainAvaterDagda.SetActive(false);
                //charStats.setCharacterStats("Mio");
                break;
            case "King":
                isKing = true;
                isDagda = false;
                isMio = false;
                player = playerKing;
                characterName = "King";
                vcam.Follow = playerKing.transform;
                playerAnimator = player.GetComponent<Animator>();
                mainAvaterMio.SetActive(false);
                mainAvaterKing.SetActive(true);
                mainAvaterDagda.SetActive(false);
                //charStats.setCharacterStats("King");
                break;

            default:
                return;
        }


    }


    public void checkSpritesForSpawnTest(GameObject _enemyTestScene)
    {
        if (_enemyTestScene.transform.position.x > player.transform.position.x)
        { _enemyTestScene.GetComponent<SpriteRenderer>().flipX = false; }
        else
        {
            _enemyTestScene.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
   

    /******************************************************************/
    /*___________________________FIXED UPDATE_________________________*/
    /******************************************************************/
    private void FixedUpdate()
    {
        Vector2 colliderPos = testBackgrounds[0].transform.position;
        float randomPosX = Random.Range(colliderPos.x - colliderTestLevel.size.x / 2, colliderPos.x + colliderTestLevel.size.x / 2);
        float randomPosY = Random.Range(colliderPos.y - colliderTestLevel.size.y / 2, colliderPos.y + colliderTestLevel.size.y / 2);


        if (testLevelEnemyCounter == 0 && activated_TestLevel && activated_TestWave)

        {

            activated_TestWave = false;
            testBook.SetActive(true);
            testBook.transform.position = new Vector3(randomPosX, randomPosY, 0);

            if (testLevelWaveCounter == 6)
            {
                testBook.transform.position = new Vector3(0, 0, 0);
                testBook.SetActive(false);

                testLevelEnemyCounter = 0;

                StopAllCoroutines();

                activated_TestLevel = false;

                SkipText(dialogPanel);
                player.transform.position = dagdaSpawnPos;
                beginGame("Dagda");

            }
        }


        if (isLevelingUp)
        {
            fx_levelup.transform.position = player.transform.position;
        }


        if (AshadiFolowingKing)
        {

            if (
           playerAshadi.transform.position.x >= playerKing.transform.position.x - followingRadius &&
           playerAshadi.transform.position.x <= playerKing.transform.position.x + followingRadius
           )
            {
                playerAshadi.GetComponent<Animator>().SetBool("isWalking", false);
                //MioIsNearDagda = true;
            }
            else
            {
                CheckSprite(playerAshadi, player, 3, true);
                playerAshadi.GetComponent<Animator>().SetBool("isWalking", true);
                playerAshadi.transform.position =
                     Vector3.MoveTowards(playerAshadi.transform.position, playerKing.transform.position, speed);
            }


        }
        if (MioFolowingPlayer)
        {

            if (
           playerMio.transform.position.x >= player.transform.position.x - followingRadius &&
           playerMio.transform.position.x <= player.transform.position.x + followingRadius
           )
            {
                rightFootMio.SetActive(true);
                rightLegMio.SetActive(true);
                rightUpperLegMio.SetActive(true);

                rightRunningFootMio.SetActive(false);
                rightRunningLegMio.SetActive(false);
                rightRunningUpperLegMio.SetActive(false);
                playerMio.GetComponent<Animator>().SetBool("isWalking", false);
                playerMio.GetComponent<Animator>().SetBool("isWalkingRight", false);

                if (playerMio.transform.position.x > player.transform.position.x)
                {
                    playerMio.GetComponent<Animator>().SetBool("isIdle", true);
                }
                else
                {
                    playerMio.GetComponent<Animator>().SetBool("isIdleRight", true);
                }
                //MioIsNearDagda = true;
            }
            else
            {
                playerMio.GetComponent<Animator>().SetBool("isIdle", false);
                playerMio.GetComponent<Animator>().SetBool("isIdleRight", false);
                rightFootMio.SetActive(false);
                rightLegMio.SetActive(false);
                rightUpperLegMio.SetActive(false);

                rightRunningFootMio.SetActive(true);
                rightRunningLegMio.SetActive(true);
                rightRunningUpperLegMio.SetActive(true);
                //CheckSprite(playerMio, player, 3, true);
                //playerMio.GetComponent<Animator>().SetBool("isWalking", true);

                if (playerMio.transform.position.x > player.transform.position.x)
                {
                    playerMio.GetComponent<Animator>().SetBool("isWalking", true);
                }
                else
                {
                    playerMio.GetComponent<Animator>().SetBool("isWalkingRight", true);
                }

                playerMio.transform.parent.position =
                     Vector3.MoveTowards(playerMio.transform.parent.position, player.transform.parent.position+ new Vector3(0,5,0), speed);
            }


        }
        if (DagdaFolowingPlayer)
        {

            if (
           playerDagda.transform.position.x >= player.transform.position.x - followingRadius &&
           playerDagda.transform.position.x <= player.transform.position.x + followingRadius
           )
            {
                rightFoot.SetActive(true);
                rightLeg.SetActive(true);
                rightUpperLeg.SetActive(true);

                rightRunningFoot.SetActive(false);
                rightRunningLeg.SetActive(false);
                rightRunningUpperLeg.SetActive(false);
                playerDagda.GetComponent<Animator>().SetBool("isWalking", false);
                playerDagda.GetComponent<Animator>().SetBool("isWalkingRight", false);
            }
            else
            {
                playerDagda.GetComponent<Animator>().SetBool("isIdle", false);
                playerDagda.GetComponent<Animator>().SetBool("isIdleRight", false);
                rightFoot.SetActive(false);
                rightLeg.SetActive(false);
                rightUpperLeg.SetActive(false);

                rightRunningFoot.SetActive(true);
                rightRunningLeg.SetActive(true);
                rightRunningUpperLeg.SetActive(true);
                //CheckSprite(playerMio, player, 3, true);
                //playerMio.GetComponent<Animator>().SetBool("isWalking", true);

                if (playerDagda.transform.position.x > player.transform.position.x)
                {
                    playerDagda.GetComponent<Animator>().SetBool("isWalking", true);
                }
                else
                {
                    playerDagda.GetComponent<Animator>().SetBool("isWalkingRight", true);
                }

                playerDagda.transform.parent.position =
                     Vector3.MoveTowards(playerDagda.transform.parent.position, player.transform.parent.position, speed);
            }


        }
        if (AshadiFolowingPlayer)
        {

            if (
                 playerAshadi.transform.position.x >= player.transform.position.x - 30 &&
           playerAshadi.transform.position.x <= player.transform.position.x + 30
                 )

            {
                playerAshadi.GetComponent<Animator>().SetBool("isWalking", false);

                AshadiFolowingPlayer = false;
            }
            else
            {

                playerAshadi.GetComponent<Animator>().SetBool("isWalking", true);
                playerAshadi.transform.position =
                     Vector3.MoveTowards(playerAshadi.transform.position, player.transform.position - new Vector3(30f, 0, 0), speed);
            }
        }
        if (isMoving)
        {
            storyLineSlider.value = 1395f / (1395f + ((1395f - player.transform.position.x) * 3));

            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isIdleRight", false);
            if (isDagda)
            {
                rightFoot.SetActive(false);
                rightLeg.SetActive(false);
                rightUpperLeg.SetActive(false);

                rightRunningFoot.SetActive(true);
                rightRunningLeg.SetActive(true);
                rightRunningUpperLeg.SetActive(true);
            }

            if (isMio)
            {
                rightFootMio.SetActive(false);
                rightLegMio.SetActive(false);
                rightUpperLegMio.SetActive(false);

                rightRunningFootMio.SetActive(true);
                rightRunningLegMio.SetActive(true);
                rightRunningUpperLegMio.SetActive(true);
            }
          

            if (player.transform.position.x > targetPos.x)
            {
                playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                playerAnimator.SetBool("isWalkingRight", true);
            }
               
            player.transform.parent.position =
                  Vector3.MoveTowards(player.transform.parent.position, targetPos + walkOffset, speed);

            if (
                player.transform.parent.position.x <= targetPos.x + clickOffsetX
                && player.transform.parent.position.y <= targetPos.y + clickOffsetY
                &&
                 player.transform.parent.position.x >= targetPos.x - clickOffsetX
                && player.transform.parent.position.y >= targetPos.y - clickOffsetY
                )
            {
                if (isDagda)
                {
                    rightFoot.SetActive(true);
                    rightLeg.SetActive(true);
                    rightUpperLeg.SetActive(true);

                    rightRunningFoot.SetActive(false);
                    rightRunningLeg.SetActive(false);
                    rightRunningUpperLeg.SetActive(false);
                }

                if (isMio)
                {
                    rightFootMio.SetActive(true);
                    rightLegMio.SetActive(true);
                    rightUpperLegMio.SetActive(true);

                    rightRunningFootMio.SetActive(false);
                    rightRunningLegMio.SetActive(false);
                    rightRunningUpperLegMio.SetActive(false);
                }
              

                isMoving = false;
                if (player.transform.localScale.x > 0)
                {
                   
                    playerAnimator.SetBool("isIdle", true);
                }
                else
                {
                   
                    playerAnimator.SetBool("isIdleRight", true);

                }
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isWalkingRight", false);
                /*
                if (MioFoloowingDagda)
                {
                    playerMio.GetComponent<Animator>().SetBool("isWalking", false);
                    MioFoloowingDagda = false;
                }
                */
                if (isMovingToFight)
                {
                    isMovingToFight = false;

                    isInFight = true;
                    int _attackOffset = 3;
                    bool _flipFirst = true;
                    if (activated_TestLevel)
                    { 
                        _attackOffset = -15;
                    
                    }

                    if (MioFolowingPlayer)
                    {
                        if (currentEnemyTarget != null)
                        {
                            CheckSprite(playerMio, currentEnemyTarget, _attackOffset, _flipFirst);
                        }
                    }

                    if (currentEnemyTarget != null)
                    { 
                        CheckSprite(player, currentEnemyTarget, _attackOffset, _flipFirst); 
                    }

                   


                    if (teleportForFight)
                    {
                        player.transform.position = battleBackgrounds[0].transform.position - new Vector3(3, 0, 0);
                        enemyTargetObject[0].transform.position = battleBackgrounds[0].transform.position - new Vector3(-3, 0, 0);
                    }
                }
            }
        }
        if (isFlyingAway)
        {
            /* rocket.transform.position = Vector3.Lerp(rocket.transform.position,
                 flyPos1.position, 0.001f);
            */

            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize,
           10.5f, zoomOutSpeed);

        }
        if (isZooming)
        {
            /* rocket.transform.position = Vector3.Lerp(rocket.transform.position,
                 flyPos1.position, 0.001f);
            */

            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize,
           8f, zoomOutSpeed);

        }
        if (isInFight == false)
        {
            if (mainUpdateLoop.isInside == true)
            {
                playerDagda.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)playerDagda.transform.position.y;
                /*foreach (SpriteRenderer spriteRends in playerDagda.transform.parent.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRends.sortingOrder = 300 - (int)playerDagda.transform.position.y;
                }*/
                for (int i = 0; i < playerDagda.transform.parent.GetComponentsInChildren<SpriteRenderer>().Length; i++)
                {
                    playerDagda.transform.parent.GetComponentsInChildren<SpriteRenderer>()[i].sortingOrder = 300 +i - (int)playerDagda.transform.position.y;
                }
                float localScaleSizeVarPlayer = 3.2f - (playerDagda.transform.localPosition.y / 10);
                playerDagda.transform.parent.localScale = new Vector3(localScaleSizeVarPlayer, localScaleSizeVarPlayer, localScaleSizeVarPlayer);

                playerMio.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)playerMio.transform.position.y;
                /*foreach (SpriteRenderer spriteRends in playerMio.transform.parent.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRends.sortingOrder = 300 - (int)playerMio.transform.position.y;
                }*/
                for (int i = 0; i < playerMio.transform.parent.GetComponentsInChildren<SpriteRenderer>().Length; i++)
                {
                    playerMio.transform.parent.GetComponentsInChildren<SpriteRenderer>()[i].sortingOrder = 300 + i - (int)playerMio.transform.position.y;
                }
                float localScaleSizeVarMio = 1f - (playerMio.transform.localPosition.y / 100);
                playerMio.transform.parent.localScale = new Vector3(localScaleSizeVarMio, localScaleSizeVarMio, localScaleSizeVarMio);
            }
            else
            {
                playerDagda.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)playerDagda.transform.position.y;
                float localScaleSizeVarPlayer = 1f - (playerDagda.transform.parent.localPosition.y / 10);
                playerDagda.transform.parent.localScale = new Vector3(localScaleSizeVarPlayer, localScaleSizeVarPlayer, localScaleSizeVarPlayer);

                playerMio.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)playerMio.transform.position.y;
                float localScaleSizeVarMio = .46f - (playerMio.transform.parent.localPosition.y / 24);
                playerMio.transform.parent.localScale = new Vector3(localScaleSizeVarMio, localScaleSizeVarMio, localScaleSizeVarMio);

            }


                if (gameHasStarted)
            {
                foreach (GameObject _enemy in allMapEnemies)
                {
                    if (_enemy != null)
                    {
                        _enemy.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)_enemy.transform.position.y;
                    }

                }
            }
        }
        if (Mission1 == true && mission1EnemyCounter == 0)

        {
            mission1EnemyCounter = 10;
            mainStory.StartCoroutine(mainStory.M1ServantScene());


        }
    }


    /******************************************************************/
    /*___________________________CHECK SPRITE_________________________*/
    /******************************************************************/

    public void CheckSprite(GameObject _playerSource, GameObject _enemyTarget, int _attackOffsetX, bool _flipFirst)
    {
        if (_playerSource.transform.position.x > _enemyTarget.transform.position.x)
        {
           
            //_playerSource.GetComponent<SpriteRenderer>().flipX = _flipFirst;
            attackFXOffset = new Vector3(-_attackOffsetX, 3, 0);
        }
        else
        {
           
            //_playerSource.GetComponent<SpriteRenderer>().flipX = !_flipFirst;
            attackFXOffset = new Vector3(_attackOffsetX, 3, 0);
        }
    }

    public void CheckPlayerSpriteToTargetPos()
    {
        if (player.transform.position.x > targetPos.x)
        {
          
            //player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
           
            //player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    
}


