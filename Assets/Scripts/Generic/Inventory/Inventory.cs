using System.Collections.Generic;
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

}
