export interface CreateNotePayload {
  title: string;
  content: string;
  createdBy: string;
}

export interface UpdateNotePayload {
  id: string;
  title: string;
  content: string;
}

export interface Note {
  id: string;
  title: string;
  content: string;
  createdBy: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface Notes {
  totalCount: number;
  notes: Note[];
}
