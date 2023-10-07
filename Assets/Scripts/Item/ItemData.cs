using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoes, Heal}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;//0레벨 데미지
                            //Glove의 경우 연사력
                            //Shoes의 경우 이동속도
    public int baseCount;//근접 시 무기 수, 원거리 시 관통력
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;//투사체
    public Sprite hand;

}
