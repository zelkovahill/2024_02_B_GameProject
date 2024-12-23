using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
   // 퀘스트의 현재 상태를 나타내는 열거형
   public enum QuestStatus
    {
        NotStarted,         // 퀘스트가 아직 시작되지 않은 상태
        InProgress,         // 퀘스트가 현재 진행 중인 상태
        Completed,          // 퀘스트가 완료된 상태
        Failed              // 퀘스트가 실패한 상태
    }

    // 퀘스트의 유형을 구분하는 열거형
    public enum QuestType
    {
        Collection,         // 아이템을 수집하는 퀘스트
        Kill,               // 몬스터를 처치하는 퀘스트
        Dialog,             // NPC와 대화하는 퀘스트
        Exploration,        // 특정 지역을 탐험하는 퀘스트
        Escort,             // NPC를 보호/호위 하는 퀘스트
    }


}
