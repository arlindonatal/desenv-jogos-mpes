using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public int health;

	public void TakeDamage(int dmg) 
    {
        health -= dmg;

		Animator a;

		if(transform.collider.tag == "Player"){
			transform.GetComponent<PlayerController>().damagerStartTime = Time.time;

			Transform barravida = GameObject.Find("barra_vida_diminuir.fw").transform;
			
			barravida.transform.localScale = new Vector3(barravida.transform.localScale.x + 0.1f, barravida.transform.localScale.y, barravida.transform.localScale.z);


		}
		else if(transform.collider.tag == "Enemy"){
			transform.GetComponent<EnemyController>().damagerStartTime = Time.time;
		}
		a = transform.FindChild("Sprite").GetComponent<Animator>();
		a.SetBool("Damager", true );

        if (health <= 0) {
            Die();
        }
        Debug.Log(gameObject.name + " has " + health + " health");
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is dead");
        Destroy(gameObject);
    }
}
