using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDeath
{
    [Header("頭の当たり判定")]
    [SerializeField]
    private Collider _head;


    [Header("右の二の腕の当たり判定")]
    [SerializeField]
    private Collider _rightArm;

    [Header("右腕の当たり判定")]
    [SerializeField]
    private Collider _rightForeArm;

    [Header("右手の当たり判定")]
    [SerializeField]
    private Collider _rightHand;


    [Header("左の二の腕の当たり判定")]
    [SerializeField]
    private Collider _leftArm;

    [Header("左腕の当たり判定")]
    [SerializeField]
    private Collider _leftForeArm;

    [Header("左手の当たり判定")]
    [SerializeField]
    private Collider _leftHand;


    [Header("右上脚の当たり判定")]
    [SerializeField]
    private Collider _rightUpLeg;

    [Header("右脚の当たり判定")]
    [SerializeField]
    private Collider _rightLeg;


    [Header("左上脚の当たり判定")]
    [SerializeField]
    private Collider _leftUpLeg;

    [Header("左脚の当たり判定")]
    [SerializeField]
    private Collider _leftLeg;
}
