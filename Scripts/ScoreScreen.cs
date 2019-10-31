using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreen : MonoBehaviour
{
    TextMeshProUGUI timerScoreText;

    TextMeshProUGUI baseScoreText;

    TextMeshProUGUI collectableScoreText;

    TextMeshProUGUI artifactScoreText;

    TextMeshProUGUI totalScoreText;

    public GameObject[] stars;

    [SerializeField] int baseScoreDisplay;
    [SerializeField] int twoStarRewardRequirement = 100000;
    [SerializeField] int threeStarRewardRequirement = 200000;

    string timeScoreDisplayMinutes;
    string timeScoreDisplaySeconds;

    float totalTimeScore;

    int collectableScore;
    public int collectableTotal;

    int artifactScore;
    public int artifactTotal;

    int totalScore;

    AudioSource audioSource;
    [SerializeField] AudioClip starSound;
    [SerializeField] AudioClip openingSound;

    public int totalGameStars = 0;

    public int rewardedStars = 0;

    public bool levelCompleted;

    public bool starsActive;

    public bool twoStarsActive;

    public int levelNumber;

    public List<int> planetLevelData = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    public int levelsReached = 1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        baseScoreText = GameObject.Find("Score Label Text").GetComponent<TextMeshProUGUI>();
        timerScoreText = GameObject.Find("Time Label Text").GetComponent<TextMeshProUGUI>();
        collectableScoreText = GameObject.Find("Collectables Label Text").GetComponent<TextMeshProUGUI>();
        artifactScoreText = GameObject.Find("Artifacts Label Text").GetComponent<TextMeshProUGUI>();
        totalScoreText = GameObject.Find("Total Label Text").GetComponent<TextMeshProUGUI>();
    }

    public void RestartLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneIndex = currentSceneIndex + 1;

        if (sceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public void CalculateTimeScore()
    {
        PlayerData data = SavingSystem.LoadPlayer();
        totalGameStars += data.totalStars;
        twoStarsActive = data.twoStarsActive;
        levelCompleted = data.levelComplete;
        planetLevelData = data.planetLevelData;
        levelsReached = data.levelsReached;

        audioSource.PlayOneShot(openingSound);
        timeScoreDisplayMinutes = GameObject.Find("Time Display").GetComponent<TimeDisplay>().minutes;
        timeScoreDisplaySeconds = GameObject.Find("Time Display").GetComponent<TimeDisplay>().secdonds;
        totalTimeScore = GameObject.Find("Time Display").GetComponent<TimeDisplay>().timeTotalScore;
        timerScoreText.text = timeScoreDisplayMinutes + ":" + timeScoreDisplaySeconds;
    }

    public void CalculateBaseScore()
    {
        baseScoreDisplay = GameObject.Find("Score Display").GetComponent<ScoreDisplay>().score;
        totalScore += baseScoreDisplay * 2;
        baseScoreText.text = baseScoreDisplay.ToString();
    }

    public void CalculateCollectablesScore()
    {
        collectableScore = GameObject.Find("Score Display").GetComponent<ScoreDisplay>().collectableScore;

        if (collectableScore == collectableTotal)
        {
            totalScore += (collectableScore * 2) + 555;
        }
        else
        {
            totalScore += collectableScore * 2;
        }
        collectableScoreText.text = collectableScore.ToString() + " / " + collectableTotal.ToString();
    }

    public void CalculateArtifactScore()
    {
        artifactScore = GameObject.Find("Score Display").GetComponent<ScoreDisplay>().artifactScore;
        artifactScoreText.text = artifactScore.ToString() + " / " + artifactTotal.ToString();
        int artifactPoints = GameObject.Find("Score Display").GetComponent<ScoreDisplay>().artifactPoints;
        totalScore += artifactPoints;
    }

    public void CalculateTotalScore()
    {
        totalScore -= (int)totalTimeScore;
        totalScore *= 10;
        totalScoreText.text = totalScore.ToString();
        CalculateStarRewards();
    }

    private void CalculateStarRewards()
    {
        if (totalScore < twoStarRewardRequirement && totalScore > 0)
        {
            StartCoroutine(AddOneStar());
        }

        if (totalScore > twoStarRewardRequirement && totalScore < threeStarRewardRequirement)
        {
            StartCoroutine(AddTwoStar());
        }

        if (totalScore > threeStarRewardRequirement)
        {
            StartCoroutine(AddThreeStar());
        }
    }

    public IEnumerator AddOneStar()
    {
        yield return new WaitForSeconds(0.6f);
        stars[0].SetActive(true);
        audioSource.PlayOneShot(starSound);
        SendOneStarData();
        Save();
    }

    private void SendOneStarData()
    {
        if (planetLevelData[levelNumber] >= 1) return;

        if (planetLevelData[levelNumber] == 0)
        {
            totalGameStars += 1;
        }

        else if (planetLevelData[levelNumber] == 1)
        {
            totalGameStars += 0;
        }

        else if (planetLevelData[levelNumber] == 2)
        {
            totalGameStars += 0;
        }

        else if (planetLevelData[levelNumber] == 3)
        {
            totalGameStars += 0;
        }

        planetLevelData.RemoveAt(levelNumber);
        planetLevelData.Insert(levelNumber, 1);
        starsActive = true;

        print(levelsReached);

        if (levelNumber == levelsReached - 1)
        {
            levelsReached += 1;
            print(levelsReached);
        }
    }

    public IEnumerator AddTwoStar()
    {
        yield return new WaitForSeconds(0.6f);
        stars[0].SetActive(true);
        audioSource.PlayOneShot(starSound);
        yield return new WaitForSeconds(0.6f);
        stars[1].SetActive(true);
        audioSource.PlayOneShot(starSound);
        SendTwoStarData();
        Save();
    }

    private void SendTwoStarData()
    {
        if (planetLevelData[levelNumber] >= 2) return;

        if (planetLevelData[levelNumber] == 0)
        {
            totalGameStars += 2;
        }

        else if (planetLevelData[levelNumber] == 1)
        {
            totalGameStars += 1;
        }

        else if (planetLevelData[levelNumber] == 2)
        {
            totalGameStars += 0;
        }

        else if (planetLevelData[levelNumber] == 3)
        {
            totalGameStars += 0;
        }

        planetLevelData.RemoveAt(levelNumber);
        planetLevelData.Insert(levelNumber, 2);
        twoStarsActive = true;

        print(levelsReached);

        if (levelNumber == levelsReached - 1)
        {
            levelsReached += 1;
            print(levelsReached);
        }
    }

    public IEnumerator AddThreeStar()
    {
        yield return new WaitForSeconds(0.6f);
        stars[0].SetActive(true);
        audioSource.PlayOneShot(starSound);
        yield return new WaitForSeconds(0.6f);
        stars[1].SetActive(true);
        audioSource.PlayOneShot(starSound);
        yield return new WaitForSeconds(0.6f);
        stars[2].SetActive(true);
        audioSource.PlayOneShot(starSound);
        SendThreeStarData();
        Save();
    }

    private void SendThreeStarData()
    {
        if (planetLevelData[levelNumber] == 3) return;

        if (planetLevelData[levelNumber] == 0)
        {
            totalGameStars += 3;
        }

        else if (planetLevelData[levelNumber] == 1)
        {
            totalGameStars += 2;
        }

        else if (planetLevelData[levelNumber] == 2)
        {
            totalGameStars += 1;
        }

        else if (planetLevelData[levelNumber] == 3)
        {
            totalGameStars += 0;
        }

        planetLevelData.RemoveAt(levelNumber);
        planetLevelData.Insert(levelNumber, 3);
        levelCompleted = true;

        print(levelsReached);

        if (levelNumber == levelsReached - 1)
        {
            levelsReached += 1;
            print(levelsReached);
        }
    }

    public void Save()
    {
        SavingSystem.SavePlayer(this);
    }
}
