using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Transactions;

public class Character : MonoBehaviour
{
    // 캐릭터 스탯
    public bool isPlayer;       // 플레이어 여부
    public int hp = 100;        // 체력
    public int speed;           // 속도 (턴 순서 결정)


    // UI 요소
    private TextMeshProUGUI nameText;   // 이름 표시
    private TextMeshProUGUI hpText;     // HP 표시
    private Vector3 startPosition;      // 시작 위치 (공격 애니메이션 용)

    private void Start()
    {
        SetupNameText();                    // UI 텍스트 초기화
        startPosition = transform.position; // 시작 위치 저장
    }



    // UI 텍스트 초기화
    private void SetupNameText()
    {
        // 이름 텍스트 설정
        GameObject textObj = new GameObject("NameText");
        textObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        nameText = textObj.AddComponent<TextMeshProUGUI>();
        nameText.text = isPlayer ? "P" : "E";
        nameText.fontSize = 36;
        nameText.alignment = TextAlignmentOptions.Center;

        // HP 텍스트 설정
        GameObject hpObj = new GameObject("HPText");
        hpObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        hpText = hpObj.AddComponent<TextMeshProUGUI>();
        hpText.fontSize = 30;
        hpText.alignment = TextAlignmentOptions.Center;

    }

    public void Attack(Character target)
    {
        // 죽은 타겟이면 무시               // 예외 처리 
        if (!target.gameObject.activeSelf || !this.gameObject.activeSelf)
        {
            return;
        }


        StartCoroutine(AttackRoutine(target));  // 공격 애니메이션 코루틴
    }

    private void OnDisable()
    {
        // 게임 오브젝트가 꺼지면 UI 제거
        if (nameText != null) Destroy(nameText.gameObject);
        if (hpText != null) Destroy(hpText.gameObject);
    }

    private void Update()
    {
        // 캐릭터가 비활성화 되면 UI 제거
        // 문제점 : 게임 오브젝트가 꺼지면 업데이트가 진행되지 않아 예외처리가 발생하지 않음
        if (!gameObject.activeSelf)
        {
            if (nameText != null) Destroy(nameText.gameObject);
            if (hpText != null) Destroy(hpText.gameObject);
            return;
        }

        // UI 위치 업데이트
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        nameText.transform.position = screenPosition;
        hpText.transform.position = screenPosition + Vector3.down * 30;

        // 현재 턴 캐릭터 표시 (초록색) BattleSystem 구현 이후 구현
        nameText.color = (BattleSystem.Instance.GetCurrentChar() == this) ? Color.green : Color.white;
        hpText.text = hp.ToString();
    }


    /// <summary>
    /// 공격 애니메이션 코루틴
    /// </summary>
    private IEnumerator AttackRoutine(Character target)
    {
        // 타겟 쪽으로 이동
        Vector3 attackPosition = target.transform.position + (target.transform.position - transform.position).normalized * 1.5f;
        float moveTime = 0.3f;
        float elapsed = 0;

        // 이동 애니메이션
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(startPosition, attackPosition, t);
            yield return null;
        }


        // 데미지 처리
        target.hp -= 20;

        if (target.hp <= 0)
        {
            target.gameObject.SetActive(false);
        }

        // 원 위치로 복귀
        elapsed = 0;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(attackPosition, startPosition, t);
            yield return null;
        }
        transform.position = startPosition;
    }
}