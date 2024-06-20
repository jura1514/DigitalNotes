"use client";

import NoteForm from "@/components/noteForm";
import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import { useEffect, useState } from "react";
import styles from "./page.module.css";

export default function Home() {
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | undefined>(undefined);

  const displayNoteRecentDate = (note: Note): string => {
    const recentDate = note.updatedAt || note.createdAt;
    return new Date(recentDate).toLocaleDateString();
  };

  const fetchNotes = async (): Promise<void> => {
    const noteService = new NoteService();
    const lastRowNumber = await noteService.getLastRowNumber("user2");
    if (lastRowNumber && lastRowNumber !== 0) {
      const fetchedNotes = await noteService.getAll("user2", lastRowNumber);
      setNotes(fetchedNotes);
    }
  };

  useEffect(() => {
    fetchNotes();
  }, []);

  return (
    <main className={styles.content}>
      <div className={styles.container}>
        <div
          className={`${styles.columnOne} xl:flex-1 sm:flex-auto md:flex-auto`}
        >
          <div>
            {notes.map((note) => (
              <a key={note.id} onClick={() => setSelectedNote(note)}>
                <div>
                  <h2>{note.title}</h2>
                  <p>{displayNoteRecentDate(note)}</p>
                </div>
              </a>
            ))}
          </div>
        </div>
        <div className={`${styles.columnTwo} p-3`}>
          <NoteForm fetchNotes={fetchNotes} selectedNote={selectedNote} />
        </div>
      </div>
    </main>
  );
}
