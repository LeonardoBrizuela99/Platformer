using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 respawnPosition;
    private void Awake()
    {
       Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {
        StartCoroutine("RespawnWaiter");
        //PlayerController.instance.gameObject.SetActive(false);
       // PlayerController.instance.transform.position = respawnPosition;
       // PlayerController.instance.gameObject.SetActive(true);
    }

    public IEnumerator RespawnWaiter()
    {
        PlayerController.instance.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        
        PlayerController.instance.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
    }
}
