using System.Collections;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    private float _Sec;
    private int _Min;

    

    

    public void MoveShip()
    {
        Vector3 destination = new Vector3(19f, 0.5f, -33f);
        Vector3 speed = Vector3.zero;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref speed, 1f);
        
    }

    IEnumerator CanvasGroupStart()
    {
        yield return new WaitForSeconds(8);
      
        

    }
}
