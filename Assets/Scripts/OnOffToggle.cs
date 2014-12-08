using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnOffToggle : MonoBehaviour
{
		public Toggle brother;
		private Toggle myself;
		// Use this for initialization
		void Start ()
		{
				this.myself = this.GetComponent<Toggle> ();
		}

		public void updateState ()
		{
				if (this.brother.isOn) {
						this.myself.isOn = false;
						this.myself.interactable = false;
				} else {
						this.myself.isOn = true;
						this.myself.interactable = true;
				}
		}
}
