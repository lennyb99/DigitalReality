using System.Collections;
using UnityEngine;
using FMODUnity;

public class Dose : MonoBehaviour
{
    private Rigidbody rb;
    public bool umgekippt;

    [SerializeField] private EventReference tippedSoundEvent;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        umgekippt = false;

        StartCoroutine(CheckRotation());
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
