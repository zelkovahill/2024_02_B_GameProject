using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MyGame.CraftingSystem
{
    public class CraftingUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        private Transform recipeContent;

        [SerializeField]
        private GameObject recipeButtonPrefabs;

        [SerializeField]
        private TextMeshProUGUI selectedRecipeInfo;

        [SerializeField]
        [Tooltip("크래프트 버튼")]
        private Button craftingButton;

        private CraftingManager craftingManager;
        private Recipe selectedRecipe;

        private void Start()
        {
            craftingManager = CraftingManager.Instance;
            craftingButton.onClick.AddListener(OnCraftingButtonClick);

            RefreshRecipeList();
        }

        private void RefreshRecipeList()
        {
            // 기존 목록 제거
            foreach (Transform child in recipeContent)
            {
                Destroy(child.gameObject);
            }

            // 새 목록 생성
            foreach (var recipe in craftingManager.GetAvailableRecipes())
            {
                CreateRecipeButton(recipe);
            }
        }

        private void CreateRecipeButton(Recipe recipe)
        {
            GameObject buttonObject = Instantiate(recipeButtonPrefabs, recipeContent);
            Button button = buttonObject.GetComponent<Button>();
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();

            text.text = recipe.resultItem.Name;
            button.onClick.AddListener(() => SelectRecipe(recipe));
        }

        private void SelectRecipe(Recipe recipe)
        {
            selectedRecipe = recipe;
            UpdateRecipeInfo();
        }

        private void UpdateRecipeInfo()
        {
            if (selectedRecipe == null)
            {
                selectedRecipeInfo.text = "Select a recipe";
                craftingButton.interactable = false;
                return;
            }

            string info = $"Recipe : {selectedRecipe.resultItem.Name} \n\n Required Materials : \n";

            foreach (var material in selectedRecipe.requiredMaterials)
            {
                info += $" - item ID {material.Key} : {material.Value} \n";
            }

            selectedRecipeInfo.text = info;
            craftingButton.interactable = true;
        }

        private void OnCraftingButtonClick()
        {
            if (selectedRecipe != null)
            {
                if (craftingManager.TryCraft(selectedRecipe.recipeId))
                {
                    Debug.Log($"조합 성공 {selectedRecipe.recipeId}");
                }
                else
                {
                    Debug.Log("조합 실패");
                }
            }
        }
    }
}