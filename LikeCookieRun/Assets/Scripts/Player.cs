using NUnit.Framework.Constraints;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerColider _slidingColider = new PlayerColider(new Vector2(0.11173f, -0.500551f), new Vector2(1.293866f, 0.4446f));
    private PlayerColider _standingColider = new PlayerColider(new Vector2(-0.02230167f, -0.02230167f), new Vector2(0.8721762f, 1.4f));

    [SerializeField]
    private float _jumpPower = 8.5f;

    private float _initialXLoc;
    private float _xRecoverSpeed = 3f;

    private bool _isSliding = false;
    private bool _isJumping = false;
    private bool _isDead = false;

    private int _jumpCount = 0;

    // 충돌 후 무적 시간 관리
    private float _lastHitTime;
    private float _noDamageDuration = 2f;
    private bool _isNoDamageState = true;

    // 자석 아이템 관리
    [SerializeField]
    private GameObject _magnetArea;
    private bool _isMagnetActive => _magnetArea.GetComponent<BoxCollider2D>().enabled;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxColider;
    private SpriteRenderer _renderer;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxColider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        _initialXLoc = transform.position.x;
        _magnetArea.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update() {
        if (!GameManager.Instance.IsGameOver) {
            // 점프
            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2) { 
                EndSlide();
                StartJump();
            }

            // 슬라이딩
            if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl) && !_isJumping)) { StartSlide(); } 
            else if (Input.GetKeyUp(KeyCode.LeftControl) && _isSliding) { EndSlide(); }

            // 애니메이션 동기화
            _animator.SetInteger(PlayerAnimation.JumpCount, _jumpCount);
            _animator.SetBool(PlayerAnimation.IsJumping, _isJumping);
            _animator.SetBool(PlayerAnimation.IsSliding, _isSliding);

            // 떨어지면 다시 위에서 나오게
            if (transform.position.y <= -(Camera.ScreenHeight * 0.5)) {
                transform.position = new Vector3(transform.position.x, Camera.ScreenHeight * 0.7f, 0);
            }

            // 뒤로 밀리면 다시 제자리로
            if (transform.position.x <= _initialXLoc) {
                transform.position += Vector3.right * _xRecoverSpeed * Time.deltaTime;
            }

            // 맞은 지 얼마 안되었다면, 무적인 것 처럼 깜빡이기
            if (Time.time - _lastHitTime < _noDamageDuration) { 
                _renderer.enabled = !(_renderer.enabled);
            }

            // 시간 지나면 해제
            else if (_isNoDamageState) {
                DeactivateHit();
            }

        } else if (!_isDead){
            _isDead = true;
            _animator.SetTrigger(PlayerAnimation.Dead);
            _rigidBody.linearVelocity = Vector3.zero;
            _rigidBody.gravityScale = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        // 점프 종료
        if (collision.CompareTag(Tags.Ground)) {
            EndJump();
        }

        // 맞았을 때
        else if (!gameObject.CompareTag("Magnet") && collision.CompareTag(Tags.Hit) && Time.time >= _lastHitTime + _noDamageDuration) {
            Hit();
        }
    }

    private void Hit() {
        // 무적상태 활성화
        _isNoDamageState = true;
        // 체력 줄이기
        GameManager.Instance.ReduceHealth(GameManager.Instance.HitEnergyReduce);
        // 안전발판 활성화
        GameManager.Instance.ActivateSafeZone();
        // 충돌 시간 적용
        _lastHitTime = Time.time;
    }

    private void DeactivateHit() {
        // 무적 비활성화
        _isNoDamageState = false;
        // 안전발판 비활성화
        GameManager.Instance.DeactivateSafeZone();
        // 모습 다시 보이게
        _renderer.enabled = true;
    }

    // 자석 아이템 활성화
    public void ActivateMagnet() {
        _magnetArea.GetComponent<MagnetArea>().ActivateMagnet();
    }

    /// <summary>
    /// 현재 _isSliding 상태에 맞게 충돌박스 크기 변경
    /// </summary>
    private void UpdateColliderSize() {
        if (_isSliding) {
            _boxColider.offset = _slidingColider.Offset;
            _boxColider.size = _slidingColider.Size;
        } else {
            _boxColider.offset = _standingColider.Offset;
            _boxColider.size = _standingColider.Size;
        }
    }

    private void StartJump() {
        _jumpCount++;
        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

        _isJumping = true;
    }
    private void EndJump() {
        _isJumping = false;
        _jumpCount = 0;
    }
    private void StartSlide() {
        _isSliding = true;
        UpdateColliderSize();
    }
    private void EndSlide() {
        _isSliding = false;
        UpdateColliderSize();
    }
}
