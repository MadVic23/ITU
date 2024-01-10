using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonesPausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;

    [Header("Menús")]
    public GameObject opciones;
    public GameObject controles;

    [Header("Números")]
    public TMP_Text sensibilidadT;
    public TMP_Text velocidadT;

    public GameObject bloquear;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sensibilidadT.text = (player.GetComponent<Movimiento>().sensibilidad.x).ToString();
        velocidadT.text = (player.GetComponent<Movimiento>().walkSpeed).ToString();

    }

    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<Movimiento>().bloqueado = false;
    }

    public void opcionesButton()
    {
        opciones.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void atrasButton()
    {
        opciones.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void exitButton()
    {
        Debug.Log("saliendo...");
        Application.Quit();
    }

    public void aceptarControles()
    {
        controles.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        bloquear.GetComponent<tutorial>().aceptar = true;
        player.GetComponent<Movimiento>().bloqueado = false;
    }

    public void aumentarSen()
    {
        Movimiento movimiento = player.GetComponent<Movimiento>();

        Vector2 nuevaSensibilidad = movimiento.sensibilidad + new Vector2(0.25f, 0.25f);
        nuevaSensibilidad.x = Mathf.Clamp(nuevaSensibilidad.x, 0.25f, 4f);
        nuevaSensibilidad.y = Mathf.Clamp(nuevaSensibilidad.y, 0.25f, 4f);

        movimiento.sensibilidad = nuevaSensibilidad;

        Debug.Log("sensibilidad = " + movimiento.sensibilidad);
    }

    public void disminuirSen()
    {
        Movimiento movimiento = player.GetComponent<Movimiento>();

        Vector2 nuevaSensibilidad = movimiento.sensibilidad - new Vector2(0.25f, 0.25f);
        nuevaSensibilidad.x = Mathf.Clamp(nuevaSensibilidad.x, 0.25f, 4f);
        nuevaSensibilidad.y = Mathf.Clamp(nuevaSensibilidad.y, 0.25f, 4f);

        movimiento.sensibilidad = nuevaSensibilidad;

        Debug.Log("sensibilidad = " + movimiento.sensibilidad);
    }

    public void aumentarVel()
    {
        player.GetComponent<Movimiento>().walkSpeed++;
        player.GetComponent<Movimiento>().walkSpeed = Mathf.Clamp(player.GetComponent<Movimiento>().walkSpeed, 2.0f, 11.0f);
        Debug.Log("velocidad = " + player.GetComponent<Movimiento>().walkSpeed);
    }

    public void disminuirVel()
    {
        player.GetComponent<Movimiento>().walkSpeed--;
        player.GetComponent<Movimiento>().walkSpeed = Mathf.Clamp(player.GetComponent<Movimiento>().walkSpeed, 2.0f, 11.0f);
        Debug.Log("velocidad = " + player.GetComponent<Movimiento>().walkSpeed);
    }
}
