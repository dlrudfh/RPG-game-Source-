using System.Collections;
using UnityEngine;

public class Pattern1 : MonoBehaviour
{
    [SerializeField]
    private GameObject pattern;

    private void Awake()
    {
        StartCoroutine(Pattern(0.1f));
    }

    private IEnumerator Pattern(float delay)
    {
        GameObject pat = Instantiate(pattern, new Vector3(7, transform.position.y, 0), Quaternion.identity);
        pat.GetComponent<Movement2D>().Setup(7.0f, Vector3.left);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Pattern(1f));
    }
}