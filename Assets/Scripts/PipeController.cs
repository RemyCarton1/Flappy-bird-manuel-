using System;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float maxTime = 1.5f;
    public float heightRange = 0.45f;
    public GameObject pipe;
    public float pipeSpeed = 6f;
    public float pipeDestroy = -10f;
    public float verticalMoveRange = 0.5f; // qué tanto sube y baja
    public float verticalMoveSpeed = 1.5f; // velocidad del movimiento vertical
    public int scoreToStartMoving = 10; // a partir de qué score se activa el movimiento

    private float timer;

    private void Start()
    {
        if (GameController.instance.canPlay)
        {
            SpawnPipe();
        }
    }

    private void Update()
    {
        if (GameController.instance.canPlay)
        {
            timer += Time.deltaTime;

            if (timer > maxTime)
            {
                SpawnPipe();
                timer = 0;
            }
        }
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, UnityEngine.Random.Range(-heightRange, heightRange));
        GameObject newPipe = Instantiate(pipe, spawnPos, Quaternion.identity);

        if (GameController.instance.canPlay)
        {
            // Movimiento horizontal (normal)
            Vector3 targetPos = new Vector3(pipeDestroy, newPipe.transform.position.y, newPipe.transform.position.z);
            LeanTween.move(newPipe, targetPos, pipeSpeed)
                     .setEaseLinear()
                     .setOnComplete(() => Destroy(newPipe));

            // 💡 Movimiento vertical (dificultad adicional)
            int currentScore = ScoreManager.instance.GetScore();
            if (currentScore >= scoreToStartMoving)
            {
                // Movimiento de vaivén vertical (oscilante)
                LeanTween.moveY(newPipe, newPipe.transform.position.y + verticalMoveRange, verticalMoveSpeed)
                         .setEaseInOutSine()
                         .setLoopPingPong();
            }
        }
        else
        {
            LeanTween.cancel(newPipe);
        }
    }
}
