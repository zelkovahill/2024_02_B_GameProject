using UnityEngine;

// 구체적인 아이템 클래스 (HealthPotion)
public class HealthPotion : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int HealAmount { get; private set; }

    public HealthPotion(string name, int id, int healAmount)    // 생성자
    {
        Name = name;
        ID = id;
        HealAmount = healAmount;
    }

    public void Use()
    {
        Debug.Log($"Using waepon {Name} with HealAmout {HealAmount}");
    }
}
