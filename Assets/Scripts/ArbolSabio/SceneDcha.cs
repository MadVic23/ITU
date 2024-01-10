using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine;

public class SceneDch : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private float typingTime = 0.05f;

    private int lineIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if(lineIndex < dialogueLines.Length){
                if ((dialogueText.text == dialogueLines[lineIndex]))
                {
                    NextDialogueLine();
                }
                else{
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
            }
                else if(lineIndex == dialogueLines.Length){
                    dialoguePanel.SetActive(false);
                    player.GetComponent<Movimiento>().estaEnDialogo = false;
                    SceneManager.LoadScene(4);
                }
            }
        }
            

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            startDialogue();
            
        }
    }

    private void startDialogue()
    {
        // bloquear movimiento y camara del personaje
        player.GetComponent<Movimiento>().estaEnDialogo = true;
        
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());

    }

    private void NextDialogueLine()
    {

        if ((lineIndex < dialogueLines.Length))
        {
            lineIndex++;
            StartCoroutine(ShowLine());
        }
        if(lineIndex == dialogueLines.Length){
            dialoguePanel.SetActive(false);
            player.GetComponent<Movimiento>().estaEnDialogo = false;
            SceneManager.LoadScene(3);
        }
    }

     private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
            foreach (char ch in dialogueLines[lineIndex])
            {
                dialogueText.text += ch;
                yield return new WaitForSecondsRealtime(typingTime);
            }
        }
}