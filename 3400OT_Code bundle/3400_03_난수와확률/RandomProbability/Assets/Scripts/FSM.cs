using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {
	
	public enum FSMState
    {
        Chase,
        Flee
    }
	
	public int chaseProbabiilty = 80;
	public int fleeProbabiilty = 20;
	
	public ArrayList statesPoll = new ArrayList();
	
	// Use this for initialization
	void Start () {
		//fill the array
		for (int i = 0; i < chaseProbabiilty; i++) {
			statesPoll.Add(FSMState.Chase);		
		}
		
		for (int i = 0; i < fleeProbabiilty; i++) {
			statesPoll.Add(FSMState.Flee);
		}
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(10,10,150,40), "Player on sight")) {
			int randomState = Random.Range(0, statesPoll.Count);			
			Debug.Log(statesPoll[randomState].ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
