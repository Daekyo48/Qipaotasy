using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData Data { get; private set; }

    public void Initialize(NoteData data)
    {
        Data = data;
    }
}
