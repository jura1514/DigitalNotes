"use client";

import {
  Button,
  Input,
  NoteActions,
  Textarea,
  useNotes,
} from "@/components/index";
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
import { signalRService } from "@/services/signalRService";
import { zodResolver } from "@hookform/resolvers/zod";
import { HubConnection } from "@microsoft/signalr";
import { useSession } from "next-auth/react";
import { useCallback, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

function NoteForm() {
  const [connection, setConnection] = useState<HubConnection | undefined>();
  const { selectedNote, fetchNotes, pageNumber, setPageNumber } = useNotes();
  const { data: session } = useSession();
  if (!session?.user?.email) throw new Error("User session is not set");
  const { email } = session.user;
  const accessToken = session.accessToken;
  const signalRHubEndpoint = `note/${email}`;

  const noteService: NoteService = new NoteService();

  const onNoteCreateOrUpdate = useCallback(async () => {
    if (pageNumber !== 1) {
      // will force to fetch notes
      setPageNumber(1);
    } else {
      await fetchNotes();
    }
  }, [pageNumber, fetchNotes, setPageNumber]);

  useEffect(() => {
    const connection = signalRService.createConnection(
      accessToken,
      signalRHubEndpoint
    );
    connection.on("SendNoteReadOnlySynced", async () => {
      await onNoteCreateOrUpdate();
    });
    signalRService.startConnection(signalRHubEndpoint);
  }, [accessToken, onNoteCreateOrUpdate, pageNumber, signalRHubEndpoint]);

  useEffect(() => {
    return () => {
      signalRService.stopConnection(signalRHubEndpoint);
    };
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

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
    } else if (
      form.getValues().title !== "" ||
      form.getValues().content !== ""
    ) {
      form.reset();
    }
  }, [selectedNote, form]);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    const { title, content } = values;
    if (selectedNote && selectedNote.id) {
      await noteService.update(selectedNote.id, {
        title,
        content,
        id: selectedNote.id,
      });
    } else {
      await noteService.create({ title, content, createdBy: email });
      form.reset();
    }

    // await onNoteCreateOrUpdate();
  }

  return (
    <>
      <NoteActions />
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
    </>
  );
}

export default NoteForm;
