using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDeath
{
    [Header("“ª‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _head;


    [Header("‰E‚Ì“ñ‚Ì˜r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _rightArm;

    [Header("‰E˜r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _rightForeArm;

    [Header("‰Eè‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _rightHand;


    [Header("¶‚Ì“ñ‚Ì˜r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _leftArm;

    [Header("¶˜r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _leftForeArm;

    [Header("¶è‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _leftHand;


    [Header("‰Eã‹r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _rightUpLeg;

    [Header("‰E‹r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _rightLeg;


    [Header("¶ã‹r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _leftUpLeg;

    [Header("¶‹r‚Ì“–‚½‚è”»’è")]
    [SerializeField]
    private Collider _leftLeg;
}
