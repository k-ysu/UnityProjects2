using UnityEngine;
using System.Collections;

public class TargetFPS : MonoBehaviour {
	void Awake() {
		Application.targetFrameRate = 60;
	}
}
