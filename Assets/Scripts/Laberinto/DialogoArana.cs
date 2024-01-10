using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject BotonRespuesta1;
    [SerializeField] private GameObject BotonRespuesta2;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField, TextArea(4, 6)] private string[] ayudaLines;
    [SerializeField, TextArea(4, 6)] private string[] noAyudaLines;


    //[SerializeField] TextMeshProUGUI textResUno;
    //[SerializeField] TextMeshProUGUI textResDos;
    //[SerializeField] TextMeshProUGUI textResTres;

    //[SerializeField] PlantillaDialogo plantilla;
    //[SerializeField] PlantillaDialogo[] arrayPlantillas;


    public GameObject puerta;
    public GameObject player;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool isDialogueStarted;
    private int lineIndex;
    public GameObject canvas;
    public GameObject bloqueo;
    private bool heHablado = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !heHablado)
        {
            // mostrar cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (!isDialogueStarted)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                StopAllCoroutines();

                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueStarted = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            isDialogueStarted = false;
            dialoguePanel.SetActive(false);
            BotonRespuesta1.SetActive(false);
            BotonRespuesta2.SetActive(false);

            Time.timeScale = 1f;
            typingTime = 0.05f;
        }
        if (lineIndex == dialogueLines.Length - 1)
        {
            BotonRespuesta1.SetActive(true);
            BotonRespuesta2.SetActive(true);

            typingTime = 0f;
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

    public void Ayuda()
    {
        player.GetComponent<Movimiento>().puntuacionKarma += 5;
        lineIndex = 0;
        StartCoroutine(ShowAyuda());
        BotonRespuesta1.SetActive(false);
        BotonRespuesta2.SetActive(false);

        heHablado = true;

        player.GetComponent<Movimiento>().ayuda = true;

        canvas.SetActive(true);

        Destroy(bloqueo);
    }
    public void noAyuda()
    {
        player.GetComponent<Movimiento>().puntuacionKarma -= 5;
        lineIndex = 0;
        StartCoroutine(ShowNoAyuda());
        BotonRespuesta1.SetActive(false);
        BotonRespuesta2.SetActive(false);

        heHablado = true;

        player.GetComponent<Movimiento>().ayuda = false;
        Destroy(puerta);
        Destroy(bloqueo);
    }

    private IEnumerator ShowNoAyuda()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in noAyudaLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        yield return new WaitForSecondsRealtime(3f);

        isDialogueStarted = false;
        dialoguePanel.SetActive(false);

        Time.timeScale = 1f;
        typingTime = 0.05f;
    }

    private IEnumerator ShowAyuda()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in ayudaLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        yield return new WaitForSecondsRealtime(3f);

        isDialogueStarted = false;
        dialoguePanel.SetActive(false);

        Time.timeScale = 1f;
        typingTime = 0.05f;
    }
}
