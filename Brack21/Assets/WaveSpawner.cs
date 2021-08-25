using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    //set up an enum which contains the options/states of spawning, waiting, and counting
    public enum SpawnState { Spawning, Waiting, Counting };

    //allows us to change the values of instances of Wave class in the unity editor
    [System.Serializable]
    public class Wave
    {
        //name of the wave
        public string name;
        //reference to the prefab we want to instantiate at the spawn point - the enemy
        public Transform enemy;
        //amount of enemies
        public int count;
        //spawn rate
        public float rate;

    }

    //array of waves
    public Wave[] waves;
    //wave that is coming next
    private int nextWave = 0;

    //5 seconds between waves
    public float timeBetweenWaves = 5f;
    //countdown to next wave (=0)
    public float waveCountdown;

    private float searchCountdown = 1f;

    //set up the spawnstate and set the state of it to "counting"
    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        //countdown to next wave is going to start at 5 seconds upon start
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        //CHECK IF ENEMIES ARE STILL ALIVE
        if (state == SpawnState.Waiting)
        {
            //if EnemyIsAlive check returns as false (!)
            //BEGIN A NEW ROUND/WAVE IF NO ENEMIES ARE LEFT
            if (!EnemyIsAlive())
            {
                //begin new round
                Debug.Log ("Wave Completed");
                return;
            }
            else
            {
                //prevents lower code running because its not yet relevant
                return;
            }
        }

        //COUNT DOWN FROM 5 AT THE START OF THE GAME
        //START SPAWNING WHEN TIMER REACHES 0 AND WAVE IS NOT IN PROGRESS
        //if it is time to start spawning a wave
        if (waveCountdown <= 0)
        {
            //if the game is not currently spawning enemies (ie in the middle of a wave)
            if (state != SpawnState.Spawning)
            {
                //because the method is an IE, we need to use the StartCoroutine to call it
                //this code calls the SpawnWave code, and calls the next wave in the waves array
                StartCoroutine( SpawnWave ( waves[nextWave] ) );
            }
        }
        //OR WAIT UNTIL THE WAVE IS FINISHED
        else
        {
            //drops the countdown 1 unit per second 
            //makes the time countdown relevant to actual time and not the number of frames drawn per second
            waveCountdown -= Time.deltaTime;
        }
    }

    //check for whether any enemies are alive
    bool EnemyIsAlive()
    {
        //drops the countdown 1 unit per second 
        //makes the time countdown relevant to actual time and not the number of frames drawn per second
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {              
            //reset the countdown
            searchCountdown = 1f;
            //Search the current game objects to see if any of them have the enemy tag
            //taxing on the system -- is restrained to checking once per second rather than every frame
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    //A method that is able to wait a certain amount of seconds before continuing (as opposed to void)
    //IE is called SpawnWave, takes in Wave as an argument type, the argument name is _wave
    //SPAWN THE NUMBER OF ENEMIES WE WANT DURING A SINGLE WAVE
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log ("Spawning Wave: " + _wave.name);

        //SET SPAWN STATE TO SPAWNING
        state = SpawnState.Spawning;

        //will loop 'count' number of times, count being the number of enemies we want to spawn
        //adds i to the integer every time it runs until i goes over _wave.count
        //LOOP THROUGH THE NUMBER OF ENEMIES WE WANT TO SPAWN, SPAWNING ONE ENEMY PER LOOP UNTIL i = THE NUMBER OF ENEMIES WE SPECIFY
        for (int i = 0; i < _wave.count; ++i)
        {
            //SPAWN AN ENEMY USING THE SPAWNENEMY METHOD
            SpawnEnemy(_wave.enemy);
            //WAIT BEFORE LOOPING AGAIN
            //create a new wait time of a certain amount of seconds: 1 divided by the rate of the waves
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        //SET SPAWN STATE TO WAITING
        state = SpawnState.Waiting;
        //IE requires a returned value - this gives it something
        yield break;
    }

    //SPAWNS AN ENEMY
    void SpawnEnemy (Transform _enemy)
    {
        //spawn enemy
        
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Instantiate (_enemy, transform.position, transform.rotation);
    }

}
