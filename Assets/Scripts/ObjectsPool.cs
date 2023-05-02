using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = null;
    [SerializeField] private List<GameObject> _activeObjects = null;
    [SerializeField] private GameObject _prefab;
    //[SerializeField] private GameObject _parentObjects;

    private float _randomX;
    private float _randomY;
    //private float _randomZ;

    [SerializeField] private Transform _player;

    private void Awake()
    {
        for (int i = 0; i < 500; i++)
        {
            GameObject obj = Instantiate(_prefab, Vector2.zero, Quaternion.identity);
            _objects.Add(obj);
            obj.transform.parent = transform;
            obj.SetActive(false);
        }
    }
    private void OnEnable()
    {
        EnemyController.OnDestroy += DestroyingEnemy;
    }

    private void DestroyingEnemy(GameObject obj)
    {
        if (_activeObjects.Contains(obj))
        {
            obj.SetActive(false);
            _objects.Add(obj);
            _activeObjects.Remove(obj);
            if (obj.GetComponent<EnemyController>() != null)
            {
                Destroy(obj.GetComponent<EnemyController>());
            } 
        }
    }

    void Start()
    {
        _player = FindAnyObjectByType<PlayerMovement>().transform;

        InvokeRepeating("CallEnemies", 0, 10);
    }

    void CallEnemies()
    {
        if (_objects.Count >= 20)
        {
            for (int i = 0; i < 20; i++)
            {
                //Vector2 randomPos = Random.insideUnitSphere * 10;
                _randomX = Random.Range(_player.transform.position.x - 20, _player.transform.position.x + 20);
                _randomY = Random.Range(_player.transform.position.y - 20, _player.transform.position.y + 20);
                //_randomZ = Random.Range(_player.transform.position.z + 10, _player.transform.position.z + 20);
                Vector2 randomPos = new Vector2(_randomX, _randomY);
                _activeObjects.Add(_objects[_objects.Count - 1]);
                _objects[_objects.Count - 1].SetActive(true);
                _objects[_objects.Count - 1].transform.position = randomPos;
                _objects[_objects.Count - 1].AddComponent<EnemyController>();
                _objects.Remove(_objects[_objects.Count - 1]);
            }
        }
    }
}
