using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fade;
    public float duration = 1f;


    public void ChangeToGame()
    {
        StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            fade.alpha = time / duration;
            yield return null;
        }

        SceneManager.LoadScene("Game");
    }
}