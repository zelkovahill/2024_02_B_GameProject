using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyGame.QuestSystem;
using UnityEngine.UI;

public class SimpleQuestProgressUI : MonoBehaviour
{
    [Header("Quest List")]
    [Tooltip("����Ʈ ����� ǥ�õ� �θ� Transform")]
    [SerializeField]
    private Transform questListParent;

    [Tooltip("����Ʈ UI ������")]
    [SerializeField]
    private GameObject questPrefabs;


    [Header("Pregress Test")]
    [Tooltip("�� óġ �׽�Ʈ ��ư")]
    [SerializeField]
    private Button KillEnemyButton;

    [SerializeField]
    [Tooltip("������ ���� �׽�Ʈ ��ư")]
    private Button CollectItemButton;

    private QuestManager questManager;

    private void Start()
    {
        questManager = QuestManager.Instance;

        // ��ư �̺�Ʈ ����
        KillEnemyButton.onClick.AddListener(OnKillEnemy);
        CollectItemButton.onClick.AddListener(OnCollectItem);

        // �̺�Ʈ ���
        questManager.OnQuestStarted += UpdateQuestUI;
        questManager.OnQuestCompleted += UpdateQuestUI;

        // �ʱ� ����Ʈ ���� ǥ��
        RefreshQuestList();
    }


    // ���� ����Ʈ UI ����
    private void CreateQuestUI(Quest quest)
    {
        GameObject questObj = Instantiate(questPrefabs, questListParent);

        TextMeshProUGUI titleText = questObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI progressText = questObj.transform.Find("ProgressText").GetComponent<TextMeshProUGUI>();

        titleText.text = quest.Title;
        progressText.text = $"Progress : {quest.GetProgress():P0}";
    }


    // ����Ʈ ���� ���� �� UI ������Ʈ
    private void UpdateQuestUI(Quest quest)
    {
        RefreshQuestList();
    }

    // ����Ʈ ��� ���ΰ�ħ
    private void RefreshQuestList()
    {
        // ���� UI ����
        foreach (Transform child in questListParent) // ���� UI  ����
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in questManager.GetActiveQuest())    // Ȱ�� ����Ʈ ǥ��
        {
            CreateQuestUI(quest);
        }
    }


    // �� óġ ��ư �̺�Ʈ
    private void OnKillEnemy()
    {
        questManager.OnEnemyKilled("Rat");
        RefreshQuestList();
    }

    // ������ ���� ��ư �̺�Ʈ
    private void OnCollectItem()
    {
        questManager.OnItemCollected("Herb");
        RefreshQuestList();
    }
}
