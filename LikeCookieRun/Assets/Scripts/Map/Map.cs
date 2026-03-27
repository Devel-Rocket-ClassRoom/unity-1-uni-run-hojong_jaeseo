using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    public event Action OnDisapper;

    [SerializeField]
    private GameObject[] _items;

    private void Update() {
        if (transform.position.x <= -Camera.ScreenWidth) {
            Debug.Log($"{gameObject.name} : 스폰 요청");
            OnDisapper?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        foreach (var item in _items) {
            item.SetActive(true);
        }
    }
}
