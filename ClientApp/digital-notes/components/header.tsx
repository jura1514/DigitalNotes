import Link from "next/link";
import { Suspense } from "react";
import { MobileMenu } from "./mobileMenu";

export const Header = () => {
  return (
    <nav className="flex h-[60px] items-center gap-4 border-b bg-gray-100/40 px-6 dark:bg-gray-800/40 justify-between">
      <div className="block md:hidden">
        <Suspense>
          <MobileMenu />
        </Suspense>
      </div>
      <Link className="flex items-center gap-2 font-semibold" href="/">
        <span className="">Digital Notes</span>
      </Link>
    </nav>
  );
};
