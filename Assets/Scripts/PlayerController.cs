using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int speed = 5;
    public Rigidbody playerRB;
    float inputAxisV;
    float inputAxisH;
    [SerializeField] Transform camPos;
    bool isGrounded;
    bool hasJumped;
    bool damagable;
    bool damaged;
    [SerializeField] int jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider box;

    PlayerStats playerStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        damagable = true;
        damaged = false;
        playerRB = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        playerStatsScript = GameObject.FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit ray;
        inputAxisV = Input.GetAxis("Vertical");
        inputAxisH = Input.GetAxis("Horizontal");
        isGrounded = Physics.SphereCast(transform.position, (box.size.y / 2), Vector3.down, out ray, 2, groundLayer);

        if (transform.position.y < -20)
        {
            GameEnd();
        }

        var movementVector = new Vector3(inputAxisH, 0, inputAxisV);

        anim.SetFloat("speed", movementVector.magnitude);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            StartCoroutine(Jump());
        }
       

        #region
        if (isGrounded)
        {
            anim.SetTrigger("isGrounded");
        }
        else
        {
            anim.ResetTrigger("isGrounded");
        }

        if (hasJumped)
        {
            anim.SetTrigger("hasJumped");
        }
        else
        {
            anim.ResetTrigger("hasJumped");
        }
        #endregion
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(camPos.rotation.x, 0,0);
        Vector3 camRight = camPos.right;
        Vector3 camForward = camPos.forward;

        camRight.y = 0;
        camRight.Normalize();

        camForward.y = 0;
        camForward.Normalize();

        Vector3 forwardCam = inputAxisV * camForward;
        Vector3 rightCam = inputAxisH * camRight;

        Vector3 animationVector = (forwardCam + rightCam);

        Vector3 movementVector = (forwardCam + rightCam) * speed;

        movementVector.y = playerRB.velocity.y;

        playerRB.velocity = movementVector;

        if (inputAxisV != 0 || inputAxisH != 0)
        {
            anim.transform.forward = animationVector;
        }
        
    }
    IEnumerator Jump()
    {
        hasJumped = true;
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.2f);
        hasJumped = false;
    }

    IEnumerator Damaged()
    {
        damaged = true;
        damagable = false;
        anim.SetTrigger("isDamaged");
        playerStatsScript.DecreaseHealth();
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("isDamaged");
        damaged = false;
        damagable = true;
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 6 && damagable == true)
        {
            StartCoroutine(Damaged());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            playerStatsScript.IncreaseScore();
        }
    }
    void GameEnd()
    {
        FindAnyObjectByType<SceneManagement>().GetComponent<SceneManagement>().GameOver();
    }
}
