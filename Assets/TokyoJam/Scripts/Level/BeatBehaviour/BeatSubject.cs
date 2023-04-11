using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSubject : MonoBehaviour
{
    public List<IBeatNotifier> beatNotifiers = new List<IBeatNotifier>();
    public int _BPM;
    private static BeatSubject _Instance;
    public static BeatSubject Instance { 
        get {
            if (_Instance == null)
                _Instance = GameObject.FindObjectOfType<BeatSubject>();
            return _Instance;
        } 
    }

    Coroutine beatProcess;

    public void AddListener(IBeatNotifier notifier) { 
        beatNotifiers.Add(notifier);
    }
    public void RemoveListener(IBeatNotifier notifier) { 
        beatNotifiers.Remove(notifier);
    }
    private void OnEnable()
    {
        if(beatProcess != null)
            StopCoroutine(beatProcess);

        StartCoroutine(NotifyBeat());
    }
    public IEnumerator NotifyBeat() { 
        while (true)
        {
            beatNotifiers.ForEach(notifier => { notifier.OnBeatNotify(); });
            yield return new WaitForSeconds(1/((float)_BPM / 60f));
        }
    }
}
