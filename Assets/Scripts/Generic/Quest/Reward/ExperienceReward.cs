using UnityEngine;

namespace MyGame.QuestSystem
{
    // ����ġ ������ �����ϴ� Ŭ����
    public class ExperienceReward : IQuestReward
    {
        private int experienceAmount;       // �������� ������ ����ġ��


        // ����ġ ���� �ʱ�ȭ ������
        public ExperienceReward(int amount)
        {
            this.experienceAmount = amount;
        }

        public void Grant(GameObject player)
        {
            // TODO : ���� ����ġ ���� ���� ����
            Debug.Log($"Granted {experienceAmount} experience");
        }

        public string GetDescription() => $"{experienceAmount} Experience Points";  // ���� ������ ���ڿ��� ��ȯ
    }


}
