using System;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {

    private float _magnetSpeed = 10f;
    private GameObject _player = null;
    private bool _isMagnetActivated = false;
    private Vector3 _initialPosition;

    // 생성 시 위치 저장
    private void Awake() {
        _initialPosition = transform.localPosition;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        // 플레이어에 닿으면 사라지기
        if (IsPlayer(collision)) {
            gameObject.SetActive(false);
        }

        // 자석에 닿으면 빨아당기기 효과 시작
        else if (IsMagnet(collision)) {
            _player = collision.gameObject;
            _isMagnetActivated = true;
        }
    }

    protected void OnTriggerStay2D(Collider2D collision) {
        // 자석 먹었을 때 범위 내에 존재하는 아이템들도 빨아들이는 효과 적용
        if (IsMagnet(collision)) {
            _player = collision.gameObject;
            _isMagnetActivated = true;
        }
    }

    private void OnEnable() {
        // 재활성화 시 위치 초기화
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

    // 자석에 닿으면 플레이어로 빨려가도록
    private void Update() {
        if (_isMagnetActivated) {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _magnetSpeed * Time.deltaTime);
        }
    }
}
