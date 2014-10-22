using UnityEngine;

[ExecuteInEditMode]
public class ParticleSortingLayer : MonoBehaviour
{

	void Awake()
	{
		var particleRenderer = GetComponent<Renderer>();
		particleRenderer.sortingLayerName = "GUI";
	}
}
