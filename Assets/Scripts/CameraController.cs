using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;                  //Ŀ��λ��
    [SerializeField] Vector3 offset;                    //��Ŀ��ƫ����     
    [SerializeField] float transitionSpeed = 2;         //����ƶ��ٶ�

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, transitionSpeed * Time.deltaTime);
        }
    }
}
