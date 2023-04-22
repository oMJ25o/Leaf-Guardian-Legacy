using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    public int hp;
    public int attackDamage;
    public float moveSpeed;
    public int exp;
}
