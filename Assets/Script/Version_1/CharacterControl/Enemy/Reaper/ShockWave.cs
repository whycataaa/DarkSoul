using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game2;
public class ShockWave : MonoBehaviour
{
    public float BaseAttack;
    private ParticleSystem mainPartic;
    private SphereCollider sphereCollider;
    public float speed = 10.8f;
    private void Start()
    {
        mainPartic = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        //粒子不再播放
        if (!mainPartic.IsAlive())
            Destroy(mainPartic.gameObject);
        if(sphereCollider.radius<=21.5f)
            sphereCollider.radius += speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //该技能可以跳跃躲避
        if (other.tag == "PlayerBody" && other.GetComponentInParent<PlayerControl>().IsGround)
        {
            other.GetComponentInParent<PlayerStateMachine>().GetDamage(new AttackInfo(BaseAttack,WeaponType.Default));
        }
    }
}
