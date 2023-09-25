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

    private void FixedUpdate()//Player도 FixedUpdate로 이동하니 Health관련 Rect의 이동도 FixedUpdate에서 처리
    {
        //Player는 World좌표, HealthUI는 Screen 좌표임.
        //WorldToScreenPoint : 월드 상 Obj 좌표를 스크린 좌표로 변환.
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Ins.player.transform.position);
    }

}
