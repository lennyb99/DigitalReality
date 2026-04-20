using System.Collections;
using UnityEngine;

public class Dose : MonoBehaviour
{
    private Rigidbody rb;

    public bool umgekippt;

    private AudioSource _source;

    public AudioClip soundClip;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Dose has no rigid body!");

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
            DosenManager.Instance.NotifyDosenKipp();
            PlayObjectSound();
        }
    }
    public void PlayObjectSound()
    {
        if (_source != null && !_source.isPlaying)
        {
            _source.PlayOneShot(soundClip);
        }
    }

}
