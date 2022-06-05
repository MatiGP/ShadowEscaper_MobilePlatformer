using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Code.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UICounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_CounterText = null;
        [SerializeField] private AnimationCurve m_AnimationCurve = null;

        private int m_StartValue = 0;
        private int m_EndValue = 100;

        private Coroutine m_CountUpCoroutine = null;

        public void SetValues(int start, int end)
        {
            m_StartValue = start;
            m_EndValue = end;
        }

        public void StartCountUp()
        {
            m_CountUpCoroutine = StartCoroutine(CountUp());
        }

        private IEnumerator CountUp()
        {
            float currentTime = 0.0f;
            int currentValue = m_StartValue;
            int diff = m_EndValue - m_StartValue;

            while (currentTime != m_EndValue)
            {
                currentValue = m_StartValue + Mathf.RoundToInt(diff * m_AnimationCurve.Evaluate(currentTime));
                currentTime += Time.deltaTime;

                m_CounterText.text = currentValue.ToString();

                yield return null;
            }

            m_CounterText.text = m_EndValue.ToString();
        }

        private void OnDestroy()
        {
            if(m_CountUpCoroutine != null)
            {
                StopCoroutine(m_CountUpCoroutine);
                m_CountUpCoroutine = null;
            }
        }
    }
}
