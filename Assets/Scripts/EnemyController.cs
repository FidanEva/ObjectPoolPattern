using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event Action<GameObject> OnDestroy;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 0.1f;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerMovement>().transform;
    }
    private void OnMouseDown()
    {
        OnDestroy?.Invoke(this.gameObject);
    }
    private void Update()
    {
        transform.Translate((_player.transform.position - transform.position) * _speed * Time.deltaTime);
    }
}
