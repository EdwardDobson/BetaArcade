using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public RawImage TeamLogo;
    public RawImage TheArcade;
    public string loadLevel;

    IEnumerator Start()
    {
        TeamLogo.canvasRenderer.SetAlpha(0.0f);
        TheArcade.canvasRenderer.SetAlpha(0.0f);

        TeamLogo.CrossFadeAlpha(1.0f, 1.5f, false);
        yield return new WaitForSeconds(3.5f);
        TeamLogo.CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(3.5f);
        TheArcade.CrossFadeAlpha(1.0f, 1.5f, false);
        yield return new WaitForSeconds(3.5f);
        TheArcade.CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene(loadLevel);
    }

   

    
}
