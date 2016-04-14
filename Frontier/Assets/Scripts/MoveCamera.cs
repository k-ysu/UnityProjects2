using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	//initialize
	public Vector3 gazedOrgPosition = new Vector3(0,0,0);
	public Vector3 tgtPosition = new Vector3(0,0,0);
	public Vector3 gazedPosition = new Vector3(0,0,0);
	public bool is_org = false;
	public bool is_moving = false;

	//setting
	public float gazeTime=0f;
	private float gazeBonus=0f;
	private float timeTriggered=0.5f;
	private float to_tgt=300.0f;
	private float from_tgt=30.0f;
	private float to_height =5.0f;
	private float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		//reduce gaze time bonus
		gazeBonus -= Time.deltaTime;
		if (gazeBonus < 0) {
			gazeBonus = 0;
		}

		//head position
		GameObject head =  GameObject.FindGameObjectsWithTag("Cardboard")[0];

		// position reset
		if (is_org == true) {

			// check keeping gazing.
			float distance = Vector3.SqrMagnitude (gazedOrgPosition - gazedPosition);
			if(distance>3.0f || is_moving){
				is_org = false;
				gazedOrgPosition = new Vector3 (0, 0, 0);
			}

			// check distance
			float tgt_distance = Vector3.SqrMagnitude (head.transform.position - gazedOrgPosition);
			if (tgt_distance > to_tgt || tgt_distance < from_tgt) {
				is_org = false;
				gazedOrgPosition = new Vector3 (0, 0, 0);
			}

			//check height

			float height_difference =  gazedOrgPosition.y -  head.transform.position.y;
			if ( Mathf.Abs(height_difference) > to_height) {
				is_org = false;
				gazedOrgPosition = new Vector3 (0, 0, 0);
			}

		}

		float tgt_time = timeTriggered - gazeBonus;
		Debug.Log (tgt_time);
		bool gazeTriggered = ( Time.realtimeSinceStartup - gazeTime  >= tgt_time  );

		if ( ( gazeTriggered && is_org ) || is_moving ) {

			if (is_moving == false) {
				tgtPosition = new Vector3 (gazedOrgPosition.x, gazedOrgPosition.y+2 , gazedOrgPosition.z);
				is_moving = true;
			}
				

			head.transform.position = Vector3.MoveTowards(  head.transform.position , tgtPosition , speed * Time.deltaTime   ) ;
			if (head.transform.position == tgtPosition) {
				
				is_org = false;
				gazedOrgPosition = new Vector3 (0, 0, 0);

				is_moving = false;
				gazeBonus =  1;
			}
				
		}



	
	}

}
