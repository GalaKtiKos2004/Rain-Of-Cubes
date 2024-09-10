using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _poolCapcity = 5;
    [SerializeField] private int _maxPoolSize = 5;
    [SerializeField] private int _minXPosition = -5;
    [SerializeField] private int _maxXPosition = 5;

    private ObjectPool<GameObject> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: cube => ActionOnGet(cube),
            actionOnRelease: cube => cube.SetActive(false),
            actionOnDestroy: cube => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapcity,
            maxSize: _maxPoolSize);
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), 0f, 0f);

        if (cube.TryGetComponent(out Cube cubeComponent))
            cubeComponent.Init();

        cube.SetActive(true);
    }
}
