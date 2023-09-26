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
                //차라리 10개의 ray를 쏴서 Enemy가 없는 쪽으로 가고
                // 다 Enemy가 있다면 가장 Enemy가 먼쪽으로 가는 걸로 해볼까/
                Vector3 dir = GameManager.Ins.player.transform.position - scanner.nearestTr.position;
                dir = dir.normalized;
                GameManager.Ins.player.inputVec = dir;
            }
        }
    }
}
