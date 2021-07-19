using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Panels
{
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract void Initialize();

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        public abstract void ClosePanel();
        public abstract void HidePanel();

        public abstract void ShowPanel();
        public abstract void BindEvents();
        public abstract void UnBindEvents();
    }
}