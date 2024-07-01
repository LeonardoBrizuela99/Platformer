using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        // Asegúrate de que la imagen de fade comience transparente
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0.0f;
            fadeImage.color = color;
            fadeImage.enabled = false; // Deshabilitar la imagen al inicio
        }
    }

    public IEnumerator FadeOut()
    {
        fadeImage.enabled = true; // Habilitar la imagen antes de empezar el fade
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1.0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0.0f;
        fadeImage.color = color;
        fadeImage.enabled = false; // Deshabilitar la imagen después del fade
    }
}
