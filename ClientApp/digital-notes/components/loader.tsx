"use client";

import useLoadingStore from "@/stores/useLoadingStore";
import { LoadingSpinner } from "./ui/loadingSpinner";

export const Loader = () => {
  const isLoading = useLoadingStore((state) => state.isLoading);

  if (!isLoading) return null;

  return (
    <div className="fixed inset-0 flex justify-center items-center bg-black bg-opacity-50 z-50">
      <LoadingSpinner />
    </div>
  );
};
