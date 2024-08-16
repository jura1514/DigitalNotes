"use client";

import { SearchIcon } from "@/components/icons";
import { Input } from "@/components/index";
import { useNotes } from "./notesProvider";

export const SearchBar: React.FC = () => {
  const { query, setPageNumber, setQuery, setSelectedNote, selectedNote } =
    useNotes();

  const onSearch = (event: any) => {
    setPageNumber(1);
    setQuery(event.target.value);
  };

  return (
    <div className="flex flex-row p-3">
      <div className="flex flex-grow relative rounded-md shadow-sm m-1">
        <div className="relative w-full">
          <Input
            type="search"
            id="search"
            placeholder="type something to search"
            value={query}
            className="pl-9"
            onChange={onSearch}
          />
          <SearchIcon className="absolute left-0 top-0 m-2.5 h-5 w-5 text-muted-foreground" />
        </div>
      </div>
    </div>
  );
};
