using UnityEngine;

class Jelly : ItemBase {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            // 점수 증가 로직 등
        }
    }
}