using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fighter : MonoBehaviour
{
    public GameObject explosion;
    public LineRenderer[] laser;
    public GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(UI, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.name == "Rocky")
            {
                Destroy(hit.transform.gameObject);
                GameObject exp = Instantiate(
                    explosion,
                    hit.transform.position,
                    hit.transform.rotation
                );
                Destroy(exp, 2);
            }
            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].enabled = true;
                laser[i].SetPosition(1, transform.InverseTransformPoint(hit.transform.position));
            }
        }
        else
        {
            for (int i = 0; i < laser.Length; i++)
                laser[i].enabled = false;
        }
    }
}
