using UnityEngine;
using TMPro;
using FMODUnity;

public class TafelManager : MonoBehaviour
{
    [SerializeField] TMP_Text dosenToClearText;
    [SerializeField] private StudioEventEmitter scoreBoardScribbleSound;
    [SerializeField] private StudioEventEmitter canGameWinSound;
    [SerializeField] private ParticleSystem canGameWinPS;
    private int lastCount = -1; 

    public void SetDosenToClear(int count)
    {
        if (count == lastCount) return;
        lastCount = count;
        scoreBoardScribbleSound.Play();
        dosenToClearText.text = count.ToString();
        if (count == 0) {
            canGameWinSound.Play();
            canGameWinPS.Play();
        }
    }
}