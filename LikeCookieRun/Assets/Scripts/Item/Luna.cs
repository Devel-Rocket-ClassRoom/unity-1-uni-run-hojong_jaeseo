using Unity.VisualScripting;
using UnityEngine;

class Luna : ItemBase {
    private float _score = 500;
    private float _heal = 5; 

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (IsPlayer(collision)) {
            base.OnTriggerEnter2D(collision);

            GameManager.Instance.AddScore(_score);
            GameManager.Instance.AddEnergy(_heal);
        }
    }
}