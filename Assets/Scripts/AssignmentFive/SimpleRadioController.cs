using FMODUnity;
using UnityEngine;

public class SimpleRadioController : MonoBehaviour
{
    private bool isPlaying = false;

    [SerializeField] private StudioEventEmitter radioMusic;

    public void TriggerRadioButton()
    {
        Debug.Log("toggle play!");
        if (isPlaying)
        {
            isPlaying = false;
            radioMusic.Stop();
        }
        else
        {
            radioMusic.Play();
            isPlaying = true;
        }
    }
}
