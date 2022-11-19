using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景切换控制器（加载动画）
/// </summary>
public class Loading : MonoBehaviour
{
    private static string target;
    public static AsyncOperation operation = null;
    public static Animator activeAnimator;
    public static bool finished = false;
    #region 公开静态方法
    /// <summary>
    /// 启动场景切换
    /// </summary>
    /// <param name="scene">目标场景的名称（注意加入Scenes in Build）</param>
    public static void Run(string scene)
    {
        GameObject prefab = Resources.Load<GameObject>("LoadingPrefab"); // 载入加载动画预制体
        GameObject loading = Instantiate(prefab);
        target = scene;
        loading.SetActive(true);
        finished = false;
    }
    #endregion
    #region 内部成员
    [Tooltip("屏幕动画")]
    public Animator screen;
    private void Awake()
    {
        // 防止在当前场景被卸载时加载动画一同被销毁
        DontDestroyOnLoad(this.gameObject);
        activeAnimator = screen;
    }

    public void LoadTargetScene()
    {
        // 当加载动画已经覆盖屏幕时，异步载入新场景
        operation = SceneManager.LoadSceneAsync(target, LoadSceneMode.Single);
        // 注册加载完毕回调函数
        SceneManager.sceneLoaded += SceneLoaded;
        // 暂停屏幕动画
        screen.SetFloat("speed", 0f);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 场景加载完毕
        // 继续屏幕动画
        if (!BGMController.PausedDueToBGM)
            screen.SetFloat("speed", 1f);
        // 删除回调
        SceneManager.sceneLoaded -= SceneLoaded;
        operation = null;
        finished = true;
    }

    public void Dispose()
    {
        // 当动画播放结束时，销毁加载动画物体
        Destroy(this.gameObject);
    }
    #endregion
}
