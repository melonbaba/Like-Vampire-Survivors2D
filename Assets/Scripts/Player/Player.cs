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
        hands = GetComponentsInChildren<Hand>(true);//true ��Ŵ���ν� ��Ȱ��ȭ�� Obj���� ���� �����ϰ� ��.
    }

    private void FixedUpdate()//���� ���� �����Ӹ��� ȣ���
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        /* �̵���� ����
        //�̵���� 1. ���� �ش�. rb.AddForce(inputVec);
        //�̵���� 2. �ӵ� ����. rb.velocity = inputVec;
        //�̵���� 3. ��ġ �̵�. rb.MovePosition ���*/
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;//fixedDeltaTime : ���� ������ �ϳ��� �Һ��� �ð�.
        rb.MovePosition(rb.position + nextVec);//P = P0 + VT ��ӵ��.
    }

    private void LateUpdate()//�������� ���� �Ǳ� �� �����.
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        //flip, anim�� ��ó���� ó���ϱ� ���� LateUpdate���� �����.

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
            spriter.flipX = inputVec.x < 0 ? true : false;
    }

    public void OnMove(InputValue value)//value�� normalized �� ��.
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        inputVec = value.Get<Vector2>();
    }
}
