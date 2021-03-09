using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TruckGoal : MonoBehaviour
{
    public float finalScore;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Player reached the goal");
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<ScoreController>().AddScore(FindObjectOfType<Countdown>().timeStart * 100);
            finalScore = FindObjectOfType<ScoreController>().score;
            PlayerPrefs.SetFloat("score", finalScore);
            SceneManager.LoadScene("GameWin");
        }
    }
}
