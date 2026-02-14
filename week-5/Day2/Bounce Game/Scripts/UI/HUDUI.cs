using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ringsText;
    public TMPro.TextMeshProUGUI levelText;
    public Image energyBar;

    public void UpdateRings(int count)
    {
        if (ringsText != null)
            ringsText.text = $"Rings: {count}";
    }

    public void UpdateEnergy(float value)
    {
        if (energyBar != null)
            energyBar.fillAmount = value;
    }
    
    public void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }
}