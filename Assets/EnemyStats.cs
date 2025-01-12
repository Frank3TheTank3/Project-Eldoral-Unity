using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    public string enemyName = "Wolf";
    public int enemyLevel = 1;
    public int enemyCurrentHealth = 24;
    public int enemyMaxHealth = 35;
    public int enemyAttackStrength = 35;
    public Image Enemy_HP_Slider;
    public Text LevelText;
    public Text HealthText;
    public GameObject enemyStatsContent;




    public void SetEnemyStats()
    {
        LevelText = FindObjectOfType<EnemyLevelText>().gameObject.GetComponent<Text>();
        HealthText = FindObjectOfType<EnemyHealthText>().gameObject.GetComponent<Text>();
        Enemy_HP_Slider = FindObjectOfType<EnemyHealthSlider>().gameObject.GetComponent<Image>();
        LevelText.text = enemyName + " - Level " + enemyLevel.ToString();
        HealthText.text = enemyCurrentHealth.ToString();

        Enemy_HP_Slider.fillAmount = (1f / enemyMaxHealth) * enemyCurrentHealth;
    }

    public void getHitByPlayer(int _damage)
    {
        enemyCurrentHealth -= _damage;
        HealthText.text = enemyCurrentHealth.ToString();
        Enemy_HP_Slider.fillAmount = (1f / enemyMaxHealth) * enemyCurrentHealth;
    }

}
