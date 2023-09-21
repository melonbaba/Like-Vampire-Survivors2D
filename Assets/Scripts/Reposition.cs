using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;//box, cpasule, sphere등의 2D 콜라이더를 포함.

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
                    transform.Translate(Vector3.right * dirX * 40);//Translate : 지정된 값 만큼 현재 위치에서 이동. 이동할 양을 넣어줘야함.
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * dirY * 40);//40은 두 개의 타일의 크기 
                break;

            case "ENEMY":

                if (coll.enabled)//cf) Enemy Dead되면 coll enable = false할 꺼임.
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));

                break;
        }
    }
}
