using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Transactions;

public class Character : MonoBehaviour
{
    // ĳ���� ����
    public bool isPlayer;       // �÷��̾� ����
    public int hp = 100;        // ü��
    public int speed;           // �ӵ� (�� ���� ����)


    // UI ���
    private TextMeshProUGUI nameText;   // �̸� ǥ��
    private TextMeshProUGUI hpText;     // HP ǥ��
    private Vector3 startPosition;      // ���� ��ġ (���� �ִϸ��̼� ��)

    private void Start()
    {
        SetupNameText();                    // UI �ؽ�Ʈ �ʱ�ȭ
        startPosition = transform.position; // ���� ��ġ ����
    }



    // UI �ؽ�Ʈ �ʱ�ȭ
    private void SetupNameText()
    {
        // �̸� �ؽ�Ʈ ����
        GameObject textObj = new GameObject("NameText");
        textObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        nameText = textObj.AddComponent<TextMeshProUGUI>();
        nameText.text = isPlayer ? "P" : "E";
        nameText.fontSize = 36;
        nameText.alignment = TextAlignmentOptions.Center;

        // HP �ؽ�Ʈ ����
        GameObject hpObj = new GameObject("HPText");
        hpObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        hpText = hpObj.AddComponent<TextMeshProUGUI>();
        hpText.fontSize = 30;
        hpText.alignment = TextAlignmentOptions.Center;

    }

    public void Attack(Character target)
    {
        // ���� Ÿ���̸� ����               // ���� ó�� 
        if (!target.gameObject.activeSelf || !this.gameObject.activeSelf)
        {
            return;
        }


        StartCoroutine(AttackRoutine(target));  // ���� �ִϸ��̼� �ڷ�ƾ
    }

    private void OnDisable()
    {
        // ���� ������Ʈ�� ������ UI ����
        if (nameText != null) Destroy(nameText.gameObject);
        if (hpText != null) Destroy(hpText.gameObject);
    }

    private void Update()
    {
        // ĳ���Ͱ� ��Ȱ��ȭ �Ǹ� UI ����
        // ������ : ���� ������Ʈ�� ������ ������Ʈ�� ������� �ʾ� ����ó���� �߻����� ����
        if (!gameObject.activeSelf)
        {
            if (nameText != null) Destroy(nameText.gameObject);
            if (hpText != null) Destroy(hpText.gameObject);
            return;
        }

        // UI ��ġ ������Ʈ
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        nameText.transform.position = screenPosition;
        hpText.transform.position = screenPosition + Vector3.down * 30;

        // ���� �� ĳ���� ǥ�� (�ʷϻ�) BattleSystem ���� ���� ����
        nameText.color = (BattleSystem.Instance.GetCurrentChar() == this) ? Color.green : Color.white;
        hpText.text = hp.ToString();
    }


    /// <summary>
    /// ���� �ִϸ��̼� �ڷ�ƾ
    /// </summary>
    private IEnumerator AttackRoutine(Character target)
    {
        // Ÿ�� ������ �̵�
        Vector3 attackPosition = target.transform.position + (target.transform.position - transform.position).normalized * 1.5f;
        float moveTime = 0.3f;
        float elapsed = 0;

        // �̵� �ִϸ��̼�
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(startPosition, attackPosition, t);
            yield return null;
        }


        // ������ ó��
        target.hp -= 20;

        if (target.hp <= 0)
        {
            target.gameObject.SetActive(false);
        }

        // �� ��ġ�� ����
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