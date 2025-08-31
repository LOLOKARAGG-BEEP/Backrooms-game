using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    private bool isOpening = false;
    public float openAngle = 90f;     
    public float speed = 2f;          

    private Quaternion closedRotation;
    private Quaternion targetRotation;

    void Start()
    {
        closedRotation = transform.rotation;
    }

    public void OpenDoor()
    {
        if (!isOpen && !isOpening)
        {
            isOpening = true;
            targetRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {
                transform.rotation = targetRotation;
                isOpening = false;
                isOpen = true;
            }
        }
    }
}
