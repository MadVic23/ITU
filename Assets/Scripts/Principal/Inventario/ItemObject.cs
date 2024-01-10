using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;
    bool isPlayerInRange = false;
    [SerializeField] private GameObject collarPanel;
    [SerializeField] private TMP_Text collarText;

    private bool hayTexto;

    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButton(0) && !hayTexto)
        {
            OnHandlePickUp();
        }
        if (Input.GetKeyDown("e") && hayTexto)
        {
            collarPanel.SetActive(false);
            hayTexto = false;
        }
    }

    public void OnHandlePickUp()
    {
        hayTexto = true;
        collarPanel.SetActive(true);
        InventorySystem.Instance.Add(itemData);

        transform.position = new Vector3(transform.position.x, -7f, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("dentro del collar");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Fuera del collar");
        }
    }
}
