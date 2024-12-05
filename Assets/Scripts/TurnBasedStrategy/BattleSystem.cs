using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // �̱��� ����
    public static BattleSystem Instance { get; private set; }

    // ĳ���� �迭
    public Character[] players = new Character[3];
    public Character[] enemies = new Character[3];

    // UI ��ҵ�
    public Button attackBtn;                // ���� ��ư
    public TextMeshProUGUI turnText;        // ���� �� ǥ�� �ؽ�Ʈ
    public GameObject damageTextProfab;     // ������ ǥ�ÿ� ������
    public Canvas uiCanvas;                 // UI ĵ����

    // ���� ���� ����
    private Queue<Character> turnQueue = new Queue<Character>();    // �� ���� ť
    private Character currentChar;                                  // ���� �� ĳ����
    private bool selectingTarget;                                   // Ÿ�� ���� ������ ����

    private void Awake() => Instance = this;


    // ���� �� ĳ���� ��ȯ
    public Character GetCurrentChar() => currentChar;

    // ���� ��ư Ŭ���� Ÿ�� ���� ��� Ȱ��ȭ
    private void OnAttackClick() => selectingTarget = true;

    private void Start()
    {
        var orderedChars = players.Concat(enemies).OrderByDescending(c => c.speed);

        foreach (var c in orderedChars)
        {
            turnQueue.Enqueue(c);
        }

        // ���� ��ư�� �̺�Ʈ ����
        attackBtn.onClick.AddListener(OnAttackClick);

        // ù �� ����
        NextTurn();
    }

    private void Update()
    {
        // Ÿ�� ���� ��忡�� ���콺 Ŭ�� ó��
        if (selectingTarget && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Character target = hit.collider.GetComponent<Character>();

                if (target != null)
                {
                    currentChar.Attack(target);                         // ���� ����
                    ShowDamageText(target.transform.position, "20");    // ������ �ؽ�Ʈ ǥ��
                    selectingTarget = false;                            // ���� ������
                    NextTurn();
                }
            }
        }
    }

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    private void NextTurn()
    {
        // ���� �� ĳ���� ����
        currentChar = turnQueue.Dequeue();
        turnQueue.Enqueue(currentChar);
        turnText.text = $"{currentChar.name} �� �� (Speed : {currentChar.speed})";

        // �÷��̾� / �� �� ó��
        if (currentChar.isPlayer)
        {
            attackBtn.gameObject.SetActive(true);   // �÷��̾� �� : ���ݹ�ư Ȱ��ȭ
        }
        else
        {
            attackBtn.gameObject.SetActive(false);  // ���� : ���ݹ�ư ��Ȱ��ȭ
            Invoke(nameof(EnemyAttack), 1f);        // 1�� �� �� ����
        }
    }

    /// <summary>
    /// ������ �ؽ�Ʈ ���� �� ǥ��
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damage"></param>
    private void ShowDamageText(Vector3 position, string damage)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        GameObject damageObj = Instantiate(damageTextProfab, screenPosition, Quaternion.identity, uiCanvas.transform);
        damageObj.GetComponent<TextMeshProUGUI>().text = damage;
        Destroy(damageObj, 1f);
    }

    /// <summary>
    /// AI�� �� ���� ó��
    /// </summary>
    private void EnemyAttack()
    {
        // ������ �÷��̾� �� ���� Ÿ�� ����
        var aliveTargets = players.Where(p => p.gameObject.activeSelf).ToArray();

        // ��� �÷��̾ ������ ����
        if (aliveTargets.Length == 0)
            return;

        var target = aliveTargets[Random.Range(0, aliveTargets.Length)];
        currentChar.Attack(target);
        ShowDamageText(target.transform.position, "20");
        NextTurn();
    }

}
