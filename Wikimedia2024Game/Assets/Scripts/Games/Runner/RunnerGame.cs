using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunnerGame : Game
{
    [SerializeField] private TMP_Text debugText;

    [SerializeField] private RunnerLevelData[] levelsData;
    [SerializeField] private Transform[] obstacleSpawnPoints;
    [SerializeField] private int[] layerByLine;
    [SerializeField] private float obstacleDissapearPoint;
    [SerializeField] private RunnerShip ship;
    [SerializeField] private Slider shipStatusBar;
    [SerializeField] private Slider travelDistanceBar;
    [SerializeField] private float SpeedMultiplierWhenRushing = 3;
    [SerializeField] private float SpeedMultiplierWhenNotRushing = 1.5f;

    private int currentLevel;
    private List<GameObject> activeObstacles;
    private RunnerLevelData currentLevelData;
    public float RushSpeedMultiplier { get; private set; }

    private int maxAchievablePoints;
    private int totalPoints;
    private int totObstaclesSpawned;

    protected override void StartGame()
    {
        MySoundManager.PlayMusicLoop("Sound/Runner/MusicRunner");

        debugText.text = "";

        RushSpeedMultiplier = 1;

        maxAchievablePoints = 0;
        foreach (var lvlData in levelsData)
        {
            maxAchievablePoints += lvlData.obstaclesToLevelUp;
        }
        totalPoints = maxAchievablePoints;

        shipStatusBar.minValue = 0;
        shipStatusBar.maxValue = maxAchievablePoints;
        shipStatusBar.value = totalPoints;

        totObstaclesSpawned = 0;
        travelDistanceBar.minValue = 0;
        travelDistanceBar.maxValue = maxAchievablePoints + 1;
        travelDistanceBar.value = totObstaclesSpawned;

        ship.OnCrash.AddListener(OnObstacleCrash);

        StartCoroutine(UpdateGameState());
    }

    private void OnObstacleCrash(GameObject obstacle)
    {
        MySoundManager.PlaySfxSound("Sound/Runner/SFXImpact");

        obstacle.GetComponent<RunnerEnemy>().CrashAnim();

        totalPoints--;
        shipStatusBar.value = totalPoints;

        activeObstacles.Remove(obstacle);
        StartCoroutine(DestroyInSeconds(obstacle, 1));
    }

    private IEnumerator DestroyInSeconds(GameObject objToDestroy, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(objToDestroy);
    }

    private IEnumerator UpdateGameState()
    {
        activeObstacles = new List<GameObject>();

        currentLevel = 0;
        while (currentLevel < levelsData.Length)
        {
            currentLevelData = levelsData[currentLevel];

            var spawnedObstacles = 0;
            var lastSpawnedLine = 0;
            while (spawnedObstacles < currentLevelData.obstaclesToLevelUp)
            {
                float secondsToNextsObstacle = Random.Range(currentLevelData.minTimeBetweenObstacles, currentLevelData.maxTimeBetweenObstacles);
                yield return new WaitForSeconds(secondsToNextsObstacle / RushSpeedMultiplier);

                while (isPaused) { yield return new WaitForEndOfFrame(); }

                var lineToSpawn = ARandomLineToSpawn(spawnedObstacles, lastSpawnedLine);
                lastSpawnedLine = lineToSpawn;
                var obstacleToSpawn = currentLevelData.availableObstacleTypes[Random.Range(0, currentLevelData.availableObstacleTypes.Length)];
                var spawnPoint = obstacleSpawnPoints[lineToSpawn];
                var newObstacle = Instantiate<GameObject>(obstacleToSpawn, spawnPoint);
                newObstacle.GetComponent<SpriteRenderer>().sortingOrder = layerByLine[lineToSpawn];
                activeObstacles.Add(newObstacle);
                spawnedObstacles++;
                totObstaclesSpawned++;
                travelDistanceBar.value = totObstaclesSpawned;
            }
            //yield return new WaitForSeconds(currentLevelData.minTimeBetweenObstacles / RushSpeedMultiplier);
            while (isPaused) { yield return new WaitForEndOfFrame(); }

            currentLevel++;
        }
        totObstaclesSpawned++;
        travelDistanceBar.value = totObstaclesSpawned;

        yield return new WaitForSeconds(2f);
        MySoundManager.StopMusic();
        yield return new WaitForEndOfFrame();
        MySoundManager.PlayMusicLoop("Sound/Runner/SFXWinRunner", false);
        yield return new WaitForSeconds(2.5f);
        MySoundManager.PlayMusicLoop("Sound/Runner/MusicRunner");
        while (isPaused) { yield return new WaitForEndOfFrame(); }
        ShowGameOverPopup(true, totalPoints, maxAchievablePoints);
    }

    private int ARandomLineToSpawn(int spawnedObstacles, int lastSpawnedLine)
    {
        if (spawnedObstacles == 0)
            return ship.currentLine;
        else
        {
            var aLine = Random.Range(0, obstacleSpawnPoints.Length);
            while (aLine == lastSpawnedLine)
            {
                aLine = Random.Range(0, obstacleSpawnPoints.Length);
            }
            return aLine;
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            bool wasRushingAlready = RushSpeedMultiplier != SpeedMultiplierWhenNotRushing;
            RushSpeedMultiplier = Input.GetKey(KeyCode.RightArrow) ? SpeedMultiplierWhenRushing : SpeedMultiplierWhenNotRushing;

            if(!wasRushingAlready && RushSpeedMultiplier != SpeedMultiplierWhenNotRushing) //Empezó a acelerar en este frame
            {
                MySoundManager.PlaySfxSound("Sound/Runner/SFXAcceleration");
            }

            List<GameObject> objectsToRemove = new List<GameObject>();

            foreach (GameObject obstacle in activeObstacles)
            {
                obstacle.transform.Translate(-currentLevelData.obstaclesSpeed * Time.fixedDeltaTime * RushSpeedMultiplier, 0, 0);

                if (obstacle.transform.localPosition.x < obstacleDissapearPoint)
                {
                    objectsToRemove.Add(obstacle);
                }
            }

            foreach (var objectToRemove in objectsToRemove)
            {
                activeObstacles.Remove(objectToRemove);
                Destroy(objectToRemove);

            }
        }
#if DEBUG
        debugText.text = "maxAchievablePoints: " + maxAchievablePoints + "\n" +
                         "totalPoints: " + totalPoints + "\n" +
                         "level: " + (currentLevel+1) + "/" + levelsData.Length + "\n" +
                         "obstacles spawned: " + totObstaclesSpawned + "/" + maxAchievablePoints;
#endif
    }

    public void BtnGanarClick()
    {
        ShowGameOverPopup(true, maxAchievablePoints, maxAchievablePoints);
    }

    public void BtnPerderClick()
    {
        ShowGameOverPopup(false, 0, maxAchievablePoints);
    }

    protected override void ContinueToNextLevel()
    {
        SceneManager.LoadScene("Cutscene01_a");
    }

    protected override void PlayAgain()
    {
        SceneManager.LoadScene("Game01_Runner");
    }

    protected override void MoreInfo()
    {
        Application.OpenURL(Links.InfoLinkMonstruosMarinos);
    }

    protected override string ResultText(int achievedStars)
    {
        switch (achievedStars)
        {
            case 0:
                return "¡Peligro de naufragio!";
                break;
            case 1:
                return "¡Bien!";
                break;
            case 2:
                return "¡Lo hiciste muy bien!";
                break;
            case 3:
                return "¡Viaje perfecto!";
                break;
            default:
                return "";
                break;
        }
    }

    protected override int LvlNumber()
    {
        return 1;
    }
}