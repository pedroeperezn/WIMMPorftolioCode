using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinamicDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int Index;


    private void OnEnable()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        Index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[Index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
