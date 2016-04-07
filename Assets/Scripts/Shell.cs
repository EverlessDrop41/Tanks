using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Shell : MonoBehaviour
{
    public float Speed = 100f;

    private Rigidbody RB;
    public GameObject Explosion;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.velocity = transform.forward * Speed * Time.fixedDeltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
