using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class NoteSpawner : MonoBehaviour
{
    public bool IsAuto;
    public AudioSource sound;

    Note n;
    void Start()
    {
        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        //for(int i = 0; i < times.Count; i++)
        //{
        //    int divide = (int)(times[i] / bitSlice);
        //    if (times[i] % bitSlice < bitSlice / 2m)
        //        times[i] = bitSlice * divide;
        //    else
        //        times[i] = bitSlice * (divide + 1);
        //}
        //
    }

    void Update()
    {
        // ���� �� ����
        RhythmManager.Instance.CreateBar();

        //if (NoteLoad.Count > 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        // ��Ʈ Ŭ����
        //        if (NoteLoad.Peek().SendJudge() != Judge.None)
        //            NoteClear();
        //    }
        //    // ���� Ŭ����
        //    if (IsAuto)
        //    {
        //        if (NoteLoad.Peek().SendJudge() == Judge.Perfect)
        //        {
        //            Debug.Log(NoteLoad.Peek().Timing);
        //            NoteClear();
        //        }
        //    }
        //}

        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        //if (BarLoad.Count > 0 && BarLoad.Peek().Timing < -0.12501m)
        //    QueueSwaping(BarLoad, Bars);

        //if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
        //    QueueSwaping(NoteLoad, Notes);
    }
}
