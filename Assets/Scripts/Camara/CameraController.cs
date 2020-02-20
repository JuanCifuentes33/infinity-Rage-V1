using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0f, 1f)] public float smoothFactor = 0.06f;

    //punto donde sera el centro de la camara
    public Transform puntoBoss;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isWithBoss == false)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothFactor);
        }
        if (GameManager.Instance.isWithBoss == true)
        {
            transform.position = puntoBoss.position;
            Camera.main.orthographicSize = 25f;
        }
    }
}
