using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public Image background;
    public Sprite[] images;

    public TMP_Text storyText;

    [TextArea]
    public string[] texts;

    public float imageTime = 3f;
    public float typingSpeed = 0.04f;

    public string gameScene = "RIVERUN";

    // Fade transición
    public CanvasGroup fadePanel;
    public float fadeDuration = 1.5f;


    IEnumerator Start()
    {
        // Asegurar que el fade empieza invisible
        fadePanel.alpha = 0;


        for (int i = 0; i < images.Length; i++)
        {
            background.sprite = images[i];

            yield return StartCoroutine(TypeText(texts[i]));

            yield return new WaitForSeconds(imageTime);
        }


        // Transición a negro
        yield return StartCoroutine(FadeToBlack());


        // Cambiar al gameplay
        SceneManager.LoadScene(gameScene);
    }


    IEnumerator TypeText(string text)
    {
        storyText.text = "";


        foreach (char letter in text)
        {
            storyText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }


    IEnumerator FadeToBlack()
    {
        float time = 0;


        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            fadePanel.alpha = time / fadeDuration;

            yield return null;
        }


        fadePanel.alpha = 1;
    }
}