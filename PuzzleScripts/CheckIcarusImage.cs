using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIcarusImage : MonoBehaviour
{
    [SerializeField] private Sprite _correctSprite;
    public bool isImageCorrect;

    private void Update()
    {
        if (this.GetComponent<Image>().sprite == _correctSprite)
        {
            isImageCorrect = true;
            Debug.Log("Correct");
        }

        else 
        {
            isImageCorrect = false;
            Debug.Log("WRONG ANSWER");
        }
    }
}
