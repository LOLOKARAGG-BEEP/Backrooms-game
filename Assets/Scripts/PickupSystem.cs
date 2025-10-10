using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 4f; 
    private IPickUp pickUp = null;    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green, 1f);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                IPickUp Up = hit.collider.GetComponent<IPickUp>();
                if (Up != null)
                {
                    bool Pick = Up.PickUp(transform);
                    if (Pick)
                    {
                        pickUp = Up;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(pickUp != null)
            {
                pickUp.Drop();
                pickUp = null;
            }          
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(pickUp != null)
            {
                IUsable usable = pickUp as IUsable;
                if (usable != null)
                {
                    bool Ret = usable.Use();
                    if(!Ret)
                    pickUp = null;
                }
            }            
        }
    }
}
