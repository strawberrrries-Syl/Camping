using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Singleton;
    private TMP_Text liveDisplay;
    public int Score;

    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }

    public static int GetScore()
    {
        return Singleton.GetScoreInternal();
    }

    void Start()
    {
        Singleton = this;
        liveDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        //ScorePointsInternal(0);
    }

    private void ScorePointsInternal(int delta)
    {
        Score += delta;
        liveDisplay.text = $"Score: {Score} / 24 ";
    }

    private int GetScoreInternal()
    {
        return Score;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

