using NUnit.Framework.Constraints;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerColider _slidingColider = new PlayerColider(new Vector2(0.11173f, -0.500551f), new Vector2(1.293866f, 0.4446f));
    private PlayerColider _standingColider = new PlayerColider(new Vector2(-0.02230167f, -0.02230167f), new Vector2(0.8721762f, 1.4f));

    [SerializeField]
    private float _jumpPower = 5f;

    private bool _isSliding = false;
    private bool _isJumping = false;

    private int _jumpCount = 0;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxColider;
    private CircleCollider2D _circleColider;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxColider = GetComponent<BoxCollider2D>();
        _circleColider = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2) { 
            EndSlide();
            StartJump();
        }


        // 슬라이딩
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isJumping) { StartSlide(); } 
        else if (Input.GetKeyUp(KeyCode.LeftControl) && _isSliding) { EndSlide(); }

        _animator.SetInteger(PlayerAnimation.JumpCount, _jumpCount);
        _animator.SetBool(PlayerAnimation.IsJumping, _isJumping);
        _animator.SetBool(PlayerAnimation.IsSliding, _isSliding);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        // 점프 종료
        if (collision.CompareTag(Tags.Ground)) {
            EndJump();
        }
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
