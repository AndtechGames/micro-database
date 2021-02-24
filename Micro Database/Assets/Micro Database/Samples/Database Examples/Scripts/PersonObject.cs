using UnityEngine;
using UnityEngine.UI;

namespace Andtech.MicroDatabase {

	public class PersonObject : MonoBehaviour {
		public PersonData Data { get; set; }

		[SerializeField]
		private Text nameText;

		public void Download() {
			nameText.text = string.Format("{0}\n{1} y/o\n{2}%", Data.Name, Data.Age, Data.Health);
		}
	}
}
