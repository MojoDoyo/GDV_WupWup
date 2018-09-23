using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    private AudioSource source;
    private GameObject player;


    public AudioClip jump;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float waitingTime = 3.0f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        CharacterController controller = GetComponent<CharacterController>();
        player = this.gameObject;

            if (Input.GetKeyDown(KeyCode.Space) && player.transform.position.y <= 2.9f)
            {
            source.PlayOneShot(jump);
            moveDirection.y = jumpSpeed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
    }
}
