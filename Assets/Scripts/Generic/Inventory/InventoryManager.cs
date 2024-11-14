using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory<IItem> playerInventory = new Inventory<IItem>();
    public int UseBagindex;

    private void Start()
    {
        playerInventory = new Inventory<IItem>();

        // 아이템 추가
        playerInventory.AddItem(new Weapon("Sword", 1, 10));
        playerInventory.AddItem(new HealthPotion("Small Potion", 2, 10));

        playerInventory.AddItem(new CraftingMaterial("Iron Ingot", 101));       // ID 101 : 철 주괴
        playerInventory.AddItem(new CraftingMaterial("Iron Ingot", 101));       // ID 101 : 철 주괴
        playerInventory.AddItem(new CraftingMaterial("Wood", 102));             // ID 102 : 니무

        playerInventory.AddItem(new CraftingMaterial("Herb", 201));         // ID 201 : 약초
        playerInventory.AddItem(new CraftingMaterial("Herb", 201));         // ID 201 : 약초
        playerInventory.AddItem(new CraftingMaterial("Water", 202));        // ID 202 : 물
    }

    // 인벤토리 접근자 메서드 추가
    public Inventory<IItem> GetInventory()
    {
        return playerInventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerInventory.ListItems();        // 인벤토리 내용 출력
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInventory.UseItem(UseBagindex);         // 첫번째 아이템 사용
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerInventory.AddItem(new Weapon("Sword", 1, 10));         // 첫번째 아이템 사용
        }


    }

}
