using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public int LifeTime => _lifeTime;

    private Renderer _renderer;
    private ObjectPool _pool;

    private int _changeColorCount = 1;
    private int _startChangeColorCount = 1;
    private int _lifeTime;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 6;
    private Coroutine _coroutine;
    private Color _startColor;
    private int _wait = 1;
    private WaitForSeconds _waitTime;
    private Coroutine _timer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _pool = FindObjectOfType<ObjectPool>();

        _waitTime = new WaitForSeconds(_wait);
        _startColor = _renderer.material.color;
    }

    private void Update()
    {
        if (_lifeTime == 0)
        {
            _pool.PutObject(this.transform);
        }
    }

    private void OnEnable()
    {
        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _changeColorCount = _startChangeColorCount;

        if (_renderer != null)
        {
            _renderer.material.color = _startColor;
        }
    }

    private void OnDisable()
    {
        if (_timer != null)
        {
            StopCoroutine(_timer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Platform platform) && _changeColorCount > 0)
        {
            _changeColorCount--;
            _timer = StartCoroutine(StartLifeTimer());
            _renderer.material.color = Random.ColorHSV();
        }
    }

    public IEnumerator StartLifeTimer()
    {
        while (enabled && _lifeTime > 0)
        {
            _lifeTime--;
            yield return _waitTime;
        }
    }
}
