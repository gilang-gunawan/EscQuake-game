using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	public float speed;
	public Vector3 dir;
	public GameObject ps;
	private bool isDead;
	public GameObject resetBtn;
    public GameObject quitBtn;
    public Animator gameOveranim;
	public int score = 0;
    public Text newHighScore;
	public Text scoreText;
    public Image background;
    public Text[] scoreTexts;
    public LayerMask whatIsGround;
    private bool isPlaying = false;
    public Transform contactPoint;

	// Use this for initialization
	void Start () {
		isDead = false;
		dir = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {

        if (!IsGrounded() && isPlaying)
        {
            isDead = true;

            GameOver();
            resetBtn.SetActive(true);
            if (transform.childCount > 0)
            {
                transform.GetChild(0).transform.parent = null;
            }
            quitBtn.SetActive(true);
        }

        if (Input.GetKeyDown("space") && !isDead) {

            isPlaying = true;

			score++;
            
			scoreText.text = score.ToString ();

            //menambah kecepatan setiap point kelipatan 10
            if (score % 20 == 0)
            {
                speed++;
            }

            if (dir == Vector3.forward) {
				dir = Vector3.left;
                SoundManager.PlaySound("tone");
            } 
			else {
                SoundManager.PlaySound("tone");
                dir = Vector3.forward;
			}
        }

		float amoutToMove = speed * Time.deltaTime;
		transform.Translate (dir * amoutToMove); 
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Pickup") {
			other.gameObject.SetActive(false);
			Instantiate (ps, transform.position, Quaternion.identity);
			score++;
			scoreText.text = score.ToString ();
            CombatTextManager.Instance.CreateText(other.transform.position, "+3", new Color32(255, 235, 0, 255));
            SoundManager.PlaySound("collect");
        }
	}

	void OnTriggerExit(Collider other)
	{
		//if (other.tag == "Tile") {

		//	RaycastHit hit;
		//	Ray downRay = new Ray (transform.position, -Vector3.up);

		//	if (!Physics.Raycast (downRay, out hit)) {

               
  //              //Kill player
  //              isDead = true;

  //              GameOver();
  //              resetBtn.SetActive (true);
		//		if (transform.childCount > 0) {
		//			transform.GetChild (0).transform.parent = null;
		//		}

		//	}
		//}
	}

    private void GameOver()
    {
        gameOveranim.SetTrigger("GameOver");
        SoundManager.PlaySound("gameover");
        scoreTexts[1].text = score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if(score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            newHighScore.gameObject.SetActive(true);

            background.color = new Color32(255, 118, 246, 255);
            foreach (Text txt in scoreTexts)
            {
                txt.color = Color.white;
            }
        }

        scoreTexts[3].text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(contactPoint.position, .5f, whatIsGround);

        for(int i=0; i< colliders.Length;i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
