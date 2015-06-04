using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour {
	
	public float spinDuration = 2.0f;
	public int numberOfSym = 10;
	public GameObject betResult;
	
	private bool startSpin = false;
	private bool firstReelSpinned = false;
	private bool secondReelSpinned = false;
	private bool thirdReelSpinned = false;
	
	private string betAmount = "100";
	
	private int firstReelResult = 0;
	private int secondReelResult = 0;
	private int thirdReelResult = 0;
	
	private float elapsedTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		betResult.guiText.text = "";
	}
	
	void OnGUI() {
		
		GUI.Label(new Rect(200, 40, 100, 20), "Your bet: ");
        betAmount = GUI.TextField(new Rect(280, 40, 50, 20), betAmount, 25);
		
		if (GUI.Button(new Rect(200,300,150,40), "Pull Lever")) {
			Start();			
			startSpin = true;
		}
	}
	
	void checkBet() {
		if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult) {
			betResult.guiText.text = "YOU WIN!";
		}
		else {
			betResult.guiText.text = "YOU LOSE!";
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {		
		if (startSpin) {
			elapsedTime += Time.deltaTime;
			int randomSpinResult = Random.Range(0, numberOfSym);
			if (!firstReelSpinned) {
				GameObject.Find("firstReel").guiText.text = randomSpinResult.ToString();
				if (elapsedTime >= spinDuration) {
					firstReelResult = randomSpinResult;
					firstReelSpinned = true;
					elapsedTime = 0;
				}
			}
			else if (!secondReelSpinned) {
				GameObject.Find("secondReel").guiText.text = randomSpinResult.ToString();
				if (elapsedTime >= spinDuration) {
					secondReelResult = randomSpinResult;
					secondReelSpinned = true;
					elapsedTime = 0;
				}
			}
			else if (!thirdReelSpinned) {
				GameObject.Find("thirdReel").guiText.text = randomSpinResult.ToString();
				if (elapsedTime >= spinDuration) {		
					thirdReelResult = randomSpinResult;
					
					startSpin = false;
					elapsedTime = 0;
					firstReelSpinned = false;
					secondReelSpinned = false;
					
					checkBet();
				}
			}
		}
	}
}
