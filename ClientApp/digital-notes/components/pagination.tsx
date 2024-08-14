"use client"

import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
  useNotes,
} from "@/components/index";

export const PaginationComponent: React.FC = () => {
  const { pageNumber, totalPages, setPageNumber } = useNotes();

  return (
    <Pagination>
      <PaginationContent>
        <PaginationItem>
          <PaginationPrevious
            href="#"
            className={
              pageNumber === 1 ? "pointer-events-none opacity-50" : undefined
            }
            onClick={() => {
              setPageNumber(pageNumber - 1);
            }}
          />
        </PaginationItem>
        <PaginationItem>
          <PaginationLink
            href="#"
            className={
              pageNumber === 1 ? "pointer-events-none opacity-50" : undefined
            }
            isActive={pageNumber === 1}
            onClick={() => {
              setPageNumber(1);
            }}
          >
            1
          </PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationEllipsis />
        </PaginationItem>
        <PaginationItem>
          <PaginationNext
            href="#"
            className={
              pageNumber === totalPages
                ? "pointer-events-none opacity-50"
                : undefined
            }
            onClick={() => {
              setPageNumber(pageNumber + 1);
            }}
          />
        </PaginationItem>
      </PaginationContent>
    </Pagination>
  );
};
