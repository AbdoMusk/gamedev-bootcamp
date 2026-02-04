using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMPro.TextMeshProUGUI collectibleText;
    int count;

    void Awake() { instance = this; }

    public void AddCollectible()
    {
        count++;
        collectibleText.text = "Collected: " + count;
    }

}
