import ApiClient from "./apiClient";
import { CreateNotePayload, Note, UpdateNotePayload } from "./types";

class NoteService {
  private noteEndpoint: string = "note";
  private apiClient: ApiClient = new ApiClient();

  createNote(payload: CreateNotePayload): Promise<string> {
    return this.apiClient.post(this.noteEndpoint, payload);
  }

  updateNote(id: string, payload: UpdateNotePayload): Promise<void> {
    return this.apiClient.put(`${this.noteEndpoint}/${id}`, payload);
  }

  getAll(createdBy: string, lastRowNumber: number): Promise<Note[]> {
    return this.apiClient.get(`${this.noteEndpoint}/${createdBy}/${lastRowNumber}`);
  }

  getLastRowNumber(createdBy: string): Promise<number> {
    return this.apiClient.get(`${this.noteEndpoint}/${createdBy}`);
  }
}

export default NoteService;
