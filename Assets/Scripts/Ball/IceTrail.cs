using System.Collections.Generic;
using UnityEngine;

public class IceTrail : MonoBehaviour
{
    private List<GameObject> path = null;
    private List<float> timestamps = null;
    private float timer = 0.0f;
    private float tick = 0.0f;
    private const float MAX_TICK = 0.1f;
    private bool active = false;

    [SerializeField] private GameObject point;
    [SerializeField] private GameObject particles;
    [SerializeField] private float duration;

    void Awake()
    {
        if (path == null) {
            path = new List<GameObject>();
            timestamps = new List<float>();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Floor") ||
            collision.gameObject.tag.Contains("Furniture")) {
            active = true;
            tick = MAX_TICK;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Floor") ||
            collision.gameObject.tag.Contains("Furniture")) {
            active = false;
            tick = 0.0f;
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        while (timestamps.Count > 0 && timestamps[0] < timer) {
            timestamps.RemoveAt(0);
            Destroy(path[0]);
            path.RemoveAt(0);

            timestamps.TrimExcess();
            path.TrimExcess();
        }

        if (!active) return;

        tick += Time.deltaTime;
        if (tick >= MAX_TICK) {
            while (tick > MAX_TICK) tick -= MAX_TICK;
            Vector3 pos = gameObject.transform.position;
            if (path.Count == 0 || Vector3.Distance(pos, path[path.Count - 1].transform.position) > 0.5f) {
                GameObject newpoint = GameObject.Instantiate(point, pos, Quaternion.identity);

                float radius = gameObject.transform.localScale.x; // any dimension works here because spheres are uniform
                newpoint.GetComponent<SphereCollider>().radius = radius;
                GameObject particleObj = GameObject.Instantiate(particles, newpoint.transform);
                particleObj.transform.localScale *= (2 * radius);
                Destroy(particleObj, duration);

                path.Add(newpoint);
                timestamps.Add(timer + duration);
            }
        }
    }
}
