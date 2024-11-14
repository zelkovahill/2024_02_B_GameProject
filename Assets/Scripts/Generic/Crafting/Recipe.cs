using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.CraftingSystem
{

    [System.Serializable]
    public class Recipe
    {
        public string recipeId;                         // 레시피 고유 ID
        public IItem resultItem;                        // 결과 아이템
        public int resultAmount;                        // 제작 개수
        public Dictionary<int, int> reqyiredMaterials;  // 필요 재료 <아이템 ID, 수량>
        public int requiredLevel;                       // 요구 제작 레벨
        public float basdeSuccessRate;                  // 기본 성공 확률
        public float craftTime;                         // 제작 시간


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
