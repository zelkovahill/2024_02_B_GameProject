using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public class Quest
    {
        public string Id { get; set; }                  // 퀘스트 고유 식별자
        public string Title { get; set; }               // 퀘스트 제목
        public string Description { get; set; }         // 퀘스트의 상세 설명
        public QuestType Type { get; set; }             // 퀘스트 유형
        public QuestStatus Status { get; set; }         // 퀘스트 현재 상태
        public int Level { get; set; }                  // 퀘스트 요구 레벨

        private List<IQuestCondition> conditions;       // 퀘스트 완료 조건 목록
        private List<IQuestReward> rewards;             // 퀘스트 보상 목록
        private List<string> prerequisiteQuestIds;      // 선행 퀘스트 ID 목록

        // 쿠ㅐ스트 초기화 생성자
        public Quest(string id, string title, string description, QuestType type,int level)
        {
            Id = id;
            Title = title;
            Description = description;
            Type = type;
            Status = QuestStatus.NotStarted;
            Level = level;

            this.conditions           = new List<IQuestCondition>();
            this.rewards              = new List<IQuestReward>();
            this.prerequisiteQuestIds = new List<string>();
        }

        public List<IQuestCondition> GetConditions()
        {
            return conditions;
        }

        // 퀘스트에 완료 조건을 추가하는 메서드
        public void AddCondition(IQuestCondition condition)
        {
            conditions.Add(condition);
        }

        // 퀘스트에 보상을 추가하는 메서드
        public void AddReward(IQuestReward reward)
        {
            rewards.Add(reward);
        }

        // 퀘스트를 시작하는 메서드
        public void Start()
        {
            if(Status == QuestStatus.NotStarted)
            {
                Status = QuestStatus.InProgress;

                foreach(var condition in conditions)
                {
                    condition.Initialize();
                }
            }
        }

        // 퀘스트 완료 조건을 검사하는 메서드
        public bool CheckCompletion()
        {
            if(Status != QuestStatus.InProgress)
                return false;

            return conditions.All(c => c.IsMet());
        }

        // 퀘스트를 완료하고 보상을 지급하는 메서드
        public void Complete(GameObject player)
        {
            if(Status != QuestStatus.InProgress)
                return;

            if (!CheckCompletion())
                return;

            foreach(var reward in rewards)
            {
                reward.Grant(player);
            }

            Status = QuestStatus.Completed;
        }

        // 퀘스트의 전체 진행도를 계산하는 메서드
        public float GetProgress()
        {
            if(conditions.Count == 0)
                return 0;

            return conditions.Average(c => c.GetProgress());
        }


        // 모든 퀘스트 조건의 설명을 가져오는 메서드
        public List<string> GetConditionDescriptions()
        {
            return conditions.Select(c=>c.GetDescription()).ToList();
        }

        // 모든 퀘스트 보상의 설명을 가져오는 메서드
        public List<string> GetRewardDescriptions()
        {
            return rewards.Select(r=>r.GetDescription()).ToList();
        }
    }

}
