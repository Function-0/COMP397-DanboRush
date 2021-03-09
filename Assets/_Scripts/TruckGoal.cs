using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TruckGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Player reached the goal");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameWin");
        }
    }
}
