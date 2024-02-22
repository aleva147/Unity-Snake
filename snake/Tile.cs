using UnityEngine;

public class Tile : MonoBehaviour {
    public bool isOccupiedHead, isOccupiedBody;

    

    public void ChangeColor(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") isOccupiedHead = true;
        if (collision.tag == "Bodypart") isOccupiedBody = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") isOccupiedHead = false;
    }
}
