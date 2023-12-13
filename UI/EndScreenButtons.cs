using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Player has quit the game");
    }
}
