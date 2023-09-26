using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTest : MonoBehaviour
{
    bool isAuto = false;
    Image imgae;
    public Scanner scanner;
    private void Start()
    {
        imgae = GetComponent<Image>();
        scanner = GameManager.Ins.player.scanner;
    }

    public void AutoCtrl()
    {
        isAuto = !isAuto;
        if (isAuto)
        {
            imgae.color = Color.yellow;
        }
        else
        {
            imgae.color = Color.white;
        }
    }
    private void FixedUpdate()
    {
        if (isAuto)
        {
            if (scanner.nearestTr != null)
            {
                //���� 10���� ray�� ���� Enemy�� ���� ������ ����
                // �� Enemy�� �ִٸ� ���� Enemy�� �������� ���� �ɷ� �غ���/
                Vector3 dir = GameManager.Ins.player.transform.position - scanner.nearestTr.position;
                dir = dir.normalized;
                GameManager.Ins.player.inputVec = dir;
            }
        }
    }
}
