using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public float speed;
    public float acceleration;
    public float gravity;
   // public float jumpSpeed;
    private float moodTime;
    private int mood;
    private Vector3 currentSpeed;
    private Vector3 targetSpeed;
	private Vector3 fwdVector;
	private bool firstcollider = true;

	private float attackStartTime;
	private float attackDuration = 400f;

	private Animator animator;

    private bool jump;

    private Transform spriteTransform;
    private Transform playerTransform;
    private CharacterController charControl;

	// Use this for initialization
	void Start () {
        charControl = GetComponent<CharacterController>();
        currentSpeed = Vector3.zero;
		fwdVector = Vector3.right;
        spriteTransform = transform.FindChild("Sprite");
		animator = spriteTransform.GetComponent<Animator>();
		playerTransform = null;
        mood = 0;
        moodTime = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //wait for player spawn to grab player transform
        if(playerTransform == null) {
			if (GameObject.Find("Player(Clone)")){
            	playerTransform = GameObject.Find("Player(Clone)").transform;
			}
			else{
				Debug.Log("game over");
				Application.LoadLevel ("MainMenu"); 
			}
        }

        //handle (not actually jump)
        if (charControl.isGrounded) {
            currentSpeed.y = 0;

        }
        else {
            //handle gravity
            currentSpeed.y -= gravity * Time.deltaTime;
        }

        // STATE MACHINE
		Vector3 targetSpeed = Vector3.zero;
		switch(mood) {
                //move right
            case 0:
			    targetSpeed = new Vector3(-speed, 0, 0);
                //need to check for player character set mood if close
                if(playerTransform != null && (moodTime < 2.0f) && ((playerTransform.position - transform.position).magnitude < 4.0f)) {
                    mood = 2;
                    moodTime = 2.0f;
                    break;
                }
                moodTime -= Time.deltaTime;
                if (moodTime < 0.0f)
                {
                    moodTime = 3.0f;
                    mood = 1;
                }
                break;
                //move left
            case 1:
				targetSpeed = new Vector3(speed, 0, 0);
                //need to check for player character set mood if close
                if (playerTransform != null && (moodTime < 2.0f) && ((playerTransform.position - transform.position).magnitude < 4.0f))
                {
					mood = 2;
                    moodTime = 2.0f;
                    break;
                }
                moodTime -= Time.deltaTime;
                if (moodTime < 0.0f)
                {
                    moodTime = 3.0f;
                    mood = 0;
                }
                break;

                //chase player!
            case 2:
				targetSpeed = speed * (playerTransform.position - transform.position).normalized;
                moodTime -= Time.deltaTime;
                if (moodTime < 0.0f)
                {
                    moodTime = 2.0f;
                    mood = 0;
                }
                break;
        }

        //Vector3 targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.z = MoveToward(currentSpeed.z, targetSpeed.z, acceleration);

		if (Mathf.Abs(currentSpeed.x) > 0 || Mathf.Abs(currentSpeed.z) > 0) {
			animator.SetFloat("Speed", 1);
		}
		else {
			animator.SetFloat("Speed", 0);
		}


			Animator a;
			RaycastHit hit;
			//Debug.DrawRay(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector * 3f, Color.red);
			if (Physics.Raycast(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector, out hit, 3f)) {
				if (hit.collider.tag == "Player") {
					
					if (firstcollider){
						attackStartTime = Time.time;
						firstcollider = false;
					}
					
					if (Time.time - attackStartTime >= attackDuration) {
						animator.SetBool("Attack", false);
						a = hit.transform.FindChild("Sprite").GetComponent<Animator>();
						a.SetBool("Damager", false );
						firstcollider = true;
					}
					else{
						a = hit.transform.FindChild("Sprite").GetComponent<Animator>();
						animator.SetBool("Attack", true);
						hit.transform.GetComponent<Entity>().TakeDamage(1, 0);
						a.SetBool("Damager", true );
						Debug.Log(":::::::::::::::::::::: LEVANDO DANO :::::::::::::::::::::::");
						
					}
				}
			}


        float facing = Mathf.Sign(targetSpeed.x);
        if (targetSpeed.x != 0) {
            // flip character sprite if going right
            spriteTransform.eulerAngles = (facing > 0) ? (Vector3.up * 180) + (Vector3.right * 315) : Vector3.right * 45;
        }

        charControl.Move(currentSpeed * Time.deltaTime);
	}


	void OnCollisionEnter(Collision collision) {
		Animator a;
		if(collision.gameObject.name == "Player(Clone)"){
				a = playerTransform.FindChild("Sprite").GetComponent<Animator>();
				a.SetBool("Damager", true );
				animator.SetBool("Attack", true);
				playerTransform.GetComponent<Entity>().TakeDamage(1, 1);
				return;
			}
			else{
				animator.SetBool("Attack", false);
				a = playerTransform.FindChild("Sprite").GetComponent<Animator>();
				a.SetBool("Damager", false );
			}
	}




    float MoveToward(float curr, float targ, float accel)
    {
        if (curr == targ) {
            return curr;
        }

        float direction = Mathf.Sign(targ - curr);
        curr += accel * direction;
        // return the target if curr has passed it
        return (Mathf.Sign(curr - targ) == direction) ? curr : targ;
    }



}
