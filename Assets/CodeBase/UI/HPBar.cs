using Assets.CodeBase.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _imageCurrent;

        public void Construct(IDamageable damageable)
        {
            damageable.HPChanged += OnHPChanged;
        }

        public void SetState(bool state)
        {
            gameObject.SetActive(state);
        }

        private void OnHPChanged(int current, int max)
        {
            _imageCurrent.fillAmount = (float)current / max;
        }
    }
}
