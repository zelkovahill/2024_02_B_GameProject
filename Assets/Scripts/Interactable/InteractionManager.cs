using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderKeywordFilter;


public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("UI ���۷���")]
    [SerializeField] private TextMeshProUGUI promptext;
    [SerializeField] private float checkRadius = 3f;
    [SerializeField] private LayerMask interactableLayers;

    private IInteractable currentInteractable;
    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckInteractables();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.OnInteract(player);
        }
    }


    private void CheckInteractables()
    {
        // �ֺ� ��ȣ�ۿ� ������ ��ü Ž��
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius, interactableLayers);
        IInteractable closest = null;
        float cloesetsDistance = float.MaxValue;

        foreach (var col in colliders)
        {
            if (col.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(player.transform.position, col.transform.position);

                if (distance <= interactable.GetInteractionDistance() && distance < cloesetsDistance && interactable.CanInteract(player))
                {
                    closest = interactable;
                    cloesetsDistance = distance;
                }
            }
        }

        // ���� ����� ��ȣ�ۿ� ��� ������Ʈ
        currentInteractable = closest;
        UpdatePrompt();
    }

    /// <summary>
    /// ��ȣ�ۿ��� �� �ִ� ������Ʈ
    /// </summary>
    private void UpdatePrompt()
    {
        if (currentInteractable != null)
        {
            promptext.text = $"[E] {currentInteractable.GetInteractPrompt()}";
            promptext.gameObject.SetActive(true);
        }
        else
        {
            promptext.gameObject.SetActive(false);
        }
    }
}
