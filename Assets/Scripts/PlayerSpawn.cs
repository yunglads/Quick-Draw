using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        //player.transform.rotation = Quaternion.identity;
        //Instantiate(player, transform.position, player.transform.rotation, transform);

        player.transform.position = spawnpoint.transform.position;
        player.transform.rotation = spawnpoint.transform.rotation;
        //player.transform.SetParent(spawnpoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
