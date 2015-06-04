using UnityEngine;
using System.Collections;

public class DiceGame : MonoBehaviour {
	
	public string inputValue = "1";
	
	int throwNormalDice() {
		Debug.Log("Throwing dice...");
		Debug.Log("Finding random between 1 to 6...");
		int diceResult = Random.Range(1,7);
		
		Debug.Log("Result: " + diceResult);
		
		return diceResult;		
	}
	
	int throwLoadedDice() {
		Debug.Log("Throwing dice...");
		
		int randomProbability = Random.Range(1,101);
		int diceResult = 0;
		if (randomProbability < 36) {
			diceResult = 6;
		}
		else {
			diceResult = Random.Range(1,5);
		}		
		
		Debug.Log("Result: " + diceResult);
		
		return diceResult;		
	}
	
    void OnGUI() {		
	    GUI.Label(new Rect (10, 10, 50, 20), "Input: ");
        inputValue = GUI.TextField(new Rect(60, 10, 50, 20), inputValue, 25);
		
		if (GUI.Button(new Rect(60,40,50,30),"Play")) {
			int totalSix = 0;
			for (int i=0;i<10;i++) {
				int diceResult = throwLoadedDice();
				if (diceResult == 6) totalSix++;
				if (diceResult == int.Parse(inputValue)) {
					guiText.text = "DICE RESULT: " + diceResult.ToString() + "\r\nYOU WIN!";
				}
				else {
					guiText.text = "DICE RESULT: " + diceResult.ToString() + "\r\nYOU LOSE!";
				}
			}
			Debug.Log("Total of six: " + totalSix.ToString());
		}
    }
	
	void Start () {
	}
	
	void Update () {
	}
}
