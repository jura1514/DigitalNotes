import type { ToastActionElement, ToastProps } from "@/components//ui/toast";
import { create } from "zustand";

const TOAST_REMOVE_DELAY = 5000;

type ToasterToast = ToastProps & {
  id: string;
  title?: React.ReactNode;
  description?: React.ReactNode;
  action?: ToastActionElement;
};

export type ToasterToastState = {
  toasts: ToasterToast[];
};

type Toast = Omit<ToasterToast, "id">;

export type ToasterToastActions = {
  addToast: (toast: Toast) => void;
  removeToast: (toastId: string) => void;
};

export type ToasterToastStore = ToasterToastState & ToasterToastActions;

let count = 0;

function genId() {
  count = (count + 1) % Number.MAX_SAFE_INTEGER;
  return count.toString();
}

const useToastsStore = create<ToasterToastStore>((set) => ({
  toasts: [],
  addToast: (toast) => {
    const toastId = genId();
    const updatedToasts = { ...toast, id: toastId };
    set((state) => ({ toasts: [...state.toasts, updatedToasts] }));

    setTimeout(() => {
      set((state) => ({
        toasts: state.toasts.filter((t) => t.id !== toastId),
      }));
    }, TOAST_REMOVE_DELAY);
  },
  removeToast: (toastId) =>
    set((state) => ({ toasts: state.toasts.filter((t) => t.id !== toastId) })),
}));

export default useToastsStore;
