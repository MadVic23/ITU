using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject BotonRespuesta1;
    [SerializeField] private GameObject BotonRespuesta2;
    [SerializeField] private GameObject BotonRespuesta3;
    [SerializeField] private GameObject BotonRespuesta4;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField, TextArea(4, 6)] private string[] dialogueAcertado;
    [SerializeField, TextArea(4, 6)] private string[] dialogueFallado;

    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool isDialogueStarted;
    private bool acierto = false;
    private bool error = false;
    private bool puente = false;
    private int lineIndex = 0;
    private int lineIndexMax;
    private int lineIndexAcertado = 0;
    private int lineIndexFallado = 0;
    private CapsuleCollider collider;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // mostrar cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (!puente)
            {
                StartDialogue();
            }
            else if ((dialogueText.text == dialogueLines[lineIndex]) && (!isDialogueStarted) || (dialogueText.text == dialogueAcertado[lineIndexAcertado]) && (!isDialogueStarted) || (dialogueText.text == dialogueFallado[lineIndexFallado]) && (!isDialogueStarted))
            {
                NextDialogueLine();
            }
            
            else if (!isDialogueStarted)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
            
        }
        // desbloquear movimiento y camara del personaje
        //player.GetComponent<Movimiento>().estaEnDialogo = false;
    }

    private void StartDialogue()
    {
        // bloquear movimiento y camara del personaje
        player.GetComponent<Movimiento>().estaEnDialogo = true;
        
        puente = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        //Time.timeScale = 0f;
        lineIndexMax = dialogueLines.Length;
        StartCoroutine(ShowLine());
        collider = GameObject.Find("NPC").GetComponent<CapsuleCollider>();
    }

    private void NextDialogueLine()
    {

        if ((lineIndex < dialogueLines.Length) && (!acierto && !error))
        {
            lineIndex++;
            StartCoroutine(ShowLine());
        }
        else
        {

            if (error == true && lineIndexFallado < dialogueFallado.Length - 1)
            {
                lineIndexFallado++;
                StartCoroutine(ShowLine());

            }
            else if (acierto == true && lineIndexAcertado < dialogueAcertado.Length - 1)
            {
                lineIndexAcertado++;
                StartCoroutine(ShowLine());

            }
            else if (lineIndexAcertado >= dialogueAcertado.Length - 1 || lineIndexFallado >= dialogueFallado.Length - 1)
            {
                isDialogueStarted = true;
                //puente = false;
                dialoguePanel.SetActive(false);

                // Time.timeScale = 1f;

                // desbloquear movimiento y camara del personaje
                player.GetComponent<Movimiento>().estaEnDialogo = false;

                typingTime = 0.05f;
                acierto = false;
                error = false;
            }
        }
        if (lineIndex == dialogueLines.Length - 1)
        {
            BotonRespuesta1.SetActive(true);
            BotonRespuesta2.SetActive(true);
            BotonRespuesta3.SetActive(true);
            BotonRespuesta4.SetActive(true);
            typingTime = 0f;
        }

    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        if (!error && !acierto)
        {
            foreach (char ch in dialogueLines[lineIndex])
            {
                dialogueText.text += ch;
                yield return new WaitForSecondsRealtime(typingTime);
            }

        }
        else if (error)
        {
            foreach (char ch in dialogueFallado[lineIndexFallado])
            {
                dialogueText.text += ch;
                yield return new WaitForSecondsRealtime(typingTime);
            }

        }
        else if (acierto)
        {
            foreach (char ch in dialogueAcertado[lineIndexAcertado])
            {
                dialogueText.text += ch;
                yield return new WaitForSecondsRealtime(typingTime);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Se puede iniciar el diálogo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("No se puede iniciar el diálogo");
        }
    }

    public void acertado()
    {
        
        // hacemos invisible el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        acierto = true;
        lineIndexAcertado = -1;
        lineIndex--;
        Debug.Log("Clickado");
        NextDialogueLine();

        BotonRespuesta1.SetActive(false);
        BotonRespuesta2.SetActive(false);
        BotonRespuesta3.SetActive(false);
        BotonRespuesta4.SetActive(false);
        
        typingTime = 0.05f;
        collider.enabled = false;
        
        
    }

    public void fallado()
    {
        
        // hacemos invisible el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        error = true;
        lineIndexFallado = -1;
        lineIndex--;
        Debug.Log("Clickado fallado");
        NextDialogueLine();

        BotonRespuesta1.SetActive(false);
        BotonRespuesta2.SetActive(false);
        BotonRespuesta3.SetActive(false);
        BotonRespuesta4.SetActive(false);

        typingTime = 0.05f;
    }
}