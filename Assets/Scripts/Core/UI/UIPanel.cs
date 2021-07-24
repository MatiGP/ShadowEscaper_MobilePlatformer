using System;
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
       
        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }
        public abstract void BindEvents();
        public abstract void UnBindEvents();
    }
}