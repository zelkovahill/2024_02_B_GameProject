using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��� ��ȣ�ۿ� ������ ��ü�� �����ؾ��ϴ� �⺻ �������̽�
public interface IInteractable
{
    /// <summary>
    /// ��ȣ�ۿ�� ǥ���� �ؽ�Ʈ
    /// </summary>
    /// <returns></returns>
    string GetInteractPrompt();

    /// <summary>
    /// ��ȣ�ۿ� �� ����� �޼���
    /// </summary>
    /// <param name="player"></param>
    void OnInteract(GameObject player);

    /// <summary>
    /// ��ȣ�ۿ� ���� �Ÿ�
    /// </summary>
    /// <returns></returns>
    float GetInteractionDistance();

    /// <summary>
    /// ��ȣ�ۿ� ���� ����
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool CanInteract(GameObject player);
}