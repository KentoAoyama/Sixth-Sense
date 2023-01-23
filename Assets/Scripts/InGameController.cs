using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    [Tooltip("プレイヤーのクラス")]
    [SerializeField]
    private PlayerController _player;

    void Start()
    {
        CursorInit();
        _player.Initialize();
    }

    private void CursorInit()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _player.ManualUpdate(deltaTime);
    }
}
