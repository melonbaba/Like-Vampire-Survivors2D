using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()//Player�� FixedUpdate�� �̵��ϴ� Health���� Rect�� �̵��� FixedUpdate���� ó��
    {
        //Player�� World��ǥ, HealthUI�� Screen ��ǥ��.
        //WorldToScreenPoint : ���� �� Obj ��ǥ�� ��ũ�� ��ǥ�� ��ȯ.
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Ins.player.transform.position);
    }

}
