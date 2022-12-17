using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClimp : MonoBehaviour
{
   
    private bool climbing;
    private float climbTime;
	public static CClimp instance;
	private Vector3 ledgeClimbPos;
	private Vector3 ledgeOrigPos;
	public GameObject cam;
	public Rigidbody rb;
	private bool rootMotion;

	private float invulnCooldown;
	private void Awake()
    {
		if (instance != null && instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		instance = this;

	}
    private void Update()
    {
		if (climbing)
		{
			rb.MovePosition(Vector3.Lerp(ledgeOrigPos, ledgeClimbPos + Vector3.up * 2f, climbTime));
			climbTime += Time.fixedDeltaTime * 10f;
			if (climbTime >= 1f)
			{
				climbing = false;
				//StopClimbingAnim();
			}
		}
	}
    public void FixedUpdate()
    {
		Vector3 velocity = rb.velocity;
		velocity.x = Mathf.Lerp(velocity.x, 0f, Time.fixedDeltaTime * 30f);
		velocity.z = Mathf.Lerp(velocity.z, 0f, Time.fixedDeltaTime * 30f);
		rb.velocity = velocity;
		if (climbing)
		{
			rb.MovePosition(Vector3.Lerp(ledgeOrigPos, ledgeClimbPos + Vector3.up * 2f, climbTime));
			climbTime += Time.fixedDeltaTime * 10f;
			if (climbTime >= 1f)
			{
				climbing = false;
				//StopClimbingAnim();
			}
		}
		if (invulnCooldown > 0f)
		{
			invulnCooldown -= Time.fixedDeltaTime;
			if (invulnCooldown <= 0f)
			{
				base.gameObject.layer = LayerMask.NameToLayer("Player");
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		Vector3 normal = collision.GetContact(0).normal;
		if (climbing || rootMotion || !collision.collider.CompareTag("Climbable") || !(Mathf.Abs(Vector3.Dot(normal, Vector3.up)) < 0.1f))
		{
			return;
		}
		Vector3 forward = cam.transform.forward;
		forward.y = 0f;
		forward.Normalize();
		if (!(Vector3.Angle(forward, -normal) <= 45f))
		{
			return;
		}
		bool flag = true;
		if (Physics.Raycast(cam.transform.position + Vector3.up * 0.5f, -normal, out var hitInfo, 1f, LayerMask.GetMask("Ground")))
		{
			flag = false;
		}
		if (!flag)
		{
			return;
		}
		Vector3 vector = cam.transform.position + Vector3.up * 0.5f + Vector3.down * 0.05f;
		while (!Physics.Raycast(vector, -normal, out hitInfo, 1f, LayerMask.GetMask("Ground")))
		{
			vector += Vector3.down * 0.05f;
			if (vector.y < cam.transform.position.y - 2f)
			{
				break;
			}
		}
		if (vector.y >= cam.transform.position.y - 2f && !Physics.Raycast(cam.transform.position, Vector3.up, out var _, 0.5f, LayerMask.GetMask("Ground")))
		{
			ledgeOrigPos = base.transform.position;
			ledgeClimbPos = vector - normal * 0.5f;
			climbing = true;
			climbTime = 0f;
			//playerAnim.SetBool("climbing", value: true);
			//leftHandIK.active = true;
			//rightHandIK.active = true;
			Vector3 vector2 = Vector3.Cross(normal, Vector3.up);
			//leftHandIK.TargetPos = hitInfo.point - vector2 * 0.25f + normal * 0.2f;
			//rightHandIK.TargetPos = hitInfo.point + vector2 * 0.25f + normal * 0.2f;
			//armMotion.Disable();
			//AudioManager.instance.PlayOneShot("Slap");
		}
	}
}
