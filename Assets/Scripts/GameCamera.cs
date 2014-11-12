using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target;
    public float minX;
    public float maxX;
    public bool locked;
	public bool gameover = false;
	public bool win = false;

	// Use this for initialization
	void Start () {
        locked = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!GameObject.Find("Player(Clone)")){
			gameover = true;
		}

		if (!GameObject.Find("Enemy(Clone)") && !GameObject.Find("Enemy")){
			win = true;
		}
	
	}


	void OnGUI()
	{
		if (gameover)
		{


			//layout start
			GUI.BeginGroup(
				new Rect(
				Screen.width / 2 - 150, 
				50, 
				350, 
				100));
			
			//the menu background box
			GUI.Box(new Rect(0, 0, 300, 300), "");
			
			//Dialog detail - updated to get better detail
			GUI.Label(
				new Rect(15, 10, 300, 68), 
				"GAME OVER");


			if (GUI.Button(new Rect(10, 50, 100, 50),"New Game"))
			{
				NavigationManager.NavigateTo("NewBattle");
			}



			
			//layout end
			GUI.EndGroup();
		}

	}

	//do cam movement late!
	void LateUpdate(){
        if (target != null) {
            if (!locked)
            {
                //set camera for player's X position, keep previous y/z position. (with min/max X position)
                if (target.position.x < minX)
                    transform.position = new Vector3(minX, transform.position.y, transform.position.z);
                else if (target.position.x > maxX)
                    transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
        }
	}

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
