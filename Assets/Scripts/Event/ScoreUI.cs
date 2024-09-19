using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{


    // Ȱ��ȭ�� �� �̺�Ʈ ���
    private void OnEnable() 
    {
        EventSystem.OnScoreChanged += UpdateScore;
        EventSystem.OnGameOver += ShowGameOver;
    }

    // ��Ȱ��ȭ�ɶ� �̺�Ʈ ����
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
