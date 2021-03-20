using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    private int score = 1;
    private float time = 10f;
    public Counter counter;
    public Countdown countTime;
    private float scorecount = 100f;
    public HealthBarScreenSpaceController health;

    public ScoreController scoreController;

    [Header("Sound")]
    public AudioSource Sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Sound.Play();
            counter.counter(score);
            scoreController.AddScore(scorecount);
            countTime.AddTime(time);
            health.Reset();
            Destroy(gameObject);
        }
    }
    }
