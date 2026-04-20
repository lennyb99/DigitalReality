using UnityEngine;
using TMPro;

public class TafelManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text dosenToClearText;

    public void SetDosenToClear(int count)
    {
        dosenToClearText.text = count.ToString();
    }
}
