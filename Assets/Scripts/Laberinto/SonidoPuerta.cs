using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPuerta : MonoBehaviour
{
    public GameObject player;
    public GameObject puerta;
    private AudioSource sonidoPuerta;
    public GameObject canvas;
    private bool acabado = false;

    // Start is called before the first frame update
    void Start()
    {
        sonidoPuerta = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Movimiento>().manzanasRecolectadas == 6 && !acabado)
        {
            sonidoPuerta.Play();
            Destroy(puerta);
            canvas.SetActive(false);
            acabado = true;
        }
    }
}
