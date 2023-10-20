using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMovementSpeed = 1.0f;
    public float arrowMinPostion = -0.5f;
    public float arrowMaxPostion = 0.5f;
    public Transform throwingArrow;
    public Transform ballSpawnPoint;
    public float throwForce = 5.0f;
    public Animator throwingArrowAnim;

    public Rigidbody[] balls;

    private float horizontalInput;
    private Vector3 ballOffSet;
    private bool wasBallThrown;
    private Rigidbody selectedBall;

    // Start is called before the first frame update
    void Start()
    {
        ballOffSet = ballSpawnPoint.position - throwingArrow.position;

        StartThrow();
    }


    private void StartThrow()
    {
        throwingArrowAnim.SetBool("Aiming", true);
        wasBallThrown = false;

        //Spawn A New Ball When Start Throw is called
        int randomNumber = GetRandomNumber(0, balls.Length);
        selectedBall = Instantiate(balls[randomNumber],ballSpawnPoint.position, Quaternion.identity);
    }
    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrowWithConstraints();
    }

    private void MoveArrowWithoutConstraints()
    {
        //Moving Without Constraints
        horizontalInput = Input.GetAxis("Horizontal");
        throwingArrow.transform.position += throwingArrow.transform.right * horizontalInput * playerMovementSpeed * Time.deltaTime;
    }

    private void MoveArrowWithConstraints()
    {
        if (wasBallThrown == false)
      {

            //Moving With Constraints
            float movePosition = Input.GetAxis("Horizontal") * playerMovementSpeed * Time.deltaTime;
        throwingArrow.position = new Vector3(
        Mathf.Clamp(throwingArrow.position.x + movePosition, arrowMinPostion, arrowMaxPostion),
        throwingArrow.position.y,
        throwingArrow.position.z
        );
        //Set Ball Position based on Throwing Directon Position
        selectedBall.position = throwingArrow.position + ballOffSet;
      } 
    }


    private void TryThrowBall()
    {
        //Throw The Ball
        if(Input.GetKey(KeyCode.Space))
         {
            wasBallThrown = true;
            ballSpawnPoint.GetComponent<Rigidbody>().AddForce(throwingArrow.forward * throwForce, ForceMode.Impulse);
            throwingArrowAnim.SetBool("Aiming", false);
         }
    }

}