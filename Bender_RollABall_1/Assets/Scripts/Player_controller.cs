using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controller : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    Material playerMat;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        playerMat = GetComponent<Renderer>().material;

        originalColor = playerMat.color;

    }

    private void OnDestroy()
    {
        Destroy(playerMat);
    }

    private void Update()
    {
        playerMat.color = Color.Lerp(playerMat.color, originalColor, Time.deltaTime);
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            playerMat.color = Random.ColorHSV();

        }
        
        if (other.gameObject.CompareTag("BoostPad"))
        {

            rb.velocity = rb.velocity * 2.5f;
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            winText.text = "You Win!";
        }
    }
}
     
