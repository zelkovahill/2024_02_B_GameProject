using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderKeywordFilter;


public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("UI 레퍼런스")]
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
        // 주변 상호작용 가능한 객체 탐색
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

        // 가장 가까운 상호작용 대상 업데이트
        currentInteractable = closest;
        UpdatePrompt();
    }

    /// <summary>
    /// 상호작용할 수 있는 프롬프트
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
