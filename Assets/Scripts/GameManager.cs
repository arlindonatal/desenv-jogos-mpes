using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject enemy;
    private GameCamera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<GameCamera>();
        Object clone = Instantiate(player, new Vector3(0, 5, 0), Quaternion.identity);
		Object Enemy1 = Instantiate(enemy, new Vector3(50, 7, 0), Quaternion.identity);
		Object Enemy2 = Instantiate(enemy, new Vector3(40, 7, 0), Quaternion.identity);
        Object Enemy3 = Instantiate(enemy, new Vector3(80, 3, 0), Quaternion.identity);
		Object Enemy4 = Instantiate(enemy, new Vector3(118, 3, 0), Quaternion.identity);
		Object Enemy5 = Instantiate(enemy, new Vector3(100, 3, 0), Quaternion.identity);

		Object Enemy6 = Instantiate(enemy, new Vector3(120, 3, 0), Quaternion.identity);
		Object Enemy7 = Instantiate(enemy, new Vector3(140, 3, 0), Quaternion.identity);
		Object Enemy8 = Instantiate(enemy, new Vector3(150, 3, 0), Quaternion.identity);

		Object Enemy9 = Instantiate(enemy, new Vector3(40, 7, 3), Quaternion.identity);
		Object Enemy10 = Instantiate(enemy, new Vector3(80, 3, 3), Quaternion.identity);
		Object Enemy11 = Instantiate(enemy, new Vector3(118, 3, 3), Quaternion.identity);
		Object Enemy12 = Instantiate(enemy, new Vector3(100, 3, 3), Quaternion.identity);

		Object Enemy13 = Instantiate(enemy, new Vector3(40, 7, 5), Quaternion.identity);
		Object Enemy14 = Instantiate(enemy, new Vector3(80, 3, 5), Quaternion.identity);
		Object Enemy15 = Instantiate(enemy, new Vector3(118, 3, 5), Quaternion.identity);
		Object Enemy16 = Instantiate(enemy, new Vector3(100, 3, 5), Quaternion.identity);


		Object Enemy22 = Instantiate(enemy, new Vector3(120, 3, 3), Quaternion.identity);
		Object Enemy17 = Instantiate(enemy, new Vector3(140, 3, 3), Quaternion.identity);
		Object Enemy18 = Instantiate(enemy, new Vector3(150, 3, 3), Quaternion.identity);

		Object Enemy19 = Instantiate(enemy, new Vector3(120, 3, 5), Quaternion.identity);
		Object Enemy20 = Instantiate(enemy, new Vector3(140, 3, 5), Quaternion.identity);
		Object Enemy21 = Instantiate(enemy, new Vector3(150, 3, 5), Quaternion.identity);
	
		camera.SetTarget((clone as GameObject).transform);

	}
}
