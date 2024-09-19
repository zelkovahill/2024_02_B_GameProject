using UnityEngine;

public class SimpleGeneric : MonoBehaviour
{
    private void Start()
    {
        PrintValue(42);                 // int32
        PrintValue("������ �ʹ�");      // string
        PrintValue(3.14f);              // single (float)
        PrintValue('��');               // char
    }

    private void PrintValue<T>(T value)
    {
        print($"Value : {value}, Type : {typeof(T)}");
    }
    
}
