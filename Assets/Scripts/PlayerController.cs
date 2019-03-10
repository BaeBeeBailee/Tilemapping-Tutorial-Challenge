using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

        private Rigidbody2D rb2d;
        public float speed;
        public float jumpForce;
        public Text countText;
        public Text livesText;
        public Text winText;
        
        private int count;
        private int lives;

    public AudioClip winSound;
    public AudioSource source;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       count = 0;
       lives = 3;
       winText.text = "";
       SetCountText ();

       livesText.text = "Lives: " + lives.ToString ();
       source=GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (Input.GetKey("escape"))
             Application.Quit();
    }

    void FixedUpdate()
    {
       float moveHorizontal = Input.GetAxis("Horizontal");
       Vector2 movement = new Vector2(moveHorizontal, 0);
       rb2d.AddForce(movement * speed);

       if (Input.GetKey("escape"))
             Application.Quit();
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
       if(collision.collider.tag == "Ground") {
           if(Input.GetKey(KeyCode.UpArrow)) {
               rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    }

       if(collision.collider.tag == "Platforms") {
           if(Input.GetKey(KeyCode.UpArrow)) {
               rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    }
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
     
     if (other.gameObject.CompareTag("Enemy"))
         {
             other.gameObject.SetActive (false);
             lives--;
	     if(lives==0)
             {
               winText.text = "You Lose";
               //Destroy(GetComponent<Rigidbody>());
               Destroy(other.gameObject); 
               Destroy(gameObject);
            
               if (Input.GetKey("escape"))
             Application.Quit();
             }

             livesText.text = "Lives: " + lives.ToString ();
             SetCountText ();
            
         }  
      } 

     void SetCountText ()
     {
        countText.text = "Score: " + count.ToString ();
        if (count >= 4)
        {
            winText.text = "You win!";

            if (Input.GetKey("escape"))
             Application.Quit();

            //Tried this...
            //source.PlayOneShot(winSound,1.0f);

            //And this...
            //source.clip = winSound;
            //source.Play();
	}
        
     }
   
}
