using UnityEngine;

public class MapMovement : MonoBehaviour {

	public AnimationCurve MovementCurve;
	Vector3 StartLocation;
	Vector3 TargetLocation;
	float timer = 0;
	bool startedTravelling = false;
	bool inputActive = true;
	bool inputReady = true;
	
	int EncounterChance = 85;
	float EncounterDistance = 0;

	void Awake()
	{
		this.collider2D.enabled = false;
		var lastPosition = GameState.GetLastScenePosition(Application.loadedLevelName);
		if (lastPosition != Vector3.zero)
		{
			transform.position = lastPosition;
		}
	}

	void Start()
	{
		MessagingManager.Instance.SubscribeUIEvent(UpdateInputAction);
	}

	private void UpdateInputAction(bool uiVisible)
	{
		inputReady = !uiVisible;
	}

	void Update()
	{
		if (inputActive && Input.GetMouseButtonUp(0))
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
			startedTravelling = true;
			//Work out if a battle is going to happen and if it's likely 
			//then set the distance the player will travel before it happens
			var EncounterProbability = Random.Range(1, 100);
			if (EncounterProbability < EncounterChance && !GameState.playerReturningHome)
			{
				EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation) / 100) * Random.Range(10, 100);
			}
			else
			{
				EncounterDistance = 0;
			}
		}
		else if (inputActive && Input.touchCount == 1)
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
			startedTravelling = true;
			var EncounterProbability = Random.Range(1, 100);
			if (EncounterProbability < EncounterChance && !GameState.playerReturningHome)
			{
				EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation) / 100) * Random.Range(10, 100);
			}
			else
			{
				EncounterDistance = 0;
			}
		}
		
		if (TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation != StartLocation)
		{
			transform.position = Vector3.Lerp(StartLocation, TargetLocation, MovementCurve.Evaluate(timer));
			timer += Time.deltaTime;
		}
		if (startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.5)
		{
			this.collider2D.enabled = true;
			startedTravelling = false;
		}

		//If there is an encounter distance, then a battle must occur. 
		//So when the player has travelled far enough, stop and enter the battle scene
		if (EncounterDistance > 0)
		{
			if (Vector3.Distance(StartLocation, transform.position) > EncounterDistance)
			{
				TargetLocation = Vector3.zero;
				NavigationManager.NavigateTo("Battle");
			}
		}
		
		if (!inputReady && inputActive)
		{
			TargetLocation = this.transform.position;
		}
		inputActive = inputReady;
	}

	void OnDestroy()
	{
		if (MessagingManager.Instance != null)
		{
			MessagingManager.Instance.UnSubscribeUIEvent(UpdateInputAction);
		}
		GameState.SetLastScenePosition(Application.loadedLevelName, transform.position);
	}
}
