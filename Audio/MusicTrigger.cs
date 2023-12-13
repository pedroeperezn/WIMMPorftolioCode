using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{


    [Header("Music Area")]
    [SerializeField] private MusicPlayer area;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            AudioManager.AMInstance.SetMusicArea(area);
        }
    }
}
