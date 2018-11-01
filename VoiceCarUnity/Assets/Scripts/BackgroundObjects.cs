using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MonoBehaviour {

    public GameObject tree;
    public GameObject cloud;

    public int numberOfTrees = 20;
    public int numberOfClouds = 20;

    private float treeYRange = 0.5f;

    private float startX = -250f;
    private float endX = 50f;

    public void PlaceAllObjects() {
        PlaceTrees();
        PlaceClouds();
    }
	
    private void PlaceTrees() {
        if (tree) {
            float treeStartX = startX;
            for (int i = 0; i < numberOfTrees; i++) {
                Vector3 newPosition = new Vector3(treeStartX, 4.3f + Random.Range(0f, 1.5f), 0);
                GameObject newTree = Instantiate(tree, newPosition, transform.rotation);
                newTree.transform.localScale = tree.transform.localScale.x * Vector3.one * Random.Range(0.5f, 1.5f);
                treeStartX += Random.Range(10, 30);
            }
        }
    }

    private void PlaceClouds() {
        if (cloud) {
            float cloudStartX = startX;
            for (int i = 0; i < numberOfClouds; i++) {
                Vector3 newPosition = new Vector3(cloudStartX, 11f + Random.Range(0f, 1.5f), 0);
                GameObject newCloud = Instantiate(cloud, newPosition, transform.rotation);
                newCloud.transform.localScale = tree.transform.localScale.x * Vector3.one * Random.Range(0.5f, 1.5f);
                cloudStartX += Random.Range(5, 30);
            }
        }
    }

    private void PlaceObjects(GameObject theObject, int numberOfObjects, int xRangeStart, int xRangeEnd) {

    }
}
