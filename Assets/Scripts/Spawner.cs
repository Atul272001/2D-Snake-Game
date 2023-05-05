using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject smallPower;
    public GameObject bigPower;
    public float posX;
    public float posY;
    private int countDown = 6;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(PowerUps), 1f, 10f);
    }

    public void PowerUps()
    {
        GameObject[] smallFruit = GameObject.FindGameObjectsWithTag("Small");
        GameObject[] bigFruit = GameObject.FindGameObjectsWithTag("Big");
        CheckActive(smallFruit);
        CheckActive(bigFruit);

        Vector3 pos = new (Random.Range(-posX, posX), Random.Range(-posY, posY), 0f);
        if(countDown > 0)
        {
            Instantiate(smallPower, pos, smallPower.transform.rotation);
            countDown--;
        }
        else
        {
            Instantiate(bigPower, pos, bigPower.transform.rotation);
            countDown = 6;
        }
    }

    public void CheckActive(GameObject[] gameObjects)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

}
