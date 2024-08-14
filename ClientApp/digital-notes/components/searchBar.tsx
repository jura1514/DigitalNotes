"use client"

import { PlusCircleIcon, SearchIcon } from "@/components/icons";
import { Input } from "@/components/index";
import { useNotes } from "./notesProvider";

export const SearchBar: React.FC = () => {
  const { query, setPageNumber, setQuery, setSelectedNote } = useNotes();

  const onSearch = (event: any) => {
    setPageNumber(1);
    setQuery(event.target.value);
  };

  return (
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
  );
};
