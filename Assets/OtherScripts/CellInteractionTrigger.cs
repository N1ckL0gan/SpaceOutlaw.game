using UnityEngine;
using TMPro;

public class CellInteractionTrigger : MonoBehaviour
{
    public GameObject[] objectsToHide;
    public GameObject collisionObject;
    public TextMeshProUGUI interactionPrompt;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerAction();
            interactionPrompt.gameObject.SetActive(false); // Hide prompt after use
        }
    }

    void TriggerAction()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                Renderer rend = obj.GetComponent<Renderer>();
                if (rend != null) rend.enabled = false;
            }
        }

        if (collisionObject != null)
        {
            Collider col = collisionObject.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionPrompt.gameObject.SetActive(false);
        }
    }
}
