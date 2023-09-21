using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;//box, cpasule, sphere���� 2D �ݶ��̴��� ����.

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("AREA"))
            return;

        Vector3 playerPos = GameManager.Ins.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Ins.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;


        switch (transform.tag)
        {
            case "GROUND":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * 40);//Translate : ������ �� ��ŭ ���� ��ġ���� �̵�. �̵��� ���� �־������.
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * dirY * 40);//40�� �� ���� Ÿ���� ũ�� 
                break;

            case "ENEMY":

                if (coll.enabled)//cf) Enemy Dead�Ǹ� coll enable = false�� ����.
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));

                break;
        }
    }
}
