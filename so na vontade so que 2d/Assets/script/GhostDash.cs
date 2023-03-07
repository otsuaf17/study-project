using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDash : MonoBehaviour
{
    public float GhostDelay;
    private float GhostDelaySeconds;
    public GameObject Ghost;
    public bool MakeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        GhostDelaySeconds = GhostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (MakeGhost)
        {
            if (GhostDelaySeconds > 0)
            {
                GhostDelaySeconds -= Time.deltaTime;

            }
            else
            {
                GameObject currentGhost = Instantiate(Ghost, transform.position, transform.rotation);
                GhostDelaySeconds = GhostDelay;
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
