using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	public int health;
	public Transform barravida;
	private Animator animator;

	public void TakeDamage (int dmg, int alvo) 

	{
		//alvo = 0 -> hero sofre dano
		//alvo = 1 -> enemy sofre dano

		barravida = GameObject.Find("barra_vida_diminuir.fw").transform;

		health -= dmg;

		if(alvo == 0){

			if (health == 9) {
				barravida.transform.localScale = new Vector3(0.1f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 8) {
				barravida.transform.localScale = new Vector3(0.2f, barravida.transform.localScale.y, barravida.transform.localScale.z);

			}
			if (health == 7) {
				barravida.transform.localScale = new Vector3(0.3f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 6) {
				barravida.transform.localScale = new Vector3(0.4f, barravida.transform.localScale.y, barravida.transform.localScale.z);

			}
			if (health == 5) {
				barravida.transform.localScale = new Vector3(0.5f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 4) {
				barravida.transform.localScale = new Vector3(0.6f, barravida.transform.localScale.y, barravida.transform.localScale.z);	
			}
			if (health == 3) {
				barravida.transform.localScale = new Vector3(0.7f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 2) {
				barravida.transform.localScale = new Vector3(0.8f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 1) {
				barravida.transform.localScale = new Vector3(0.9f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
			if (health == 0) {
				barravida.transform.localScale = new Vector3(1f, barravida.transform.localScale.y, barravida.transform.localScale.z);
			}
					
		}


		if (health <= 0) {
			Die();
		}
		
		Debug.Log(":::::::::::::::::::::: " + gameObject.name + " has " + health + " health :::::::::::::::::::::::::::::::::::");
		Debug.Log(":::::::::::::::::::::: TAMANHO BARRA VIDA : "+barravida.transform.localScale.x +" :::::::::::::::::::::::::::::::::::");
	}
	
	public void Die()
	{
		Debug.Log(gameObject.name + " is dead");
		Destroy(gameObject);
	}
}
