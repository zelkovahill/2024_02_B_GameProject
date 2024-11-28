using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{

    public string GetInteractPrompt() => "��ȭ�ϱ�";

    public float GetInteractionDistance() => 2f;

    public bool CanInteract(GameObject player) => true;

    public void OnInteract(GameObject player)
    {
        FloatingTextManager.Instance.ShowFloatingText("�ȳ��ϼ���! ", transform.position);
    }

}
