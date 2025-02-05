import { auth } from "@/auth";
import {
  NoteList,
  PaginationComponent,
  SearchBar
} from "@/components/index";
import NoteForm from "@/components/noteForm";
import { redirect } from "next/navigation";

export default async function Home() {
  const session = await auth();

  if (!session) {
    redirect("/login");
  }

  return (
    <div className="flex">
      <div
        className={
          "h-[calc(100vh-60px)] overflow-hidden bg-slate-100 w-1/2 max-w-md flex flex-col justify-between hidden md:block"
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
  );
}
