using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologo : MonoBehaviour
{
    public float tiempo_start = 0f; //Los segundos por los quales comienza i la variable que utilizaremos para que vaya contando segundos.
    public float tiempo_end = 42f; //Segundos que queremos que pasen para que cambie de escena

    void Update()
    {
        tiempo_start += Time.deltaTime;//Función para que la variable tiempo_start vaya contando segundos.
        if (tiempo_start >= tiempo_end) //Si pasan los segundos que hemos puesto antes...
        {
            SceneManager.LoadScene(2);
        }
    }
}
