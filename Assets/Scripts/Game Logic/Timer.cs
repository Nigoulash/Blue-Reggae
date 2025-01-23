using UnityEngine;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOver;


    private void Start()
    {
        gameOver.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 11f)
            {
                timerText.color = Color.red;
            }

        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
            GameOver();
        }


        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    void GameOver()
    {

        GameManager.canMove = false;
        StartCoroutine ("StartingOver");
        gameOver.SetActive(true);

    }

    IEnumerator StartingOver()
    { 
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
