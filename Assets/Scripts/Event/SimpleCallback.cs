using System;
using UnityEngine;

public class SimpleCallback : MonoBehaviour
{
    private Action greetingAction;  // �׼� ����


    // Start is called before the first frame update
    void Start()
    {
        greetingAction = SayHello;        // Action �Լ� �Ҵ�
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
