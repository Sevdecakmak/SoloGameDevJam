using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothX;
    public float smoothY;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        float posX = Mathf.MoveTowards(transform.position.x, player.position.x, smoothX);
        float posY = Mathf.MoveTowards(transform.position.y, player.position.y, smoothY);
        transform.position = new Vector3(posX, posY, transform.position.z);

    }
}
