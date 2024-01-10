using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject player;
    public bool aceptar = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        while (!aceptar)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.GetComponent<Movimiento>().bloqueado = true;
            aceptar = true;
        }

    }
}
