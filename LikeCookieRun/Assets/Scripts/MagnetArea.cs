using UnityEngine;

class MagnetArea : MonoBehaviour {
    
    private GameObject _player;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidBody;

    private float _magnetStartTime = -10f;
    private float _magnetDuration = 5f;

    private void Awake() {
        _player = GameObject.FindWithTag(Tags.Player);
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = false;

        _rigidBody= GetComponent<Rigidbody2D>();
        _rigidBody.simulated = true;
        _rigidBody.gravityScale = 0;
    }

    private void Update() {
        // 항상 플레이어를 따라가도록
        transform.position = _player.transform.position;

        // 활성화 상태면 Magnet 켜기
        if (_boxCollider.enabled && Time.time > _magnetStartTime + _magnetDuration) {
            _boxCollider.enabled = false;
        }
    }

    public void ActivateMagnet() {
        _magnetStartTime = Time.time;
        _boxCollider.enabled = true;
    }

    public void DeactivateMagnet() {

    }
}