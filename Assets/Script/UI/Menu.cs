using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void scenechange(int scene)
    {
        GameController.enemylife = 12;

        gamevalue.gettanklifeup = 3;
        gamevalue.gettimerfreeze = false;
        gamevalue.getgranadenuke = false;
        gamevalue.getshovelIwall = false;

        gamevalue.gameover = false;

        gamevalue.score = 0;
        gamevalue.scoreT = 0;
        gamevalue.scoreFT = 0;

        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
