using UnityEngine;

public class PushZone : MonoBehaviour
{
    public Vector3 force = new Vector3(0, 0, 10);

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb) rb.AddForce(force);
    }
}
