import { NoteRow } from "@/components/noteRow";
import { Note } from "@/services/types";

interface NoteListProps {
  notes: Note[];
  selectedNote: Note | undefined;
  setSelectedNote: (note: Note | undefined) => void;
}

export const NoteList: React.FC<NoteListProps> = ({
  notes,
  selectedNote,
  setSelectedNote,
}) => {
  return (
    <div className="flex-grow overflow-y-auto">
      {notes.length > 0 ? (
        notes.map((note, idx) => (
          <NoteRow
            note={note}
            index={idx}
            key={idx}
            setSelectedNote={setSelectedNote}
            isRowSelected={note.id === selectedNote?.id}
          />
        ))
      ) : (
        <div className="p-4 text-center text-2xl">No Results Found</div>
      )}
    </div>
  );
};
