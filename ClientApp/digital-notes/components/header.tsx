"use server";

import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/index";
import { User } from "next-auth";
// import { signOut } from "next-auth/react";
import { signOut } from "@/auth";
import Link from "next/link";
import { Suspense } from "react";
import { LogOutIcon } from "./icons";
import { MobileMenu } from "./mobileMenu";

interface HeaderProps {
  user: User | undefined;
}

export const Header = ({ user }: HeaderProps) => {
  const userInitials = user?.name
    ?.split(" ")
    .map((name) => name[0])
    .join("");

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
      <div>
        {user && (
          <DropdownMenu>
            <DropdownMenuTrigger>
              <Avatar>
                <AvatarImage src={user.image || ""} />
                <AvatarFallback>{userInitials}</AvatarFallback>
              </Avatar>
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              <DropdownMenuLabel>My Account</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuItem>
                <LogOutIcon />
                <form
                  action={async () => {
                    "use server";
                    await signOut();
                  }}
                >
                  <button type="submit">Sign Out</button>
                </form>
                {/* <Link onClick={() => signOut()} href={""}>Log Out</Link> */}
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        )}
      </div>
    </nav>
  );
};
