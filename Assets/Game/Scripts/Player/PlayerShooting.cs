using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject PlayerBulletPrefab;
    private Player _player;

    private void Awake()
    {
        _player = transform.GetComponentInParent<Player>();
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (PlayerBulletPrefab != null)
            {
              
                Quaternion quaternion = _player.IsFacingRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                PoolingManager.Spawn(PlayerBulletPrefab, transform.position, quaternion);
            }
                
        }
    }
}
