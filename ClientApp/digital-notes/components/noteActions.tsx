"use client";

import {
  Button,
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  useNotes,
} from "@/components/index";
import NoteService from "@/services/noteService";
import { DialogTrigger } from "@radix-ui/react-dialog";
import { useState } from "react";
import { PlusCircleIcon, TrashIcon } from "./icons";

export function NoteActions() {
  const { selectedNote, fetchNotes, setSelectedNote } = useNotes();
  const [dialogOpen, setDialogOpen] = useState(false);
  const noteService: NoteService = new NoteService();

  const deleteNote = async () => {
    if (selectedNote?.id) {
      await noteService.delete(selectedNote.id);
      setSelectedNote(undefined);
      await fetchNotes();
    }
  };

  return (
    <div className="flex justify-start p-5">
      <span
        className={`mr-2 ${
          selectedNote?.id ? "hover:cursor-pointer" : "pointer-events-none"
        }`}
        onClick={() => setSelectedNote(undefined)}
      >
        <PlusCircleIcon className="size-7 text-black hover:text-accent-foreground hover:drop-shadow-md" />
      </span>

      <Dialog>
        <DialogTrigger asChild>
          <span
            className={
              selectedNote?.id ? "hover:cursor-pointer" : "pointer-events-none"
            }
          >
            <TrashIcon className="size-7 text-black hover:text-accent-foreground hover:drop-shadow-md" />
          </span>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader className="text-start">
            <DialogTitle>
              Are you sure you want to delete this note?
            </DialogTitle>
            <DialogDescription>
              This action cannot be undone. This will permanently delete your
              note.
            </DialogDescription>
          </DialogHeader>
          <DialogFooter className="justify-between sm:justify-start flex-row">
            <DialogClose asChild>
              <Button type="button" variant="default" onClick={deleteNote}>
                Yes
              </Button>
            </DialogClose>
            <DialogClose asChild>
              <Button type="button" variant="secondary">
                Close
              </Button>
            </DialogClose>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
