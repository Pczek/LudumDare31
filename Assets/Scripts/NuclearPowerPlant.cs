using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NuclearPowerPlant : MonoBehaviour
{
		public static Color cgreen = new Color (0, 170, 0);
		public static Color cred = new Color (170, 0, 0);
		// timing
		private float time = 0f;
		private float delta = 0.314f;
		// money
		private static float money = 0f;
		public Text moneyLabel;
		// price
		static float price = 0f;
		public Text priceLabel;
		// need
		private static int need = 0;
		public Text needLabel;
		// supply	
		private static int maxSupply = 10000;
		private static float supply = 0;
		public Text supplyLabel;

		


		
		// Use this for initialization
		void Start ()
		{
				// money
				this.moneyLabel.text = NuclearPowerPlant.money.ToString ();
				InvokeRepeating ("updateMoney", 0, 1);

				// price
				this.priceLabel.text = NuclearPowerPlant.price.ToString ();
				InvokeRepeating ("updatePrice", 0, 15);

				// need
				NuclearPowerPlant.need = Mathf.FloorToInt (Random.Range (0.1f * NuclearPowerPlant.maxSupply, 0.2f * NuclearPowerPlant.maxSupply));
				this.needLabel.text = NuclearPowerPlant.need.ToString ();
				InvokeRepeating ("updateNeed", 5, 15);
				
				// supply		
				this.supplyLabel.text = NuclearPowerPlant.supply.ToString ();
				InvokeRepeating ("updateSupply", 0, 1);
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		private void updateMoney ()
		{
				if (NuclearPowerPlant.supply < NuclearPowerPlant.need) {
						NuclearPowerPlant.money += (NuclearPowerPlant.supply * NuclearPowerPlant.maxSupply) * NuclearPowerPlant.price;
				} else {
						NuclearPowerPlant.money += NuclearPowerPlant.need * NuclearPowerPlant.price;
				}
				this.moneyLabel.text = (Mathf.RoundToInt (NuclearPowerPlant.money * 100) / 100).ToString ();
			
		}

		private void updatePrice ()
		{
				NuclearPowerPlant.price = (50 + Random.Range (-20, 20)) / 100f;
				this.priceLabel.text = NuclearPowerPlant.price.ToString ();
		}

		private void updateNeed ()
		{
				NuclearPowerPlant.need += Random.Range (0, Mathf.RoundToInt (0.1f * (NuclearPowerPlant.maxSupply - NuclearPowerPlant.supply)));
				this.needLabel.text = NuclearPowerPlant.need.ToString ();
		}
		
		private void updateSupply ()
		{
				this.supplyLabel.text = Mathf.FloorToInt (NuclearPowerPlant.maxSupply * NuclearPowerPlant.supply).ToString ();
		}

		public static void setSupply (float s)
		{
				NuclearPowerPlant.supply = s;
		}

		public static float getSupply ()
		{
				return NuclearPowerPlant.supply;
		}
}
