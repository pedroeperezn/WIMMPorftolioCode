using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastHuskDebugger : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            _door.GetComponent<ComboCheck>().comboTriggered = false;
        
        }
    }
}
