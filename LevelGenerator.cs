using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public int numberOfObstacles = 10;
    private bool nextObstacleIsHorizontal;
    public GameObject obstacle, map, finalObstacle;
    private Vector3 temp;
    private GameObject player, lastObj;
    //Vector3 teoo = Vector3.zero;

    void Awake()
    {
        InitializeVariables();
        GenerateObstacles();    
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        CreateOneObstacle(teoo, obstacle);
    //        teoo = temp;
    //    }
    //} 

    void InitializeVariables ()
    {
        //teoo = new Vector3(0, 1, 0);
        if (Random.Range(0,2) == 0)
        {
            nextObstacleIsHorizontal = true;
        } else
        {
            nextObstacleIsHorizontal = false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void GenerateObstacles ()
    {
        Vector3 pos = new Vector3(0, 1, 0);

        for (int i = 0; i < numberOfObstacles; i++)
        {
            if (!CreateOneObstacle(pos, obstacle))
            {
                break;
            }
            else
            {
                //if (Random.Range(0, 1) == 0)
                //{
                //    CreateFakeObs(pos);
                //}
                pos = temp;
            }
        }
        Vector3 loc = lastObj.transform.position;
        Destroy(lastObj);
        Instantiate(finalObstacle, loc, Quaternion.identity);
    }

    void CreateFakeObs (Vector3 loc)
    {
        //nextObstacleIsHorizontal = !nextObstacleIsHorizontal;
        Vector3 dir = GetDirection();
        int num = Random.Range(3, 5);

        if (CheckIfValidPos(loc + (dir * num))) {
            GameObject obs = Instantiate(obstacle, loc + (dir * num), Quaternion.identity) as GameObject;
            obs.transform.parent = map.transform;
        }
        //nextObstacleIsHorizontal = !nextObstacleIsHorizontal;

    }
 

    bool CreateOneObstacle (Vector3 position, GameObject toSpawn)
    {
        Vector3 dir = GetDirection();
        int num = Random.Range(4,8);


        if (!CheckIfValidPos(position + (dir * num)))
        {
            //CreateOneObstacle(position, toSpawn);
            return false;
        }
        else
        {
            nextObstacleIsHorizontal = !nextObstacleIsHorizontal;
            position += dir * num;
            GameObject obs = Instantiate(toSpawn, position, Quaternion.identity) as GameObject;
            obs.GetComponent<Obstacle>().SetDir(dir);
            obs.transform.parent = map.transform;
            lastObj = obs;
            temp = position - dir;
            return true;
        }
    }

    bool CheckIfValidPos (Vector3 pos)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        bool above = false, below = false, left = false, right = false;

        //if (pos == player.transform.position)
        //{
        //    return false;
        //}

        if (Vector3.Distance(pos, player.transform.position) == 0f)
        {
            return false;
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Vector3.Distance(pos, obstacles[i].transform.position) < 3f)
            {
                return false;
            }


            if (obstacles[i].transform.position.x == pos.x)
            {
                if (obstacles[i].transform.position.y > pos.y)
                {
                    above = true;
                } else if (obstacles[i].transform.position.y < pos.y)
                {
                    below = true;
                } else
                {
                    return false;
                }

            } else if (obstacles[i].transform.position.y == pos.y)
            {
                if (obstacles[i].transform.position.x > pos.x)
                {
                    left = true;
                } else
                {
                    right = true;
                }
            }
        }

        //return !(above && below) || !(right && left);

        return !(above && below) || !(right && left);
    }

    Vector3 GetDirection ()
    {
        int num = Random.Range(0, 2);
        if (nextObstacleIsHorizontal)
        {
            //nextObstacleIsHorizontal = false;
            if (num == 0)
            {
                return new Vector3(1, 0, 0);
            }
            return new Vector3(-1, 0, 0);
        } else
        {
            //nextObstacleIsHorizontal = true;
            if (num == 0)
            {
                return new Vector3(0, 0, 1);
            }
            return new Vector3(0, 0, -1);
        }
    }
}
