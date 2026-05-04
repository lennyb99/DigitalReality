using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WhackAMoleManager : MonoBehaviour
{
    [SerializeField] private int gameTime;
    [SerializeField] private int moleTime;
    [SerializeField] private float moleFrequencyInSeconds;

    [SerializeField] private List<Mole> moles;

    [Header("Text Visuals")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;

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
            if (value > HighScore) HighScore = value;
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
        foreach (Mole mole in moles)
        {
            mole.RegisterManager(this);
        }

        Score = 0;
        HighScore = 0;

        StartGame();
    }

    private void Update()
    {
          
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

    public void StartGame()
    {
        playActive = true;
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
        playActive = false;
    }

    public void IncrementCounter()
    {
        Score += 1;
    }

    public float GetMoleTime()
    {
        return moleTime;
    }
}
