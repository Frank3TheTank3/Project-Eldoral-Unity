using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class MainUpdateLoop : MonoBehaviour
{
    MainScript mainScript;
    MainStory mainStory;
    CinemachineVirtualCamera vcam;
    CharacterStats charStats;
    MainDialog mainDia;
    MainFightSystem fightSystem;
    public bool isInside;
    private void Awake()
    {
        vcam = FindAnyObjectByType<CinemachineVirtualCamera>();
        mainScript = FindAnyObjectByType<MainScript>();
        mainStory = FindAnyObjectByType<MainStory>();
        charStats = FindAnyObjectByType<CharacterStats>();
        mainDia = FindAnyObjectByType<MainDialog>();
        fightSystem = FindAnyObjectByType<MainFightSystem>();
    }


    /******************************************************************/
    /*__________________UPDATE AND CLICK LISTERNER____________________*/
    /******************************************************************/
    void SpawnTestWaveEnemies(GameObject _enemyToSpawn)
    {
        fightSystem.enemyTargetObject = new GameObject[5];
        //Spawn Map Enemies
        for (int i = 0; i < mainScript.allEnemyTestSpawnPoints.Length; i++)
        {
         
            GameObject enemyTestScene_1 = Instantiate(_enemyToSpawn, mainScript.allEnemyTestSpawnPoints[i].transform.position, Quaternion.identity);
            
            int enemyMaxHealth = enemyTestScene_1.GetComponent<EnemyStats>().enemyMaxHealth;
            int enemyCurrentHealth = enemyTestScene_1.GetComponent<EnemyStats>().enemyCurrentHealth;
            int enemyAttackStrength = enemyTestScene_1.GetComponent<EnemyStats>().enemyAttackStrength;
            int enemyLevel= enemyTestScene_1.GetComponent<EnemyStats>().enemyLevel;

            enemyTestScene_1.GetComponent<EnemyStats>().enemyMaxHealth = enemyMaxHealth + (1+ i * enemyMaxHealth) + 5;
            enemyTestScene_1.GetComponent<EnemyStats>().enemyCurrentHealth = enemyCurrentHealth + (1+ i * enemyCurrentHealth);
            enemyTestScene_1.GetComponent<EnemyStats>().enemyAttackStrength = enemyAttackStrength + (1+ i * enemyAttackStrength);
            enemyTestScene_1.GetComponent<EnemyStats>().enemyLevel = enemyLevel + i;
            if (enemyTestScene_1.GetComponentInChildren<Text>())
            { 
                enemyTestScene_1.GetComponentInChildren<Text>().text = "Level " + enemyTestScene_1.GetComponent<EnemyStats>().enemyLevel;
            }
            enemyTestScene_1.transform.localScale = new Vector3(10+i, 10+i, 10+i);


            mainScript.checkSpritesForSpawnTest(enemyTestScene_1);
            mainScript.testLevelEnemyCounter++;
            fightSystem.enemyTargetObject[i] = enemyTestScene_1;

           

        }
        mainScript.testLevelWaveCounter++;
    }

    void Update()
    {
        if (
            mainScript.isShowingPlayerMenu == false && Input.GetMouseButtonDown(0) && 
            mainScript.isInFight == false && 
            mainScript.isShowingDialog == false && 
            mainScript.isShowingMainMenu == false &&
            mainScript.isMovingToFight == false)

        {
            //Return if over UI
            if (EventSystem.current.IsPointerOverGameObject()) return;

            mainScript.mousePos = Input.mousePosition;
            mainScript.mousePosWorld = mainScript.mainCamera.ScreenToWorldPoint(mainScript.mousePos);
            if (mainScript.mousePosWorld.x != 0 && mainScript.mousePosWorld.y != 0)
            {
                //print("Z INDEX FOR PLAYER WOULD BE" + mousePosWorld.y);
                mainScript.mousePosWorld2D = new Vector2(mainScript.mousePosWorld.x, mainScript.mousePosWorld.y);


                RaycastHit2D hit;
                hit = Physics2D.Raycast(mainScript.mousePosWorld2D, Vector2.zero);
                if (hit.point.x != 0 && hit.point.y != 0)
                {
                    mainScript.targetPos = hit.point;
                    mainScript.CheckPlayerSpriteToTargetPos();
                }


                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Castle":
                            mainScript.isInFight = false;
                            mainScript.vcam.m_Lens.OrthographicSize = 20f;
                            mainScript.speed = 0.75f;
                            isInside = true;
                            if (mainScript.MioFolowingPlayer){
                                mainScript.playerMio.transform.parent.position = mainScript.castleBackgrounds[0].transform.position - new Vector3(10f, 0, 0);
                                mainScript.followingRadius = 20f;
                            }

                            if (mainScript.DagdaFolowingPlayer)
                            {
                                mainScript.playerDagda.transform.parent.position = mainScript.castleBackgrounds[0].transform.position - new Vector3(10f, 0, 0);
                                mainScript.followingRadius = 20f;
                            }

                            if (mainScript.Mission1 == true && mainScript.Mission1MiniBoss == true){
                                mainStory.StartCoroutine(mainStory.KingDeathScene());
                            }else{
                                mainScript.positionBeforeInside = mainScript.player.transform.position;
                                mainScript.positionCameraBeforeInside = vcam.transform.position;
                                mainScript.setCamDampening(0f);
                                mainScript.vcam.Follow = mainScript.castleBackgrounds[0].transform;
                                mainScript.player.transform.parent.position = mainScript.castleBackgrounds[0].transform.position - new Vector3(0, 0, 0);
                                mainScript.targetPos = mainScript.castleBackgrounds[0].transform.position - new Vector3(0, 0.2f, 0);
                                mainScript.audioCenter.setSound(6);
                            }
                            break;
                        case "Castle2":
                            mainScript.setCamDampening(6f);
                            vcam.Follow = mainScript.castleBackgrounds[1].transform;
                            mainScript.targetPos = mainScript.castleBackgrounds[1].transform.position - new Vector3(48f, 11f, 0);
                            //mainScript.isMoving = true;
                            StartCoroutine(mainScript.resetCamDamping(10));
                            break;
                        case "DoorBack":
                            isInside = false;
                            mainScript.isInFight = false;
                            mainScript.setCamDampening(0f);

                            if (mainScript.MioFolowingPlayer)
                            { mainScript.playerMio.transform.parent.position = mainScript.positionBeforeInside + new Vector2(10, 0); }

                            if (mainScript.DagdaFolowingPlayer)
                            { mainScript.playerDagda.transform.parent.position = mainScript.positionBeforeInside + new Vector2(10, 0); }

                            mainScript.player.transform.parent.position = mainScript.positionBeforeInside;

                            vcam.Follow = mainScript.player.transform;
                            vcam.m_Lens.OrthographicSize = 12f;
                            mainScript.speed = 0.1f;
                            mainScript.followingRadius = 3f;
                            StartCoroutine(mainScript.resetCamDamping(10));
                            break;
                        case "Door":
                            hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpening", true);
                            break;
                        case "Book":
                            mainScript.testBook = hit.collider.gameObject;
                            mainScript.testBook.transform.position = new Vector3(0, 0, 0);
                            mainScript.testBook.SetActive(false);
                            StartCoroutine(mainScript.DisplayText(mainScript.DialogText, mainDia.allBooksDialog[mainScript.bookNumber]));

                            mainScript.bookNumber++;
                            mainScript.activated_TestWave = true;
                          

                            GameObject enemyToSpawn;
                            switch (mainScript.bookNumber)
                            {
                                case 1:
                                    enemyToSpawn = mainScript.prefab_Wolf;
                                    SpawnTestWaveEnemies(enemyToSpawn);
                                    return;
                                case 2:
                                    enemyToSpawn = mainScript.prefab_Gnoll;
                                    SpawnTestWaveEnemies(enemyToSpawn);
                                    break;
                                case 3:
                                    enemyToSpawn = mainScript.prefab_Goblin;
                                    SpawnTestWaveEnemies(enemyToSpawn);
                                    break;
                                case 4:
                                    enemyToSpawn = mainScript.prefab_Troll;
                                    SpawnTestWaveEnemies(enemyToSpawn);
                                    break;
                                case 5:
                                    enemyToSpawn = mainScript.prefab_Werewolf;
                                    SpawnTestWaveEnemies(enemyToSpawn);
                                    break;
                                case 6:
                                    mainScript.activated_TestLevel = false;
                                    mainScript.setCamDampening(0f);
                                    mainScript.player.transform.position = mainScript.dagdaSpawnPos;
                                    vcam.m_Lens.OrthographicSize = 12f;
                                    mainScript.speed = 0.1f;
                                    mainScript.followingRadius = 4f;
                                    StartCoroutine(mainScript.resetCamDamping(4));
                                    break;
                                default:
                                    return;
                                   
                            }
                            break;
                        case "MioIntro":
                            mainScript.isSpeakingWithMio = true;
                            mainScript.targetPos = mainScript.playerMio.transform.position - new Vector3(15f, 0f, 0);
                            mainScript.isMoving = true;
                            StartCoroutine(mainScript.DisplayText(mainScript.DialogText, mainDia.allMioIntroDialog[mainScript.mioTextCounter]));
                            mainScript.checkDialogAvatar("Mio");
                            break;
                        case "Background":
                            print("Pos: " + mainScript.mousePosWorld2D + " " + hit.collider.gameObject.tag);
                            mainScript.audioCenter.setSound(9);
                            mainScript.isMoving = true;
                            break;
                        case "XP":
                            mainScript.audioCenter.setSound(7);
                            print("Pos: " + mainScript.mousePosWorld2D + " " + hit.collider.gameObject.tag);
                            charStats.addXPToPlayer(80);
                            charStats.UpdateStats("Intelligence", 10);
                            mainScript.isMoving = true;
                            print(hit.collider.gameObject.tag);
                            Destroy(hit.collider.gameObject);
                            mainScript.worldPanelXP.SetActive(true);
                            mainScript.worldPanelXP.transform.position = hit.point;
                            mainScript.worldPanelXP.GetComponent<RectTransform>().position = hit.point;
                            mainScript.worldPanelXP.GetComponentInChildren<Text>().text = "+ " + 50 + " XP";
                            mainScript.StartCoroutine(mainScript.RemoveXPPanel());
                            break;
                        case "Mission1":
                            if (mainScript.Mission1 == false){
                                mainScript.targetPos = mainScript.ruins.transform.position + new Vector3(3f, -5f, 0);
                                Collider2D hitObject = hit.collider;
                                mainStory.StartCoroutine(mainStory.M1Scene(hitObject));
                            }
                            break;
                        case "RedStone":
                            //StopAllCoroutines();
                            charStats.addXPToPlayer(25);
                            charStats.addManaToPlayer(15);
                            mainScript.SkipText(mainScript.dialogPanel);
                            print("Pos: " + mainScript.mousePosWorld2D + " " + hit.collider.gameObject.tag);
                            mainScript.isMoving = true;
                            print(hit.collider.gameObject.tag);
                            Destroy(hit.collider.gameObject);
                            mainScript.itemNumber++;
                            mainScript.charInventory.addToInventory("Redstone");
                            break;


                        case "Wolf":
                            mainScript.audioCenter.setSound(10);
                            mainScript.SkipText(mainScript.dialogPanel);
                            vcam.Follow = mainScript.player.transform;
                            //Set moving to fight moving trigger
                            mainScript.isMovingToFight = true;

                            //Fight Music
                            mainScript.audioCenter.setTrack(3);

                            //Save playerposition before fight in case of battlegrounds teleportation
                            mainScript.positionBeforeFight = mainScript.player.transform.position;

                            //Set gith panel on
                            mainScript.fightPanel.SetActive(true);

                            //Add enemy to target list
                            mainScript.chosenEnemyAttackCounter = 0;

                            if (mainScript.Mission1 == false || mainScript.Mission1MiniBoss == true)
                            {
                                mainScript.enemyTargetObject = new GameObject[1];
                                mainScript.enemyTargetObject[0] = hit.collider.transform.gameObject;
                                //new system
                                mainScript.currentEnemyTarget = hit.collider.transform.gameObject;
                            }
                            else
                            { mainScript.audioCenter.setTrack(4); }


                            bool _flipFirst = true;
                            int _attackOffset = 3;
                            if (mainScript.activated_TestLevel == true)
                            {
                                _flipFirst = !_flipFirst;
                                _attackOffset = -15;
                            }


                            if (mainScript.enemyTargetObject[0].GetComponent<SpriteRenderer>().flipX == false)
                            { mainScript.targetPos = hit.point + new Vector2(_attackOffset, -1); mainScript.player.GetComponent<SpriteRenderer>().flipX = _flipFirst; }
                            else
                            { mainScript.targetPos = hit.point - new Vector2(_attackOffset, 0); mainScript.player.GetComponent<SpriteRenderer>().flipX = !_flipFirst; }


                            if (mainScript.activated_TestLevel == false)
                            { mainScript.setCamDistanceNear(); }


                            print("Pos: " + mainScript.mousePosWorld2D + " " + hit.collider.gameObject.tag);
                            mainScript.isMoving = true;

                            print(hit.collider.gameObject.tag);

                            mainScript.setCamDampening(0.1f);
                            mainScript.enemyTargetObject[0].GetComponent<EnemyStats>().SetEnemyStats();
                            mainScript.fightPanelAttacks = FindObjectOfType<PanelContentAttack>().gameObject;

                            //Destroy(hit.collider.gameObject);
                            break;
                        case "Zoom":
                            mainScript.isZooming = true;
                            mainScript.isMoving = true;
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    mainScript.playerAnimator.SetBool("isIdle", false);
                    mainScript.playerAnimator.SetBool("isIdleRight", false); 
                    print("No collider found!"); };
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (mainScript.isMoving == false && mainScript.isAttacking == false && mainScript.isInFight == false && mainScript.isShowingDialog == false)
            {
                //StopAllCoroutines();
                mainScript.SkipText(mainScript.dialogPanel);

                mainScript.mousePos = Input.mousePosition;
                mainScript.mousePosWorld = mainScript.mainCamera.ScreenToWorldPoint(mainScript.mousePos);
                if (mainScript.mousePosWorld.x != 0 && mainScript.mousePosWorld.y != 0)
                {
                    mainScript.mousePosWorld2D = new Vector2(mainScript.mousePosWorld.x, mainScript.mousePosWorld.y);
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(mainScript.mousePosWorld2D, Vector2.zero);
                    mainScript.targetPos = hit.point;
                    mainScript.CheckPlayerSpriteToTargetPos();

                    fightSystem.worldAttack();



                }

            }

        }


    }
}
