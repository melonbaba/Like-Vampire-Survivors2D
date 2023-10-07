using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);//true 시킴으로써 비활성화된 Obj에도 접근 가능하게 함.
    }

    private void FixedUpdate()//물리 연산 프레임마다 호출됨
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        /* 이동방식 설명
        //이동방식 1. 힘을 준다. rb.AddForce(inputVec);
        //이동방식 2. 속도 제어. rb.velocity = inputVec;
        //이동방식 3. 위치 이동. rb.MovePosition 사용*/
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;//fixedDeltaTime : 물리 프레임 하나가 소비한 시간.
        rb.MovePosition(rb.position + nextVec);//P = P0 + VT 등속도운동.
    }

    private void LateUpdate()//프레임이 종료 되기 전 실행됨.
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        //flip, anim은 후처리로 처리하기 위해 LateUpdate에서 사용함.

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
            spriter.flipX = inputVec.x < 0 ? true : false;
    }

    public void OnMove(InputValue value)//value는 normalized 된 값.
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        inputVec = value.Get<Vector2>();
    }
}
