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
  rowNumber: number;
  id: string;
  title: string;
  content: string;
  createdBy: string;
  createdAt: Date;
  updatedAt: Date;
}
