"use client";

import { Button, Input, Textarea } from "@/components/index";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import NoteService from "@/services/noteService";
import { Note } from "@/services/types";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

interface NoteFormInput {
  fetchNotes: () => Promise<void>;
  selectedNote?: Note;
}

function NoteForm({ fetchNotes, selectedNote }: NoteFormInput) {
  const noteService: NoteService = new NoteService();

  const formSchema = z.object({
    title: z
      .string()
      .min(1, {
        message: "Title must be at least 1 characters.",
      })
      .max(50),
    content: z.string().min(1, {
      message: "Content must be at least 1 characters.",
    }),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: selectedNote?.title || "",
      content: selectedNote?.content || "",
    },
  });

  useEffect(() => {
    if (selectedNote && selectedNote.id) {
      form.setValue("title", selectedNote.title);
      form.setValue("content", selectedNote.content);
    }
  }, [selectedNote, form]);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    const { title, content } = values;
    if (selectedNote && selectedNote.id) {
      await noteService.updateNote(selectedNote.id, {
        title,
        content,
        id: selectedNote.id,
      });
    } else {
      await noteService.createNote({ title, content, createdBy: "user2" });
    }

    await fetchNotes();
    form.reset();
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>{selectedNote ? "Update note" : "Create note"}</CardTitle>
        <CardDescription>your digital note in one-click.</CardDescription>
      </CardHeader>
      <CardContent>
        <Form {...form}>
          <form
            id="create-update-note-form"
            onSubmit={form.handleSubmit(onSubmit)}
            className="space-y-8"
          >
            <FormField
              control={form.control}
              name="title"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Title</FormLabel>
                  <FormControl>
                    <Input placeholder="enter note title" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="content"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Content</FormLabel>
                  <FormControl>
                    <Textarea placeholder="enter note content" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </form>
        </Form>
      </CardContent>
      <CardFooter className="flex justify-between">
        <Button type="submit" form="create-update-note-form">
          Submit
        </Button>
      </CardFooter>
    </Card>
  );
}

export default NoteForm;
