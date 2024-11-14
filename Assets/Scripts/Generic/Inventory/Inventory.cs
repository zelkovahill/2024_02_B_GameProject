using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 제네릭 인벤토리 클래스
public class Inventory<T> where T : IItem
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
        items.Add(item);
        Debug.Log($"Add {item.Name} to inventory");
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items[index].Use();
        }
        else
        {
            Debug.Log("Invalid item index");
        }
    }


    public void ListItems()
    {
        foreach (var item in items)
        {
            Debug.Log($"Item : {item.Name} , ID : {item.ID}");
        }
    }

    public void RemoveItems(int itemId, int amount)
    {
        int removed = 0;
        for(int i=items.Count -1; i>=0; i--)
        {
            if (items[i].ID == itemId)
            {
                items.RemoveAt(i);
                removed++;

                if(removed >= amount)
                {
                    break;
                }
            }
        }
    }

    public bool HasEnough(int itemId, int amount)   // 아팀이 충분한지 검사
    {
        return GetItemCount(itemId) >= amount;
    }

    public int GetItemCount(int itemId)             // 아이템 카운트 함수
    {
        return items.Count(item=> item.ID == itemId);
    }

}
