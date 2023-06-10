using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public static float moveSpeed;
    public float steerSpeed;
    public GameObject bodyPrefab;
    public int Gap;

    public int score;
    public Text scoreText;
    public int level;
    public Text levelText;

    public GameObject GameOver;
    public GameObject Win;
    public GameObject ResetButtonObject;
    public GameObject snake;
    public GameObject level1trash;

    public GameObject level2Food;
    public GameObject level2trash;

    public GameObject level3Food;
    public GameObject level3trash;

    public AudioSource levelUpAudio;
    public AudioSource catchFoodAudio;

    List<GameObject> BodyParts = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();

    void Start()
    {
        GameOver.SetActive(false);
        ResetButtonObject.SetActive(false);
        level2Food.SetActive(false);
        level = 1;
        levelUpAudio = GetComponent<AudioSource>();
        catchFoodAudio = GetComponent<AudioSource>();

    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        PositionHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Clamp(index * Gap, 0, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }

        if(score == 8){
            level1trash.SetActive(false);
            level2Food.SetActive(true);
            level2trash.SetActive(true);
        }

        if(score == 16){
            level2Food.SetActive(false);
            level2trash.SetActive(false);
            level3Food.SetActive(true);
            level3trash.SetActive(true);
        }

        scoreText.text = "Score: " + score.ToString();
    }

    void GrowSnake()
    {
        GameObject body = Instantiate(bodyPrefab);
        BodyParts.Add(body);

        if (BodyParts.Count > 1)
        {
            Transform previousBody = BodyParts[BodyParts.Count - 2].transform;
            body.transform.parent = previousBody;
            body.transform.localPosition = new Vector3(0, 0, -previousBody.localScale.x);
            body.transform.localRotation = Quaternion.identity;
        }
        else
        {
            body.transform.parent = snake.transform;
            body.transform.localPosition = Vector3.zero;
            body.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);
            score++;
            catchFoodAudio.Play();

            if(score == 8){
                level++;
                moveSpeed += 1f;
                levelUpAudio.Play();
            }

            if(score == 16){
                level++;
                moveSpeed += 1.5f;
                levelUpAudio.Play();
            }

            if(score == 24){
                Win.SetActive(true);
                Time.timeScale = 0;
                ResetButtonObject.SetActive(true);
            }

            levelText.text = "Level " + level.ToString();
        }

        if (collider.gameObject.tag == "wall" || collider.gameObject.tag == "trash")
        {
            GameOver.SetActive(true);
            Time.timeScale = 0;
            ResetButtonObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenu");
    }
}