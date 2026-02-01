using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward, 500f * Time.deltaTime, Space.Self);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Enemy") )
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!collision.gameObject.CompareTag("Enemy") )
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
