import { Session } from "next-auth";
import { create } from "zustand";

export type SessionState = {
  session: Session | null;
};

export type SessionActions = {
  setSession: (session: Session | null) => void;
};

export type SessionStore = SessionState & SessionActions;

const useSessionStore = create<SessionStore>((set) => ({
  session: null,
  setSession: (session: Session | null) => set({ session }),
}));

export default useSessionStore;
