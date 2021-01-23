//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This is a script used to keep the game object scaled to 2/(Screen.height).
/// If you use it, be sure to NOT use UIOrthoCamera at the same time.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	static public List<UIRoot> list = new List<UIRoot>();

	/// <summary>
	/// List of all UIRoots present in the scene.
	/// </summary>

	public enum Scaling
	{
		PixelPerfect,
		FixedSize,
		FixedSizeOnMobiles,
	}

	/// <summary>
	/// Type of scaling used by the UIRoot.
	/// </summary>

	public Scaling scalingStyle = Scaling.FixedSize;

	/// <summary>
	/// Height of the screen when the scaling style is set to FixedSize.
	/// </summary>

	public int manualHeight = 720;

	/// <summary>
	/// If the screen height goes below this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int minimumHeight = 320;

	/// <summary>
	/// If the screen height goes above this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int maximumHeight = 1536;

	/// <summary>
	/// UI Root's active height, based on the size of the screen.
	/// </summary>

	public int activeHeight
	{
		get
		{
			int height = Mathf.Max(2, Screen.height);
			if (scalingStyle == Scaling.FixedSize) return manualHeight;

#if UNITY_IPHONE || UNITY_ANDROID
			if (scalingStyle == Scaling.FixedSizeOnMobiles)
				return manualHeight;
#endif
			if (height < minimumHeight) return minimumHeight;
			if (height > maximumHeight) return maximumHeight;
			return height;
		}
	}

	/// <summary>
	/// Pixel size adjustment. Most of the time it's at 1, unless the scaling style is set to FixedSize.
	/// </summary>

	public float pixelSizeAdjustment { get { return GetPixelSizeAdjustment(Screen.height); } }

	/// <summary>
	/// Helper function that figures out the pixel size adjustment for the specified game object.
	/// </summary>

	static public float GetPixelSizeAdjustment (GameObject go)
	{
		UIRoot root = NGUITools.FindInParents<UIRoot>(go);
		return (root != null) ? root.pixelSizeAdjustment : 1f;
	}

	/// <summary>
	/// Calculate the pixel size adjustment at the specified screen height value.
	/// </summary>

	public float GetPixelSizeAdjustment (int height)
	{
		height = Mathf.Max(2, height);

		if (scalingStyle == Scaling.FixedSize)
			return (float)manualHeight / height;

#if UNITY_IPHONE || UNITY_ANDROID
		if (scalingStyle == Scaling.FixedSizeOnMobiles)
			return (float)manualHeight / height;
#endif
		if (height < minimumHeight) return (float)minimumHeight / height;
		if (height > maximumHeight) return (float)maximumHeight / height;
		return 1f;
	}

	Transform mTrans;

	protected virtual void Awake () {

        //让UIRoot一直存在于所有场景中
        DontDestroyOnLoad(gameObject);
        m_uiCamera = UICamera;
        m_canvasScaler = GetComponent<CanvasScaler>();
        mTrans = transform; 
    }
	protected virtual void OnEnable () { list.Add(this); }
	protected virtual void OnDisable () { list.Remove(this); }

	protected virtual void Start ()
	{
		UIOrthoCamera oc = GetComponentInChildren<UIOrthoCamera>();

		if (oc != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", oc);
			Camera cam = oc.gameObject.GetComponent<Camera>();
			oc.enabled = false;
			if (cam != null) cam.orthographicSize = 1f;
		}
		else Update();
	}

	void Update ()
	{
		if (mTrans != null)
		{
			float calcActiveHeight = activeHeight;

			if (calcActiveHeight > 0f )
			{
				float size = 2f / calcActiveHeight;
				
				Vector3 ls = mTrans.localScale;
	
				if (!(Mathf.Abs(ls.x - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.y - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.z - size) <= float.Epsilon))
				{
					mTrans.localScale = new Vector3(size, size, size);
				}
			}
		}
	}

	/// <summary>
	/// Broadcast the specified message to the entire UI.
	/// </summary>

	static public void Broadcast (string funcName)
	{
		for (int i = 0, imax = list.Count; i < imax; ++i)
		{
			UIRoot root = list[i];
			if (root != null) root.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// Broadcast the specified message to the entire UI.
	/// </summary>

	static public void Broadcast (string funcName, object param)
	{
		if (param == null)
		{
			// More on this: http://answers.unity3d.com/questions/55194/suggested-workaround-for-sendmessage-bug.html
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			for (int i = 0, imax = list.Count; i < imax; ++i)
			{
				UIRoot root = list[i];
				if (root != null) root.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
    /// <summary>
    /// 从UIRoot下通过名字&类型寻找一个组件对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T Find<T>(string name) where T : MonoBehaviour
    {
        GameObject obj = Find(name);
        if (obj != null)
        {
            return obj.GetComponent<T>();
        }
        return null;
    }



    public static GameObject Find(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }


        Transform obj = null;
        GameObject root = FindUIRoot();
        if (root != null)
        {
            //在新版Unity中，Find函数会将Name中的/当作分隔符
            //obj = root.transform.FindChild(name);

            Transform t = root.transform;
            for (int i = 0; i < t.childCount; i++)
            {
                Transform c = t.GetChild(i);
                if (c.name == name)
                {
                    obj = c;
                    break;
                }
            }
        }

        if (obj != null)
        {
            return obj.gameObject;
        }
        return null;
    }

    public static GameObject FindUIRoot()
    {
        GameObject root = GameObject.Find("UIRoot");
        if (root != null && root.GetComponent<UIRoot>() != null)
        {
            return root;
        }
        return null;
    }


    /// <summary>
    /// 当一个UIPage/UIWindow/UIWidget添加到UIRoot下面
    /// </summary>
    /// <param name="child"></param>
    public static void AddChild(UIBase child)
    {
        GameObject root = FindUIRoot();
        if (root == null || child == null)
        {
            return;
        }
        child.transform.SetParent(root.transform, false);

    }


    public static void Sort()
    {
        GameObject root = FindUIRoot();
        if (root == null)
        {
            return;
        }

        List<UIBase> list = new List<UIBase>();
        root.GetComponentsInChildren<UIBase>(true, list);
        //list.Sort((a, b) =>
        //{
        //    return a.Layer - b.Layer;
        //});

        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.SetSiblingIndex(i);
        }
    }


    //==============================================================================
    //UIRoot本身的逻辑
    //==============================================================================
    private static Camera m_uiCamera;
    private static CanvasScaler m_canvasScaler;

    public Camera UICamera;
    public CanvasScaler GetUIScaler()
    {
        return m_canvasScaler;
    }

    public Camera GetUICamera()
    {
        return m_uiCamera;
    }

}
