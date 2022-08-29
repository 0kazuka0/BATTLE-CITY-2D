using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreResult : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Scoretotal;
    public TextMeshProUGUI ScoreT;
    public TextMeshProUGUI ScoreFT;


    // Update is called once per frame
    void Update()
    {
        done1();
        done2();
        done3();
        Invoke("scenechange", 14f);
    }
    void done1()
    {      
         Scoretotal.text = gamevalue.score.ToString();   
    }
    void done2()
    {
        ScoreT.text = "X  " + gamevalue.scoreT.ToString();
    }
    void done3()
    {
        ScoreFT.text = "X  " + gamevalue.scoreFT.ToString();
    }
    void scenechange()
    {
        SceneManager.LoadScene(0);
    }
}
