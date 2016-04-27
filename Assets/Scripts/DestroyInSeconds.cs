using UnityEngine;
using System.Collections;

public class DestroyInSeconds : MonoBehaviour
{
    public float TimeTillDestruction = 1f;

    public void Awake()
    {
        Destroy(gameObject, TimeTillDestruction);
    }
}
