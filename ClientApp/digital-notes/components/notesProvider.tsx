"use client";

import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";

type NotesContextType = {
  query: string;
  notes: Note[];
  selectedNote: Note | undefined;
  pageNumber: number;
  totalPages: number;
  setQuery: React.Dispatch<React.SetStateAction<string>>;
  setNotes: React.Dispatch<React.SetStateAction<Note[]>>;
  setSelectedNote: React.Dispatch<React.SetStateAction<Note | undefined>>;
  setPageNumber: React.Dispatch<React.SetStateAction<number>>;
  setTotalPages: React.Dispatch<React.SetStateAction<number>>;
  fetchNotes: () => Promise<void>;
};

const NotesContext = createContext<NotesContextType | undefined>(undefined);

export function NotesProvider({ children }: { children: React.ReactNode }) {
  const [query, setQuery] = useState("");
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | undefined>(undefined);

  //pagination
  const pageSize = 5;
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const fetchNotes = useCallback(async () => {
    const data = await new NoteService().getAll(
      "user2",
      pageNumber,
      pageSize,
      query
    );
    setNotes(data.notes);
    setTotalPages(Math.ceil(data.totalCount / pageSize));
  }, [pageNumber, pageSize, query]);

  useEffect(() => {
    fetchNotes();
  }, [fetchNotes]);

  const value = {
    query,
    notes,
    selectedNote,
    pageNumber,
    totalPages,
    setQuery,
    setNotes,
    setSelectedNote,
    setPageNumber,
    setTotalPages,
    fetchNotes,
  };

  return (
    <NotesContext.Provider value={value}>{children}</NotesContext.Provider>
  );
}

export function useNotes(): NotesContextType {
  const context = useContext(NotesContext);
  if (context === undefined) {
    throw new Error("useNotes must be used within a NotesProvider");
  }
  return context;
}
