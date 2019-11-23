using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClicker : MonoBehaviour {


    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire2"))
            Clicked();

    }

    private void Clicked() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.collider.gameObject.name);

            ClickMove clickMove = hit.collider.gameObject.GetComponent<ClickMove>();
            clickMove.OnClick(hit.point);
        }
    }
}
