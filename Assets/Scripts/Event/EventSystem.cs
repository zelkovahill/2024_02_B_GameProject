using System;
using UnityEditor.UI;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;     // ���ھ� ��ȯ Action ���
    public static event Action OnGameOver;         // ���� ���� Action ���

    private int score = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            score += 10;

            OnScoreChanged?.Invoke(score);          // ���ھ� ������ ȣ��
        }

        if(score >= 100)
        {
            OnGameOver?.Invoke();              // ���� ������ ȣ��
        }
    }

}
