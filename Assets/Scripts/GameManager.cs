using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float slowTime = 10f;

    // Resests the scene when the player is hit ie. Ends the Game
    public void EndGame()
    {
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        // This slows down the time of unity 
        Time.timeScale = 1f / slowTime;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowTime;

        // This waits for a set period of 1 second before restarting the level
        yield return new WaitForSeconds(1f / slowTime);

        // Reseting the time scale back to normal (post 1 second)
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = Time.fixedDeltaTime * slowTime;

        // Reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
