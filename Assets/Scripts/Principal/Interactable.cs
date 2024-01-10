using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerInRange = false;
    [SerializeField] public GameObject botonInteract;
    [SerializeField] public GameObject text;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange){
            botonInteract.SetActive(true);
            text.SetActive(true);
        }
        else{
            botonInteract.SetActive(false);
            text.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            
        }
    }
}
