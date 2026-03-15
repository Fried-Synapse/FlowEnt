using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

public class UnityWebRequestAwaiter : INotifyCompletion
{
    //TODO
    private UnityWebRequestAsyncOperation asyncOperation;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOperation)
    {
        this.asyncOperation = asyncOperation;
        //asyncOperation.completed += OnRequestCompleted;
    }

    public bool IsCompleted { get { return asyncOperation.isDone; } }

    public void GetResult() { }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
    }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        continuation();
    }
}

public static class ExtensionMethods
{
    public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        return new UnityWebRequestAwaiter(asyncOp);
    }
}

/*
// Usage example:
UnityWebRequest www = new UnityWebRequest();
// ...
await www.SendWebRequest();
Debug.Log(req.downloadHandler.text);
*/