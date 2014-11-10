using UnityEngine;
using System.Collections;

public class FriendController : MonoBehaviour {
    public float speed;
    public float acceleration;
    public float gravity;
    public float jumpSpeed;
    public Transform punchCollider;
    public GUIText scoretext;

	private bool firstcollider = true;

	
	private float attackStartTime;
	private float attackDuration = 0.4f;

    private Vector3 currentSpeed;
    private Vector3 targetSpeed;
    private Vector3 fwdVector;
    private float punchStartTime;
    private float punchDuration = .4f;
    private int score;

	public float damagerDuration = .1f;
	public float damagerStartTime = 0;

    private bool jumping;
    private bool punching;
    private bool hitEnemy;

	private float moodTime;
	private int mood;


	private RaycastHit hit;


    private LayerMask attackLayerMask = 8 << 1;

    private Transform spriteTransform;
	private Transform shadowTransform;


	private Transform playerTransform;

	private Transform enemyTransform;

	public Vector3 distancePlayer ;

	private CharacterController charControl;
    private Animator animator;

	// Use this for initialization
	void Start () {
        charControl = GetComponent<CharacterController>();
        spriteTransform = transform.FindChild("Sprite");

//		transform.localPosition = new Vector3(0,2.180017F,0);
//		transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);

		spriteTransform.transform.localScale = new Vector3(3.121371F, 2.786767F, 1.0F);
//		spriteTransform.transform.localPosition = new Vector3(-2.4F, -0.5797048F, -2.027557F);

		shadowTransform = transform.FindChild("Blob Shadow Projector");

//		shadowTransform.transform.localPosition = new Vector3(-2F, 1.758164F, -2.88F);

		mood = 0;
		moodTime = 10.0f;

		animator = spriteTransform.GetComponent<Animator>();
		fwdVector = Vector3.right;
        currentSpeed = Vector3.zero;
        score = 0;
        if(scoretext != null)
        scoretext.text = "Score: " + score;
	}
	
	// Update is called once per frame
	void Update () {

		if(enemyTransform == null) {
			if (GameObject.Find("Enemy(Clone)")){
				playerTransform = GameObject.Find("Enemy(Clone)").transform;
			}
			else{
				Debug.Log("game over");
				//Application.LoadLevel ("MainMenu"); 
			}
		}
       
		if(playerTransform == null) {
			if (GameObject.Find("Player(Clone)")){
				playerTransform = GameObject.Find("Player(Clone)").transform;
			}
			else{
				Debug.Log("game over");
				//Application.LoadLevel ("MainMenu"); 
			}
		}


        //handle jump
        if (charControl.isGrounded) {
            animator.SetBool("Jumping", false);
            if (Input.GetButtonDown("Jump")) {
                Debug.Log("should jump now");
                animator.SetBool("Jumping", true);
                currentSpeed.y = jumpSpeed;
            }
        }
        else {
            //handle gravity
            currentSpeed.y -= gravity * Time.deltaTime;
        }

		// STATE MACHINE
		Vector3 targetSpeed = Vector3.zero;
	

		targetSpeed = speed * ((playerTransform.position- distancePlayer) - transform.position).normalized;

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
		
		
		if (!firstcollider){
			attackStartTime = Time.time;
			firstcollider = true;
			animator.SetBool("Attack", false);
			
		}
		
		if (Time.time - attackStartTime >= attackDuration) {
			animator.SetBool("Attack", false);
			firstcollider = false;
			hitEnemy = false;
		}
		else if (!hitEnemy){
			if (Physics.Raycast(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector, out hit, 3f)) {
				if (hit.collider.tag == "Enemy" || hit.collider.tag == "Enemy(Clone)") {
					
					hitEnemy = true;
					animator.SetBool("Attack", true);
					hit.transform.GetComponent<Entity>().TakeDamage(1);
					
				}
			}
		}
		
		
		if (Time.time - damagerStartTime >= damagerDuration) {
			animator.SetBool("Damager", false );
		}
		
		float facing = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
		if (targetSpeed.x != 0) {
			// flip character sprite if going left
			spriteTransform.eulerAngles = (facing < 0) ? (Vector3.up * 180) + (Vector3.right * 315) : Vector3.right * 45;
			fwdVector = (facing < 0) ? Vector3.left : Vector3.right;
		}
		
		charControl.Move(currentSpeed * Time.deltaTime);
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
