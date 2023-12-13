using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HuskComboShow : MonoBehaviour
{

    [SerializeField] private String[] numbersToShow;
    private String[] unsolvedComboRestart;
    [SerializeField] private GameObject _husk;

    [SerializeField] private int[] correctCombo;
    [SerializeField] private int[] playeddCombo;
    [SerializeField] private TMP_Text thinkText;

    private int currentIndex = 0;

    private void Start()
    {
        unsolvedComboRestart = numbersToShow;
        correctCombo = _husk.GetComponent<ComboCheck>().correctCombo;
        StartCoroutine(ChangeComboNumber());   
    
    }


    private IEnumerator ChangeComboNumber()
    {

        while (true) 
        { 
            playeddCombo = _husk.GetComponent<ComboCheck>().playedCombo;
            
            while (currentIndex < numbersToShow.Length) 
            {
                if (correctCombo[currentIndex] == playeddCombo[currentIndex])
                {
                    numbersToShow[currentIndex] = playeddCombo[currentIndex].ToString();
                }

                else 
                {
                    numbersToShow[currentIndex] = unsolvedComboRestart[currentIndex];
                }

                thinkText.text = numbersToShow[currentIndex];
                currentIndex++;
                yield return new WaitForSeconds(0.95f);
            }

            yield return new WaitForSeconds(3f);
            currentIndex = 0;
        }
    }

    private void Update()
    {
    }
}
