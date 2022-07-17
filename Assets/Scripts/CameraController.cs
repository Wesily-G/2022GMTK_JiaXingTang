using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;                  //目标位置
    [SerializeField] Vector3 offset;                    //与目标偏移量     
    [SerializeField] float transitionSpeed = 2;         //相机移动速度

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, transitionSpeed * Time.deltaTime);
        }
    }
}
