using UnityEngine;

public class Apple : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Destroy(gameObject);
            GameManager.gameManager.SpawnApple();
        }
    }
}
