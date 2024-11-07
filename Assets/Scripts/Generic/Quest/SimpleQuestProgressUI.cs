using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyGame.QuestSystem;
using UnityEngine.UI;

public class SimpleQuestProgressUI : MonoBehaviour
{
    [Header("Quest List")]
    [Tooltip("퀘스트 목록이 표시될 부모 Transform")]
    [SerializeField]
    private Transform questListParent;

    [Tooltip("퀘스트 UI 프리팹")]
    [SerializeField]
    private GameObject questPrefabs;


    [Header("Pregress Test")]
    [Tooltip("적 처치 테스트 버튼")]
    [SerializeField]
    private Button KillEnemyButton;

    [SerializeField]
    [Tooltip("아이템 수집 테스트 버튼")]
    private Button CollectItemButton;

    private QuestManager questManager;

    private void Start()
    {
        questManager = QuestManager.Instance;

        // 버튼 이벤트 설정
        KillEnemyButton.onClick.AddListener(OnKillEnemy);
        CollectItemButton.onClick.AddListener(OnCollectItem);

        // 이벤트 등록
        questManager.OnQuestStarted += UpdateQuestUI;
        questManager.OnQuestCompleted += UpdateQuestUI;

        // 초기 퀘스트 생성 표시
        RefreshQuestList();
    }


    // 개별 퀘스트 UI 생성
    private void CreateQuestUI(Quest quest)
    {
        GameObject questObj = Instantiate(questPrefabs, questListParent);

        TextMeshProUGUI titleText = questObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI progressText = questObj.transform.Find("ProgressText").GetComponent<TextMeshProUGUI>();

        titleText.text = quest.Title;
        progressText.text = $"Progress : {quest.GetProgress():P0}";
    }


    // 퀘스트 상태 변경 시 UI 업데이트
    private void UpdateQuestUI(Quest quest)
    {
        RefreshQuestList();
    }

    // 퀘스트 목록 새로고침
    private void RefreshQuestList()
    {
        // 기존 UI 제거
        foreach (Transform child in questListParent) // 기존 UI  제거
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in questManager.GetActiveQuest())    // 활성 퀘스트 표시
        {
            CreateQuestUI(quest);
        }
    }


    // 적 처치 버튼 이벤트
    private void OnKillEnemy()
    {
        questManager.OnEnemyKilled("Rat");
        RefreshQuestList();
    }

    // 아이템 수집 버튼 이벤트
    private void OnCollectItem()
    {
        questManager.OnItemCollected("Herb");
        RefreshQuestList();
    }
}
