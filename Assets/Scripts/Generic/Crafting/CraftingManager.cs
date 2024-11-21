using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.CraftingSystem
{
    public class CraftingManager : MonoBehaviour
    {
        private static CraftingManager instance;

        public static CraftingManager Instance => instance;

        private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
        private Inventory<IItem> playerInventory;
        private InventoryManager inventoryManager;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // InventoryManager 찾기
            inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                playerInventory = inventoryManager.GetInventory();
            }
            else
            {
                Debug.LogError("InventoryManager no found!");
            }

            CreateSwordRecipe();
            CreatePotionRecipe();
        }

        private void CreateSwordRecipe()
        {
            var ironSword = new Weapon("Iron Sword", 1001, 10);
            var recipe = new Recipe("RECIPE_IRON_SWORD", ironSword, 1);
            recipe.AddRequirdMaterial(101, 2);  // Iron Ingot x 2
            recipe.AddRequirdMaterial(102, 1);  // Wood x 1
            recipes.Add(recipe.recipeId, recipe);
        }

        private void CreatePotionRecipe()
        {
            var healthPoint = new HealthPotion("Health Potin", 2001, 50);
            var recipe = new Recipe("RECIPE_HEALTH_POTION", healthPoint, 1);
            recipe.AddRequirdMaterial(201, 2);      // Herb x 2
            recipe.AddRequirdMaterial(202, 1);      // Water x 1
            recipes.Add(recipe.recipeId, recipe);
        }

        // 재료 확인 함수
        public bool TryCraft(string recipeId)
        {
            if (!recipes.TryGetValue(recipeId, out Recipe recipe))
            {
                return false;
            }

            if (!CheckMaterials(recipe))
            {
                return false;
            }

            ConsumeMaterials(recipe);
            CreateResult(recipe);
            return true;
        }

        // 재료 확인 함수
        private bool CheckMaterials(Recipe recipe)
        {
            playerInventory = inventoryManager.GetInventory();

            foreach (var material in recipe.requiredMaterials)
            {
                if (!playerInventory.HasEnough(material.Key, material.Value))
                {
                    return false;
                }
            }

            return true;
        }

        // 조합시 레시피에 있는 필요 아이템을 제거
        private void ConsumeMaterials(Recipe recipe)
        {
            foreach (var material in recipe.requiredMaterials)
            {
                playerInventory.RemoveItems(material.Key, material.Value);
            }
        }


        private void CreateResult(Recipe recipe)
        {
            playerInventory.AddItem(recipe.resultItem);
        }

        // 가능한 레시피 리턴하는 함수
        public List<Recipe> GetAvailableRecipes()
        {
            return new List<Recipe>(recipes.Values);
        }


    }
}
