using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class CustomBoxCollider2D : MonoBehaviour
{
    public enum UpdateMethod
    {
        OnFrame, OnUse, FixedRate
    }

    #region Public Getters
    public UpdateMethod CurrentUpdateMethod { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return updateMethod; } }

    public bool WillSystemFunction { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return willSystemFunction; } }

    public float TimeBeforeUpdate { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return timeBeforeUpdate; } }
    public float LastUpdatedFrame { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return lastUpdatedFrame; } }
    public float LastDataChangedFrame { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return lastDataChangedFrame; } }

    public float ScaleToRealSizeX { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return ScaleToRealSizeX; } }
    public float ScaleToRealSizeY { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return ScaleToRealSizeY; } }

    public float RealSizeX { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return realSizeX; } }
    public float RealSizeY { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return realSizeY; } }

    public Vector3 CornerA { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return cornerA; } }
    public Vector3 CornerB { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return cornerB; } }
    public Vector3 CornerC { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return cornerC; } }
    public Vector3 CornerD { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return cornerD; } }

    public Vector3[] Corners { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return new Vector3[4] { cornerA, cornerB, cornerC, cornerD }; } }

    public Vector3 TopEdgeMid { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return topEdgeMid; } }
    public Vector3 BottomEdgeMid { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return bottomEdgeMid; } }
    public Vector3 LeftEdgeMid { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return leftEdgeMid; } }
    public Vector3 RightEdgeMid { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return rightEdgeMid; } }

    public Vector3 TopMostPoint { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return topMostPoint; } }
    public Vector3 BottomMostPoint { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return bottomMostPoint; } }
    public Vector3 LeftMostPoint { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return leftMostPoint; } }
    public Vector3 RightMostPoint { get { if(updateMethod == UpdateMethod.OnUse) TriggerUpdate(); return rightMostPoint; } }
    #endregion

    #region Private Fields
    [SerializeField]
    private UpdateMethod updateMethod = UpdateMethod.OnFrame;
    [SerializeField]
    private int updateRate = 60;

    [HideInInspector]
    private int lastUpdateRate = 60;
    [HideInInspector]
    private float updateTime = 1 / 60f;

    [SerializeField]
    private bool showDebuggingInfo = false;

    [SerializeField]
    [HideInInspector]
    private bool willSystemFunction = false;

    [SerializeField]
    [HideInInspector]
    private float timeBeforeUpdate = 0f;
    [SerializeField]
    [HideInInspector]
    private int lastUpdatedFrame = -1;
    [SerializeField]
    [HideInInspector]
    private int lastDataChangedFrame = -1;

    [SerializeField]
    [HideInInspector]
    private float scaleToRealSizeX = -1f;
    [SerializeField]
    [HideInInspector]
    private float scaleToRealSizeY = -1f;

    [SerializeField]
    private bool showColliderInfo = true;

    [HideInInspector]
    private Vector3 lastPosition;
    [HideInInspector]
    private Quaternion lastRotation;
    [HideInInspector]
    private Vector3 lastScale;

    [SerializeField]
    [HideInInspector]
    private float realSizeX;
    [SerializeField]
    [HideInInspector]
    private float realSizeY;

    //[SerializeField]
    //[HideInInspector]
    //private Vector3 offsetX;
    //[SerializeField]
    //[HideInInspector]
    //private Vector3 offsetY;

    [SerializeField]
    [HideInInspector]
    private Vector3 cornerA;
    [SerializeField]
    [HideInInspector]
    private Vector3 cornerB;
    [SerializeField]
    [HideInInspector]
    private Vector3 cornerC;
    [SerializeField]
    [HideInInspector]
    private Vector3 cornerD;

    [SerializeField]
    [HideInInspector]
    private Vector3 topEdgeMid;
    [SerializeField]
    [HideInInspector]
    private Vector3 bottomEdgeMid;
    [SerializeField]
    [HideInInspector]
    private Vector3 leftEdgeMid;
    [SerializeField]
    [HideInInspector]
    private Vector3 rightEdgeMid;

    [SerializeField]
    [HideInInspector]
    private Vector3 topMostPoint;
    [SerializeField]
    [HideInInspector]
    private Vector3 bottomMostPoint;
    [SerializeField]
    [HideInInspector]
    private Vector3 leftMostPoint;
    [SerializeField]
    [HideInInspector]
    private Vector3 rightMostPoint;

    [SerializeField]
    [HideInInspector]
    private bool drawCollider;
    #endregion

    private void Awake()
    {
        UpdateFunctionability();

        TriggerUpdate();
    }

    private void Update()
    {
        if(!willSystemFunction)
        {
            UpdateFunctionability();
            return;
        }

        if(updateMethod == UpdateMethod.OnFrame)
        {
            UpdateCollider();
        }
        else if(updateMethod == UpdateMethod.FixedRate)
        {
            if(updateRate != lastUpdateRate)
            {
                if(updateRate != 0)
                {
                    updateTime = 1f / updateRate;
                }
                else
                {
                    updateTime = float.PositiveInfinity;
                }

                timeBeforeUpdate = updateTime;

                lastUpdateRate = updateRate;
            }

            if(timeBeforeUpdate <= 0f)
            {
                UpdateCollider();

                timeBeforeUpdate = updateTime;
            }
            else
            {
                timeBeforeUpdate -= Time.deltaTime;
            }
        }
    }

    private void UpdateCollider()
    {
        var frameCount = Time.frameCount;
        lastUpdatedFrame = frameCount;

        var pos = transform.position;
        var rot = transform.rotation;
        var scale = transform.lossyScale;

        if(lastPosition == pos && lastRotation == rot && lastScale == scale)
        {
            return;
        }

        lastPosition = pos;
        lastRotation = rot;
        lastScale = scale;

        lastDataChangedFrame = frameCount;

        realSizeX = scale.x * scaleToRealSizeX;
        realSizeY = scale.y * scaleToRealSizeY;

        var halfSizeX = RealSizeX / 2;
        var halfSizeY = RealSizeY / 2;

        var vOffNormal = new Vector2(halfSizeX, halfSizeY);
        var vOffInverse = new Vector2(halfSizeX, -halfSizeY);

        var A = cornerA = rot * -vOffInverse + pos;
        var B = cornerB = rot * vOffNormal + pos;
        var C = cornerC = rot * vOffInverse + pos;
        var D = cornerD = rot * -vOffNormal + pos;

#if UNITY_EDITOR
        if(drawCollider)
        {
            Debug.DrawLine(A, B);
            Debug.DrawLine(B, C);
            Debug.DrawLine(C, D);
            Debug.DrawLine(D, A);
        }
#endif

        topEdgeMid = (A + B) / 2;
        bottomEdgeMid = (C + D) / 2;
        leftEdgeMid = (D + A) / 2;
        rightEdgeMid = (B + C) / 2;

        var mosts = new Vector3[4] { A, A, A, A };//respectively: rightMost, topMost, leftMost, bottomMost

        var corners = new Vector3[4] { A, B, C, D };

        for(int i = 0; i < mosts.Length; i++)
        {
            var k = i % 2;
            var moreOrLess = i < 2;

            for(int j = 1; j < corners.Length; j++)
            {
                if(moreOrLess ? corners[j][k] > mosts[i][k] : corners[j][k] < mosts[i][k])
                {
                    mosts[i] = corners[j];
                }
            }
        }

        rightMostPoint = mosts[0];
        topMostPoint = mosts[1];
        leftMostPoint = mosts[2];
        bottomMostPoint = mosts[3];
    }

    private bool UpdateFunctionability()
    {
        bool willFunction;

        var sr = GetComponent<SpriteRenderer>();

        var sprite = sr.sprite;
        if(!sprite)
        {
            willFunction = false;
        }
        else
        {
            var ppu = sprite.pixelsPerUnit;

            if(ppu == 0)
            {
                willFunction = false;
            }
            else
            {
                var rect = sprite.rect;

                scaleToRealSizeX = rect.width / ppu;
                scaleToRealSizeY = rect.height / ppu;

                willFunction = true;
            }
        }

        willSystemFunction = willFunction;
        return willFunction;
    }

    /// <summary>
    /// Tries to set the update method to updateMethod and returns whether it was able to.
    /// </summary>
    /// <param name="updateMethod"></param>
    /// <returns></returns>
    public bool SetUpdateMethod(UpdateMethod updateMethod)
    {
        this.updateMethod = updateMethod;

        Debug.LogWarning("Not fully implemented.");

        return this.updateMethod == updateMethod;
    }

    /// <summary>
    /// Updates the collider if it has not been updated in the current frame
    /// </summary>
    /// <returns>Returns true if updated; else, returns false.</returns>
    public bool TriggerUpdate()
    {
        if(!willSystemFunction || Time.frameCount == lastUpdatedFrame)
        {
            return false;
        }
        else
        {
            UpdateCollider();
            return true;
        }
    }

    /// <summary>
    /// Updates the collider even if it has been updated in the current frame. Should not be used unless explicity needed.
    /// </summary>
    public void ForceUpdate()
    {
        if(!willSystemFunction)
        {
            return;
        }

        UpdateCollider();
    }

    /// <summary>
    /// Checks the sprite and determines whether the system can function or not.
    /// </summary>
    public void CheckSprite()
    {
        UpdateFunctionability();
    }

    /// <summary>
    /// Sets the sprite and determines whether the system can function or not. The sprite is set
    /// even if the system cannot function with it. There is no need to use CheckSprite().
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSprite(Sprite sprite)
    {
        var sr = GetComponent<SpriteRenderer>();

        sr.sprite = sprite;

        var ppu = sprite.pixelsPerUnit;

        if(ppu == 0)
        {
            willSystemFunction = false;
            return;
        }

        var rect = sprite.rect;

        scaleToRealSizeX = rect.width / ppu;
        scaleToRealSizeY = rect.height / ppu;

        willSystemFunction = true;
    }

    /// <summary>
    /// Sets the real size of the object.
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(Vector2 size)
    {
        size.Scale(new Vector2(1 / scaleToRealSizeX, 1 / scaleToRealSizeY));
        transform.localScale = size;
    }
}
