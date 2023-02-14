using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Dialogo : MonoBehaviour
{
    private bool isReynaInRange; //para saber si esta en el rango de acercarse
    private bool didDialogueStart;
    private int lineIndex;

    //referencias
    [SerializeField] private GameObject dialoguemark;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    void Update()
    {
        //para iniciar el dialogo, reyna debe estar cerca de la rana
        if (isReynaInRange && Input.GetButtonDown("Submit"))
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            DialoguePanel.SetActive(false);
            dialoguemark.SetActive(true);
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        DialoguePanel.SetActive(true);
        dialoguemark.SetActive(false); //desactivar la marca para poder iniciar el dialogo
        lineIndex = 0; //el indice a 0
        StartCoroutine(ShowLine());
    }

    // el dialogo se mostrara caracter por caracter, para eso haremos ese código para que salga así

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch; //para dar efecto de una por una
            yield return new WaitForSeconds(typingTime); //el tiempo que queramos darle (0.05f 20 caracteres x segundo)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reyna"))
        {
            isReynaInRange = true;
            dialoguemark.SetActive(true);
            //Debug.Log("Se pude iniciar dialogo"); para checar que si me saliera 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reyna"))
        {
            isReynaInRange = false;
            dialoguemark.SetActive(false);
            //Debug.Log("No se puede iniciar dialogo"); checar prueba, funciono, así pude corregir

        }
    }
}
