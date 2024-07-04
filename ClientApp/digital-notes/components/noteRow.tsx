import { Note } from "@/services/types";

interface NoteRowProps {
  note: Note;
  index: number;
  setSelectedNote: (note: Note) => void;
  isRowSelected: boolean;
}

export const NoteRow = ({
  note,
  index,
  setSelectedNote,
  isRowSelected,
}: NoteRowProps) => {
  const displayNoteRecentDate = (note: Note): string => {
    const recentDate = note.updatedAt || note.createdAt;
    return new Date(recentDate).toLocaleDateString();
  };

  return (
    <div
      className={`hover:bg-neutral-200 hover:cursor-pointer ${
        isRowSelected ? "bg-neutral-200" : ""
      }`}
      onClick={() => setSelectedNote(note)}
    >
      <div
        className={`px-4 py-2 border-b border-slate-500 bg-inherit ${
          index === 0 ? "border-t" : ""
        } ${isRowSelected ? "border-l-8 border-l-black-700" : ""}`}
      >
        <h4 className="text-gray-900">
          <strong className="ml-1">{note.title}</strong>
        </h4>
        <p className="text-gray-600">{note.content.substring(0, 100)}...</p>
        <code className={"text-xs text-gray-600"}>
          {displayNoteRecentDate(note)}
        </code>
      </div>
    </div>
  );
};
