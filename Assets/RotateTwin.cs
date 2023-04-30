using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTwin : MonoBehaviour
{
   //public float RotationSpeed = 100.0f;

/*void Start(){

}
void Update() {
    //transform.Rotate(new Vector3(0,0 ,RotationSpeed) *  Time.deltaTime, Space.Self);

    //transform.Rotate(0f,100*Time.deltaTime,0f, Space.Self);
    //transform.RotateAround(Vector3.up, 100*Time.deltaTime);
}
*/
[SerializeField] private  Vector3 _rotation;
[SerializeField] private float _speed;
void Update()
{
    transform.Rotate(_rotation, _speed * Time.deltaTime);


}
}
