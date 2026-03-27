using UnityEngine;

public class Health : ItemBase
{
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            // 체력 증가
        }
    }
}
