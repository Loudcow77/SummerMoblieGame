using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject score;
    public GameObject Title;
    void Awake()
    {
        Time.timeScale = 0;
    }

    public void whenButtonClicked()
    {
        if (score.activeInHierarchy == false)
        {
            score.SetActive(true);
        }
        else
        {
            score.SetActive(true);
        }
        Time.timeScale = 1;
        Title.SetActive(false);
    }
}
