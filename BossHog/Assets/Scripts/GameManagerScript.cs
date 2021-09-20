using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject appleRef;
    public GameObject cornRef;
    public GameObject peaRef;

    public GameObject chi_enemy_Ref;
    public GameObject vert_enemy_Ref;
    public Text score_Ref;

    private int difficulty_level = 1;
    
    //public bool left_lane_open = true;
    //public bool right_lane_open = true;
    //public bool middle_open = true;

    private GameObject left_enemy;
    private GameObject right_enemy;
    private GameObject middle_enemy;

    private Vector3 left_spawn = new Vector3(9.7f, 0.21f, -19.09f);
    private Vector3 mid_spawn = new Vector3(0f, 0.21f, -19.09f);
    private Vector3 right_spawn = new Vector3(-9.7f, 0.21f, -19.09f);
    private Quaternion spawn_rot = Quaternion.Euler(0.0f, -90.0f, 0.0f);

    private int enemyCt = 0;
    private int enemyLimit = 1;

    private float scoreTimer = 0f;
    public int cowScore = 0;
    public int timeScore = 0;
    public int score = 0;

    public float spawnDelay = 300f;
    private float spawnTimer = 0f;
    //private float spawnPercentage = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void UpdateEnemyCt()
    {
        enemyCt = 3;
        if (left_enemy == null)
        {
            enemyCt--;
        }
        if(middle_enemy == null)
        {
            enemyCt--;
        }
        if (right_enemy == null)
        {
            enemyCt--;
        }
    }

    public GameObject getBulletType()
    {
        if(difficulty_level > 2)
        {
            int bulletType = Random.Range(1, 4);
            switch (bulletType)
            {
                case 1:
                    return appleRef;
                    break;
                case 2:
                    return cornRef;
                    break;
                case 3:
                    return peaRef;
                    break;
                default:
                    return appleRef;
                    break;
            }
        } 
        else
        {
            return appleRef;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateEnemyCt();
        if (spawnTimer >= spawnDelay)
        {
            if (enemyCt < enemyLimit)
            {
                int lanePick = Random.Range(1, 4);
                GameObject bullet = getBulletType();
                switch (lanePick)
                {
                    case 1:
                        if (left_enemy == null)
                        {
                            var cow = Instantiate(chi_enemy_Ref, left_spawn, spawn_rot);
                            cow.GetComponent<ChicagoEnemyScript>().bulletType = bullet;
                            left_enemy = cow;
                        }
                        break;
                    case 2:
                        if (middle_enemy == null)
                        {
                            var cow = Instantiate(vert_enemy_Ref, mid_spawn, spawn_rot);
                            cow.GetComponent<VerticalEnemyScript>().bulletType = bullet;
                            middle_enemy = cow;
                        }
                        break;
                    case 3:
                        if (right_enemy == null)
                        {
                            var cow = Instantiate(chi_enemy_Ref, right_spawn, spawn_rot);
                            cow.GetComponent<ChicagoEnemyScript>().bulletType = bullet;
                            right_enemy = cow;
                        }
                        break;
                }
            }
            spawnTimer = 0;
        } else
        {
            spawnTimer++;
        }

        //change level based on score
        scoreTimer += Time.deltaTime;
        if(scoreTimer > .5f)
        {
            timeScore++;
            scoreTimer = 0;
        }
        score = timeScore + cowScore;
        score_Ref.text = score.ToString();
        //scoreText.text = score;

        switch (difficulty_level)
        {
            case 1:
                if(score > 100)
                {
                    difficulty_level = 2;
                    enemyLimit = 2;
                }
                break;
            case 2:
                if (score > 300)
                {
                    difficulty_level = 3;
                    enemyLimit = 1;
                }
                break;
            case 3:
                if (score > 400)
                {
                    difficulty_level = 4;
                    enemyLimit = 2;
                }
                break;
            case 4:
                if (score > 700)
                {
                    difficulty_level = 5;
                    enemyLimit = 3;
                }
                break;
        }
    }

}
