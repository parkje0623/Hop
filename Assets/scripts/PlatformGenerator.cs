using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms = new List<GameObject>();
    [SerializeField] private int maxPlatform;

    private Vector3 platformPos;
    private GameObject platform;
    public int platformNum;

    // Start is called before the first frame update
    void Start()
    {
        platformNum = 5;
        platformPos = new Vector3(0, 0, -1);

        for (int i = 0; i < maxPlatform; i++)
        {
            generatePlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (platformNum < 5)
        {
            generatePlatform();
        }
    }

    private void generatePlatform()
    {
        //4~-4 range
        if (platformPos.z != -1)
        {
            platformPos.x = Random.Range(-4.0f, 4.0f);
        }

        platform = Instantiate(platforms[0], platformPos, Quaternion.identity);
        platformPos.z+=5;
    }
}
