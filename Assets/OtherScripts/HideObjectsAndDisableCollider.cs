using UnityEngine;

public class HideObjectsAndDisableCollider : MonoBehaviour
{
    public GameObject[] objectsToHide;      // Assign your 8 SingleLine objects here
    public GameObject collisionObject;      // Assign the "Collision" object here

    public void OnButtonPress()
    {
        // Hide each object's renderer
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                Renderer rend = obj.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.enabled = false;
                }
            }
        }

        // Disable the collider on the collision object
        if (collisionObject != null)
        {
            Collider col = collisionObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }
}
