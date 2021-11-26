using UnityEngine;

namespace SpellcastStudios
{
    [System.Serializable]
    public struct BulletCase
    {
        /// <summary>
        /// Prefab that is spawned when bullet is created. 
        /// </summary>
        public GameObject bulletPrefab;

        /// <summary>
        /// Count of bullets in case. Negative count means infinite
        /// </summary>
        public int count;

        /// <summary>
        /// Returns Bullet component attached to bulletPrefab 
        /// </summary>
        public Bullet bulletComponent => bulletPrefab.GetComponent<Bullet>();

        /// <summary>
        /// Whether this object is a "Null Representative"
        /// </summary>
        private bool isNullRepresentative;

        public bool IsNull => Equals(Null);

        /// <summary>
        /// Returns a BUlletCase with a "null representative"
        /// </summary>
        public static BulletCase Null
        {
            get
            {
                BulletCase nullCase = new BulletCase();
                nullCase.isNullRepresentative = true;
                return nullCase;
            }
        }

        /// <summary>
        /// Constructor with copy
        /// </summary>
        /// <param name="copy"></param>
        public BulletCase(BulletCase copy)
        {
            this.bulletPrefab = copy.bulletPrefab;
            this.count = copy.count;
            this.isNullRepresentative = copy.isNullRepresentative;
        }

        public override bool Equals(object obj)
        {
            BulletCase objCase = (BulletCase)obj;

            if (objCase.count != count)
                return false;

            if (objCase.bulletPrefab != bulletPrefab)
                return false;

            if (objCase.isNullRepresentative != isNullRepresentative)
                return false;

            return true;
        }

        //Too lazy to put actual HashCode here :^)
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}