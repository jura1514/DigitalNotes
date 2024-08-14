"use client"

import { NoteRow } from "@/components/noteRow";
import { useNotes } from "./notesProvider";

export const NoteList: React.FC = () => {
  const { notes, selectedNote, setSelectedNote } = useNotes();
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
