using System;
using UnityEngine;

public class CollisionProxy : MonoBehaviour
{
    public event Action<Collider> OnCustomTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        OnCustomTriggerEnter?.Invoke(other);
    }
}
