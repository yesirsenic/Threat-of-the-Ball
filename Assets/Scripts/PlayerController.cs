using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 15f;
    private float fall_BoardY = -5f;
    private float dead_Delay = 2f;
    private Vector3 last_Pos;
    private Rigidbody playerRb;
    private playerDirectionState playerstate;
    private GameManager gameManager;
    private SoundManager soundManager;
    private Animator player_Anim;

    enum playerDirectionState 
    {
        forward, right, back, left
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        player_Anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playerstate = playerDirectionState.forward;
        last_Pos = transform.position;
    }

    private void Update()
    {
        Player_Fall();
        
    }

    private void FixedUpdate()
    {
        if(gameManager.isActivate)
        {
            PlayerMove();
            Player_Walk();
        }
    }

    void PlayerMove() // 키 입력에 따른 방향 이동 및 캐릭터 바라보는 방향 조절
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 vel = Vector3.forward * speed;
            vel.y = playerRb.velocity.y;
            playerRb.AddForce(vel - playerRb.velocity, ForceMode.VelocityChange);
            playerstate = playerDirectionState.forward;
            transform.rotation = Quaternion.Euler(player_DirectionRotation(playerstate));
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 vel = Vector3.right * speed;
            vel.y = playerRb.velocity.y;
            playerRb.AddForce(vel - playerRb.velocity, ForceMode.VelocityChange);
            playerstate = playerDirectionState.right;
            transform.rotation = Quaternion.Euler(player_DirectionRotation(playerstate));
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 vel = Vector3.back * speed;
            vel.y = playerRb.velocity.y;
            playerRb.AddForce(vel - playerRb.velocity, ForceMode.VelocityChange);
            playerstate = playerDirectionState.back;
            transform.rotation = Quaternion.Euler(player_DirectionRotation(playerstate));
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 vel = Vector3.left * speed;
            vel.y = playerRb.velocity.y;
            playerRb.AddForce(vel - playerRb.velocity, ForceMode.VelocityChange);
            playerstate = playerDirectionState.left;
            transform.rotation = Quaternion.Euler(player_DirectionRotation(playerstate));
        }

    }

    void Player_Walk() // 플레이어 움직일 시 walk 애니메이션 작동
    {
        if(transform.position != last_Pos)
        {
            player_Anim.SetFloat("Speed_f", 0.3f);
        }

        else
        {
            player_Anim.SetFloat("Speed_f", 0f);
        }

        last_Pos = transform.position;
    }

    IEnumerator Dead() // 플레이어 Dead 애니메이션 실행 후 게임 오버 오브젝트 활성화
    {
        player_Anim.SetBool("Death_b", true);
        player_Anim.SetInteger("DeathType_int", 1);
        soundManager.Dead();

        yield return new WaitForSeconds(dead_Delay);

        gameManager.gameoverObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Ball")) // 플레이어가 공에 충돌 할 시 게임 오버 시스템 작동 및 Dead 애니메이션 실행
        {
            Debug.Log("GameOver");
            gameManager.isActivate = false;
            StartCoroutine(Dead());
            
        }
    }

    Vector3 player_DirectionRotation(playerDirectionState player_direction) // 캐릭터 바라보는 방향의 각도 구하기
    {
        return new Vector3(0, (int)player_direction * 90, 0);
    }

    void Player_Fall()
    {
        if(transform.position.y < fall_BoardY)
        {
            gameManager.isActivate = false;
            gameManager.gameoverObject.SetActive(true);
            soundManager.Dead();
        }
    }


}
