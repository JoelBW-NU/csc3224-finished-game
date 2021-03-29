using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField]
    float horizontalMaxRange = 100;

    [SerializeField]
    float horizontalMinRange = 60;

    [SerializeField]
    float verticalMaxRange = 30;

    [SerializeField]
    float verticalMinRange = -20;

    [SerializeField]
    GameObject packagePrefab;

    [SerializeField]
    ItemPointers packageFinder;

    [SerializeField]
    GameLogic game;

    [SerializeField]
    Transform homeBase;

    [SerializeField]
    GameObject collectPackageText;

    void Start()
    {
        SpawnPackage();
    }

    public void SpawnPackage()
    {
        float x = Random.value <= 0.5 ? Random.Range(-horizontalMaxRange, -horizontalMinRange) : Random.Range(horizontalMinRange, horizontalMaxRange);
        float y = Random.Range(verticalMinRange, verticalMaxRange);
        Vector2 position = new Vector2(homeBase.position.x + x, homeBase.position.y + y);
        GameObject package = Instantiate(packagePrefab, position, Quaternion.identity);
        package.GetComponentInChildren<ParticleSystem>().Stop();
        package.GetComponent<Package>().game = game;
        package.GetComponent<Package>().collectPackageText = collectPackageText;
        package.GetComponent<Package>().packageSpawner = this;
        packageFinder.UpdatePackage(package.transform);
    }
}
