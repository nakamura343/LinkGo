using LinkGo.Common.Event;
using UnityEngine;

//此处建议事件按模块分段
public class GameEventId
{
    //EventId:<module_submodule_id>
    public const int Task_init = 2000;
    public const int Task_RemoveTask_RemovePetTask = 2001;
    //...
    public const int Task_end = 2200;

    public const int Activity_start = 3000;
    //...
    public const int Activity_end = 3200;
}

public class TestEvent : MonoBehaviour
{

    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 200, 100), "AddEvent"))
        {
            EventManager.AddListener<int, string>(GameEventId.Task_RemoveTask_RemovePetTask, OnRemoveTask);
            Debug.LogFormat("AddEvent=>eventid:{0}", GameEventId.Task_RemoveTask_RemovePetTask);
        }

        if (GUI.Button(new Rect(50, 180, 200, 100), "RemoveEvent"))
        {
            EventManager.RemoveListener<int, string>(GameEventId.Task_RemoveTask_RemovePetTask, OnRemoveTask);
            Debug.LogFormat("RemoveEvent=>eventid:{0}", GameEventId.Task_RemoveTask_RemovePetTask);
        }

        if (GUI.Button(new Rect(50, 310, 200, 100), "Dispatcher"))
        {
            Debug.LogFormat("DispatcherEvent=>eventid:{0}", GameEventId.Task_RemoveTask_RemovePetTask);
            EventManager.Dispatcher<int, string>(GameEventId.Task_RemoveTask_RemovePetTask, 1000, "hello,world");
        }
    }

    public void OnRemoveTask(int task_id, string task_name)
    {
        Debug.LogFormat("OnRemoveTask=>id={0} name={1}", task_id, task_name);
    }
}
