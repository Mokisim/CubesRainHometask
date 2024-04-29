using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private int _changeColorCount;

    private Renderer _renderer;

    private int _lifeTime;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 6;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Platform>(out Platform platform) && _changeColorCount > 0)
        {
            _changeColorCount--;

            _renderer.material.color = _targetColor;
        } 
    }
}
