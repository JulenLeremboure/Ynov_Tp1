using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class KnightCollision : MonoBehaviour
{
    public int nbPiepiece;
    public Text piepieceText;
    public TMP_Text gameOverScoreText;
    public GameObject canvasGameOver;

    public SpawnManager spawnManager;

    private void Start()
    {
        SetAndUpdateScore(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piepiece"))
        {
            Destroy(collision.gameObject);
            SetAndUpdateScore(nbPiepiece + 1);
        }
        else if(collision.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnPlatformWhenTriggerEntered();
        }
        else if(collision.CompareTag("DeathTrigger"))
        {
            gameOverScoreText.text = "" + nbPiepiece;
            canvasGameOver.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void SetAndUpdateScore(int newScore)
    {
        nbPiepiece = newScore;
        piepieceText.text = nbPiepiece.ToString();
    }
}
