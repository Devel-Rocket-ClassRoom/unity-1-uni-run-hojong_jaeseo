using UnityEngine;

class Luna : ItemBase {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            // 달은 뭐할까???
        }
    }
}