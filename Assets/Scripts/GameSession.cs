using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLifes = 3;
    [SerializeField] int coinsScore = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake() {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if(numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start() {
        livesText.text = playerLifes.ToString();
        scoreText.text = (coinsScore).ToString();
    }
    void Update() {
        scoreText.text = (coinsScore).ToString(); 
    }
    public void ProcessOfDeath()
    {
        if(playerLifes > 1)
            TakeLife();
        else
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int points)
    {
        coinsScore += points;
        scoreText.text = (coinsScore).ToString();
    }
    private void TakeLife()
    {
        int currentSceeneIndex = SceneManager.GetActiveScene().buildIndex;
        playerLifes--;
        SceneManager.LoadScene(currentSceeneIndex);
        livesText.text = playerLifes.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<Persist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
