using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controller : MonoBehaviour
{
    // Create public variables for UI and speed
    public float speed;
    public Text countText;
    public Text winText;

    // Create private references for rigidbody component and the count of cubes picked up so far
    private Rigidbody rb;
    private int count;

    Material playerMat; // Reference to players material
    Color originalColor; // What the original color was

    // Start is called before the first frame update
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        // Set the count to 0
        count = 0;

        // Run the SetCountText function to update the UI
        SetCountText();

        // Setting our text to "You Win"!
        winText.text = "";

        //Save a reference to the balls material
        playerMat = GetComponent<Renderer>().material;
        // Store the original color of the ball before we flash different colors
        originalColor = playerMat.color;

    }

    private void OnDestroy()
    {
        //Make sure we collect the dynamic material otherwise it will sit in memory forever
        Destroy(playerMat);
    }

    private void Update()
    {
        // Set the players color. interpolate towards the original color frame rate independently TDT
        playerMat.color = Color.Lerp(playerMat.color, originalColor, Time.deltaTime);
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Vector movement variable
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // Adding speed
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        // If the game object we interact with is assigned "Pick up"
        if (other.gameObject.CompareTag("Pick up"))
        {
            // allowing the game object to disappear after being collided with
            other.gameObject.SetActive(false);

            // adding 1 to the count after each cube is picked up
            count = count + 1;

            //Run the SetCountText function
            SetCountText();

            //Random color mat for player
            playerMat.color = Random.ColorHSV();

        }
        // If object we interact with is assigned name "BoostPad"
        if (other.gameObject.CompareTag("BoostPad"))
        {
            // Then multiply the speed
            rb.velocity = rb.velocity * 2.5f;
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // if count is greater than or equal to 12
        if (count >= 12)
        {
            // print game over message
            winText.text = "You Win!";
        }
    }
}
     
