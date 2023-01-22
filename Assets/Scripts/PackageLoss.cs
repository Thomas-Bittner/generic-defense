using UnityEngine;

public class PackageLoss : MonoBehaviour
{
    [SerializeField]
    private GameObject package;

    private void OnDestroy()
    {
        var result = Random.Range(0, 10);

        if (result == 7)
        {
            Instantiate(package, this.transform.position, Quaternion.identity);
        }
    }
}
