using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    private Quaternion targetQuaternion;

    [SerializeField] float moveSpeed=1;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
            targetQuaternion = Quaternion.LookRotation(new Vector3(h, 0, v));
            transform.localRotation = Quaternion.Slerp(transform.localRotation,targetQuaternion,Time.deltaTime*10f);

            characterController.SimpleMove(new Vector3(h, 0, v).normalized * moveSpeed);
        }

    }
}
