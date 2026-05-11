using FMODUnity;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WhackAMoleManager : MonoBehaviour
{
    [SerializeField] private int gameTime;
    [SerializeField] public float moleMinActiveTime;
    [SerializeField] public float moleMaxActiveTime;
    [SerializeField] private float moleFrequencyInSeconds;

    [SerializeField] private List<Mole> moles;

    [SerializeField] private GameObject startButton;

    [Header("Text Visuals")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    
    [SerializeField] private ParticleSystem gameWinPS;
    [SerializeField] private StudioEventEmitter gameWinSound;
    
    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter playSound;

    public bool playActive;

    private int _timerValue;
    public int TimerValue
    {
        get { return _timerValue; }
        set { _timerValue = value; }
    }

    private int _score;
    public int Score
    {
        get { return _score; }
        set { 
            _score = value;
            scoreText.text = value.ToString();
        }
    }
    private int _highScore;
    public int HighScore
    {
        get { return _highScore; }
        set { 
            _highScore = value; 
            highscoreText.text = value.ToString();
        }
    }

    private void Start()
    {
        SetupEverything();

        Score = 0;
        HighScore = 0;

    }
    
    private void SetupEverything()
    {
        SetupMoles();
        SetupStartButton();
    }

    private void SetupMoles()
    {
        foreach (Mole mole in moles)
        {
            mole.RegisterManager(this);
        }
    }

    private void SetupStartButton()
    {
        var prox = startButton.GetComponent<CollisionProxy>();
        if (prox != null)
        {
            prox.OnCustomTriggerEnter += CheckUpForStart;
        }
    }

    private Mole GetRandomInactiveMole()
    {
        List<Mole> tempList = new List<Mole>();
        foreach(Mole m in moles)
        {
            if (!m.Active)
            {
                tempList.Add(m);
            }
        }
        if (tempList.Count <= 0)
        {
            return null;
        }
        int randIndex = Random.Range(0, tempList.Count);
        return tempList[randIndex];
    }

    public void CheckUpForStart(Collider other)
    {
        if (playActive || !other.gameObject.CompareTag("Hammer")) return;

        AnimateButton();
        StartCoroutine(InitiateStartSequence());
    }

    private void AnimateButton()
    {
        startButton.GetComponent<Animator>().SetTrigger("ButtonPressed");
        startButton.GetComponent<StudioEventEmitter>().Play();
    }

    IEnumerator InitiateStartSequence()
    {
        playActive = true;
        playSound.Play();

        Score = 0;

        yield return new WaitForSeconds(3); // Wait for countdown, music will already be playing

        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(RunGame());
        StartCoroutine(RunTimer());
    }

    IEnumerator RunGame()
    {
        while (playActive)
        {
            Mole m = GetRandomInactiveMole();

            if (m == null)
            {
                yield return new WaitForSeconds(moleFrequencyInSeconds);
                continue;
            }
            m.ActivateMole();


            yield return new WaitForSeconds(moleFrequencyInSeconds);
        }
    }

    IEnumerator RunTimer()
    {
        TimerValue = gameTime;

        while (TimerValue >= 0)
        {
            TimerValue -= 1;
            yield return new WaitForSeconds(1);
        }
        StopGame();
    }

    private void StopGame()
    {
        gameWinPS.Play();
        gameWinSound.Play();
        
        if (Score > HighScore) HighScore = Score;
        playActive = false;
        
        foreach (Mole mole in moles)
        {
            mole.InactivateMole();
        }
    
    }

    public void IncrementCounter()
    {
        if (playActive)
        {
            Score += 1;
        }
        
    }

}
