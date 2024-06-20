// "use client";

// import NoteService from "@/services/noteService";
// import { Note } from "@/services/types";
// import { useEffect, useState } from "react";
// import { NavItem } from "./navItem";

// function Notes() {
//   // const [notes, setNotes] = useState<Note[]>([]);

//   const displayNoteRecentDate = (note: Note): string => {
//     const recentDate = note.updatedAt || note.createdAt;
//     return new Date(recentDate).toLocaleDateString();
//   };

//   // useEffect(() => {
//   //   const noteService = new NoteService();
//   //   const fetchNotes = async () => {
//   //     const lastRowNumber = await noteService.getLastRowNumber("user");
//   //     const fetchedNotes = await noteService.getAll("user", lastRowNumber);
//   //     setNotes(fetchedNotes);
//   //   };

//   //   fetchNotes();
//   // }, []);

//   return (
//     <div>
//       {notes.map((note) => (
//         // <div key={note.id}>
//         //   <h2>{note.title}</h2>
//         //   <p>{displayNoteRecentDate(note)}</p>
//         // </div>
//         <nav
//           key={note.id}
//           className="grid items-start px-4 text-sm font-medium pb-1 pt-1"
//         >
//           <NavItem isSelected={false}>{note.title || 'missing'}</NavItem>
//         </nav>
//       ))}
//     </div>
//   );
// }

// export default Notes;
