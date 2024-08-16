import ApiClient from "./apiClient";
import { CreateNotePayload, Notes, UpdateNotePayload } from "./types";

class NoteService {
  private noteEndpoint: string = "note";
  private apiClient: ApiClient = new ApiClient();

  create(payload: CreateNotePayload): Promise<string> {
    return this.apiClient.post(this.noteEndpoint, payload);
  }

  update(id: string, payload: UpdateNotePayload): Promise<void> {
    return this.apiClient.put(`${this.noteEndpoint}/${id}`, payload);
  }

  delete(id: string): Promise<void> {
    return this.apiClient.delete(`${this.noteEndpoint}/?id=${id}`);
  }

  getAll(
    createdBy: string,
    pageNumber: number,
    pageSize: number,
    query: string | undefined = undefined
  ): Promise<Notes> {
    let endpoint = `${this.noteEndpoint}/${createdBy}?pageNumber=${pageNumber}&pageSize=${pageSize}`;

    if (query && query !== "") {
      endpoint += `&noteNameQuery=${query}`;
    }
    return this.apiClient.get(endpoint);
  }
}

export default NoteService;
