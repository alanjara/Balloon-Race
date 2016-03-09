using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public class FireHomingProjectile : FireProjectileScript
    {
        private GameObject target;
        private const float speed = 20f;
        private bool targetSet = false;

        public FireHomingProjectile(GameObject target)
        {
            this.target = target;
            targetSet = true;
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
            Debug.Assert(targetSet);
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);


        }
        
    }
}