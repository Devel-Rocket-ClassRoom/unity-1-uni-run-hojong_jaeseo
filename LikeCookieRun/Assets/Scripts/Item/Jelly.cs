using UnityEngine;

class Jelly : ItemBase {
    private int _score = 100;

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            GameManager.Instance.AddScore(_score);
        }
    }
}