using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Global : MonoBehaviour
{
    // use for progress bar display
    public float score;
    public float maxScore;
    // game difficulty change with time
    public float gameTimer;
    public float gamePeriod;
    // spawn enemy spacecrtafts
    public float spawnTimer;
    public float spawnPeriod;
    public int numberSpawnedEachPeriod;
    // spawn airdrops
    public float airdropTimer;
    public float airdropPeriod;
    public float airDropMaxPeriod;
    // prefabs
    public GameObject spacecraft1;
    public GameObject spacecraft2;
    public GameObject spacecraft3;
    public GameObject airdropHeart;
    public GameObject airdropBullet;
    public GameObject airdropPower;
    // on screen score and max score display
    Text dis;
    Text DisRecord;
    // progress bar filler image
    Image fillImage;
    // fire button when progress bar is full
    GameObject fireButton;
    int coin;
    float difficultyTrack;
    public List<GameObject> spacecrafts;

    // Start is called before the first frame update
    void Start()
    {
        // inital value
        score = 0;
        maxScore = 50;
        gameTimer = 0;
        gamePeriod = 0;
        spawnTimer = 0;
        spawnPeriod = 4.0f;
        numberSpawnedEachPeriod = 2;
        airdropTimer = 0;
        airDropMaxPeriod = 20;
        airdropPeriod = Random.Range(10f, airDropMaxPeriod);
        // prepare on screen display and progress bar
        dis = GameObject.Find("Dis").GetComponent<Text>();
        DisRecord = GameObject.Find("DisRecord").GetComponent<Text>();
        DisRecord.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        fillImage = GameObject.Find("ProgressBarFiller").GetComponent<Image>();
        fireButton = GameObject.Find("fire");
        fireButton.SetActive(false);
        coin = 0;
        difficultyTrack = 1f;
        spacecrafts = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        List<int> spaceChecker = new List<int>();
        for (int i = 0; i < 47; i++)
        {
            spaceChecker.Add(0);
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnPeriod) {
            spawnTimer = 0;
            for (int i = 0; i < numberSpawnedEachPeriod; i++) {
                bool find = false;
                float posX = 0f;
                while (!find) {
                    posX = Random.Range(-11f, 11f);
                    int k = (int)((posX + 11.5) * 2);
                    if (spaceChecker[k - 1] == 0 && spaceChecker[k] == 0 && spaceChecker[k + 1] == 0) {
                        find = true;
                        spaceChecker[k - 1] = 1;
                        spaceChecker[k] = 1;
                        spaceChecker[k + 1] = 1;
                    }
                }
                Vector3 spawnPos = new Vector3(posX, 0f, 23f);
                float roll = Random.Range(0f, 10f);
                if (roll < 3f)
                {
                    spawnPos.z = 31.8f;
                    GameObject obj = Instantiate(spacecraft1, spawnPos, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                    SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                    spacecrafts.Add(obj);
                    Spacecraft s = obj.GetComponent<Spacecraft>();
                    s.movingSpeed = 6f;
                    s.life = (int) (3 * difficultyTrack);
                    s.point = 2;
                    s.firingSpeed = 6.0f;
                    s.bulletSpeed = -12.0f;
                    s.bulletDamage = 2 * (1 + (int)(difficultyTrack / 6));
                    s.bulletNumber = 1;

                }
                else if (roll < 7f)
                {
                    spawnPos.z = 30.8f;
                    GameObject obj = Instantiate(spacecraft2, spawnPos, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                    SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                    spacecrafts.Add(obj);
                    Spacecraft s = obj.GetComponent<Spacecraft>();
                    s.movingSpeed = 10f;
                    s.life = (int)(2 * difficultyTrack);
                    s.point = 1;
                    s.firingSpeed = 4.0f;
                    s.bulletSpeed = -15.0f;
                    s.bulletDamage = 1 * (1 + (int)(difficultyTrack / 6));
                    s.bulletNumber = 1;
                }
                else {
                    spawnPos.z = 30.8f;
                    GameObject obj = Instantiate(spacecraft3, spawnPos, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                    SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                    spacecrafts.Add(obj);
                    Spacecraft s = obj.GetComponent<Spacecraft>();
                    s.movingSpeed = 15f;
                    s.life = (int)(1 * difficultyTrack);
                    s.point = 3;
                    s.firingSpeed = 4f;
                    s.bulletSpeed = -20.0f;
                    s.bulletDamage = 1 * (1 + (int)(difficultyTrack / 6));
                    s.bulletNumber = 1;
                }
            }
        }

        airdropTimer += Time.deltaTime;
        if (airdropTimer >= airdropPeriod)
        {
            airdropTimer = 0;
            airDropMaxPeriod *= 1.1f;
            airdropPeriod = Random.Range(6f, airDropMaxPeriod);
            bool find = false;
            float posX = 0f;
            while (!find)
            {
                posX = Random.Range(-11f, 11f);
                int k = (int)((posX + 11.5) * 2);
                if (spaceChecker[k - 1] == 0 && spaceChecker[k] == 0 && spaceChecker[k + 1] == 0)
                {
                    find = true;
                    spaceChecker[k - 1] = 1;
                    spaceChecker[k] = 1;
                    spaceChecker[k + 1] = 1;
                }
            }
            Vector3 spawnPos = new Vector3(posX, 0f, 29f);
            float roll = Random.Range(0f, 10f);
            if (roll < 2f)
            { 
                GameObject obj = Instantiate(airdropHeart, spawnPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
            }
            else if (roll < 6f)
            {
                GameObject obj = Instantiate(airdropBullet, spawnPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
            }
            else
            {
                GameObject obj = Instantiate(airdropPower, spawnPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
            }
        }

        gameTimer += Time.deltaTime;
        if (gameTimer > 20) {
            gameTimer = 0;
            if (coin == 0)
            {
                spawnPeriod *= 0.85f;
            }
            else {
                numberSpawnedEachPeriod += 1;
            }
            coin = 1 - coin;
            difficultyTrack *= 1.5f;
        }
    }

    public void gameover() {
        int curDis = int.Parse(dis.text);
        PlayerPrefs.SetInt("CurScore", curDis);
        PlayerPrefs.Save();

        SceneManager.UnloadSceneAsync("GameplayScene");
        SceneManager.LoadSceneAsync("GameoverScene", LoadSceneMode.Additive);
    }

    public void updateScore(int s) {
        score += (float) s;
        score = Mathf.Clamp(score, 0f, maxScore);
        fillImage.fillAmount = score / maxScore;
        if (score == maxScore)
        {
            fireButton.SetActive(true);
        }
    }

    public void fire() {
        for (int i = spacecrafts.Count - 1; i >= 0; i--) {
            GameObject g = spacecrafts[i];
            if (g != null) {
                Spacecraft s = g.GetComponent<Spacecraft>();
                s.UpdateLife(99);
            }
            spacecrafts.RemoveAt(i);
        }
        score = 0f;
        maxScore *= 1.2f;
        fillImage.fillAmount = score / maxScore;
        fireButton.SetActive(false);
    }

    public void destroyExplosionHelper(GameObject obj) {
        StartCoroutine(waitAndDestroy(obj));
    }

    public IEnumerator waitAndDestroy(GameObject obj) {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    }
}
