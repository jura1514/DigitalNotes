"use client";

import { useNotes } from "@/components/index";
import NoteService from "@/services/noteService";
import { PlusCircleIcon, TrashIcon } from "./icons";

export function NoteActions() {
  const { selectedNote, fetchNotes, setSelectedNote } = useNotes();
  const noteService: NoteService = new NoteService();

  const deleteNote = async () => {
    if (selectedNote?.id) {
      await noteService.delete(selectedNote.id);
      setSelectedNote(undefined);
      await fetchNotes();
    }
  };

  return (
    <div className="flex justify-start p-5">
      <span
        className={`mr-2 ${
          selectedNote?.id ? "hover:cursor-pointer" : "pointer-events-none"
        }`}
        onClick={() => setSelectedNote(undefined)}
      >
        <PlusCircleIcon className="size-8 text-black hover:text-accent-foreground hover:drop-shadow-md" />
      </span>
      <span
        className={
          selectedNote?.id ? "hover:cursor-pointer" : "pointer-events-none"
        }
        onClick={deleteNote}
      >
        <TrashIcon className="size-8 text-black hover:text-accent-foreground hover:drop-shadow-md" />
      </span>
    </div>
  );
}
