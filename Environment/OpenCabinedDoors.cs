using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinedDoors : MonoBehaviour
{

    [SerializeField] float TargetRotation = 100f;
    [SerializeField] float RotationSpeed = -30f;
    private PuzzleManager PuzzleManager;
    AudioManager audioManager;
    FMODEvents fmod;
    private bool hasRotated = false;
    // Start is called before the first frame update
    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    private void Awake()
    {
        
    }

    public void RotateDoor()
    {

        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, TargetRotation, RotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        PlayOpenCabinetSound();
        hasRotated = true;
    }

    private void PlayOpenCabinetSound() 
    {
        if (!hasRotated)
        {
            audioManager.PlayOneShot(fmod.cabinetDoor,transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleManager.IcarusSolved)
        {
            StartCoroutine(RotateDoorCoroutine());
        
        }
    }

    private IEnumerator RotateDoorCoroutine()
    {
        yield return new WaitForSeconds(1);
        RotateDoor();
    }
}
