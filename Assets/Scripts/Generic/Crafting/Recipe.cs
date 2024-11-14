using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.CraftingSystem
{

    [System.Serializable]
    public class Recipe
    {
        public string recipeId;                         // ������ ���� ID
        public IItem resultItem;                        // ��� ������
        public int resultAmount;                        // ���� ����
        public Dictionary<int, int> reqyiredMaterials;  // �ʿ� ��� <������ ID, ����>
        public int requiredLevel;                       // �䱸 ���� ����
        public float basdeSuccessRate;                  // �⺻ ���� Ȯ��
        public float craftTime;                         // ���� �ð�


        public Recipe(string id, IItem result, int amount)
        {
            recipeId = id;
            resultItem = result;
            resultAmount = amount;
            reqyiredMaterials = new Dictionary<int, int>();
            requiredLevel = 1;
            basdeSuccessRate = 1;
            craftTime = 0;
        }

        public void AddRequirdMaterial(int itemId, int amount)
        {
            if (reqyiredMaterials.ContainsKey(itemId))
            {
                reqyiredMaterials[itemId] += amount;
            }
            else
            {
                reqyiredMaterials[itemId] = amount;
            }

        }

    }
}
