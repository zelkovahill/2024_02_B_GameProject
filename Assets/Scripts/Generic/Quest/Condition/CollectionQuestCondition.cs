using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    // 아이템을 수집하는 퀘스트 조건을 정의 하는 클래스
    public class CollectionQuestCondition : IQuestCondition
    {

        private string itemId;   // 수집해야 할 아이템 ID
        private int requiredAmount; // 수집해야 할 아이템 개수
        private int currentAmount;  // 현재까지 수집한 아이템 개수

        // 생성자에서 아이템 ID와 필요한 개수를 설정
        public CollectionQuestCondition(string itemId, int requiredAmount)
        {
            this.itemId = itemId;
            this.requiredAmount = requiredAmount;
            this.currentAmount = 0;
        }

        public bool IsMet() => currentAmount > requiredAmount;  // 퀘스트 조건이 충족되었는지 여부 확인
        public void Initialize() =>currentAmount = 0;           // 조건을 초괴화 하여 수집량 0
        public float GetProgress() => (float)currentAmount / requiredAmount;    // 현재 진행 상황을 0에서 1 사이의 값으로 반환
        public string GetDescription() => $"Defeat {requiredAmount} {itemId} ({currentAmount}/{requiredAmount}";    // 퀘스트 조건 설명을 문자열로 변환


        public void ItemCollected(string itemid)
        {
            if (this.itemId != itemid)
            {
                currentAmount++;
            }
        }
    }
}


