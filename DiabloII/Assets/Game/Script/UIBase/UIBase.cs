/*****************************************************************
    created:	2021/1/20
	author:		Roken24
	purpose:	UI基类
*****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    /// <summary>
        /// 当前UI是否打开
        /// </summary>
        public bool IsOpen { get { return this.gameObject.activeSelf; } }



        void Awake()
        {
            LOG_TAG = this.GetType().Name;
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        


        private void Update()
        {
            OnUpdate();
        }


        public void Open(object arg = null)
        {
            LOG_TAG = this.GetType().Name;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            
            OnOpen(arg);
        }


        public void Close(object arg = null)
        {
            
        }



        private void CloseWorker()
        {

        }



        protected virtual void OnOpen(object arg = null)
        {
            //Layer = UILayerDef.GetDefaultLayer(UIType);
        }

        protected virtual void OnClose(object arg = null)
        {
            
        }

        protected virtual void OnUpdate()
        {
            
        }



        public string LOG_TAG { get; protected set; }



        /// <summary>
        /// 方便寻找Panel上的UI控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public T Find<T>(string controlName) where T : MonoBehaviour
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                return target.GetComponent<T>();
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+ controlName);
                return default(T);
            }
        }



        #region UI事件监听辅助函数
        /// <summary>
        /// 为UIPanel内的脚本提供便捷的UI事件监听接口
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="listener"></param>
        public void AddUIClickListener(string controlName, Action<string> listener)
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                UIEventTrigger.Get(target).onClickWithName -= listener;
                UIEventTrigger.Get(target).onClickWithName += listener;
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+ controlName);
            }
        }

        /// <summary>
        /// 为UIPanel内的脚本提供便捷的UI事件监听接口
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="listener"></param>
        public void AddUIClickListener(string controlName, Action listener)
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                UIEventTrigger.Get(target).onClick -= listener;
                UIEventTrigger.Get(target).onClick += listener;
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+controlName);
            }
        }



        /// <summary>
        /// 为UIPanel内的脚本提供便捷的UI事件监听接口
        /// </summary>
        /// <param name="target"></param>
        /// <param name="listener"></param>
        public void AddUIClickListener(UIBehaviour target, Action listener)
        {
            if (target != null)
            {
                UIEventTrigger.Get(target).onClick -= listener;
                UIEventTrigger.Get(target).onClick += listener;
            }
        }



        /// <summary>
        /// 移除UI控件的监听器
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="listener"></param>
        public void RemoveUIClickListener(string controlName, Action<string> listener)
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                if (UIEventTrigger.HasExistOn(target))
                {
                    UIEventTrigger.Get(target).onClickWithName -= listener;
                }
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+ controlName);
            }
        }

        /// <summary>
        /// 移除UI控件的监听器
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="listener"></param>
        public void RemoveUIClickListener(string controlName, Action listener)
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                if (UIEventTrigger.HasExistOn(target))
                {
                    UIEventTrigger.Get(target).onClick -= listener;
                }
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+ controlName);
            }
        }

        /// <summary>
        /// 移除UI控件的监听器
        /// </summary>
        /// <param name="target"></param>
        /// <param name="listener"></param>
        public void RemoveUIClickListener(UIBehaviour target, Action listener)
        {
            if (target != null)
            {
                if (UIEventTrigger.HasExistOn(target.transform))
                {
                    UIEventTrigger.Get(target).onClick -= listener;
                }
            }
        }


        /// <summary>
        /// 移除UI控件的所有监听器
        /// </summary>
        /// <param name="controlName"></param>
        public void RemoveUIClickListeners(string controlName)
        {
            Transform target = this.transform.Find(controlName);
            if (target != null)
            {
                if (UIEventTrigger.HasExistOn(target))
                {
                    UIEventTrigger.Get(target).onClick = null;
                }
            }
            else
            {
                Debug.LogError("未找到UI控件：{0}"+controlName);
            }
        }

        /// <summary>
        /// 移除UI控件的所有监听器
        /// </summary>
        /// <param name="target"></param>
        public void RemoveUIClickListeners(UIBehaviour target)
        {
            if (target != null)
            {
                if (UIEventTrigger.HasExistOn(target.transform))
                {
                    UIEventTrigger.Get(target).onClick = null;
                }
            }
        }

        #endregion 

}
