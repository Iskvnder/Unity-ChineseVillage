using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //variables

    private Animator anim;
    public float speed;
    private bool isFight = false;
    float rotationSpeed;
    public bool isOnGround = true;
    public bool isRun = false;
    private Rigidbody playerRb;
    public float jumpForce = 5;
    private bool isMoving = false;
    public float hp = 100;
    public float bossFight = 0;
    public bool isDmg = false;

    public Text hpText;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        speed = 7f;

        hpText.text = "hp: " + hp;
    }


    void Update()
    {
        if(hp <= 0)
        {
            GameOver();
        }

        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        //Rotation
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rotationSpeed = 360;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //Speed
        if (Input.GetKeyDown(KeyCode.LeftShift) && isRun == false && isOnGround == true)
        {
            speed = 12f;
            isRun = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isRun == true && isOnGround == true)
        {
            speed = 7f;
            isRun = false;
        }

        //Fight Mode
        if (Input.GetKeyDown(KeyCode.F) && isRun == false && isOnGround == true && isFight == false)
        {
            isFight = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && isRun == false && isOnGround == true && isFight == true)
        {
            isFight = false;
        }

        //Animations
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isRun != true && isFight != true)
        {
            anim.Play("walk");
            isRun = false;

        }
        else if (Input.GetMouseButtonDown(0) && isFight == true)
        {
            isDmg = true;
            Invoke("SetBoolBack", 0.5f);
            anim.Play("fight_beat");
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
           && isRun == true && isFight != true)
        {
            anim.Play("run");
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.F))
             && isFight == true)
        {
            anim.Play("fight_move");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            anim.Play("fight_mode");
        }
        else if (isFight != true)
        {
            anim.Play("idle");
        }

    }

    private void SetBoolBack()
    {
        isDmg = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Destination")
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && isDmg == true)
        {
            Destroy(col.gameObject);
        }else if (col.gameObject.tag == "Boss" && isDmg == true)
        {
            if (bossFight >= 10)
            {
                Destroy(col.gameObject);
                SceneManager.LoadScene(3);
            }
            else bossFight += 0.5f;
        }
        else if (col.gameObject.tag == "Enemy" && isDmg == false)
        {
            hp -= 2;
            hpText.text = "hp: " + hp;
        }
        else if (col.gameObject.tag == "Boss" && isDmg == false)
        {
            hp -= 20;
            hpText.text = "hp: " + hp;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && isDmg == true)
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Boss" && isDmg == true)
        {
            if (bossFight >= 10)
            {
                Destroy(col.gameObject);
                SceneManager.LoadScene(3);
            }
            else bossFight += 0.2f;
        }
        else if (col.gameObject.tag == "Enemy" && isDmg == false)
        {
            hp -= 2;
            hpText.text = "hp: " + hp;
        }
        else if (col.gameObject.tag == "Boss" && isDmg == false)
        {
            hp -= 0.1f;
            hpText.text = "hp: " + hp;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && isDmg == true)
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Boss" && isDmg == true)
        {
            if (bossFight >= 10)
            {
                Destroy(col.gameObject);
                SceneManager.LoadScene(3);
            }
            else bossFight += 0.5f;
        }
        else if (col.gameObject.tag == "Enemy" && isDmg == false)
        {
            hp -= 2;
            hpText.text = "hp: " + hp;
        }
        else if (col.gameObject.tag == "Boss" && isDmg == false)
        {
            hp -= 2;
            hpText.text = "hp: " + hp;
        }
    }

    void GameOver()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(4);
    }


}
