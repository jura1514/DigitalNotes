import {
  NoteList,
  NotesProvider,
  PaginationComponent,
  SearchBar
} from "@/components/index";
import NoteForm from "@/components/noteForm";

export default function Home() {
  return (
    <NotesProvider>
      <main>
        <div className="flex">
          <div
            className={
              "h-[calc(100vh-60px)] overflow-hidden bg-slate-100 w-1/2 max-w-md flex flex-col justify-between"
            }
          >
            <SearchBar />
            <NoteList />
            <div className="flex-grow-0 mb-3">
              <PaginationComponent />
            </div>
          </div>
          <div className="w-full">
            <NoteForm />
          </div>
        </div>
      </main>
    </NotesProvider>
  );
}
