using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    public UnityEvent fadeFinished;
    [SerializeField] Image fadeImage = null;

	private void Awake()
	{
        fadeFinished = new UnityEvent();
	}
	// Start is called before the first frame update
	public void Fade(bool fadeIn, float duration)
    {
        StartCoroutine(FadeImage(fadeIn, duration));
    }
    public void FadeInAndOut(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    private IEnumerator FadeIn(float duration)
    {
        Color objectColor = fadeImage.color;
        float fadeAmount;
        while (objectColor.a < 1)
        {
            fadeAmount = objectColor.a + (duration * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeImage.color = objectColor;
            yield return null;
        }
        fadeFinished.Invoke();
        yield return FadeOut(duration);
     }

    private IEnumerator FadeOut(float duration)
    {
        Color objectColor = fadeImage.color;
        float fadeAmount;
        while (objectColor.a > 0)
        {
            fadeAmount = objectColor.a - (duration * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeImage.color = objectColor;
            yield return null;
        }
    }

    private IEnumerator FadeImage(bool fadeIn, float duration)
	{
        Color objectColor = fadeImage.color;
        float fadeAmount;
        if (fadeIn)
		{
            while (objectColor.a < 1)
			{
                fadeAmount = objectColor.a + (duration * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeImage.color = objectColor;
                yield return null;
			}
		}
        else
		{
            while (objectColor.a > 0)
            {
                fadeAmount = objectColor.a - (duration * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeImage.color = objectColor;
                yield return null;
            }
        }
	}

}
