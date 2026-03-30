    using UnityEngine;

public class Magnet : ItemBase
{
    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        if (IsPlayer(collision)) {
            // 자석 아이템 활성화
            collision.gameObject.GetComponent<Player>().ActivateMagnet();
        }
    }
}
