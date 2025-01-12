using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MainFightSystem : MonoBehaviour
{

    MainScript mainScript;
    MainDialog mainDia;
    Text DialogText;
    CharacterStats charStats;
    CinemachineVirtualCamera vcam;
    public GameObject[] enemyTargetObject;
    GameObject worldPanelDamage;
    Vector2 targetPos;
    bool isMoving;
    GameObject fx_impact;
    float enemyXP;
    Vector3 currentEnemyPosition;
    GameObject lastEnemyTarget;
    private void Awake()
    {
        charStats = FindAnyObjectByType<CharacterStats>();
        mainScript = FindAnyObjectByType<MainScript>();
        mainDia = FindAnyObjectByType<MainDialog>();
        DialogText = mainScript.DialogText;
        vcam = mainScript.virtualCam.GetComponent<CinemachineVirtualCamera>();
    
    }
    public void worldAttack()
    {
        if (mainScript.isAttacking == false)
        {
           StartCoroutine(actionWorldAttack());

        }
    }

    /******************************************************************/
    /*_________________________ATTACK ROUTINE_________________________*/
    /******************************************************************/
    IEnumerator actionAttack()
    {

        enemyTargetObject = mainScript.enemyTargetObject;
        targetPos = mainScript.targetPos;
        worldPanelDamage = mainScript.worldPanelDamage;
        mainScript.isAttacking = true;
        mainScript.fightPanelAttacks.SetActive(false);

        yield return new WaitForSecondsRealtime(0.3f);
        foreach (var item in mainScript.enemyTargetObject)
        {
            item.GetComponent<SpriteRenderer>().color = new Color(152f, 152f, 152f, 255f);
        }
        for (int i = 0; i < mainScript.enemyTargetObject.Length; i++)
        {



            vcam.Follow = mainScript.player.transform;
            //Walk to enemy
            int _attackOffset = 3;
            bool flipFirst = true;
            if (mainScript.activated_TestLevel)
            {
                _attackOffset = -15;
                flipFirst = !flipFirst;
            }


            if (enemyTargetObject[i].GetComponent<SpriteRenderer>().flipX == false)
            {
                targetPos = enemyTargetObject[i].transform.position + new Vector3(_attackOffset, 0, 0); mainScript.player.GetComponent<SpriteRenderer>().flipX = flipFirst;

            }
            else
            {
                targetPos = enemyTargetObject[i].transform.position - new Vector3(_attackOffset, 0, 0); mainScript.player.GetComponent<SpriteRenderer>().flipX = !flipFirst;

            }

            mainScript.isMoving = true;
            enemyTargetObject[i].GetComponent<EnemyStats>().SetEnemyStats();
            yield return new WaitForSecondsRealtime(1f);

            //Attack Animation
            if (mainScript.chosenEnemyAttacks[i] == "Hit")
            {
                mainScript.playerAnimator.SetBool("isAttacking", true);
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(attackFX(mainScript.player));
            }

            mainScript.audioCenter.setSound(4);

            if (mainScript.activated_TestLevel)
            {
                worldPanelDamage.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else
            { worldPanelDamage.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); }

            worldPanelDamage.SetActive(true);
            worldPanelDamage.transform.position = enemyTargetObject[i].transform.position + mainScript.worldPanelOffset;
            worldPanelDamage.GetComponent<RectTransform>().position = enemyTargetObject[i].transform.position + mainScript.worldPanelOffset;
            worldPanelDamage.GetComponentInChildren<Transform>().position = enemyTargetObject[i].transform.position + mainScript.worldPanelOffset;
            worldPanelDamage.GetComponentInChildren<Transform>().GetComponent<RectTransform>().position = enemyTargetObject[i].transform.position + mainScript.worldPanelOffset;
            worldPanelDamage.GetComponentInChildren<Text>().text = charStats.dagdaOptionStats[0] + " DMG";
            yield return new WaitForSecondsRealtime(1.5f);
            worldPanelDamage.SetActive(false);

            //Player damage
            

            if (enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth > 0)
            {
                enemyTargetObject[i].GetComponent<EnemyStats>().getHitByPlayer(charStats.dagdaOptionStats[0]);

                if (enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth < 1 || enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth == 1)
                {
                    currentEnemyPosition = enemyTargetObject[i].transform.position;
                    lastEnemyTarget = enemyTargetObject[i];

                    mainScript.SpawnSingleItem(currentEnemyPosition);

                    if (mainScript.activated_TestWave)
                    { mainScript.testLevelEnemyCounter--; }
                    if (mainScript.Mission1 == true && mainScript.Mission1MiniBoss == false)
                    {
                        mainScript.mission1EnemyCounter--;
                        Destroy(enemyTargetObject[i]);
                        if (mainScript.mission1EnemyCounter == 0)
                        { exitFight(); }
                    }
                    else if (mainScript.Mission1MiniBoss == true)

                    { Destroy(enemyTargetObject[i]); exitFight(); }
                    else

                    { Destroy(enemyTargetObject[i]); exitFight(); }

                }

            }

            yield return new WaitForSecondsRealtime(1.5f);

            if (mainScript.MioFolowingPlayer && enemyTargetObject[i] != null)
            {


                //Attack Animation Mio
                mainScript.ChangePlayer("Mio");
                worldPanelDamage.SetActive(true);
                worldPanelDamage.GetComponentInChildren<Text>().text = charStats.mioOptionStats[0] + " DMG";
                mainScript.playerMio.GetComponent<Animator>().SetBool("isAttacking", true);
                StartCoroutine(attackFX(mainScript.playerMio));
                mainScript.audioCenter.setSound(4);
                yield return new WaitForSecondsRealtime(.5f);
                worldPanelDamage.SetActive(false);
                //Mio damage
                if (enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth < 1 || enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth == 1)
                {
                    enemyTargetObject[i].GetComponent<EnemyStats>().getHitByPlayer(charStats.mioOptionStats[0]);

                        mainScript.SpawnSingleItem(currentEnemyPosition);

                        if (mainScript.Mission1 == true && mainScript.Mission1MiniBoss == false)
                        {
                            mainScript.mission1EnemyCounter--;
                            currentEnemyPosition = enemyTargetObject[i].transform.position;
                            Destroy(enemyTargetObject[i]);


                            if (mainScript.mission1EnemyCounter == 0)
                            { exitFight(); 
                        }
                        }
                        else if (mainScript.Mission1MiniBoss == true)

                        { Destroy(enemyTargetObject[i]); exitFight();
                    }
                        else

                        { Destroy(enemyTargetObject[i]); exitFight(); }
                    
                }
              
            }
        }



        for (int i = 0; i < enemyTargetObject.Length; i++)
        {
            if (enemyTargetObject[i] != null)
            {
                mainScript.currentEnemyTarget = enemyTargetObject[i];
                if (enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth > 1)
                {
                    Debug.Log(enemyTargetObject.Length);
                    //Cam follow Enemy
                    vcam.Follow = enemyTargetObject[i].transform;
                    yield return new WaitForSecondsRealtime(1f);

                    //Enemy Attack
                    enemyTargetObject[i].GetComponent<Animator>().SetBool("isAttacking", true);
                    mainScript.worldPanelDamage.SetActive(true);
                    mainScript.worldPanelDamage.transform.position = mainScript.player.transform.position + mainScript.worldPanelOffset;
                    mainScript.worldPanelDamage.GetComponentInChildren<Text>().text = mainScript.currentEnemyTarget.GetComponent<EnemyStats>().enemyAttackStrength + " DMG";
                    yield return new WaitForSecondsRealtime(0.7f);
                    mainScript.audioCenter.setSound(4);

                    //Player get hurt
                    mainScript.worldPanelHealth.SetActive(true);
                    if (mainScript.activated_TestLevel == true)
                    {
                        mainScript.worldPanelHealth.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    }
                    else
                    {
                        mainScript.worldPanelHealth.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                    }
                    mainScript.worldPanelHealth.GetComponent<RectTransform>().position = charStats.GUI_HP.gameObject.transform.position + mainScript.worldPanelOffset;
                    if (mainScript.MioFolowingPlayer)
                    {
                        charStats.getDamage(mainScript.currentEnemyTarget.GetComponent<EnemyStats>().enemyAttackStrength, "Mio");
                        charStats.getDamage(mainScript.currentEnemyTarget.GetComponent<EnemyStats>().enemyAttackStrength, "Dagda");
                    }
                    else
                    { charStats.getDamage(mainScript.currentEnemyTarget.GetComponent<EnemyStats>().enemyAttackStrength, "Dagda"); }
                    

                    yield return new WaitForSecondsRealtime(0.7f);

                    //Reset enemy attack
                    mainScript.worldPanelHealth.SetActive(false);
                    mainScript.worldPanelDamage.SetActive(false);
                    enemyTargetObject[i].GetComponent<Animator>().SetBool("isAttacking", false);
                }
                else
                {
                    Debug.Log("Enemy length: " + enemyTargetObject.Length + " Int: " + i + "Enemy Health: " + enemyTargetObject[i].GetComponent<EnemyStats>().enemyCurrentHealth);

                    enemyTargetObject[i].GetComponent<Animator>().SetBool("isAttacking", false);
                    currentEnemyPosition = enemyTargetObject[i].transform.position;
                    Destroy(enemyTargetObject[i]);
                    exitFight();
                }
            }
        }

        mainScript.worldPanelHealth.SetActive(false);
        mainScript.isAttacking = false;
        mainScript.fightPanelAttacks.SetActive(true);



    }

    public IEnumerator actionWorldAttack()
    {
        mainScript.isAttacking = true;
        yield return new WaitForSecondsRealtime(0.3f);
        mainScript.playerAnimator.SetBool("isAttacking", true);
        StartCoroutine(attackFX(mainScript.player));
        mainScript.audioCenter.setSound(4);
    }
    //public string[] chosenEnemyAttacks
    //public int chosenEnemyAttackCounter;
    public void startAttack(string _attackName)
    {
        enemyTargetObject = mainScript.enemyTargetObject;
        //Number of player attacks that have been learned
        if (enemyTargetObject.Length == 1)
        { mainScript.chosenEnemyAttacks = new string[1]; }

        Debug.Log("chosenEnemyAttackCounter: " + mainScript.chosenEnemyAttackCounter + " enemyTargetObject.Length: " + enemyTargetObject.Length + " chosenEnemyAttacks.Length: " + mainScript.chosenEnemyAttacks.Length);

        if (mainScript.isAttacking == false)
        {
            if (mainScript.chosenEnemyAttackCounter >= 0 && mainScript.chosenEnemyAttackCounter < enemyTargetObject.Length)
            {
                enemyTargetObject = mainScript.enemyTargetObject;
                if (enemyTargetObject != null)
                {
                    enemyTargetObject[mainScript.chosenEnemyAttackCounter].GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                { return; }
                mainScript.chosenEnemyAttacks[mainScript.chosenEnemyAttackCounter] = _attackName;
                mainScript.chosenEnemyAttackCounter++;
                Debug.Log(mainScript.chosenEnemyAttacks[0] + "chosenEnemyAttackCounter: " + mainScript.chosenEnemyAttackCounter + " enemyTargetObject.Length: " + enemyTargetObject.Length + " chosenEnemyAttacks.Length: " + mainScript.chosenEnemyAttacks.Length);
                if (mainScript.chosenEnemyAttackCounter == enemyTargetObject.Length)
                {

                    StartCoroutine(actionAttack());
                }
            }
            else
            {
                mainScript.chosenEnemyAttackCounter = 0;
                mainScript.chosenEnemyAttacks[mainScript.chosenEnemyAttackCounter] = _attackName;
                StartCoroutine(actionAttack());
            }
        }


    }

    void exitFight()
    {


        vcam.Follow = mainScript.player.transform;
        if (mainScript.teleportForFight)
        {
            mainScript.player.transform.position = mainScript.positionBeforeFight;
        }

        mainScript.fightPanelAttacks.SetActive(true);
        mainScript.fightPanel.SetActive(false);
        mainScript.isInFight = false;
        worldPanelDamage.SetActive(false);
        
        int __addXP = lastEnemyTarget.GetComponent<EnemyStats>().enemyLevel * 7;
        int _addStrength = lastEnemyTarget.GetComponent<EnemyStats>().enemyLevel * 3;
        charStats.addXPToPlayer(__addXP);

       

        charStats.UpdateStats("Strength", _addStrength);
        mainScript.isAttacking = false;
        mainScript.audioCenter.setTrack(2);

        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 3f;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 3f;
        if (mainScript.activated_TestLevel)
        {
            mainScript.worldPanelXP.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else
        {
            mainScript.worldPanelXP.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        GameObject currentPlayerChosen;
        if (mainScript.MioFolowingPlayer)
        { currentPlayerChosen = mainScript.playerDagda;
            showXPPanelOnPlayer(currentPlayerChosen, __addXP);
        }
        else {
            currentPlayerChosen = mainScript.playerMio;
            showXPPanelOnPlayer(currentPlayerChosen, __addXP);
        }
     

        mainScript.StartCoroutine(mainScript.RemoveXPPanel());
        if (mainScript.activated_TestLevel == false)
        {
            vcam.m_Lens.OrthographicSize = 10.5f;
        }

        

    }

    void showXPPanelOnPlayer(GameObject _chosenPlayer, int _addXP)
    {
        mainScript.worldPanelXP.SetActive(true);
        mainScript.worldPanelXP.transform.position = _chosenPlayer.transform.position + mainScript.worldPanelOffset;
        mainScript.worldPanelXP.GetComponent<RectTransform>().position = _chosenPlayer.transform.position + mainScript.worldPanelOffset;
        mainScript.worldPanelXP.GetComponentInChildren<Text>().text = "+ " + _addXP + " XP";
    }

    /******************************************************************/
    /*_______________________ATTACK/SPELLS FX_________________________*/
    /******************************************************************/
    IEnumerator attackFX(GameObject _player)
    {
        
        fx_impact = mainScript.fx_impact;

        yield return new WaitForSecondsRealtime(0.3f);

        if (mainScript.activated_TestLevel)
        { fx_impact.transform.localScale = new Vector3(10f, 10f, 10f); }
        else
        { fx_impact.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f); }

        if (mainScript.currentEnemyTarget != null)
        { fx_impact.transform.position = mainScript.currentEnemyTarget.transform.position; }
        else
        { mainScript.CheckPlayerSpriteToTargetPos(); fx_impact.transform.position = targetPos; }

        if (mainScript.isMio || mainScript.ringFire == true)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            fx_impact.GetComponent<Animator>().SetBool("isFire", true);
            yield return new WaitForSecondsRealtime(0.3f);
            fx_impact.GetComponent<Animator>().SetBool("isFire", false);
        }
        else if (mainScript.isDagda)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            fx_impact.GetComponent<Animator>().SetBool("isSword", true);
            yield return new WaitForSecondsRealtime(0.3f);
            fx_impact.GetComponent<Animator>().SetBool("isSword", false);
        }
        else if (mainScript.isKing)
        {
            yield return new WaitForSecondsRealtime(0.4f);
            fx_impact.GetComponent<Animator>().SetBool("isHammer", true);
            yield return new WaitForSecondsRealtime(0.3f);
            mainScript.fx_impact.GetComponent<Animator>().SetBool("isHammer", false);
        }


        _player.GetComponent<Animator>().SetBool("isAttacking", false);
        mainScript.isAttacking = false;
    }
}
