using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public GameObject[] wayponits;

    public float platforSpeed = 2f;

    private int wayponitsIndex = 0;

    private void Update()
    {
        MovePlatform();
    }
    void MovePlatform()
    {
        if (Vector3.Distance(transform.position, wayponits[wayponitsIndex].transform.position) < 0.1f)
        { 
          wayponitsIndex++;

            if (wayponitsIndex>=wayponits.Length)
            {
                wayponitsIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayponits[wayponitsIndex].transform.position,platforSpeed*Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);          
        }       
    }
    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}