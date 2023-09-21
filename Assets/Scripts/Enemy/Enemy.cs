using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D targetRb;

    bool isLive = true;
    Rigidbody2D rb;
    SpriteRenderer spriter;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dir = targetRb.position - rb.position;
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);

        rb.velocity = Vector2.zero;//�浹 ��, �������꿡 ���� �и��� ���� ���� �ϱ� ����.
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = targetRb.position.x < rb.position.x ? true : false;
    }
}
