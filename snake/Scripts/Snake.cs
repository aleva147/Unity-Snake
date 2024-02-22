using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
    // Za kretanje:
    private Vector3 direction = Vector2.right;
    private float speed = 1.0f;        // Velicina jednog polja! Da bi se pomerilo za tacno jedno polje u zadatom smeru.
    [SerializeField] private float frequencyOfMovement = 0.3f;
    public SnakeBodypart snakeBodyPrefab;
    private List<SnakeBodypart> snakeTail = new List<SnakeBodypart>();



    private void Start() {
        InvokeRepeating("ReactToInput", 0.0f, frequencyOfMovement);
    }

    private void Update() {
        CheckForInput();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Apple") ExtendSnake(); 
        else if (collision.tag == "Bodypart" || collision.tag == "Wall")  GameManager.gameManager.RestartGame(); 
    }


    private void ReactToInput() {
        Vector3 oldPosition = transform.position;
        /// Pomeranje glave u zadatom smeru.
        transform.position += direction * speed;

        /// Pomeranje svakog dela tela, iduci od glave ka repu.
        if (snakeTail.Count > 0) {
            for (int i = 0; i < snakeTail.Count; i++) {
                Vector3 newPosition = oldPosition;              // Pozicija na kojoj je bio deo tela koji mu prethodi pre nego sto se pomerio.
                oldPosition = snakeTail[i].transform.position;
                snakeTail[i].transform.position = newPosition;
            }
            GameManager.gameManager.tiles[(int)oldPosition.x, (int)oldPosition.y].isOccupiedBody = false;
        }
    }

    private void CheckForInput() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.UpArrow)) direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2.down;
    }


    public void EraseTail() {
        if (snakeTail.Count > 0) {
            foreach (SnakeBodypart bodypart in snakeTail) {
                Destroy(bodypart.gameObject);
            }
            snakeTail.Clear();
        }
    }

    private void ExtendSnake() {
        SnakeBodypart newBodypart = Instantiate(snakeBodyPrefab, new Vector3(0,0,1), Quaternion.identity);
        newBodypart.lastBodypart = true;
        if (snakeTail.Count > 0) snakeTail[snakeTail.Count - 1].lastBodypart = false;
        snakeTail.Add(newBodypart);
    }
}
