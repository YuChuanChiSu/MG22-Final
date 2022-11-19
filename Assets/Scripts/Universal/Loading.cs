using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �����л������������ض�����
/// </summary>
public class Loading : MonoBehaviour
{
    private static string target;
    public static AsyncOperation operation = null;
    public static Animator activeAnimator;
    public static bool finished = false;
    #region ������̬����
    /// <summary>
    /// ���������л�
    /// </summary>
    /// <param name="scene">Ŀ�곡�������ƣ�ע�����Scenes in Build��</param>
    public static void Run(string scene)
    {
        GameObject prefab = Resources.Load<GameObject>("LoadingPrefab"); // ������ض���Ԥ����
        GameObject loading = Instantiate(prefab);
        target = scene;
        loading.SetActive(true);
        finished = false;
    }
    #endregion
    #region �ڲ���Ա
    [Tooltip("��Ļ����")]
    public Animator screen;
    private void Awake()
    {
        // ��ֹ�ڵ�ǰ������ж��ʱ���ض���һͬ������
        DontDestroyOnLoad(this.gameObject);
        activeAnimator = screen;
    }

    public void LoadTargetScene()
    {
        // �����ض����Ѿ�������Ļʱ���첽�����³���
        operation = SceneManager.LoadSceneAsync(target, LoadSceneMode.Single);
        // ע�������ϻص�����
        SceneManager.sceneLoaded += SceneLoaded;
        // ��ͣ��Ļ����
        screen.SetFloat("speed", 0f);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �����������
        // ������Ļ����
        if (!BGMController.PausedDueToBGM)
            screen.SetFloat("speed", 1f);
        // ɾ���ص�
        SceneManager.sceneLoaded -= SceneLoaded;
        operation = null;
        finished = true;
    }

    public void Dispose()
    {
        // ���������Ž���ʱ�����ټ��ض�������
        Destroy(this.gameObject);
    }
    #endregion
}
