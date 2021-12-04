using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript : MonoBehaviour
{
    private Material loadMat;
    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                gameObject.tag = "Purple";
                loadMat = Resources.Load<Material>("Materials/NotePurple");
                renderer.material = loadMat;
                break;
            case 1:
                gameObject.tag = "Blue";
                loadMat = Resources.Load<Material>("Materials/NoteBlue");
                renderer.material = loadMat;
                break;
            case 2:
                gameObject.tag = "Green";
                loadMat = Resources.Load<Material>("Materials/NoteGreen");
                renderer.material = loadMat;
                break;
        }
    }
}
