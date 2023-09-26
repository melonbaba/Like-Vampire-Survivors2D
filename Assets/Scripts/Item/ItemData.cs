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
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;//0���� ������
                            //Glove�� ��� �����
                            //Shoes�� ��� �̵��ӵ�
    public int baseCount;//���� �� ���� ��, ���Ÿ� �� �����
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;//����ü

}