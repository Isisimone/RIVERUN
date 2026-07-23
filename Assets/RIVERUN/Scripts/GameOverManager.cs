using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text gemsText;
    public TMP_Text distanceText;
    public TMP_Text highScoreText;

    void Start()
    {
        int gems = PlayerPrefs.GetInt("LastGems", 0);
        float distance = PlayerPrefs.GetFloat("LastDistance", 0);
        float highScore = PlayerPrefs.GetFloat("BestDistance", 0);

        gemsText.text = gems.ToString();
        distanceText.text = (distance / 1000f).ToString("F2") + " km";
        highScoreText.text = (highScore / 1000f).ToString("F2") + " km";
    }

    public void Retry()
    {
        SceneManager.LoadScene("RIVERUN");
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }
}