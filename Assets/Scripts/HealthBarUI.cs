using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour {
    [SerializeField] private Entity.HealthEntity healthEntity;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        Debug.Assert(healthEntity);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float currHealth = healthEntity.GetHealth();
        float maxHealth = healthEntity.GetMaxHealth();
        if ( Mathf.Abs(currHealth - maxHealth) < float.Epsilon) spriteRenderer.enabled = false;
        else {
            spriteRenderer.enabled = true;
            this.transform.localScale = new Vector3(currHealth / maxHealth, 0.15f, 1f);
        }
    }
}
