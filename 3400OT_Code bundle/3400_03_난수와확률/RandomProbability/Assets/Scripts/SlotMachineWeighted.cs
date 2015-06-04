using UnityEngine;
using System.Collections;

public class SlotMachineWeighted : MonoBehaviour {
	
	public float spinDuration = 2.0f;
	public int numberOfSym = 10;
	public GameObject betResult;
	
	private bool startSpin = false;
	private bool firstReelSpinned = false;
	private bool secondReelSpinned = false;
	private bool thirdReelSpinned = false;
	
	private int betAmount = 100;
	
	private int creditBalance = 1000;
	private ArrayList weightedReelPoll = new ArrayList();	
	private int zeroProbability = 30;
	
	private int firstReelResult = 0;
	private int secondReelResult = 0;
	private int thirdReelResult = 0;
	
	private float elapsedTime = 0.0f;
	
	// Use this for initialization
	void Start () {
		betResult.guiText.text = "";
		
		for (int i = 0; i < zeroProbability; i++) {
			weightedReelPoll.Add(0);		
		}
		
		int remainingValuesProb = (100 - zeroProbability)/9;
		
		for (int j = 1; j < 10; j++) {
			for (int k = 0; k < remainingValuesProb; k++) {
				weightedReelPoll.Add(j);
			}
		}
	}
	
	void OnGUI() {
		
		GUI.Label(new Rect(150, 40, 100, 20), "Your bet: ");
        betAmount = int.Parse(GUI.TextField(new Rect(220, 40, 50, 20), betAmount.ToString(), 25));		
		
		GUI.Label(new Rect(300, 40, 100, 20), "Credits: " + creditBalance.ToString());
		
		if (GUI.Button(new Rect(200,300,150,40), "Pull Lever")) {
			betResult.guiText.text = "";			
			startSpin = true;
		}
	}
	
	void checkBet() {
		if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult) {
			betResult.guiText.text = "JACKPOT!";
			creditBalance += betAmount * 50;
		}
		else if (firstReelResult == 0 && thirdReelResult == 0) {			
			betResult.guiText.text = "YOU WIN " + (betAmount/2).ToString();
			creditBalance -= (betAmount/2);
		}
		else if (firstReelResult == secondReelResult) {			
			betResult.guiText.text = "AWW... ALMOST JACKPOT!";
		}
		else if (firstReelResult == thirdReelResult) {			
			betResult.guiText.text = "YOU WIN " + (betAmount*2).ToString();
			creditBalance -= (betAmount*2);
		}
		else {
			betResult.guiText.text = "YOU LOSE!";
			creditBalance -= betAmount;
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
					int weightedRandom = Random.Range(0, weightedReelPoll.Count);	
					GameObject.Find("firstReel").guiText.text = weightedReelPoll[weightedRandom].ToString();
					firstReelResult = (int)weightedReelPoll[weightedRandom];
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
					if ((firstReelResult == secondReelResult) &&
						randomSpinResult != firstReelResult) {
						//the first two reels have resulted the same symbol
						//but unfortunately the third reel missed
						//so instead of giving a random number we'll return a symbol which is one less than the other 2
						
						randomSpinResult = firstReelResult - 1;
						if (randomSpinResult < firstReelResult) randomSpinResult = firstReelResult - 1;
						if (randomSpinResult > firstReelResult) randomSpinResult = firstReelResult + 1;
						if (randomSpinResult < 0) randomSpinResult = 0;
						if (randomSpinResult > 9) randomSpinResult = 9;
						
						GameObject.Find("thirdReel").guiText.text = randomSpinResult.ToString();	
						thirdReelResult = randomSpinResult;
					}
					else {
						int weightedRandom = Random.Range(0, weightedReelPoll.Count);	
						GameObject.Find("thirdReel").guiText.text = weightedReelPoll[weightedRandom].ToString();
						thirdReelResult = (int)weightedReelPoll[weightedRandom];
					}
					
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
