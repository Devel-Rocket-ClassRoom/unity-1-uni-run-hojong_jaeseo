using UnityEngine;

public class Background : MonoBehaviour
{
    private float _screenWidth = 17.78f;
    private float _width = 25;

    [SerializeField]
    private float _speed = 10f;

    private float _xLoc = 25f;

    // Update is called once per frame
    private void Update() {
        if (transform.position.x <= -(_screenWidth * 0.5 + _width * 0.5)) {
            transform.position = new Vector3(_xLoc, 0);
        }

        transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}
