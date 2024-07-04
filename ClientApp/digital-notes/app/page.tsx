"use client";

import { PlusCircleIcon, SearchIcon } from "@/components/icons";
import {
  Input,
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/index";
import NoteForm from "@/components/noteForm";
import { NoteRow } from "@/components/noteRow";
import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import { useEffect, useState } from "react";

export default function Home() {
  const [query, setQuery] = useState("");
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | undefined>(undefined);

  //pagination
  const rowsPerPage = 5;
  const [startIndex, setStartIndex] = useState(0);
  const [endIndex, setEndIndex] = useState(rowsPerPage);

  const fetchNotes = async (query: string): Promise<void> => {
    const noteService = new NoteService();
    const lastRowNumber = await noteService.getLastRowNumber("user2");
    if (lastRowNumber && lastRowNumber !== 0) {
      const fetchedNotes = await noteService.getAll(
        "user2",
        lastRowNumber,
        query
      );
      setNotes(fetchedNotes);
    }
  };

  useEffect(() => {
    fetchNotes(query);
  }, [query]);

  const onSearch = (event: any) => {
    setQuery(event.target.value);
  };

  return (
    <main>
      <div className="flex">
        <div
          className={
            "h-[calc(100vh-60px)] overflow-hidden bg-slate-100 w-1/2 max-w-md flex flex-col justify-between"
          }
        >
          <div className="flex-grow-0 flex flex-row p-4">
            <div className="flex flex-grow relative rounded-md shadow-sm m-2">
              <div className="relative w-full">
                <Input
                  type="search"
                  id="search"
                  placeholder="type something to search"
                  value={query}
                  className="pl-9"
                  onChange={onSearch}
                />
                <SearchIcon className="absolute left-0 top-0 m-2.5 h-4 w-4 text-muted-foreground" />
              </div>
            </div>
            <div className="flex flex-grow-0 place-items-center pl-3">
              <span
                className="hover:cursor-pointer"
                onClick={() => setSelectedNote(undefined)}
              >
                <PlusCircleIcon className="size-8 h-10 w-10 text-black hover:text-accent-foreground hover:drop-shadow-md" />
              </span>
            </div>
          </div>

          <div className="flex-grow overflow-y-auto">
            {notes.length > 0 ? (
              <>
                {notes.slice(startIndex, endIndex).map((note, idx) => {
                  return (
                    <NoteRow
                      note={note}
                      index={idx}
                      key={idx}
                      setSelectedNote={setSelectedNote}
                      isRowSelected={note.id === selectedNote?.id}
                    />
                  );
                })}
              </>
            ) : (
              <div className="p-4 text-center text-2xl">No Results Found</div>
            )}
          </div>

          <div className="flex-grow-0 mb-3">
            <Pagination>
              <PaginationContent>
                <PaginationItem>
                  <PaginationPrevious
                    href="#"
                    className={
                      startIndex === 0
                        ? "pointer-events-none opacity-50"
                        : undefined
                    }
                    onClick={() => {
                      setStartIndex(startIndex - rowsPerPage);
                      setEndIndex(endIndex - rowsPerPage);
                    }}
                  />
                </PaginationItem>
                <PaginationItem>
                  <PaginationLink
                    href="#"
                    className={
                      startIndex === 0
                        ? "pointer-events-none opacity-50"
                        : undefined
                    }
                    isActive={startIndex === 0}
                    onClick={() => {
                      setStartIndex(0);
                      setEndIndex(rowsPerPage);
                    }}
                  >
                    1
                  </PaginationLink>
                </PaginationItem>
                <PaginationItem>
                  <PaginationEllipsis />
                </PaginationItem>
                <PaginationItem>
                  <PaginationNext
                    href="#"
                    className={
                      endIndex >= notes.length
                        ? "pointer-events-none opacity-50"
                        : undefined
                    }
                    onClick={() => {
                      setStartIndex(startIndex + rowsPerPage);
                      setEndIndex(endIndex + rowsPerPage);
                    }}
                  />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        </div>

        <div className="w-full">
          <NoteForm
            fetchNotes={() => fetchNotes(query)}
            selectedNote={selectedNote}
          />
        </div>
      </div>
    </main>
  );
}
