using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrossHairController : MonoBehaviour
{
    private const float ROTATE_VALUE = 90f;

    private float _rotateTime = 0f;

    public void Initialize(float rotateTime)
    {
        //回転する時間を射撃のインターバルと合わせるため、値を受け取る
        _rotateTime = rotateTime;
    }

    public void RotateCrossHair()
    {
        //クロスヘアを90度回転させる
        gameObject.transform
            .DOLocalRotate(
            new Vector3(0f, 0f, (gameObject.transform.localRotation.eulerAngles.z +  ROTATE_VALUE) % 360),
            _rotateTime);
    }
}
