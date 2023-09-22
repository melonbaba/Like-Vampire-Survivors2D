using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detect Enemy
public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] hitInfos;
    public Transform nearestTr;

    private void FixedUpdate()
    {
        hitInfos = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, targetLayer);
        nearestTr = GetNearest();
    }

    private Transform GetNearest()
    {
        Transform result = null;

        float diff = 100f;
        foreach (RaycastHit2D target in hitInfos)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float dist = Vector3.Distance(myPos, targetPos);
            if(dist < diff)
            {
                diff = dist;
                result = target.transform;
            }
        }
        return result;
    }
}
