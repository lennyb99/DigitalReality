using System;
using UnityEngine;

public class HighStrikerWeight : MonoBehaviour
{
    private HighStrikerManager manager;

    private Rigidbody rb;

    public bool shouldObserveTravel = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (shouldObserveTravel && transform.position.y > 0.27)
        {
            manager.HandleNewHeight();
            if (rb.linearVelocity.y <= 0.01f)
            {
                manager.HandlePeak();
                shouldObserveTravel = false;
            }
        }
    }

    public void ApplyForce(float force)
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void RegisterManager(HighStrikerManager man) 
    {
        manager = man;
    }
}
