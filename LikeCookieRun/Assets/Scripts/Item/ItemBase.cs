using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        gameObject.SetActive(false);
    }

    protected bool IsPlayer(Collider2D collision) {
        return collision.CompareTag(Tags.Player);
    }
}
