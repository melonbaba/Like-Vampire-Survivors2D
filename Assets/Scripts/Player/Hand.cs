using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer playerSpriter;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0f);

    Quaternion leftRot = Quaternion.Euler(0, 0, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135f);
    private void Awake()
    {
        playerSpriter = GetComponentsInParent<SpriteRenderer>()[1];//Getcomponents�� �ڱ� �ڽ��� �켱������ ��.
                                                                   //����, 0���� �ڱ� �ڽ�.
    }

    private void LateUpdate()
    {
        bool isReverse = playerSpriter.flipX;

        if (isLeft)//��������
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else//���Ÿ� ����
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;

        }
    }

}