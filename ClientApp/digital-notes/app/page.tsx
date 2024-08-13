"use client";

import { NoteList, PaginationComponent, SearchBar } from "@/components/index";
import NoteForm from "@/components/noteForm";
import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import { useEffect, useState } from "react";

export default function Home() {
  const [query, setQuery] = useState("");
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | undefined>(undefined);

  //pagination
  const pageSize = 5;
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const fetchNotes = async (
    pageNumber: number,
    query: string
  ): Promise<void> => {
    const data = await new NoteService().getAll(
      "user2",
      pageNumber,
      pageSize,
      query
    );
    setNotes(data.notes);
    setTotalPages(Math.ceil(data.totalCount / pageSize));
  };

  useEffect(() => {
    fetchNotes(pageNumber, query);
  }, [pageNumber, query]);

  const onSearch = (event: any) => {
    setPageNumber(1);
    setQuery(event.target.value);
  };

  const onNoteFormSubmit = async () => {
    setPageNumber(1);
  };

  return (
    <main>
      <div className="flex">
        <div
          className={
            "h-[calc(100vh-60px)] overflow-hidden bg-slate-100 w-1/2 max-w-md flex flex-col justify-between"
          }
        >
          <SearchBar
            query={query}
            onSearch={onSearch}
            onPlusClick={() => setSelectedNote(undefined)}
          />

          <NoteList
            notes={notes}
            selectedNote={selectedNote}
            setSelectedNote={setSelectedNote}
          />

          <div className="flex-grow-0 mb-3">
            <PaginationComponent
              pageNumber={pageNumber}
              totalPages={totalPages}
              setPageNumber={setPageNumber}
            />
          </div>
        </div>

        <div className="w-full">
          <NoteForm
            onFormSubmit={onNoteFormSubmit}
            selectedNote={selectedNote}
          />
        </div>
      </div>
    </main>
  );
}
