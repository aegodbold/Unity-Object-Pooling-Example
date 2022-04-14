using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public void SpawnObjectFromPool(int index) {
        GameObject theObject = ObjectPooler.SharedInstance.GetPooledObject(index);
        if (theObject != null) {
            theObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            theObject.SetActive(true);
            StartCoroutine(DeSpawnTheObject(theObject));
        }
    }
    
    IEnumerator DeSpawnTheObject(GameObject theObject) {
		yield return new WaitForSeconds(5.0f);
		theObject.SetActive(false);
	}
}
