using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class GameManage : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public Material[] skyBox;
    public RigidHand hand;
    public float speed;
    public Transform randomPrefab;

    // Start is called before the first frame update
    public void Start()
    {
        RenderSettings.skybox = skyBox[Random.Range(0, skyBox.Length)];
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        randomPrefab = spawnPoints[spawnPointIndex];
        randomPrefab = Instantiate(
            randomPrefab,
            new Vector3(0, -1, 12),
            new Quaternion(0, 0, 0, 0)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.gameObject.activeSelf) //確認是否偵測到手
        {
            randomPrefab.gameObject.SetActive(true);
            Vector3 position = new Vector3(
                hand.GetPalmPosition().x * speed,
                hand.GetPalmPosition().y * speed,
                randomPrefab.position.z
            );
            randomPrefab.position = position;
        }
        else
        {
            randomPrefab.gameObject.SetActive(false);
        }
    }
}
