using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteration : MonoBehaviour, IInteractable
{

    public string GetInteractPrompt() => "���� ����";

    public float GetInteractionDistance() => 2f;

    public bool CanInteract(GameObject player) => true;

    public void OnInteract(GameObject player)
    {
        FloatingTextManager.Instance.ShowFloatingText("������ �������ϴ�! ", transform.position);
    }
}
