using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MainStory : MonoBehaviour
{
    MainScript mainScript;
    MainDialog mainDia;
    Text DialogText;
    CharacterStats charStats;
    CinemachineVirtualCamera vcam;
    private void Awake()
    {
        charStats = FindAnyObjectByType<CharacterStats>();
        mainScript = FindAnyObjectByType<MainScript>();
        mainDia = FindAnyObjectByType<MainDialog>();
        DialogText = mainScript.DialogText;
        vcam = mainScript.virtualCam.GetComponent<CinemachineVirtualCamera>();
    }

    /******************************************************************/
    /*____________________SCENE M0: MEET MIO SCENE____________________*/
    /******************************************************************/
    public IEnumerator MioScene()
    {
       
        mainScript.audioCenter.setTrack(1);
        yield return new WaitForSecondsRealtime(1.3f);
        mainScript.targetPos = mainScript.castleBackgrounds[2].transform.position - new Vector3(0, 5f, 0);
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 14;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 14;
        vcam.Follow = mainScript.castleBackgrounds[2].transform;
        mainScript.isMoving = true;
        yield return new WaitForSecondsRealtime(0.8f);
        mainScript.MioFolowingPlayer = true;
        mainScript.playerMio.GetComponent<SpriteRenderer>().flipX = false;
        mainScript.door.GetComponent<Animator>().SetBool("isOpening", true);
        yield return new WaitForSecondsRealtime(6f);
        StartCoroutine(mainScript.resetCamDamping(12));
        mainScript.door.GetComponent<Animator>().SetBool("isOpening", false);
        mainScript.isSpeakingWithKing = true;
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allKingIntroDialog[mainScript.kingTextCounter]));
        mainScript.checkDialogAvatar("King");
        yield return new WaitForSecondsRealtime(10f);
        mainScript.SkipText(mainScript.dialogPanel);
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 14;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 14;
        vcam.Follow = mainScript.playerAshadi.transform;
        mainScript.AshadiFolowingKing = true;
   
        StopCoroutine(mainScript.DisplayText(DialogText, mainDia.allKingIntroDialog[mainScript.kingTextCounter]));
        yield return new WaitForSecondsRealtime(0.1f);
        mainScript.SkipText(mainScript.dialogPanel);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allAshadiIntroDialog[0]));
        mainScript.checkDialogAvatar("Ashadi");
        yield return new WaitForSecondsRealtime(8f);
        mainScript.SkipText(mainScript.dialogPanel);

        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allMioIntroDialog[2]));
        mainScript.checkDialogAvatar("Mio");
        yield return new WaitForSecondsRealtime(5f);
        mainScript.player.GetComponent<SpriteRenderer>().flipX = true;
        mainScript.SkipText(mainScript.dialogPanel);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allDagdaIntroDialog[0]));
        mainScript.checkDialogAvatar("Dagda");
        yield return new WaitForSecondsRealtime(5f);
        mainScript.player.GetComponent<SpriteRenderer>().flipX = false;
        mainScript.SkipText(mainScript.dialogPanel);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allKingIntroDialog[2]));
        mainScript.checkDialogAvatar("King");
        yield return new WaitForSecondsRealtime(5f);
        mainScript.SkipText(mainScript.dialogPanel);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allAshadiIntroDialog[2]));
        mainScript.checkDialogAvatar("Ashadi");
        yield return new WaitForSecondsRealtime(4f);
        vcam.Follow = mainScript.player.transform;
        mainScript.playerMio.tag = "Mio";
        mainScript.isSpeakingWithKing = false;
        mainScript.MioFolowingPlayer = true;
        mainScript.AshadiFolowingKing = false;
        mainScript.castleBackgrounds[0].GetComponent<BoxCollider2D>().enabled = false;
        mainScript.ringSpriteIndex = 3;
        charStats.addCharacterToPlayerPanel("Mio", true);

        //Switch to Mio to avoid bug
        //mainScript.ChangePlayer("Mio");

        //Switch back to dagda and set witch as standard dialog avatar
        // mainScript.ChangePlayer("Mio");
        mainScript.ChangePlayer("Dagda");
        mainScript.checkDialogAvatar("Witch");
    }

   
    /******************************************************************/
    /*______________________SCENE M1: AMBUSH SCENE____________________*/
    /******************************************************************/

    public IEnumerator M1Scene(Collider2D _spawnPointsHolder)
    {
        mainScript.chosenEnemyAttacks = new string[3];
        mainScript.enemyTargetObject = new GameObject[3];
        //enemyTargetObject = new GameObject[5];
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 14;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 14;
        vcam.Follow = mainScript.ruins.transform;
        mainScript.Mission1 = true;
        yield return new WaitForSecondsRealtime(1f);
        mainScript.allSpawnPointsM1 = _spawnPointsHolder.gameObject.GetComponentsInChildren<Transform>();
        //targetPos = allSpawnPointsM1[1].position;
        mainScript.isMoving = true;
        yield return new WaitForSecondsRealtime(1f);
        mainScript.checkDialogAvatar("Enemy");
        //allSpawnPointsM1.length
        for (int i = 0; i < 3; i++)
        {
            mainScript.fx_spawning.GetComponent<Animator>().SetBool("isSpawning", true);
            mainScript.fx_spawning.transform.position = mainScript.allSpawnPointsM1[i + 1].transform.position;

            mainScript.fx_spawning.GetComponent<Animator>().SetBool("isSpawning", false);
            if (i < 3)
            {
                mainScript.audioCenter.setSound(3);
                StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allM1EnemyDialog[i]));
                yield return new WaitForSecondsRealtime(2f);
                GameObject enemyM1Scene = Instantiate(mainScript.prefab_Gnoll, mainScript.allSpawnPointsM1[i + 1].transform.position, Quaternion.identity);
                mainScript.enemyTargetObject[i] = enemyM1Scene;
                if (i == 2)
                {
                    enemyM1Scene.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            yield return new WaitForSecondsRealtime(1f);
            mainScript.SkipText(mainScript.dialogPanel);

        }

    }

    /******************************************************************/
    /*_____________________SCENE M2: RUMORS SCENE_____________________*/
    /******************************************************************/
    public IEnumerator M1ServantScene()
    {
       
        mainScript.targetPos = mainScript.ruins.transform.position + new Vector3(3f, -5f, 0);
        mainScript.isMoving = true;
        mainScript.CheckSprite(mainScript.player, mainScript.playerYuiDienerin, 3, true);
        yield return new WaitForSecondsRealtime(1.5f);
        mainScript.player.GetComponent<SpriteRenderer>().flipX = true;

        mainScript.checkDialogAvatar("Dienerin");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[0]));
        mainScript.playerYuiDienerin.SetActive(true);
        mainScript.vcam.Follow = mainScript.playerYuiDienerin.transform;

        yield return new WaitForSecondsRealtime(3f);

        mainScript.checkDialogAvatar("Dagda");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[1]));

        yield return new WaitForSecondsRealtime(3f);
        mainScript.checkDialogAvatar("Dienerin");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[2]));
       
        yield return new WaitForSecondsRealtime(6f);
        mainScript.checkDialogAvatar("Dagda");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[3]));
       
        yield return new WaitForSecondsRealtime(6f);
        if (mainScript.MioFolowingPlayer)
        {
            mainScript.checkDialogAvatar("Mio");
            StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[4]));
           
            yield return new WaitForSecondsRealtime(6f);
        }
        mainScript.checkDialogAvatar("Dienerin");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[5]));
      
        yield return new WaitForSecondsRealtime(3f);
        mainScript.checkDialogAvatar("Dagda");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[6]));
     
        yield return new WaitForSecondsRealtime(3f);
        mainScript.checkDialogAvatar("Dienerin");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allYuiSceneDialog[7]));
    

        mainScript.vcam.Follow = mainScript.player.transform;
        mainScript.resetCamDamping(1);
        mainScript.M1MiniBossScene();
        mainScript.checkDialogAvatar("Witch");
    }
    /******************************************************************/
    /*____________________SCENE M3: KING DEATH SCENE__________________*/
    /******************************************************************/
    public IEnumerator KingDeathScene()
    {
       
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        vcam.Follow = mainScript.castleBackgrounds[0].transform;
        yield return new WaitForSecondsRealtime(1f);
        mainScript.player.transform.position = mainScript.castleBackgrounds[0].transform.position - new Vector3(0, 0, 0);
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 11;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 11;
        vcam.Follow = mainScript.castleBackgrounds[2].transform;
        yield return new WaitForSecondsRealtime(1f);
        mainScript.targetPos = mainScript.castleBackgrounds[2].transform.position - new Vector3(0, 15f, 0);
        yield return new WaitForSecondsRealtime(7f);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allKingIntroDialog[3]));
        mainScript.checkDialogAvatar("King");
        yield return new WaitForSecondsRealtime(3f);
        mainScript.playerKing.GetComponent<Animator>().SetBool("isDead", true);
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allDagdaIntroDialog[1]));
        mainScript.checkDialogAvatar("Dagda");
        yield return new WaitForSecondsRealtime(3f);
        mainScript.checkDialogAvatar("King");
        StartCoroutine(mainScript.DisplayText(DialogText, mainDia.allKingIntroDialog[4]));
       

    }
}
