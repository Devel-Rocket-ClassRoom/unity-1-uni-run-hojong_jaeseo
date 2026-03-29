using System;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {

    private float _magnetSpeed = 10f;
    private GameObject _player = null;
    private bool _isMagnetActivated = false;
    private Vector3 _initialPosition;

    private void Awake() {
        _initialPosition = transform.localPosition;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            gameObject.SetActive(false);
        }

        else if (IsMagnet(collision)) {
            _player = collision.gameObject;
            _isMagnetActivated = true;
        }
    }

    protected void OnTriggerStay2D(Collider2D collision) {
        if (IsMagnet(collision)) {
            _player = collision.gameObject;
            _isMagnetActivated = true;
        }
    }

    private void OnEnable() {
        // 새로 생성될 때 위치 초기화
        transform.localPosition = _initialPosition;
        _isMagnetActivated = false;
        _player = null;
    }

    protected bool IsPlayer(Collider2D collision) {
        return collision.CompareTag(Tags.Player);
    }

    protected bool IsMagnet(Collider2D collision) {
        return collision.CompareTag(Tags.Magnet);
    }

    private void Update() {
        if (_isMagnetActivated) {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _magnetSpeed * Time.deltaTime);
        }
    }
}
