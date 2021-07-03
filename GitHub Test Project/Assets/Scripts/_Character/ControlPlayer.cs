using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed;

    public Vector2 minMaxY;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        this.transform.Translate(new Vector3(x, y, 0));
        Clampposition();
    }

    private void Clampposition()
    {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(this.transform.position.x, -8.5f, 8.5f);
        pos.y = Mathf.Clamp(this.transform.position.y, minMaxY.x, minMaxY.y);
        this.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Shop")
        {
            GameManager.instance.ClothesUIPanel(true);
        }
    }
}
