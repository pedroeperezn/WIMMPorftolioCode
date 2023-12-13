using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //loads game over screen when player overlaps with trigger volume
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
