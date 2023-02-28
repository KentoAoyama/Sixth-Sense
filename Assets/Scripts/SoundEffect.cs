using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    private void Update()
    {
        transform.LookAt(_player.transform.position);
    }
}
