public class NotesScript : MonoBehaviour
{
    private string[] notes = {"do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la", "la#", "si"};
    public int initialNote = 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LA ESCALA ES:");
        string[] scale = MajorScale(initialNote);
        Debug.Log("LOS ACORDES SON:");
        List<string[]> chords = Chords(scale);
    }

    string[] MajorScale(int index)
    {
        string[] resNotes = {
            notes[index],
            notes[(index+2)%12],
            notes[(index+4)%12],
            notes[(index+5)%12],
            notes[(index+7)%12],
            notes[(index+9)%12],
            notes[(index+11)%12]
        };

        foreach (string note in resNotes)
            Debug.Log(note);

        return resNotes;
    }

    List<string[]> Chords(string[] scale)
    {
        List<string[]> chords = new List<string[]>();

        for (int i = 0; i < 7; i++)
        {
            Debug.Log("ACORDE " + scale[i]);
            string[] chordNotes = {
                scale[i],
                scale[(i+2)%7],
                scale[(i+4)%7]
            };
            chords.Add(chordNotes);
            foreach (string chord in chordNotes)
                Debug.Log(chord);
        }

        return chords;
    }
}
