using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void changetoMain(){
		Application.LoadLevel("Main");
	}
	public void changetoMenu(){
		Application.LoadLevel("Menu");
	}
}
