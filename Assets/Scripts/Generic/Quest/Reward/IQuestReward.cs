using UnityEngine;

namespace MyGame.QuestSystem
{
    // ����Ʈ ������ �����ϴ� �⺻ �������̽�
    public interface IQuestReward
    {
        void Grant(GameObject player);  // �÷��̾�� ������ �����ϴ� �Լ�
        string GetDescription(); // ���� ���� ������ ��ȯ�ϴ� �Լ�
    }
}
