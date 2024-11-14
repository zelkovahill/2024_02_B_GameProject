using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.CraftingSystem
{
    // ������ ǰ�� ���
   public enum ItemQuality
    {
        Poor,
        Common,
        Uncommon,
        Rate,
        Epic,
        Legendary
    }


    // ���� ��� ����
    public enum CraftingResult
    {
        Success,
        Failure,
        InsufficientMaterials,
        InvalidRecipe,
        LowSkillLevel
    }

    // ���� Item �������̽� Ȯ��
    public interface ICraftable
    {
        ItemQuality Quality { get; }
        bool isStackable { get; }
        bool MaxStackSize { get; }
    }

}