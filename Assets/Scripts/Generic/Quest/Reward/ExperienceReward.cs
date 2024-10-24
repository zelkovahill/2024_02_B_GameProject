using UnityEngine;

namespace MyGame.QuestSystem
{
    // 경험치 보상을 구현하는 클래스
    public class ExperienceReward : IQuestReward
    {
        private int experienceAmount;       // 보상으로 지급할 경험치량


        // 경험치 보상 초기화 생성자
        public ExperienceReward(int amount)
        {
            this.experienceAmount = amount;
        }

        public void Grant(GameObject player)
        {
            // TODO : 실제 경험치 지급 로직 구현
            Debug.Log($"Granted {experienceAmount} experience");
        }

        public string GetDescription() => $"{experienceAmount} Experience Points";  // 보상 내용을 문자열로 반환
    }


}
