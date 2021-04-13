/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-12 20:43:49 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-13 00:54:22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementsController : MonoBehaviour
{
    [Header("Achievements Progress")]
    public GameObject enemyAchievement;
    public GameObject boxAchievement;
    public GameObject fireAchievement;

    [Header("Achievements Completed Message")]
    public GameObject achievementsMsg;
    private int messageTimer = 0;
    private int messageTimeOut = 50;

    private int enemyCount = 0;
    private int boxPickedCount = 0;
    private int fireCount = 0;

    // Observer Pattern - Observer
    void OnEnable()
    {
        EnemyTest.EnemyKilledEvent += OnEnemyKilled;
        PlayerThrowBox.BoxPickedUpEvent += OnBoxPickedUp;
        PlayerBehaviour.FireEvent += OnFire;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Hide the Achievements msg after a timeframe
        if (achievementsMsg.activeInHierarchy)
        {
            HideAchievementsMsgAfterTimeOut();
        }
    }

    private void UpdateAchievementUI(GameObject achievementObject, int number)
    {
        Transform progressBar = achievementObject.transform.Find("ProgressBar");
        Slider slider = progressBar.GetComponent<Slider>();
        TextMeshProUGUI progressText = progressBar.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        slider.value += number;
        Debug.Log("Fire Count ===========" + slider.value);
        progressText.text = slider.value.ToString() + "/" + slider.maxValue;
    }

    public void OnEnemyKilled()
    {
        enemyCount++;
        UpdateAchievementUI(enemyAchievement, 1);

        // Reward - Box + 1
        if (enemyCount == 1)
        {
            // Give reward
            FindObjectOfType<InventoryController>().AddItem( new InventoryItem {type = InventoryItem.Type.Box, quantity = 1} );
            
            // Show achievements msg
            achievementsMsg.GetComponent<TextMeshProUGUI>().text = "Achievement Earned Box + 1";
            ShowAchievementsMsg();
        }
    }

    public void OnBoxPickedUp()
    {
        boxPickedCount++;
        UpdateAchievementUI(boxAchievement, 1);

        // Reward - Coin + 10
        if (boxPickedCount == 1)
        {
            // Give reward
            FindObjectOfType<CoinScript>().TakeCoin(10f);

            // Show achievements msg
            achievementsMsg.GetComponent<TextMeshProUGUI>().text = "Achievement Earned Coin + 10";
            ShowAchievementsMsg();
        }
    }

    public void OnFire()
    {
        fireCount++;
        UpdateAchievementUI(fireAchievement, 1);

        // Reward - Health + 10
        if (fireCount == 50)
        {
            // Give reward
            FindObjectOfType<PlayerBehaviour>().Heal(10f);

            // Show achievements msg
            achievementsMsg.GetComponent<TextMeshProUGUI>().text = "Achievement Earned Health + 10";
            ShowAchievementsMsg();
        }
    }

    private void ShowAchievementsMsg()
    {
        achievementsMsg.SetActive(true);
    }

    private void HideAchievementsMsgAfterTimeOut()
    {
        messageTimer++;
        if (messageTimer > messageTimeOut)
        {
            achievementsMsg.SetActive(false);
            messageTimer = 0;
        }
    }
}
