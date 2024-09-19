using System;
using UnityEngine;

public class SimpleCallback : MonoBehaviour
{
    private Action greetingAction;  // 액션 선언


    // Start is called before the first frame update
    void Start()
    {
        greetingAction = SayHello;        // Action 함수 할당
        PerformGreeting(greetingAction);
    }

    private void SayHello()
    {
        print("Hello, World!");
    }

    private void PerformGreeting(Action greetingFunc)
    {
        greetingFunc?.Invoke();
    }
}
