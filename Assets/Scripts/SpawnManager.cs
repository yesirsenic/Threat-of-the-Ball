using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private float start_Delay = 1.5f;
    private float repeat_Delay = 1.5f;
    private float delay_Decrease = 0.02f;
    private float time = 0;
    private GameManager gameManager;

    public GameObject[] spawnPos;
    public GameObject[] ball;
    public Text time_Text;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        time_Text.text = "Time: " + time;
        StartCoroutine(BallSpawn());
        StartCoroutine(TimeFlow());
    }

    void BallRandomSpawn() // ���� ��ġ�� ���� �� ����
    {
        int randomIndex = Random.Range(0, spawnPos.Length);
        int randomIndex_Ball = Random.Range(0, ball.Length);

        GameObject spawnBall = ball[randomIndex_Ball];
        Vector3 randomspawnPos = spawnPos[randomIndex].transform.position;

        Instantiate(spawnBall, randomspawnPos, spawnBall.transform.rotation);
    }

    IEnumerator BallSpawn() // ���� ������ �� ���� ���� �������� Ư�� �ݺ� ������ ������ �� ����
    {
        yield return new WaitForSeconds(start_Delay);

        while(gameManager.isActivate)
        {
            BallRandomSpawn();
            yield return new WaitForSeconds(repeat_Delay);
            repeat_Delay -= delay_Decrease;
        }
    }

    IEnumerator TimeFlow() // 1�ʰ� ���������� Time text ����
    {
        while(gameManager.isActivate)
        {
            yield return new WaitForSeconds(1f);
            time += 1;

            time_Text.text = "Time: " + time;
        }
    }
}
