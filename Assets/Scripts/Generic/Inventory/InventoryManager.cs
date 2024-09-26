using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory<IItem> playerInventory;
    public int UseBagindex;

    private void Start()
    {
        playerInventory = new Inventory<IItem>();

        // 아이템 추가
        playerInventory.AddItem(new Weapon("Sword", 1, 10));
        playerInventory.AddItem(new HealthPotion("Small Potion", 2, 10));


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
