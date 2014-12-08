using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pumps : MonoBehaviour
{
		private int meltdown = 400;
		// Pressure
		private bool[] isEnabled = new bool[3] {false,false,false};
		private float[] maxPower = new float[] {0.33f,0.33f,0.33f};
		public float[] currentVoltage = new float[] {0f,0f,0f};
		public GameObject[] pumps;
		//private int[] health = new int[] {100,100,100};
		private float desiredPumpPower = 0f;
		private float currentPumpPower = 0f;
		public GameObject pressureNeedle;
		private float maxAngle = 130f;
		private float currentAngleP = 0f;
		public AudioSource beeper1;
		public AudioSource beeper2;
		// Temperature
		public GameObject temperatureNeedle;
		private float currentTemperature = 0f;
		private float currentAngleT = 0f;

		// Signals
		public Toggle pressureLamp;
		public Toggle temperatureLamp;
		// Use this for initialization
		void Start ()
		{
				this.currentTemperature = 0.1f;
				//this.currentTemperature = ((this.maxAngle * 2) - 30) / (this.maxAngle * 2);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (this.currentTemperature > 0.9) {
						this.meltdown--;
				}

				if (this.meltdown < 0) {
						Application.LoadLevel ("Meltdown");
				}

		}

		// Update is called once per frame
		void FixedUpdate ()
		{
	
				// Pressure
				this.desiredPumpPower = calcDesiredPumpPower ();

				if (this.desiredPumpPower > this.currentPumpPower) {
						float diff = (this.desiredPumpPower - this.currentPumpPower);
						this.currentPumpPower += 0.0025f * diff;
						
				} else if (this.currentPumpPower > 0) {
						this.currentPumpPower -= 0.0008f;
				}
				this.currentAngleP = -1 * (((2 * this.maxAngle) * this.currentPumpPower) - this.maxAngle);

				this.pressureNeedle.transform.eulerAngles = new Vector3 (0f, 0f, this.currentAngleP);

				
				for (int i=0; i<this.pumps.Length; i++) {
						if (this.isEnabled [i]) {
								float currentPitch = this.pumps [i].audio.pitch;
								float desiredPitch = 0.5f * this.currentVoltage [i];
								float delta = 0.05f; 
								this.pumps [i].audio.pitch += (desiredPitch - currentPitch) * delta; 
						}
				}
				

				// Temperature
				if (this.currentTemperature < 1.001f && this.currentTemperature > 0) {
						this.currentTemperature += ((0.55f * NuclearPowerPlant.getSupply ()) - (0.45f * this.currentPumpPower)) * 0.05f;
				} else if (this.currentTemperature >= 1.001f) {
						this.currentTemperature = 1f;
				} else if (this.currentTemperature <= 0f) {
						this.currentTemperature = 0.001f;
				}

				this.currentAngleT = -1 * (((2 * this.maxAngle) * this.currentTemperature) - (this.maxAngle));
				this.temperatureNeedle.transform.eulerAngles = new Vector3 (0f, 0f, this.currentAngleT);


				// Signals
				if (this.currentTemperature > 0.8f) {
						this.temperatureLamp.isOn = true;
						if (!this.beeper1.isPlaying) {
								this.beeper1.Play ();
						}
					
	
				} else {
						this.temperatureLamp.isOn = false;
						this.beeper1.Stop ();
				}
	
				// Signals
				if (this.currentPumpPower > 0.8f) {
						this.pressureLamp.isOn = true;
						if (!this.beeper2.isPlaying) {
								this.beeper2.Play ();
						}
			
			
				} else {
						this.pressureLamp.isOn = false;
						this.beeper2.Stop ();
				}
		
		}
	
		private float calcDesiredPumpPower ()
		{
				float p = 0;		
				for (int i=0; i<this.isEnabled.Length; i++) {
						if (this.isEnabled [i]) {
								p += this.currentVoltage [i] * this.maxPower [i];
						}
				}
				return p;
				
		}

		public void togglePump (int pumpno)
		{
				pumpno -= 1;
				this.isEnabled [pumpno] = !this.isEnabled [pumpno];
				if (this.isEnabled [pumpno]) {
						this.pumps [pumpno].audio.pitch = 0;
						this.pumps [pumpno].audio.Play ();
				} else {
						this.pumps [pumpno].audio.Stop ();
				}
		}
	
		public void setPump1Voltage (float u)
		{
				this.currentVoltage [0] = u;
		}

		public void setPump2Voltage (float u)
		{
				this.currentVoltage [1] = u;
		}

		public void setPump3Voltage (float u)
		{
				this.currentVoltage [2] = u;
		}
}
