"use client";

import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import useLoadingStore, { LoadingStore } from "@/stores/useLoadingStore";
import useSessionStore, { SessionStore } from "@/stores/useSessionStore";
import { useSession } from "next-auth/react";
import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { useDebounce } from "use-debounce";

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
  const { data: session } = useSession();
  if (!session?.user?.email) throw new Error("User session is not set");
  const { email } = session.user;

  const setSession = useSessionStore((state: SessionStore) => state.setSession);
  setSession(session);

  const setLoading = useLoadingStore((state: LoadingStore) => state.setLoading);

  const [query, setQuery] = useState("");
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | undefined>(undefined);
  const [searchQuery] = useDebounce(query, 700);

  //pagination
  const pageSize = 7;
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const fetchNotes = useCallback(async () => {
    try {
      setLoading(true);
      const data = await new NoteService().getAll(
        email,
        pageNumber,
        pageSize,
        searchQuery
      );

      if (pageNumber !== 1 && data.notes.length === 0) {
        setPageNumber(1);
      } else {
        setNotes(data.notes);
        setTotalPages(Math.ceil(data.totalCount / pageSize));
      }
    } finally {
      setLoading(false);
    }
  }, [pageNumber, pageSize, searchQuery, setLoading, email]);

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
