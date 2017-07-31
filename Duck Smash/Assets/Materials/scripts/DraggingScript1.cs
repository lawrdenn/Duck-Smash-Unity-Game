using UnityEngine;
using System.Collections;

public class DraggingScript1 : MonoBehaviour
{
	//This code is for 2D click/drag gameobject
	//Please make sure to change Camera Projection to Orthographic
	//Add Collider (not 2DCollider) to gameObject  

	public GameObject gameObjectTodrag; //refer to GO that being dragged

	private Vector3 GOcenter; //gameobjectcenter
	private Vector3 touchPosition; //touch or click position
	private Vector3 offset;//vector between touchpoint/mouseclick to object center
	private Vector3 newGOCenter; //new center of gameObject

	RaycastHit hit; //store hit object information

	private bool draggingMode = false;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		//***********************
		// *** CLICK TO DRAG ****
		//***********************

		#if UNITY_EDITOR
		//first frame when user click left mouse
		if (Input.GetMouseButtonDown(0))
		{
			//convert mouse click position to a ray
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//if ray hit a Collider ( not 2DCollider)
			if (Physics.Raycast(ray, out hit))
			{
				gameObjectTodrag = hit.collider.gameObject;
				GOcenter = gameObjectTodrag.transform.position;
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				offset = touchPosition - GOcenter;
				draggingMode = true;
			}
		}

		//every frame when user hold on left mouse
		if (Input.GetMouseButton(0))
		{
			if (draggingMode)
			{
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newGOCenter = touchPosition - offset;
				gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, 5, newGOCenter.z);
			}
		}

		//when mouse is released
		if (Input.GetMouseButtonUp(0))
		{
			draggingMode = false;
		}
		#endif

		//***********************
		// *** TOUCH TO DRAG ****
		//***********************
		foreach (Touch touch in Input.touches)
		{
			switch (touch.phase)
			{
			//When just touch
			case TouchPhase.Began:
				//convert mouse click position to a ray
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				//if ray hit a Collider ( not 2DCollider)
				// if (Physics.Raycast(ray, out hit))
				if (Physics.SphereCast(ray, 0.3f, out hit))
				{
					gameObjectTodrag = hit.collider.gameObject;
					GOcenter = gameObjectTodrag.transform.position;
					touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					offset = touchPosition - GOcenter;
					draggingMode = true;
				}
				break;

			case TouchPhase.Moved:
				if (draggingMode)
				{
					touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					newGOCenter = touchPosition - offset;
					gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, 5, newGOCenter.z);
				}
				break;

			case TouchPhase.Ended:
				draggingMode = false;
				break;
			}
		}
	}
}