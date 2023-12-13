using System.Collections;
using System.Collections.Generic;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisableKalimbaOnSolved : MonoBehaviour
{
    private InputActionListener _listener; 

    // Start is called before the first frame update
    void Start()
    {
        _listener = GetComponent<InputActionListener>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
