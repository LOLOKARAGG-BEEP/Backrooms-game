using UnityEngine;

public class EdibleItem : MonoBehaviour
{
    public float restoreAmount = 20f;
    public AudioClip pickupSound;
    public AudioClip eatSound;

    private bool isHeld = false;
    private Transform originalParent;

    public void PickUp(Transform hand)
    {
        isHeld = true;
        originalParent = transform.parent;
        transform.SetParent(hand);
        transform.localPosition = new Vector3(0.4f, -0.5f, 1f);
        transform.localRotation = Quaternion.identity;

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, hand.position);
    }

    public void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;

        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
    }

    public void Use()
    {
        PlayerStats stats = Camera.main.GetComponentInParent<PlayerStats>();
        if (stats != null)
        {
            stats.RestoreStability(restoreAmount);
        }

        if (eatSound != null)
            AudioSource.PlayClipAtPoint(eatSound, transform.position);

        Destroy(gameObject); 
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
