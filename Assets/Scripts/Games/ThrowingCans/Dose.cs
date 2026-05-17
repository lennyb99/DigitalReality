using System;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class Dose : MonoBehaviour
{
    private Rigidbody rb;
    public bool umgekippt;

    [SerializeField] private EventReference tippedSoundEvent;
    [SerializeField] private StudioEventEmitter canImpactSound;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        umgekippt = false;

        StartCoroutine(CheckRotation());
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 1f) {
            canImpactSound.Play();
        }
    }

    IEnumerator CheckRotation()
    {
        while (true)
        {
            CheckIfTipped();
            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void CheckIfTipped()
    {
        if (rb.linearVelocity.magnitude < 0.1f && Vector3.Angle(transform.up, Vector3.up) > 45)
        {
            umgekippt = true;
            if(DosenManager.Instance != null) DosenManager.Instance.NotifyDosenKipp();
        }
    }

}
