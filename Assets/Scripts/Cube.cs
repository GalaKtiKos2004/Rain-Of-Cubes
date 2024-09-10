using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private List<Platform> _platforms = new List<Platform>();

    public event Action<Cube> Disappearing;

    public Renderer Renderer { get; private set; }

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_platforms.Contains(platform) == false)
            {
                _platforms.Add(platform);
                Renderer.material.color = platform.Dyeing—olor;
                StartCoroutine(Dead(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime)));
            }
        }
    }

    public void Init()
    {
        Renderer.material.color = _startColor;
        _platforms.Clear();
    }

    private IEnumerator Dead(float lifeTime)
    {
        var wait = new WaitForSeconds(lifeTime);

        yield return wait;

        Disappearing?.Invoke(this);
    }
}
