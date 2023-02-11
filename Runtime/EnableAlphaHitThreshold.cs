using UnityEngine;
using UnityEngine.UI;

namespace TKLibs
{
    public class EnableAlphaHitThreshold : MonoBehaviour
    {
        [SerializeField] float alphaThreshold = 0.1f;

        public float AlphaThreshold
        {
            get => alphaThreshold;
            set
            {
                alphaThreshold = value;
                GetComponent<Image>().alphaHitTestMinimumThreshold = value;
            }
        }

        void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
        }
    }
}