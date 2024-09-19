using UnityEngine;

public class SimpleGeneric : MonoBehaviour
{
    private void Start()
    {
        PrintValue(42);                 // int32
        PrintValue("집가고 싶다");      // string
        PrintValue(3.14f);              // single (float)
        PrintValue('밥');               // char
    }

    private void PrintValue<T>(T value)
    {
        print($"Value : {value}, Type : {typeof(T)}");
    }
    
}
