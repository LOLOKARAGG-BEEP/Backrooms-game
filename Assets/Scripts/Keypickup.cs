using UnityEngine;

public class KeyPickup : MonoBehaviour, IPickUp, IUsable
{
    private bool isHeld = false;




    public bool PickUp(Transform parent)
    {
        isHeld = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        GetComponent<Collider>().enabled = false;

        transform.SetParent(parent);
        transform.localPosition = new Vector3(0.4f, -0.5f, 1f);
        transform.localRotation = Quaternion.Euler(0f, -86f, 0f);
        return true;
    }

    public void Drop()
    {
        isHeld = false;
        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;

        rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
    }
    public LayerMask interactableLayer;
    public bool Use()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 4f, interactableLayer))
        {
            DoorTransition door = hit.collider.GetComponent<DoorTransition>();
            if (door != null)
            {
                door.OpenDoor();
                Destroy(gameObject);             
            }
        }
        return false;
    }
}
