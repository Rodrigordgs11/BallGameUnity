using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public static float moveSpeed;
    public float steerSpeed;
    public GameObject bodyPrefab;

    private int score;
    public Text scoreText;
    private int level;
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

    public AudioSource[] sounds;

    void Start()
    {
        GameOver.SetActive(false);
        ResetButtonObject.SetActive(false);
        level2Food.SetActive(false);
        level = 1;
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);
            score++;
            sounds[0].Play();

            if(score == 8){
                level++;
                moveSpeed += 1.5f;  
                sounds[1].Play();
            }

            if(score == 16){
                level++;
                moveSpeed += 1.5f;
                sounds[1].Play();
            }

            if(score == 24){
                Win.SetActive(true);
                Time.timeScale = 0;
                ResetButtonObject.SetActive(true);
                sounds[3].Play();
                sounds[4].Stop();
            }

            levelText.text = "Level " + level.ToString();
        }

        if (collider.gameObject.tag == "wall" || collider.gameObject.tag == "trash")
        {
            GameOver.SetActive(true);
            Time.timeScale = 0;
            ResetButtonObject.SetActive(true);
            sounds[2].Play();
            sounds[4].Stop();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenu");
    }
}