using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float _width = 25;
    private float _xLoc = 25f;

    private void Update() {
        if (transform.position.x <= -(Camera.ScreenWidth * 0.5 + _width * 0.5)) {
            transform.position = new Vector3(_xLoc, 0);
        }
    }
}
