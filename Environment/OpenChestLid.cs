using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestLid : MonoBehaviour
{
    private Animator ChestLidAnimator;
    private PuzzleManager PuzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoOpenChestLid();
        }
    }
    public void DoOpenChestLid()
    {
        ChestLidAnimator.SetTrigger("OpenLid");
        Debug.Log("open the damn chest");
    }

    public void CloseChest()
    {
        ChestLidAnimator.SetTrigger("CloseLid");
    }

    private void Start()
    {
        ChestLidAnimator = GetComponent<Animator>();
        PuzzleManager = PuzzleManager.PMInstance;
    }

    private void FixedUpdate()
    {
        if (PuzzleManager.PressurePlate2Solved) 
        {
            DoOpenChestLid();
           
        }
    }

}
