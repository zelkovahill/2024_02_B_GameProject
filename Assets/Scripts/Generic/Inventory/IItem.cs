// 모든 아이템의 기본 인터페이스
// 메소드 이벤트 인덱서 프로퍼티
// 모든이 무조건 public 으로 선언
// 구현부 X


using UnityEngine;
using System.Diagnostics;

public interface IItem
{
    string Name { get; }
    int ID { get; }

    void Use();

}

// CraftingMaterial 클래스 추가
public class CraftingMaterial : IItem
{
    public string Name { get; set; }
    public int ID { get; set; }

    public CraftingMaterial(string name, int id)
    {
        Name = name;
        ID = id;
    }

    public void Use()
    {
        UnityEngine.Debug.Log($"This is a crafting material : {Name}");
    }

}