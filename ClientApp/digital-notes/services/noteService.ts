import ApiClient from "./apiClient";
import { CreateNotePayload, Note, UpdateNotePayload } from "./types";

class NoteService {
  private noteEndpoint: string = "note";
  private apiClient: ApiClient = new ApiClient();

  create(payload: CreateNotePayload): Promise<string> {
    return this.apiClient.post(this.noteEndpoint, payload);
  }

  update(id: string, payload: UpdateNotePayload): Promise<void> {
    return this.apiClient.put(`${this.noteEndpoint}/${id}`, payload);
  }

  getAll(
    createdBy: string,
    lastRowNumber: number,
    query: string | undefined = undefined
  ): Promise<Note[]> {
    let endpoint = `${this.noteEndpoint}/${createdBy}/${lastRowNumber}`;
    if (query && query !== "") {
      endpoint += `?noteNameQuery=${query}`;
    }
    return this.apiClient.get(endpoint);
  }

  getLastRowNumber(createdBy: string): Promise<number> {
    return this.apiClient.get(`${this.noteEndpoint}/${createdBy}`);
  }
}

export default NoteService;
