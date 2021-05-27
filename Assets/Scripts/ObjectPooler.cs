using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour {
	public static ObjectPooler SharedInstance;

	[System.Serializable]
	public class ObjectPoolItem{
		public string key;
		public GameObject objectToPool;
		public int amountToPool;
		public bool shouldExpand = true;
		public List<GameObject> objectPoolList;
		public Transform parentObject;
	}

	public List<ObjectPoolItem> itemsToPool; //the items that can be pooled

	void Awake (){
		SharedInstance = this;
	}

	void Start (){
		//instantiate all of the pooled objects and add them to the list
		foreach (ObjectPoolItem item in itemsToPool) {
			item.objectPoolList = new List<GameObject> ();

			for (int i = 0; i < item.amountToPool; i++) {
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive (false);
				obj.transform.SetParent (item.parentObject);
				item.objectPoolList.Add (obj);
			}

			item.parentObject.gameObject.name = item.key + "(Pool Size: " + item.objectPoolList.Count + ")";
		}
	}

	public GameObject GetPooledObject (int itemIndex){
		//get the items from the list
		for (int i = 0; i < itemsToPool [itemIndex].objectPoolList.Count; i++) {
			if (!itemsToPool [itemIndex].objectPoolList [i].activeInHierarchy) {
				return itemsToPool [itemIndex].objectPoolList [i];
			}
		}

		//there weren't enough items in the list, so make a new one, if the list is expandable
		if (itemsToPool [itemIndex].shouldExpand == true) {
			GameObject obj = (GameObject)Instantiate(itemsToPool [itemIndex].objectToPool);
			obj.SetActive (false);
			obj.transform.SetParent (itemsToPool [itemIndex].parentObject);
			itemsToPool [itemIndex].objectPoolList.Add (obj);

			//change the parent object name to reflect there are more GO in the list
			itemsToPool [itemIndex].parentObject.gameObject.name = itemsToPool [itemIndex].key + "(Pool Size: " + itemsToPool [itemIndex].objectPoolList.Count + ")";
			return obj;

		} else {
			//change the parent object name to the list is not expandable and doesn't hold enough
			itemsToPool [itemIndex].parentObject.gameObject.name = itemsToPool [itemIndex].key + "(NOT ENOUGH OBJECTS)";
			return null;
		}
	}
}
