import Link from "next/link";

export const Header = () => {
  return (
    <header className="flex h-[60px] items-center gap-4 border-b bg-gray-100/40 px-6 dark:bg-gray-800/40 justify-between">
      <Link className="flex items-center gap-2 font-semibold" href="/">
        <span className="">Digital Notes</span>
      </Link>
    </header>
  );
};
