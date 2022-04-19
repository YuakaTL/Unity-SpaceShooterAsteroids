using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocky : MonoBehaviour
{
    public List<Transform> RockyList = new List<Transform>();
    public Transform rockyPoint;
    public Text text;
    private bool spaceTrigger = false;
    int count = 4;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    void GameStart()
    {
        text.text = "Ready !";
        count = 4;
        Invoke("CountDown", 1);
    }

    void CountDown()
    {
        if (count == 1)
        {
            text.gameObject.SetActive(false);
            InvokeRepeating("CreatRocky", 1, 5);
        }
        else
        {
            count -= 1;
            text.text = count.ToString();
            Invoke("CountDown", 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = "按空格開始遊戲\n P 暫停遊戲 \n Esc 結束遊戲";
        Invoke("GameStart", 2);
    }

    void CreatRocky()
    {
        int rockyPointIndex = Random.Range(0, RockyList.Count);
        rockyPoint = RockyList[rockyPointIndex];
        rockyPoint = Instantiate(rockyPoint, new Vector3(0, 5, 170), new Quaternion(0, 0, 0, 0));

        rockyPoint.gameObject.SetActive(true);
        rockyPoint.name = "Rocky";
        rockyPoint.transform.position =
            rockyPoint.position + ((Vector3)Random.insideUnitCircle * 4.0f);
        MeshCollider collider = rockyPoint.gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rockyPoint != null)
        {
            rockyPoint.Translate(Vector3.back * 0.6f);

            if (rockyPoint.position.z < 3)
            {
                Destroy(rockyPoint.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spaceTrigger)
            {
                spaceTrigger = false;
                Time.timeScale = 1f;
                text.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            spaceTrigger = true;
            Time.timeScale = 0f;
            text.gameObject.SetActive(true);
            text.text = "暫停\n 空格開始遊戲 \n Esc 結束遊戲";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
