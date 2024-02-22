using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    // Za tablu i zidove:
    public Tile[ , ] tiles;
    public Tile tilePrefab, wallPrefab;
    [SerializeField] private int rows, cols;
    private Color grassColor1 = new Color(0.3f, 0.6f, 0.4f),
                  grassColor2 = new Color(0.3f, 0.7f, 0.4f);
    // Za zmiju:
    public Snake snakePrefab;
    private Snake snake;
    // Za jabuke:
    public Apple applePrefab;
    private Apple apple;



    private void Awake() {
        gameManager = this;

        tiles = new Tile[rows, cols];
    }


    private void Start() {
        DrawGrassTiles();
        DrawWalls();

        Camera.main.transform.position = new Vector3(rows / 2, cols / 2, -10.0f);

        SpawnSnake();
        SpawnApple();
    }


    private void DrawGrassTiles() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                Tile tile = Instantiate(tilePrefab, new Vector3(i, j, 0.0f), Quaternion.identity);
                tiles[i, j] = tile;

                if ((i + j) % 2 == 0) tiles[i, j].ChangeColor(grassColor1); //tiles[i, j].ChangeColor(grassColor1);
                else tiles[i, j].ChangeColor(grassColor2);

            }
        }
    }

    private void DrawWalls() {
        for (int i = -1; i < rows + 1; i++) Instantiate(wallPrefab, new Vector3(-1, i, 0.0f), Quaternion.identity);
        for (int i = -1; i < rows + 1; i++) Instantiate(wallPrefab, new Vector3(cols, i, 0.0f), Quaternion.identity);
        for (int i = 0; i < cols; i++) Instantiate(wallPrefab, new Vector3(i, -1, 0.0f), Quaternion.identity);
        for (int i = 0; i < cols; i++) Instantiate(wallPrefab, new Vector3(i, rows, 0.0f), Quaternion.identity);
    }


    private void SpawnSnake() {
        Tile middleTile = tiles[rows / 2, cols / 2];
        snake = Instantiate(snakePrefab, middleTile.transform.position, Quaternion.identity);
    }

    public void SpawnApple() {
        int tileRow = Random.Range(0, rows);
        int tileCol = Random.Range(0, cols);
        while (tiles[tileRow, tileCol].isOccupiedBody) {
            Debug.Log("Polje [" + tileRow + ',' + tileCol + "] je zauzeto.");
            tileCol++;
            if (tileCol >= cols) { 
                tileCol = 0; 
                tileRow++;
                if (tileRow >= rows) tileRow = 0;
            }
        }
        apple = Instantiate(applePrefab, tiles[tileRow, tileCol].transform.position, Quaternion.identity);
    }


    public void RestartGame() {
        snake.EraseTail();
        snake.transform.position = tiles[rows / 2, cols / 2].transform.position;

        Destroy(apple.gameObject);
        SpawnApple();
    }
}
