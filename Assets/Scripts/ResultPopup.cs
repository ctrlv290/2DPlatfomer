using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{
    //public GameObject highScoreLabel;
    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;
    public GameObject highScorePopup;

    // Start is called before the first frame update

    private void OnEnable()
    {
        Time.timeScale = 0;

        if (GameManager.Instance.isCleared)
        {

            SaveHighScore();


            // 단순 하이스코어 
            //    float highScore = PlayerPrefs.GetFloat("HighScore", 0);

            //    if (highScore < GameManager.Instance.timeLimit)
            //    {

            //        highScoreLabel.SetActive(true);

            //        PlayerPrefs.SetFloat("HighScore", GameManager.Instance.timeLimit);
            //        PlayerPrefs.Save();
            //    }
            //    else {
            //        highScoreLabel.SetActive(false);
            //    }




            titleLabel.text = "Cleared!!";
            scoreLabel.text = GameManager.Instance.timeLimit.ToString("#.##");
        }
        else {
            titleLabel.text = "Game Over..";
            scoreLabel.text = "";
        }
    }

    void SaveHighScore() { 
        float score = GameManager.Instance.timeLimit;
        string currenScoreString = score.ToString("#.###");

        string savedScoreString = PlayerPrefs.GetString("HighScores","");

        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currenScoreString);
        }
        else {
            string[] scoreArray = savedScoreString.Split(',');
            List<string> scoresList = new List<string>(scoreArray);

            for (int i = 0; i < scoresList.Count; i++) {
                float savedScore = float.Parse(scoresList[i]);
                if (savedScore < score) {
                    scoresList.Insert(i, currenScoreString);
                    break;
                }
            }
            if (scoreArray.Length == scoresList.Count) { 
                scoresList.Add(currenScoreString);
            }

            if (scoresList.Count > 10) {
                scoresList.RemoveAt(10);
            }

            string result = string.Join(",", scoresList);

            PlayerPrefs.SetString("HighScores", result);
        }
        Debug.Log(PlayerPrefs.GetString("HighScores"));
        PlayerPrefs.Save();



    }




    public void PlayAgainPressed() {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void HighScorePressed() { 
        highScorePopup.SetActive(true);
    }
}
