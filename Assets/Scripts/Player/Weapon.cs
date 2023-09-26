using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;//PoolMgr 참고를 위한 Id
    public float damage;
    public int count;//원거리 무기의 경우 count는 관통력을 의미
    public float speed;

    //원거리 무기 관련 변수
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
                speed = 150;//'+' = 반시께 방향, '-' = 시계방향.
                SetPos();
                break;
            case 1:
                speed = 0.5f;//연사속도
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
        //count 수에 따른 위치 변경
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)//기존 무기 재활용하기 위함.
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Ins.poolMgr.GetObj(prefabId).transform;
                bullet.parent = transform;
            }


            bullet.localPosition = Vector3.zero;//위치 초기화.
            bullet.localRotation = Quaternion.identity;//identity = 초기 회전 값.

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
            SetPos();//LevelUP시 근접 무기 재배치.
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
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//FromToRotation = 지정된 축을 중심으로 목표를 향해 회전

        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
