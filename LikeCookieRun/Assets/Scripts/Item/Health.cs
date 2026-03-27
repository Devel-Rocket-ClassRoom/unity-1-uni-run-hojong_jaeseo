using UnityEngine;

public class Health : ItemBase
{
    private float _heal = 30;

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            // 체력 증가
            GameManager.Instance.AddHealth(_heal);
        }
    }
}
