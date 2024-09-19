using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{


    // 활성화될 때 이벤트 등록
    private void OnEnable() 
    {
        EventSystem.OnScoreChanged += UpdateScore;
        EventSystem.OnGameOver += ShowGameOver;
    }

    // 비활성화될때 이벤트 해제
    private void OnDisable()
    {
        EventSystem.OnScoreChanged -= UpdateScore;
        EventSystem.OnGameOver -= ShowGameOver;
    }

    private void UpdateScore(int newScore)
    {
        print($"Score update : {newScore}");
    }

    private void ShowGameOver()
    {
        print("Game Over!");
    }
}
