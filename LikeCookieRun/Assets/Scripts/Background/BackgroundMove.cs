using UnityEngine;

public class BackgroundMove : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    private void Awake() {
        GameManager.Instance.OnChangeSpeed += ChangeSpeed;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameOver) {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
    }

    private void ChangeSpeed(float newSpeed) {
        _speed = newSpeed;
    }
}
