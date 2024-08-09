using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 pos;
    private float speed = 5f;
    private bool isMoving = false;
    public SoundEmitter soundEmitter;
    
    void Update()
    {
        pos = transform.position;
        if(Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            pos.z -= Time.deltaTime*speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            pos.z += Time.deltaTime*speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            pos.x -= Time.deltaTime*speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            pos.x += Time.deltaTime*speed;
        }

        if (!Input.GetKey(KeyCode.W) && 
            !Input.GetKey(KeyCode.A) && 
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D))
        {
            isMoving = false;
        }

        if (isMoving && soundEmitter != null)
        {
                soundEmitter.playSound();
        }
        transform.position = pos;
    }
}

