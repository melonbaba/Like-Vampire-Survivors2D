using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;//RuntimeAnimatorController = Controller�� ����ϴ� Class
    public Rigidbody2D targetRb;

    bool isLive;
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;//ĳ��
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        targetRb = GameManager.Ins.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rb.simulated = true;//rb�� ������ Ȱ��ȭ
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void FixedUpdate()
    {
        //GetCurrentAnimatorStateInfo = �ִϸ��̼��� ���� ���� ������ ������.
        //GetCurrentAnimatorStateInfo(0) = Base Layer
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("BULLET") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBackCo());//"Hit" ���� ����Ǵ� ���� �ڿ������� ����.

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;//rb�� ������ ��Ȱ��ȭ
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);

            GameManager.Ins.kill++;
            GameManager.Ins.GetExp();
        }
    }
    IEnumerator KnockBackCo()
    {
        //yield return null;//1������ ����.
        yield return wait;
        Vector3 playerPos = GameManager.Ins.player.transform.position;
        Vector3 dir = transform.position - playerPos;//�÷��̾ ���� ���� ����
        rb.AddForce(dir.normalized * 3f, ForceMode2D.Impulse);
    }
    void Dead()//Animation Event�� �����.
    {
        gameObject.SetActive(false);
    }
}
