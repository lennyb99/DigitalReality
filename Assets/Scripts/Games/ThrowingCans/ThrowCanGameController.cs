using UnityEngine;

public class ThrowCanGameController : MonoBehaviour
{
    [Header("Gameplay Elements")]
    [SerializeField] private TafelManager scoreBoardManager;
    [SerializeField] private DosenManager canManager;

    
    

    public void StartGame()
    {
        ResetBalls();
    }

    private void ResetBalls()
    {
        
    }


}
