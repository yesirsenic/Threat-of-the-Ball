using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed;
    float minspeed;
    float maxspeed;
    float y_Board;
    float strength = 50f;
    Rigidbody ballRb;
    Vector3 goal;
    Vector3 direction;

    AudioSource audioSource;
    GameManager gameManager;
    SoundManager soundManager;

    private void Awake()
    {
        goal = new Vector3(0, 0, 0);
        minspeed = 30f;
        maxspeed = 60f;
    }

    private void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        direction = (goal - transform.position).normalized;
        ballRb.AddForce(direction * RandomSpeed(), ForceMode.Impulse);
        y_Board = -5f;
    }

    private void Update()
    {
        DestroyBall();
    }

    float RandomSpeed()
    {
        speed = Random.Range(minspeed, maxspeed);

        return speed;
    }

    void DestroyBall() // 특정 Y pos 밑으로 이동할 경우 공 삭제
    {
        if(transform.position.y < y_Board)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            soundManager.StoneHit(audioSource);
        }
    }
}
