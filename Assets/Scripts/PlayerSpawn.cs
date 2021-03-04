using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        //player.transform.rotation = Quaternion.identity;
        Instantiate(player, transform.position, player.transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
