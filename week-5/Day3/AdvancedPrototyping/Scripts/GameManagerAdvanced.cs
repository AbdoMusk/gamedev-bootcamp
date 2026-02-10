using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMPro.TextMeshProUGUI messageText;
    public TMPro.TextMeshProUGUI coinsText;

    public static int coins;


    void Awake()
    {
        instance = this;
        messageText.text = "Quest:\nEnter BlueZone";
    }

    public void Win()
    {
        messageText.text = "YOU WIN!";
        Invoke("Restart", 3f);
    }

    public void TouchDanger()
    {
        messageText.text = "You Died";
        Invoke("Restart", .5f);
    }

    public void CollectCoin()
    {
        coins += 1;
        coinsText.text = "Magical Green Cubes: " + coins;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
