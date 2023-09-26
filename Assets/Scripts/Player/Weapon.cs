using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;//PoolMgr ���� ���� Id
    public float damage;
    public int count;//���Ÿ� ������ ��� count�� ������� �ǹ�
    public float speed;

    //���Ÿ� ���� ���� ����
    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.Ins.player;
    }

    public void Init(ItemData data)
    {
        name = "Weapon" + data.itemId;//GameObj Name
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int i = 0; i < GameManager.Ins.poolMgr.prefabs.Length; i++)
        {
            if (data.projectile == GameManager.Ins.poolMgr.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;//'+' = �ݽò� ����, '-' = �ð����.
                SetPos();
                break;
            case 1:
                speed = 0.5f;//����ӵ�
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }
        //if (Input.GetButtonDown("Jump"))
        //{
        //    LevelUp(10, 1);
        //}
    }

    void SetPos()
    {
        //count ���� ���� ��ġ ����
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)//���� ���� ��Ȱ���ϱ� ����.
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Ins.poolMgr.GetObj(prefabId).transform;
                bullet.parent = transform;
            }


            bullet.localPosition = Vector3.zero;//��ġ �ʱ�ȭ.
            bullet.localRotation = Quaternion.identity;//identity = �ʱ� ȸ�� ��.

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);// ==> Vector3.up * 1.5f * Space.Self

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1 is Infinity Per.
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            SetPos();//LevelUP�� ���� ���� ���ġ.
    }

    private void Fire()
    {
        if (!player.scanner.nearestTr)
            return;

        Vector3 targetpos = player.scanner.nearestTr.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Ins.poolMgr.GetObj(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//FromToRotation = ������ ���� �߽����� ��ǥ�� ���� ȸ��

        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
