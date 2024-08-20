import { create } from "zustand";

export type LoadingState = {
  isLoading: boolean;
};

export type LoadingActions = {
  setLoading: (loading: boolean) => void;
};

export type LoadingStore = LoadingState & LoadingActions;

const useLoadingStore = create<LoadingStore>((set) => ({
  isLoading: false,
  setLoading: (loading: boolean) => set({ isLoading: loading }),
}));

export default useLoadingStore;
