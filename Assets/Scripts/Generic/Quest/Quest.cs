using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public class Quest
    {
        public string Id { get; set; }                  // ����Ʈ ���� �ĺ���
        public string Title { get; set; }               // ����Ʈ ����
        public string Description { get; set; }         // ����Ʈ�� �� ����
        public QuestType Type { get; set; }             // ����Ʈ ����
        public QuestStatus Status { get; set; }         // ����Ʈ ���� ����
        public int Level { get; set; }                  // ����Ʈ �䱸 ����

        private List<IQuestCondition> conditions;       // ����Ʈ �Ϸ� ���� ���
        private List<IQuestReward> rewards;             // ����Ʈ ���� ���
        private List<string> prerequisiteQuestIds;      // ���� ����Ʈ ID ���

        // �����Ʈ �ʱ�ȭ ������
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

        // ����Ʈ�� �Ϸ� ������ �߰��ϴ� �޼���
        public void AddCondition(IQuestCondition condition)
        {
            conditions.Add(condition);
        }

        // ����Ʈ�� ������ �߰��ϴ� �޼���
        public void AddReward(IQuestReward reward)
        {
            rewards.Add(reward);
        }

        // ����Ʈ�� �����ϴ� �޼���
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

        // ����Ʈ �Ϸ� ������ �˻��ϴ� �޼���
        public bool CheckCompletion()
        {
            if(Status != QuestStatus.InProgress)
                return false;

            return conditions.All(c => c.IsMet());
        }

        // ����Ʈ�� �Ϸ��ϰ� ������ �����ϴ� �޼���
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

        // ����Ʈ�� ��ü ���൵�� ����ϴ� �޼���
        public float GetProgress()
        {
            if(conditions.Count == 0)
                return 0;

            return conditions.Average(c => c.GetProgress());
        }


        // ��� ����Ʈ ������ ������ �������� �޼���
        public List<string> GetConditionDescriptions()
        {
            return conditions.Select(c=>c.GetDescription()).ToList();
        }

        // ��� ����Ʈ ������ ������ �������� �޼���
        public List<string> GetRewardDescriptions()
        {
            return rewards.Select(r=>r.GetDescription()).ToList();
        }
    }

}
