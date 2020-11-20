using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms = new List<GameObject>();
    [SerializeField] private int maxPlatform;

    private Vector3 platformPos;
    private GameObject platform;
    private Vector3 platformScale;

    public static int platformNum;
    public static float speed;

    // Start is called before the first frame update
    void Start()
    {
        platformNum = 5;
        speed = 7;
        platformPos = new Vector3(0, 0, 0.5f);
        platformScale = new Vector3(3, 0.5f, 3);

        for (int i = 0; i < maxPlatform; i++)
        {
            generatePlatform();
            platformPos.z += 7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (platformNum == 4)
        {
            generatePlatform();
            platformNum++;
        }
    }

    private void generatePlatform()
    {
        if (platformPos.z != 0.5f)
        {
            platformPos.x = Random.Range(-4.0f, 4.0f);
        }

        platform = Instantiate(platforms[Random.Range(0, platforms.Count)], platformPos, Quaternion.identity);
        if (Controller.score > 80 && Controller.score % 5 == 0 && platformScale.x > 1.5)
        {
            platformScale = new Vector3(platformScale.x - 0.1f, 0.5f, platformScale.z - 0.1f);
        }
        platform.transform.localScale = platformScale;
    }

    public static void IncreaseSpeed()
    {
        speed = speed + 0.8072f;
    }
}
