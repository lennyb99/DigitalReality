using FMODUnity;
using UnityEngine;

public class HighStrikerBell : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter bellSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HighStrikerWeight"))
        {
            if (other.gameObject.GetComponent<Rigidbody>().linearVelocity.y > 0) // Upwards movement
            {
                PlayBell();
            }
        }
    }

    public void PlayBell()
    {
        if (bellSound.IsPlaying()) bellSound.Stop();
        bellSound.Play();
    }
}
