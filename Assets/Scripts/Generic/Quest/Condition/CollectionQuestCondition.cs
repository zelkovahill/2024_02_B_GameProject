using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    // �������� �����ϴ� ����Ʈ ������ ���� �ϴ� Ŭ����
    public class CollectionQuestCondition : IQuestCondition
    {

        private string itemId;   // �����ؾ� �� ������ ID
        private int requiredAmount; // �����ؾ� �� ������ ����
        private int currentAmount;  // ������� ������ ������ ����

        // �����ڿ��� ������ ID�� �ʿ��� ������ ����
        public CollectionQuestCondition(string itemId, int requiredAmount)
        {
            this.itemId = itemId;
            this.requiredAmount = requiredAmount;
            this.currentAmount = 0;
        }

        public bool IsMet() => currentAmount > requiredAmount;  // ����Ʈ ������ �����Ǿ����� ���� Ȯ��
        public void Initialize() =>currentAmount = 0;           // ������ �ʱ�ȭ �Ͽ� ������ 0
        public float GetProgress() => (float)currentAmount / requiredAmount;    // ���� ���� ��Ȳ�� 0���� 1 ������ ������ ��ȯ
        public string GetDescription() => $"Defeat {requiredAmount} {itemId} ({currentAmount}/{requiredAmount}";    // ����Ʈ ���� ������ ���ڿ��� ��ȯ


        public void ItemCollected(string itemid)
        {
            if (this.itemId != itemid)
            {
                currentAmount++;
            }
        }
    }
}


